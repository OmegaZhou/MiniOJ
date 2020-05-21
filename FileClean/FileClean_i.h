

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.01.0622 */
/* at Tue Jan 19 11:14:07 2038
 */
/* Compiler settings for FileClean.idl:
    Oicf, W1, Zp8, env=Win32 (32b run), target_arch=X86 8.01.0622 
    protocol : dce , ms_ext, c_ext, robust
    error checks: allocation ref bounds_check enum stub_data 
    VC __declspec() decoration level: 
         __declspec(uuid()), __declspec(selectany), __declspec(novtable)
         DECLSPEC_UUID(), MIDL_INTERFACE()
*/
/* @@MIDL_FILE_HEADING(  ) */



/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 500
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif /* __RPCNDR_H_VERSION__ */

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __FileClean_i_h__
#define __FileClean_i_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IFileCleaner_FWD_DEFINED__
#define __IFileCleaner_FWD_DEFINED__
typedef interface IFileCleaner IFileCleaner;

#endif 	/* __IFileCleaner_FWD_DEFINED__ */


#ifndef __FileCleaner_FWD_DEFINED__
#define __FileCleaner_FWD_DEFINED__

#ifdef __cplusplus
typedef class FileCleaner FileCleaner;
#else
typedef struct FileCleaner FileCleaner;
#endif /* __cplusplus */

#endif 	/* __FileCleaner_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"
#include "shobjidl.h"

#ifdef __cplusplus
extern "C"{
#endif 


#ifndef __IFileCleaner_INTERFACE_DEFINED__
#define __IFileCleaner_INTERFACE_DEFINED__

/* interface IFileCleaner */
/* [unique][nonextensible][dual][uuid][object] */ 


EXTERN_C const IID IID_IFileCleaner;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("a5430fed-6bab-4a87-b4f6-e76ff266b246")
    IFileCleaner : public IDispatch
    {
    public:
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE AddFile( 
            /* [in] */ BSTR dir) = 0;
        
        virtual /* [id] */ HRESULT STDMETHODCALLTYPE StartClean( void) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IFileCleanerVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            IFileCleaner * This,
            /* [in] */ REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            IFileCleaner * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            IFileCleaner * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfoCount )( 
            IFileCleaner * This,
            /* [out] */ UINT *pctinfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetTypeInfo )( 
            IFileCleaner * This,
            /* [in] */ UINT iTInfo,
            /* [in] */ LCID lcid,
            /* [out] */ ITypeInfo **ppTInfo);
        
        HRESULT ( STDMETHODCALLTYPE *GetIDsOfNames )( 
            IFileCleaner * This,
            /* [in] */ REFIID riid,
            /* [size_is][in] */ LPOLESTR *rgszNames,
            /* [range][in] */ UINT cNames,
            /* [in] */ LCID lcid,
            /* [size_is][out] */ DISPID *rgDispId);
        
        /* [local] */ HRESULT ( STDMETHODCALLTYPE *Invoke )( 
            IFileCleaner * This,
            /* [annotation][in] */ 
            _In_  DISPID dispIdMember,
            /* [annotation][in] */ 
            _In_  REFIID riid,
            /* [annotation][in] */ 
            _In_  LCID lcid,
            /* [annotation][in] */ 
            _In_  WORD wFlags,
            /* [annotation][out][in] */ 
            _In_  DISPPARAMS *pDispParams,
            /* [annotation][out] */ 
            _Out_opt_  VARIANT *pVarResult,
            /* [annotation][out] */ 
            _Out_opt_  EXCEPINFO *pExcepInfo,
            /* [annotation][out] */ 
            _Out_opt_  UINT *puArgErr);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *AddFile )( 
            IFileCleaner * This,
            /* [in] */ BSTR dir);
        
        /* [id] */ HRESULT ( STDMETHODCALLTYPE *StartClean )( 
            IFileCleaner * This);
        
        END_INTERFACE
    } IFileCleanerVtbl;

    interface IFileCleaner
    {
        CONST_VTBL struct IFileCleanerVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IFileCleaner_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IFileCleaner_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IFileCleaner_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IFileCleaner_GetTypeInfoCount(This,pctinfo)	\
    ( (This)->lpVtbl -> GetTypeInfoCount(This,pctinfo) ) 

#define IFileCleaner_GetTypeInfo(This,iTInfo,lcid,ppTInfo)	\
    ( (This)->lpVtbl -> GetTypeInfo(This,iTInfo,lcid,ppTInfo) ) 

#define IFileCleaner_GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId)	\
    ( (This)->lpVtbl -> GetIDsOfNames(This,riid,rgszNames,cNames,lcid,rgDispId) ) 

#define IFileCleaner_Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr)	\
    ( (This)->lpVtbl -> Invoke(This,dispIdMember,riid,lcid,wFlags,pDispParams,pVarResult,pExcepInfo,puArgErr) ) 


#define IFileCleaner_AddFile(This,dir)	\
    ( (This)->lpVtbl -> AddFile(This,dir) ) 

#define IFileCleaner_StartClean(This)	\
    ( (This)->lpVtbl -> StartClean(This) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IFileCleaner_INTERFACE_DEFINED__ */



#ifndef __FileCleanLib_LIBRARY_DEFINED__
#define __FileCleanLib_LIBRARY_DEFINED__

/* library FileCleanLib */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_FileCleanLib;

EXTERN_C const CLSID CLSID_FileCleaner;

#ifdef __cplusplus

class DECLSPEC_UUID("1faef408-1986-4988-a38d-a5ab6268eb7d")
FileCleaner;
#endif
#endif /* __FileCleanLib_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree(     unsigned long *, BSTR * ); 

unsigned long             __RPC_USER  BSTR_UserSize64(     unsigned long *, unsigned long            , BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal64(  unsigned long *, unsigned char *, BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal64(unsigned long *, unsigned char *, BSTR * ); 
void                      __RPC_USER  BSTR_UserFree64(     unsigned long *, BSTR * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


