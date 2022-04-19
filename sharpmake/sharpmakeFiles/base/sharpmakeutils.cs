using System.IO;
using System.Reflection;

namespace Puma
{
    public class SharpmakeUtils
    {
        static public Sharpmake.Target GetDefaultTarget()
        {
            return new Sharpmake.Target(
                    Sharpmake.Platform.win64,
                    Sharpmake.DevEnv.vs2022,
                    Sharpmake.Optimization.Debug | Sharpmake.Optimization.Release
                    );
        }

        static public string GetOptimizationSuffix(Sharpmake.Optimization _optimization)
        {
            return Sharpmake.Optimization.Debug == _optimization ? "_d" : "_r";
        }

        static public string GetRepositoryPath()
        {
            return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\..\..";
        }

        static public string GetProjectsPath()
        {
            return GetRepositoryPath() + @"\_projects";
        }

        static public string GetOutputPath()
        {
            return GetRepositoryPath() + @"\_output";
        }

        static public string GetSourcePath()
        {
            return GetRepositoryPath() + @"\source";
        }

        static public string GetExternPath()
        {
            return GetRepositoryPath() + @"\extern";
        }
    }
}