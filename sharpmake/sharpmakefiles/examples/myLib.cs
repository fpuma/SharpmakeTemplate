using System.IO;

namespace Example.Projects
{
    [Sharpmake.Generate]
    public class MyLib : Puma.SharpmakeBase.IStaticLibrary
    {
        public MyLib()
        : base("MyLib", @"SharpmakeExamplesSource\MyLib")
        {}

        public override void ConfigureAll(Configuration conf, Sharpmake.Target target)
        {
            base.ConfigureAll(conf, target);
            conf.IncludePaths.Add(@"");
        }
    }
}