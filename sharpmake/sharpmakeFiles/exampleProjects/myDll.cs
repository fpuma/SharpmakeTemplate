using System.IO;

namespace Example.Projects
{
    [Sharpmake.Generate]
    public class ExampleDll : Puma.Common.IMyDll
    {
        public ExampleDll()
        : base("ExampleDll", @"SharpmakeExamplesSource\ExampleDll")
        {}
    }
}