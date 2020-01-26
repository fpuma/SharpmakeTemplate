#define DLL_SOURCE_FILE
#include "externDll.h"
#include "iostream"

namespace Examples::Extern::DynamicLinkLibrary
{
    void ExternExportedClass::someMethod()
    {
        std::cout << "Extern DLL method called from an exported class." << std::endl;
    }

    void ExternClass::exportedMethod()
    {
        std::cout << "Extern DLL exported method called from a NON-exported class." << std::endl;
    }
}
