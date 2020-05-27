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
		String^ target_dir = current_dir+"\\tmp\\" + uuid;
		try {
			
			Directory::CreateDirectory(target_dir);
			if (!compile_program(info->code, target_dir, info->language)) {
				
				result->status = JudgeStatus::CompileError;
			} else {
				result = execute_program(info, target_dir,info->language);
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
		String^ file_name = target_dir + "\\Main";
		File::AppendAllText(file_name+"."+language_info->extension_name, code);
		argument->AppendFormat(language_info->argument, file_name, file_name);
		Process^ p = gcnew Process();
		setStartInfo(p->StartInfo, language_info->compiler_name, argument->ToString());
		p->Start();
		p->WaitForExit(100000);
		if (!p->HasExited) {
			p->Kill();
		}
		p->WaitForExit();
		return !p->ExitCode;
	}
	JudgeResult^ Judger::execute_program(const ProblemInfo^ info, String^ target_dir,Language language)
	{
		JudgeResult^ result = gcnew JudgeResult();
		Process^ p = gcnew Process();
		String^ start_pro;
		String^ pro_arg;
		switch (language) {
		case Language::C: case Language::CPP:
			start_pro = target_dir + "\\Main.exe";
			pro_arg = nullptr;
			break;
		case Language::JAVA:
			start_pro = "java";
			pro_arg = "-classpath "+ target_dir +" " +"Main";
			break;

		default:
			start_pro = target_dir + ".exe";
			pro_arg = nullptr;
			break;
		}
		setStartInfo(p->StartInfo, start_pro, pro_arg);
		p->Start();
		p->StandardInput->Write(info->test_case);
		p->StandardInput->Close();
		long long peak_mem = 0;
		do {
			if (!p->HasExited) {
				//p->Refresh();
				peak_mem = p->PeakPagedMemorySize64;
				if (peak_mem > info->max_mem * 1024) {
					break;
				}
			} else {
				break;
			}
			if (p->WaitForExit(1000)) {
				break;
			}
		} while (p->TotalProcessorTime.TotalMilliseconds < info->max_time);
		if (!p->HasExited) {
			p->Kill();
			p->WaitForExit();
		}
		
		auto& t = p->TotalProcessorTime;
		result->time = t.TotalMilliseconds;
		result->memory = peak_mem / 1024;
		if (info->max_time < t.TotalMilliseconds) {
			result->status = JudgeStatus::TimeLimitError;
			return result;
		}
		if (info->max_mem * 1024 < peak_mem) {
			result->status = JudgeStatus::MemoryLimitError;
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
		auto i = test_result->Replace("\r", "")->Trim();
		auto j= right_result->Replace("\r", "")->Trim();
		return i->Equals(j);
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
