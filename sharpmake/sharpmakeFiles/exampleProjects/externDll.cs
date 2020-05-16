using System.IO;

namespace Example.Projects
{
    [Sharpmake.Generate]
    public class ExternDll : Puma.Common.IExternDll
    {
        public ExternDll()
            : base("ExternDll", @"SharpmakeExternExampleSource\ExternDll")
        { }

        public override void ConfigureAll(Configuration conf, Sharpmake.Target target)
        {
            base.ConfigureAll(conf, target);
            conf.IncludePaths.Add(@"");
        }
    }

    namespace Export
    {
        [Sharpmake.Export]
        public class ExternDll : Example.Projects.ExternDll
        { }
    }
    
}