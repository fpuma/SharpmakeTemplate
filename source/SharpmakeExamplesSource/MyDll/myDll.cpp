#define DLL_SOURCE_FILE
#include "myDll.h"
#include "iostream"

namespace Examples::MyProjects::DynamicLinkLibrary
{
    void MyExportedClass::someMethod()
    {
        std::cout << "My DLL method called from an exported class." << std::endl;
    }

    void MyClass::exportedMethod()
    {
        std::cout << "My DLL exported method called from a NON-exported class." << std::endl;
    }
}