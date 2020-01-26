using System.IO;

namespace Example.Projects
{
    [Sharpmake.Generate]
    public class MyDll : Puma.Common.IMyDll
    {
        public MyDll()
        : base("MyDll", @"SharpmakeExamplesSource\MyDll")
        {}
    }
}