// FileCleaner.cpp: CFileCleaner 的实现

#include "pch.h"
#include "FileCleaner.h"
#include <cstdio>
#include <comdef.h>
#include <iostream>
// CFileCleaner



STDMETHODIMP_(HRESULT) CFileCleaner::AddFile(BSTR dir)
{
	
	std::string tmp;
	for (int i = 0; dir[i] != '\0'; ++i) {
		tmp.push_back(dir[i]);
	}
	file_queue_.push(tmp);
	std::cout << tmp << std::endl;
	return 0;
	// TODO: 在此处添加实现代码
}


STDMETHODIMP_(HRESULT) CFileCleaner::StartClean()
{
	while (!file_queue_.empty()) {
		std::cout << file_queue_.front() << std::endl;
		
		std::cout << std::remove(file_queue_.front().c_str()) << std::endl;
		file_queue_.pop();
	}
	return 0;
	// TODO: 在此处添加实现代码
}
