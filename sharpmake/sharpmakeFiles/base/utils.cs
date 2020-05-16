using System.IO;
using System.Reflection;
using Sharpmake;

namespace Puma
{
    public class Utils
    {
        static public Target GetDefaultTarget()
        {
            return new Target(
                    Platform.win64,
                    DevEnv.vs2017,
                    Optimization.Debug | Optimization.Release
                    );
        }

        static public string GetOptimizationSuffix(Optimization _optimization)
        {
            return Optimization.Debug == _optimization ? "_d" : "_r";
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