using System.IO;

namespace Example.Projects
{
    [Sharpmake.Generate]
    class ExternApplication : Puma.Common.IExternApplication
    {
        public ExternApplication()
            : base("ExternApplication", @"SharpmakeExternExampleSource\ExternApplication")
        {

        }
    }
}