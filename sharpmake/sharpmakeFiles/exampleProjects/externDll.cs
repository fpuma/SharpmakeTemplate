using System.IO;

namespace Example.Projects
{
    [Sharpmake.Generate]
    public class ExternDll : Puma.Common.IExternDll
    {
        public ExternDll()
            : base("ExternDll", @"SharpmakeExternExampleSource\ExternDll")
        { }
    }

    namespace Export
    {
        [Sharpmake.Export]
        public class ExternDll : Example.Projects.ExternDll
        { }
    }
    
}