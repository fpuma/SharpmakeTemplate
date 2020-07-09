using System.IO;


namespace Example.Projects
{
    [Sharpmake.Generate]
    class MyApplication : Puma.Common.IMyApplication
    {
        public MyApplication()
            : base("MyApplication", @"SharpmakeExamplesSource\MyApplication")
        { }

        public override void ConfigureAll(Sharpmake.Project.Configuration conf, Sharpmake.Target target )
        {
            base.ConfigureAll(conf, target);
            conf.AddPrivateDependency<MyLib>(target);
            conf.AddPrivateDependency<MyDll>(target);
            conf.AddPrivateDependency<Export.ExternLib>(target);
            conf.AddPrivateDependency<Export.ExternDll>(target);
            conf.AddPrivateDependency<ExternBinaries>(target);
        }
    }
}