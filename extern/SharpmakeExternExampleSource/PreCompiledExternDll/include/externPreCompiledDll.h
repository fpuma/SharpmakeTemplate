
#ifdef DLL_SOURCE_FILE
#define DllExport   __declspec( dllexport )
#else
#define DllExport   __declspec( dllimport )
#endif

class DllExport PreCompExportedClass
{
public:
    void exportedClassMethod();
};

class MyPreCompExternClass
{
public:
    void DllExport exportedMethod();
};