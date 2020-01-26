using System.IO;
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
            return Directory.GetCurrentDirectory();
        }
    }
}