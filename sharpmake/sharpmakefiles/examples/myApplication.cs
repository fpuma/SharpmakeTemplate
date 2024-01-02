using System.IO;


namespace Example.Projects
{
    [Sharpmake.Generate]
    class MyApplication : Puma.SharpmakeBase.IApplication
    {
        public MyApplication()
            : base("MyApplication", @"SharpmakeExamplesSource\MyApplication")
        { 
            AdditionalSourceRootPaths.Add(Export.ExternHeader.sSourceFolderPath);
            AdditionalSourceRootPaths.Add(Export.ExternBinaries.sSourceFolderPath);
        }

        public override void ConfigureAll(Sharpmake.Project.Configuration conf, Sharpmake.Target target )
        {
            base.ConfigureAll(conf, target);
            conf.AddPrivateDependency<MyLib>(target);
            conf.AddPrivateDependency<MyDll>(target);
            conf.AddPrivateDependency<Export.ExternLib>(target);
            conf.AddPrivateDependency<Export.ExternDll>(target);
            conf.AddPrivateDependency<Export.ExternBinaries>(target);
            conf.AddPrivateDependency<Export.ExternHeader>(target);
        }
    }
}