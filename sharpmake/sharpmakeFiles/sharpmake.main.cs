using System.IO;
using Sharpmake;


[module: Sharpmake.Include(@"base\*")]

//Examples
[module: Sharpmake.Include(@"exampleProjects\*")]

class SharpmaleMainClass
{
    [Main]
    public static void SharpmakeMain(Arguments sharpmakeArgs)
    {
        sharpmakeArgs.Generate<Example.Solutions.ExampleSolution>();
    }
}

