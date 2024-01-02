using System.IO;

namespace Example.Projects
{
    [Sharpmake.Generate]
    public class ExternLib : Puma.SharpmakeBase.IStaticLibrary
    {
        public ExternLib()
            : base("ExternLib", @"extern\SharpmakeExternExampleSource\ExternLib")
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
        public class ExternLib : Example.Projects.ExternLib
        {

        }
    }
    
}