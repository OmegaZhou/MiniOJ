#pragma once
using System::String;
using System::Int32;
namespace JudgeCore
{
	public enum JudgeStatus
	{
		WrongAnswer, Accept, TimeLimitError, RuntimeError, MemoryLimitError,CompileError,SystemError
	};
	public enum Language
	{
		CPP
	};
	public ref class LanguageInfo
	{
	public:
		LanguageInfo(String^ arg, String^ com, String^ ext);
		String^ argument;
		String^ compiler_name;
		String^ extension_name;
	};
	public ref class ProblemInfo
	{
	public:
		 Int32 max_time;			//单位为Mb
		 Int32 max_mem;				//单位为s
		 String^ code;
		 String^ test_case;
		 String^ right_result;
		 Language language;
		 String^ uuid;
	};

	public ref class Judger
	{
	public:
		static JudgeStatus judge(const ProblemInfo^ info);
	private:
		static void setStartInfo(System::Diagnostics::ProcessStartInfo^ info, String^ file_name, String^ argument);
		static bool compile_program(String^ code, String^ target_dir, Language language);
		static bool test_result(String^ test_result, String^ right_result);
		static JudgeStatus execute_program(const ProblemInfo^ info, String^ target_dir);
		static array<LanguageInfo^>^ language_infos = { gcnew LanguageInfo(" {0}.cpp  -O2 -o {1}.exe","g++","cpp") };
	};
}


