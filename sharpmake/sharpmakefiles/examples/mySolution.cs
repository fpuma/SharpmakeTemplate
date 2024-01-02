using System.IO;

namespace Example.Solutions
{
    [Sharpmake.Generate]
    public class ExampleSolution : Puma.SharpmakeBase.ISolution
    {
        public ExampleSolution()
            : base("ExampleSolution")
        {}

        public override void ConfigureAll(Configuration conf, Sharpmake.Target target)
        {
            base.ConfigureAll(conf, target);

            conf.AddProject<Example.Projects.MyApplication>(target);
        }
    }

    [Sharpmake.Generate]
    public class ExternExampleSolution : Puma.SharpmakeBase.ISolution
    {
        public ExternExampleSolution()
            : base("ExternExampleSolution")
        {}

        public override void ConfigureAll(Configuration conf, Sharpmake.Target target)
        {
            base.ConfigureAll(conf, target);
            conf.AddProject<Example.Projects.ExternLib>(target);
            conf.AddProject<Example.Projects.ExternDll>(target);
        }
    }
}