#include "myLib.h"
#include "iostream"

namespace Examples::MyProjects::StaticLibrary
{
    void MyClass::someMethod()
    {
        std::cout << "My static library method called." << std::endl;
    }
}

