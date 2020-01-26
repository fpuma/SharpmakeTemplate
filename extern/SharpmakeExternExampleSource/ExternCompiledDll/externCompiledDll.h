
#ifdef DLL_SOURCE_FILE
#define DllExport   __declspec( dllexport )
#else
#define DllExport   __declspec( dllimport )
#endif

class DllExport ExternExportedClass
{
public:
    void exportedClassMethod();
};

class MyExternClass
{
public:
    void DllExport exportedMethod();
};