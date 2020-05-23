#include "Judger.h"
#include <string>
using namespace System;
using namespace Diagnostics;
using namespace IO;
using namespace Text;
using Diagnostics::Process;
namespace JudgeCore
{
	JudgeResult^ Judger::judge(const ProblemInfo^ info)
	{
		JudgeResult^ result = gcnew JudgeResult();
		result->status = JudgeStatus::SystemError;
		String^ uuid = info->uuid;
		String^ current_dir = Directory::GetCurrentDirectory();
		String^ target_dir = current_dir + "\\tmp\\" + uuid;
		try {
			
			Directory::CreateDirectory(target_dir);
			if (!compile_program(info->code, target_dir, info->language)) {
				result->status = JudgeStatus::CompileError;
			} else {
				result = execute_program(info, target_dir+"\\tmp.exe");
			}
		} catch (IOException^ e) {
			result->status = JudgeStatus::SystemError;
		} finally {
			if (Directory::Exists(target_dir)) {
				Directory::Delete(target_dir, true);
			}
		}
		
		return result;
	}

	bool Judger::compile_program(String^ code,String^ target_dir, Language language)
	{
		LanguageInfo^ language_info = language_infos[(int)language];
		
		StringBuilder^ argument = gcnew StringBuilder();
		String^ file_name = target_dir + "\\tmp";
		File::AppendAllText(file_name+"."+language_info->extension_name, code);
		argument->AppendFormat(language_info->argument, file_name, file_name);
		Process^ p = gcnew Process();
		setStartInfo(p->StartInfo, language_info->compiler_name, argument->ToString());
		p->Start();
		p->WaitForExit();
		Console::WriteLine(p->StandardError->ReadToEnd());
		return !p->ExitCode;
	}
	JudgeResult^ Judger::execute_program(const ProblemInfo^ info, String^ target_dir)
	{
		JudgeResult^ result = gcnew JudgeResult();
		Process^ p = gcnew Process();
		setStartInfo(p->StartInfo, target_dir, nullptr);
		p->Start();
		p->StandardInput->Write(info->test_case);
		long long peak_mem = 0;
		for (int i = 0; i < 15; ++i) {
			if (!p->HasExited) {
				//p->Refresh();
				peak_mem = p->PeakPagedMemorySize64;
			}
			p->WaitForExit(info->max_time / 5);
		}
		if (!p->HasExited) {
			p->Kill();
		}
		p->WaitForExit();
		auto& t = p->TotalProcessorTime;
		result->time = t.TotalMilliseconds;
		result->memory = peak_mem / 1024;
		if (info->max_time < t.TotalMilliseconds) {
			result->status = JudgeStatus::TimeLimitError;
			return result;
		}
		if (info->max_mem * 1024 < peak_mem) {
			result->status = JudgeStatus::TimeLimitError;
			return result;
		}
		if (p->ExitCode != 0) {
			result->status = JudgeStatus::RuntimeError;
			return result;
		}
		if (!test_result(p->StandardOutput->ReadToEnd(), info->right_result)) {
			result->status = JudgeStatus::WrongAnswer;
			return result;
		}
		result->status = JudgeStatus::Accept;
		return result;
	}
	bool Judger::test_result(String^ test_result, String^ right_result)
	{
		return test_result->Replace("\r","")->Equals(right_result);
	}
	void Judger::setStartInfo(System::Diagnostics::ProcessStartInfo^ info, String^ file_name, String^ argument)
	{
		info->FileName = file_name;
		info->Arguments = argument;
		info->UseShellExecute = false;    //�Ƿ�ʹ�ò���ϵͳshell����
		info->RedirectStandardInput = true;//�������Ե��ó����������Ϣ
		info->RedirectStandardOutput = true;//�ɵ��ó����ȡ�����Ϣ
		info->RedirectStandardError = true;//�ض����׼�������
		info->CreateNoWindow = true;//����ʾ���򴰿�
	}
	LanguageInfo::LanguageInfo(String^ arg, String^ com, String^ ext):argument(arg),compiler_name(com),extension_name(ext)
	{
		
	}
}
