using System.IO;

namespace Example.Projects.Export
{
    [Sharpmake.Export]
    class ExternHeader : Puma.SharpmakeBase.IHeaderOnly
    {
        static private readonly string sSourceFilesFolderName = @"extern\SharpmakeExternExampleSource\ExternHeaderOnly";
        static public readonly string sSourceFolderPath = Puma.SharpmakeUtils.GetSourcePath() + @"\" + sSourceFilesFolderName;

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