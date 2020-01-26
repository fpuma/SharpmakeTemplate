#define DLL_SOURCE_FILE
#include "dllHeader.h"
#include "iostream"

void ExportedClass::exportedClassMethod()
{
    std::cout << "Call to a compiled DLL class" << std::endl;
}

void MyClass::exportedMethod()
{
    std::cout << "Call to a compiled DLL class method" << std::endl;
}

void MyClass::asd()
{
    std::cout << "hhhmm" << std::endl;
}