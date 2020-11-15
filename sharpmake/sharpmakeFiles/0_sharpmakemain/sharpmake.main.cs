using System.IO;

[module: Sharpmake.Include(@"..\base\*")]

//Examples
[module: Sharpmake.Include(@"..\exampleProjects\*")]

public static class SharpmakeMainClass
{
    [Sharpmake.Main]
    public static void SharpmakeMain(Sharpmake.Arguments sharpmakeArgs)
    {
        sharpmakeArgs.Generate<Example.Solutions.ExampleSolution>();
        sharpmakeArgs.Generate<Example.Solutions.ExternExampleSolution>();
    }
}

