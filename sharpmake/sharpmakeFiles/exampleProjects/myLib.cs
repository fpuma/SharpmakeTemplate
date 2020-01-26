using System.IO;

namespace Example.Projects
{
    [Sharpmake.Generate]
    public class ExampleLib : Puma.Common.IMyLib
    {
        public ExampleLib()
        : base("ExampleLib", @"SharpmakeExamplesSource\ExampleLib")
        {}
    }
}