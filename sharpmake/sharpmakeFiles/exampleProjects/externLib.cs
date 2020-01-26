using System.IO;

namespace Example.Projects
{
    [Sharpmake.Generate]
    public class ExternLib : Puma.Common.IExternLib
    {
        public ExternLib()
            : base("ExternLib", @"SharpmakeExternExampleSource\ExternLib")
        { }
    }

    namespace Export
    {
        [Sharpmake.Export]
        public class ExternLib : Example.Projects.ExternLib
        {

        }
    }
    
}