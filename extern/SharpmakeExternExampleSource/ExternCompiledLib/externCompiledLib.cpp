#include "externCompiledLib.h"
#include "iostream"

void ExternLibClass::libMethod()
{
    std::cout << "Call to an extern compiled static lib method" << std::endl;
}
