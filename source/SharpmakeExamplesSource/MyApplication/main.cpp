#include "iostream"

#include "myLib.h"
#include "myDll.h"

#include "externLib.h"
#include "externDll.h"

#include "precompiledDll.h"

using namespace Examples;

int main(int argc, char *argv[])
{
    std::cout<< "Hello world!" << std::endl;

    //Static Lib
    MyProjects::StaticLibrary::MyClass myStaticLibObj;
    myStaticLibObj.someMethod();

    //DLL
    MyProjects::DynamicLinkLibrary::MyExportedClass myDllExportedObj;
    myDllExportedObj.someMethod();
    MyProjects::DynamicLinkLibrary::MyClass myDllNonExportedObj;
    myDllNonExportedObj.exportedMethod();
   
    //Extern static Lib
    Extern::StaticLibrary::ExternClass externLibObj;
    externLibObj.someMethod();

    //Extern DLL
    Extern::DynamicLinkLibrary::ExternExportedClass externDllExportedObj;
    externDllExportedObj.someMethod();
    Extern::DynamicLinkLibrary::ExternClass externDllNonExportedObj;
    externDllNonExportedObj.exportedMethod();

    //Extern precompiled Dll
    Extern::PrecompiledBinaries::ExternExportedClass preCompBinariesObj;
    preCompBinariesObj.someMethod();
    Extern::PrecompiledBinaries::ExternClass preCompBinariesNonExportedObj;
    preCompBinariesNonExportedObj.exportedMethod();

    system( "pause" );

    return 0;
}