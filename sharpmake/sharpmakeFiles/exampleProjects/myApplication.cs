using System.IO;


namespace Example.Projects
{
    [Sharpmake.Generate]
    class ExampleApplication : Puma.Common.IMyApplication
    {
        public ExampleApplication()
            : base("ExampleApplication", @"SharpmakeExamplesSource\ExampleApplication")
        { }

        [Sharpmake.Configure]
        public override void ConfigureAll(Sharpmake.Project.Configuration conf, Sharpmake.Target target )
        {
            base.ConfigureAll(conf, target);
            conf.AddPrivateDependency<ExampleLib>(target);
            conf.AddPrivateDependency<ExampleDll>(target);
            conf.AddPrivateDependency<ExportExternCompiledLib>(target);
            conf.AddPrivateDependency<ExportExternCompiledDll>(target);
        }
    }
}