#include "iostream"
#include "compiledLib.h"
#include "compiledDll.h"
#include "externCompiledLib.h"
#include "externCompiledDll.h"
#include "externPreCompiledDll.h"

int main(int argc, char *argv[])
{
    std::cout<< "Hello world!" << std::endl;

    //Static Lib
    LibClass libClass;
    libClass.libMethod();

    //DLL
    ExportedClass exportedClass;
    exportedClass.exportedClassMethod();
    MyClass myClass;
    myClass.exportedMethod();
   
    //Extern static Lib
    ExternLibClass externLibClass;
    externLibClass.libMethod();

    //Extern DLL
    ExternExportedClass externExportedClass;
    externExportedClass.exportedClassMethod();
    MyExternClass myExternClass;
    myExternClass.exportedMethod();

    //Extern precompiled Dll
    PreCompExportedClass preCompDll;
    preCompDll.exportedClassMethod();
    MyPreCompExternClass preCompClassDll;
    preCompClassDll.exportedMethod();

    system( "pause" );

    return 0;
}