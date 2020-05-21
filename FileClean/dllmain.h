// dllmain.h: 模块类的声明。

class CFileCleanModule : public ATL::CAtlDllModuleT< CFileCleanModule >
{
public :
	DECLARE_LIBID(LIBID_FileCleanLib)
	DECLARE_REGISTRY_APPID_RESOURCEID(IDR_FILECLEAN, "{f5730342-1751-4055-abe7-fd3772b552dd}")
};

extern class CFileCleanModule _AtlModule;
