#include "CppLogger.h"
#include <fstream>
#include <ctime>
#include <windows.h>
std::fstream log_stream;
std::fstream err_stream;
std::string get_str_with_time(const std::string& str)
{
	std::time_t t = std::time(0);
	std::string t_str(std::ctime(&t));
	return t_str + str + "\n";
}
void Log(const char* str)
{
	log_stream << get_str_with_time(str) << std::endl;
}

void Error(const char* str)
{
	err_stream << get_str_with_time(str) << std::endl;
}

void CloseLogger()
{
	log_stream.close();
	err_stream.close();
}



void InitLogger(const char* log_dir, const char* err_dir)
{
	std::string log_dir_(log_dir);
	std::string err_dir_(err_dir);
	log_stream.open(log_dir_, std::ios::app);
	err_stream.open(err_dir_, std::ios::app);
}
