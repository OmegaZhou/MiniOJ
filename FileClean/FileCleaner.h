// FileCleaner.h: CFileCleaner 的声明

#pragma once
#include "resource.h"       // 主符号



#include "FileClean_i.h"

#include <queue>
#include <string>
#if defined(_WIN32_WCE) && !defined(_CE_DCOM) && !defined(_CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA)
#error "Windows CE 平台(如不提供完全 DCOM 支持的 Windows Mobile 平台)上无法正确支持单线程 COM 对象。定义 _CE_ALLOW_SINGLE_THREADED_OBJECTS_IN_MTA 可强制 ATL 支持创建单线程 COM 对象实现并允许使用其单线程 COM 对象实现。rgs 文件中的线程模型已被设置为“Free”，原因是该模型是非 DCOM Windows CE 平台支持的唯一线程模型。"
#endif

using namespace ATL;


// CFileCleaner

class ATL_NO_VTABLE CFileCleaner :
	public CComObjectRootEx<CComSingleThreadModel>,
	public CComCoClass<CFileCleaner, &CLSID_FileCleaner>,
	public IDispatchImpl<IFileCleaner, &IID_IFileCleaner, &LIBID_FileCleanLib, /*wMajor =*/ 1, /*wMinor =*/ 0>
{
public:
	CFileCleaner()
	{
	}

DECLARE_REGISTRY_RESOURCEID(106)


BEGIN_COM_MAP(CFileCleaner)
	COM_INTERFACE_ENTRY(IFileCleaner)
	COM_INTERFACE_ENTRY(IDispatch)
END_COM_MAP()



	DECLARE_PROTECT_FINAL_CONSTRUCT()

	HRESULT FinalConstruct()
	{
		return S_OK;
	}

	void FinalRelease()
	{
	}
private:
	std::queue<std::string> file_queue_;
public:



	STDMETHOD_(HRESULT, AddFile)(BSTR dir);
	STDMETHOD_(HRESULT, StartClean)();
};

OBJECT_ENTRY_AUTO(__uuidof(FileCleaner), CFileCleaner)
