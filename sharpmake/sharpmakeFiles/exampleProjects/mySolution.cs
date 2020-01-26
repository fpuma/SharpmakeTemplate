using System.IO;
using Sharpmake;

namespace Example.Solutions
{
    [Sharpmake.Generate]
    public class ExampleSolution : Puma.Common.IMySolution
    {
        public ExampleSolution()
            : base("ExampleSolution")
        {}

        [Sharpmake.Configure]
        public override void ConfigureAll(Configuration conf, Target target)
        {
            base.ConfigureAll(conf, target);

            conf.AddProject<Example.Projects.ExampleApplication>(target);
        }
    }

    [Sharpmake.Generate]
    public class ExternExampleSolution : Puma.Common.IMySolution
    {
        public ExternExampleSolution()
            : base("ExternExampleSolution")
        {}

        [Sharpmake.Configure]
        public override void ConfigureAll(Configuration conf, Target target)
        {
            base.ConfigureAll(conf, target);
            conf.AddProject<Example.Projects.ExternApplication>(target);
            conf.AddProject<Example.Projects.ExternCompiledLib>(target);
        }
    }
}