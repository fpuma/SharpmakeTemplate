using System.IO;

namespace Example.Projects
{
    [Sharpmake.Generate]
    class ExternHeader : Puma.SharpmakeBase.IHeaderOnly
    {
        static private readonly string sSourceFilesFolderName = @"extern\SharpmakeExternExampleSource\ExternHeaderOnly";

        public ExternHeader()
            : base ("ExternHeader", sSourceFilesFolderName)
        {

        }

        public override void ConfigureIncludes(Sharpmake.Project.Configuration conf, Sharpmake.Target target)
        {
            conf.IncludePaths.Add(@"\include\");
        }

    }
}