using System.IO;

namespace Example.Projects
{
    [Sharpmake.Export]
    class ExternHeader
    {
        private static readonly string RelativeSourcePath = @"extern\SharpmakeExternExampleSource\ExternHeaderOnly";
        public static readonly string SourcePath = Puma.SharpmakeUtils.GetSourcePath() + @"\" + RelativeSourcePath;


        public static void ConfigureIncludes(Sharpmake.Project.Configuration conf, Sharpmake.Target target)
        {
            conf.IncludePaths.Add(SourcePath + @"\include\");
        }

    }
}