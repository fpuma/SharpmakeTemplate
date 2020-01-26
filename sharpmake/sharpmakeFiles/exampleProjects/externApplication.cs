using System.IO;

namespace Example.Projects
{
    [Sharpmake.Generate]
    class ExternApplication : Puma.Common.IExternCompiledApplication
    {
        public ExternApplication()
            : base("ExternApplication", @"SharpmakeExternExampleSource\ExternApplication")
        {

        }
    }
}