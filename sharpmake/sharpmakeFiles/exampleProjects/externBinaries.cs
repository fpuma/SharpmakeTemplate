using System.IO;

namespace Example.Projects
{
    [Sharpmake.Export]
    class ExternBinaries : Puma.Common.IExternBinaries
    {
        public ExternBinaries()
            :base("ExternBinaries", @"SharpmakeExternExampleSource\ExternBinaries")
        { }

        public override void ConfigureIncludes(Configuration conf, Sharpmake.Target target)
        {
            conf.IncludePaths.Add(@"\include\");
        }

        public override void ConfigureLink(Configuration conf, Sharpmake.Target target)
        {
            conf.LibraryPaths.Add(SourceRootPath + @"\lib\");

            if(Sharpmake.Optimization.Debug ==  target.Optimization)
            {
                conf.LibraryFiles.Add(@"precompileddll_d.lib");
                conf.TargetCopyFiles.Add(SourceRootPath + @"\lib\precompileddll_d.dll");
            }
            else if (Sharpmake.Optimization.Release == target.Optimization)
            {
                conf.LibraryFiles.Add(@"precompileddll_r.lib");
                conf.TargetCopyFiles.Add(SourceRootPath + @"\lib\precompileddll_r.dll");
            }

        }
    }
}