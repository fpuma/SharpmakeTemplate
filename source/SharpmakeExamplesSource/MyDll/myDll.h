
#ifdef DLL_SOURCE_FILE
#define DllExport   __declspec( dllexport )
#else
#define DllExport   __declspec( dllimport )
#endif

namespace Examples::MyProjects::DynamicLinkLibrary
{
    class DllExport MyExportedClass
    {
    public:
        void someMethod();
    };

    class MyClass
    {
    public:
        void DllExport exportedMethod();
    };
}
