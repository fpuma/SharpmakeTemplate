#include "iostream"
#include "libHeader.h"
#include "dllHeader.h"

int main(int argc, char *argv[])
{
    std::cout<< "Hello world!" << std::endl;

    LibClass libClass;
    libClass.libMethod();

    ExportedClass exportedClass;
    exportedClass.exportedClassMethod();

    MyClass myClass;
    myClass.exportedMethod();
   
    system( "pause" );

    return 0;
}