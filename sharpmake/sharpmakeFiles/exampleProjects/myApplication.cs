using System.IO;


namespace Example.Projects
{
    [Sharpmake.Generate]
    class ExampleApplication : Puma.Common.IMyApplication
    {
        public ExampleApplication()
            : base("ExampleApplication", "ExampleApplication")
        { }
    }
}