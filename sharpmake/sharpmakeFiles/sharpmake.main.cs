using System.IO;
using Sharpmake;


[module: Sharpmake.Include(@"base\*")]
[module: Sharpmake.Include(@"exampleProjects\*")]
[module: Sharpmake.Include(@"exampleSolutions\*")]


class SharpmaleMainClass
{
    [Main]
    public static void SharpmakeMain(Arguments sharpmakeArgs)
    {
        sharpmakeArgs.Generate<Example.Solutions.ExampleSolution>();
    }
}

