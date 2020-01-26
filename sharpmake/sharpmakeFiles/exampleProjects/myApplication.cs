using System.IO;


namespace Example.Projects
{
    [Sharpmake.Generate]
    class ExampleApplication : Puma.Common.IMyApplication
    {
        public ExampleApplication()
            : base("ExampleApplication", "ExampleApplication")
        { }

        [Sharpmake.Configure]
        public override void ConfigureAll(Sharpmake.Project.Configuration conf, Sharpmake.Target target )
        {
            base.ConfigureAll(conf, target);
            conf.AddPrivateDependency<ExampleLib>(target);
        }
    }
}