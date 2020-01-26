using System.IO;

namespace Example.Projects
{
    [Sharpmake.Generate]
    public class ExternCompiledLib : Puma.Common.IExternCompiledLib
    {
        public ExternCompiledLib()
            : base("ExternCompiledLib", @"SharpmakeExternExampleSource\ExternCompiledLib")
        { }
    }

    [Sharpmake.Export]
    public class ExportExCompiledLib : ExternCompiledLib
    {

    }
}