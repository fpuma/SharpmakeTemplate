using System.IO;

namespace Puma.Common
{
    abstract public class ICompiledProject : Sharpmake.Project
    {
		public ICompiledProject()
		{
			AddTargets(Puma.Utils.GetDefaultTarget());
		}
	
		[Sharpmake.Configure]
		virtual public void ConfigureAll(Configuration conf, Sharpmake.Target target)
		{
            //Name of the project file
            conf.ProjectFileName = "[project.Name]_[target.Platform]_[target.DevEnv]";

            //Intermediate path
            conf.IntermediatePath = @"[conf.ProjectPath]\temp";

            //Name of the binary generated
            conf.TargetFileName = "[project.Name]" + Puma.Utils.GetOptimizationSuffix(target.Optimization);

            conf.Defines.Add("_CRT_SECURE_NO_WARNINGS");
            conf.Options.Add(Sharpmake.Options.Vc.Compiler.Exceptions.Enable);
            conf.Options.Add(Sharpmake.Options.Vc.General.WindowsTargetPlatformVersion.v10_0_17134_0);
            conf.Options.Add(Sharpmake.Options.Vc.Compiler.CppLanguageStandard.CPP17);
	
			string[] warningsToIgnore = {"4100"};
            Sharpmake.Options.Vc.Compiler.DisableSpecificWarnings disableSpecificWarnings = new Sharpmake.Options.Vc.Compiler.DisableSpecificWarnings(warningsToIgnore);
            conf.Options.Add(disableSpecificWarnings);

            conf.VcxprojUserFile = new Sharpmake.Project.Configuration.VcxprojUserFileSettings();
            conf.VcxprojUserFile.LocalDebuggerWorkingDirectory = @"[project.SharpmakeCsPath]\..\output\";
        }
    }

    abstract public class IPumaProject: ICompiledProject
    {
        //Name of the folder containing the project source files
        public string SourceFilesFolderName = "_PumaProjectFolderNameNotInitialized_";

        public readonly string ProjectPath   = @"[project.SharpmakeCsPath]\..\projects\[project.Name]\";
        public readonly string TargetPath    = @"[project.SharpmakeCsPath]\..\output\";

        public IPumaProject()
        {
            SourceRootPath = @"[project.SharpmakeCsPath]\..\source\[project.SourceFilesFolderName]\";
        }

        [Sharpmake.Configure]
        public override void ConfigureAll(Configuration conf, Sharpmake.Target target)
        {
            base.ConfigureAll(conf, target);

            //Path were the project will be generated
            conf.ProjectPath = ProjectPath;

            //Path were the binaries will be generated on compilation
            conf.TargetPath = TargetPath;

            conf.IncludePaths.Add(SourceRootPath);

        }
    }

    abstract public class IPumaApplication : IPumaProject
    {

    }

    abstract public class IPumaLibrary : IPumaProject
    {
        [Sharpmake.Configure]
        public override void ConfigureAll(Configuration conf, Sharpmake.Target target)
        {
            base.ConfigureAll(conf, target);

            conf.Output = Configuration.OutputType.Lib;
        }
    }

    abstract public class IExternProject : ICompiledProject
    {
        //Name of the files in the extern folder
        public string OriginalFilesFolderName = "_ExternProjectFolderNameNotInitialized_";

        public readonly string OriginalFilesPath = @"[project.SharpmakeCsPath]\..\extern\[project.OriginalFilesFolderName]\";
        public readonly string ProjectGenerationPath       = @"[project.SharpmakeCsPath]\..\projects\extern\[project.Name]\";
        public readonly string TargetOutputPath        = @"[project.SharpmakeCsPath]\..\output\extern\[project.Name]\bin\";
        
        [Sharpmake.Configure]
        public override void ConfigureAll(Configuration conf, Sharpmake.Target target)
        {
            base.ConfigureAll(conf, target);

            //Path were the project will be generated
            conf.ProjectPath = ProjectGenerationPath;

            //Path were the binaries will be generated on compilation
            conf.TargetPath = TargetOutputPath;

        }
    }

    abstract public class IExternLibrary : Sharpmake.Project
    {
        public string OriginalFilesFolderName = "_ExternLibraryFolderNameNotInitialized_";

        public readonly string OriginalFilesPath = @"[project.SharpmakeCsPath]\..\extern\[project.OriginalFilesFolderName]\";

        public IExternLibrary()
        {
            AddTargets(Puma.Utils.GetDefaultTarget());
        }

        [Sharpmake.Configure]
        virtual public void ConfigureAll(Configuration conf, Sharpmake.Target target)
        {

        }
    }

}