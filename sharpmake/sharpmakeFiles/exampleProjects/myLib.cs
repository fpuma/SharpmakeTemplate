using System.IO;

namespace Example.Projects
{
    [Sharpmake.Generate]
    public class MyLib : Puma.Common.IMyLib
    {
        public MyLib()
        : base("MyLib", @"SharpmakeExamplesSource\MyLib")
        {}
    }
}