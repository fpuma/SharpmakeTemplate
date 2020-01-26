#include "iostream"
#include "compiledLib.h"
#include "compiledDll.h"
#include "externCompiledLib.h"
#include "externCompiledDll.h"

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

    system( "pause" );

    return 0;
}