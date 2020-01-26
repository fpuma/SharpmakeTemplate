using System.IO;
using Sharpmake;

namespace Example.Projects
{
    [Sharpmake.Export]
    class ExternPreCompiledDll : Puma.Common.IExternBinaryLibrary
    {
        public ExternPreCompiledDll()
            :base("ExternPreCompDll", @"SharpmakeExternExampleSource\PreCompiledExternDll")
        { }

        public override void ConfigureIncludes(Configuration conf, Target target)
        {
            conf.IncludePaths.Add(SourceRootPath + @"\include\");
        }

        public override void ConfigureLink(Configuration conf, Target target)
        {
            conf.LibraryPaths.Add(SourceRootPath + @"\lib\");

            if(Sharpmake.Optimization.Debug ==  target.Optimization)
            {
                conf.LibraryFiles.Add(@"externprecompileddll_d.lib");
                conf.TargetCopyFiles.Add(SourceRootPath + @"\lib\externprecompileddll_d.dll");
            }
            else if (Sharpmake.Optimization.Release == target.Optimization)
            {
                conf.LibraryFiles.Add(@"externprecompileddll_r.lib");
                conf.TargetCopyFiles.Add(SourceRootPath + @"\lib\externprecompileddll_r.dll");
            }

        }
    }
}