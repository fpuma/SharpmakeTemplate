using System.IO;

namespace Example.Projects
{
    [Sharpmake.Generate]
    class ExternApplication : Puma.Common.IApplication
    {
        public ExternApplication()
            : base("ExternApplication", @"extern\SharpmakeExternExampleSource\ExternApplication")
        {

        }
    }
}