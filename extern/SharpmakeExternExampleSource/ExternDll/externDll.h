
#ifdef DLL_SOURCE_FILE
#define DllExport   __declspec( dllexport )
#else
#define DllExport   __declspec( dllimport )
#endif

namespace Examples::Extern::DynamicLinkLibrary
{
    class DllExport ExternExportedClass
    {
    public:
        void someMethod();
    };

    class ExternClass
    {
    public:
        void DllExport exportedMethod();
    };
}

