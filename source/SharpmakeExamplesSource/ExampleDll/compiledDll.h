
#ifdef DLL_SOURCE_FILE
#define DllExport   __declspec( dllexport )
#else
#define DllExport   __declspec( dllimport )
#endif

class DllExport ExportedClass
{
public:
    void exportedClassMethod();
};

class MyClass
{
public:
    void DllExport exportedMethod();
};