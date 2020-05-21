#pragma once
#include <string>
#include <fstream>
#include <Windows.h>
extern "C" __declspec(dllexport) void Log(const char* str);
extern "C" __declspec(dllexport) void Error(const char* str);
extern "C" __declspec(dllexport) void InitLogger(const char* log_dir, const char* err_dir);
extern "C" __declspec(dllexport) void CloseLogger();
	



