using System.IO;

[module: Sharpmake.Include(@"..\..\sharpmakeutils\utils\*")]

//Examples
[module: Sharpmake.Include(@"..\examples\*")]

public static class SharpmakeMainClass
{
    [Sharpmake.Main]
    public static void SharpmakeMain(Sharpmake.Arguments sharpmakeArgs)
    {
        sharpmakeArgs.Generate<Example.Solutions.ExampleSolution>();
        sharpmakeArgs.Generate<Example.Solutions.ExternExampleSolution>();
    }
}

