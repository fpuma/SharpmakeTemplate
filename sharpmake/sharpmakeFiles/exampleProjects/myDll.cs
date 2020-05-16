using System.IO;

namespace Example.Projects
{
    [Sharpmake.Generate]
    public class MyDll : Puma.Common.IMyDll
    {
        public MyDll()
        : base("MyDll", @"SharpmakeExamplesSource\MyDll")
        {}

        public override void ConfigureAll(Configuration conf, Sharpmake.Target target)
        {
            base.ConfigureAll(conf, target);
            conf.IncludePaths.Add(@"");
        }
    }
}