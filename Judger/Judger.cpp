#include "Judger.h"
#include <string>
using namespace System;
using namespace Diagnostics;
using namespace IO;
using namespace Text;
using Diagnostics::Process;
namespace JudgeCore
{
	JudgeStatus Judger::judge(const ProblemInfo^ info)
	{
		JudgeStatus result = JudgeStatus::SystemError;
		String^ uuid = info->uuid;
		String^ current_dir = Directory::GetCurrentDirectory();
		String^ target_dir = current_dir + "\\tmp\\" + uuid;
		try {
			
			Directory::CreateDirectory(target_dir);
			if (!compile_program(info->code, target_dir, info->language)) {
				result = JudgeStatus::CompileError;
			} else {
				result = execute_program(info, target_dir+"\\tmp.exe");
			}
		} catch (IOException^ e) {
			result = JudgeStatus::SystemError;
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
	JudgeStatus Judger::execute_program(const ProblemInfo^ info, String^ target_dir)
	{
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
		if (info->max_time < t.TotalMilliseconds) {
			return JudgeStatus::TimeLimitError;
		}
		if (info->max_mem * 1024 < peak_mem) {
			return JudgeStatus::MemoryLimitError;
		}
		if (p->ExitCode != 0) {
			return JudgeStatus::RuntimeError;
		}
		if (!test_result(p->StandardOutput->ReadToEnd(), info->right_result)) {
			return JudgeStatus::WrongAnswer;
		}
		return JudgeStatus::Accept;
	}
	bool Judger::test_result(String^ test_result, String^ right_result)
	{
		return test_result->Replace("\r","")->Equals(right_result);
	}
	void Judger::setStartInfo(System::Diagnostics::ProcessStartInfo^ info, String^ file_name, String^ argument)
	{
		info->FileName = file_name;
		info->Arguments = argument;
		info->UseShellExecute = false;    //是否使用操作系统shell启动
		info->RedirectStandardInput = true;//接受来自调用程序的输入信息
		info->RedirectStandardOutput = true;//由调用程序获取输出信息
		info->RedirectStandardError = true;//重定向标准错误输出
		info->CreateNoWindow = true;//不显示程序窗口
	}
	LanguageInfo::LanguageInfo(String^ arg, String^ com, String^ ext):argument(arg),compiler_name(com),extension_name(ext)
	{
		
	}
}
