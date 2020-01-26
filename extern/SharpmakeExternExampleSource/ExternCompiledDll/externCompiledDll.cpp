#define DLL_SOURCE_FILE
#include "externCompiledDll.h"
#include "iostream"

void ExternExportedClass::exportedClassMethod()
{
    std::cout << "Call to an extern compiled DLL class" << std::endl;
}

void MyExternClass::exportedMethod()
{
    std::cout << "Call to an extern compiled DLL class method" << std::endl;
}