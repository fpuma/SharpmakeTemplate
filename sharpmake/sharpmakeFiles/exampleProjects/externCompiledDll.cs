using System.IO;

namespace Example.Projects
{
    [Sharpmake.Generate]
    public class ExternCompiledDll : Puma.Common.IExternCompiledDll
    {
        public ExternCompiledDll()
            : base("ExternCompiledDll", @"SharpmakeExternExampleSource\ExternCompiledDll")
        { }
    }

    [Sharpmake.Export]
    public class ExportExternCompiledDll : ExternCompiledDll
    { }

}