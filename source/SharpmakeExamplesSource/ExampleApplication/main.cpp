#include "iostream"
#include "compiledLib.h"
#include "compiledDll.h"
#include "externCompiledLib.h"

int main(int argc, char *argv[])
{
    std::cout<< "Hello world!" << std::endl;

    LibClass libClass;
    libClass.libMethod();

    ExportedClass exportedClass;
    exportedClass.exportedClassMethod();

    MyClass myClass;
    myClass.exportedMethod();
   
    ExternLibClass externLibClass;
    externLibClass.libMethod();

    system( "pause" );

    return 0;
}