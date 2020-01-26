#include "externLib.h"
#include "iostream"

namespace Examples::Extern::StaticLibrary
{
    void ExternClass::someMethod()
    {
        std::cout << "Extern static library method called." << std::endl;
    }
}

