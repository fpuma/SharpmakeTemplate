using System.IO;

internal class ICompiledProject
{
    public void ConfigureAll(Sharpmake.Project.Configuration conf, Sharpmake.Target target)
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

        string[] warningsToIgnore = { "4100" };
        Sharpmake.Options.Vc.Compiler.DisableSpecificWarnings disableSpecificWarnings = new Sharpmake.Options.Vc.Compiler.DisableSpecificWarnings(warningsToIgnore);
        conf.Options.Add(disableSpecificWarnings);

        conf.VcxprojUserFile = new Sharpmake.Project.Configuration.VcxprojUserFileSettings();
        conf.VcxprojUserFile.LocalDebuggerWorkingDirectory = Puma.Utils.GetOutputPath();
    }
}

namespace Puma.Common
{
    //******************************************************************************************
    //My Solutions
    //******************************************************************************************

    [Sharpmake.Generate]
    public abstract class IMySolution : Sharpmake.Solution
    {
        public IMySolution(string _solutionName)
        {
            Name = _solutionName;
            AddTargets(Puma.Utils.GetDefaultTarget());
        }

        [Sharpmake.Configure]
        public virtual void ConfigureAll(Configuration conf, Sharpmake.Target target)
        {
            conf.SolutionPath = Puma.Utils.GetProjectsPath();
        }
    }

    //******************************************************************************************
    //My projects
    //******************************************************************************************
    [Sharpmake.Generate]
    public abstract class IMyApplication: Sharpmake.Project
    {
        public string SourceFilesFolderName;

        public readonly string ProjectGenerationPath = Puma.Utils.GetProjectsPath() + @"\[project.Name]\";
        public readonly string TargetOutputPath    = Puma.Utils.GetOutputPath();

        private ICompiledProject m_compiledProject = new ICompiledProject();

        public IMyApplication(string _projectName, string _sourceFolder)
        {
            Name = _projectName;
            SourceFilesFolderName = _sourceFolder;
            SourceRootPath = Puma.Utils.GetSourcePath() + @"\[project.SourceFilesFolderName]\";
            AddTargets(Puma.Utils.GetDefaultTarget());
        }

        [Sharpmake.Configure]
        public virtual void ConfigureAll(Configuration conf, Sharpmake.Target target)
        {
            m_compiledProject.ConfigureAll(conf, target);

            //Path were the project will be generated
            conf.ProjectPath = ProjectGenerationPath;

            //Path were the binaries will be generated on compilation
            conf.TargetPath = TargetOutputPath;

            conf.IncludePaths.Add(SourceRootPath);
        }
    }

    [Sharpmake.Generate]
    public abstract  class IMyLib : IMyApplication
    {
        public IMyLib(string _projectName, string _sourceFolder) 
            : base(_projectName, _sourceFolder)
        {}

        [Sharpmake.Configure]
        public override void ConfigureAll(Configuration conf, Sharpmake.Target target)
        {
            base.ConfigureAll(conf, target);

            conf.Output = Configuration.OutputType.Lib;
        }
    }

    [Sharpmake.Generate]
    public abstract class IMyDll : IMyApplication
    {
        public IMyDll(string _projectName, string _sourceFolder)
            : base(_projectName, _sourceFolder)
        {}

        [Sharpmake.Configure]
        public override void ConfigureAll(Configuration conf, Sharpmake.Target target)
        {
            base.ConfigureAll(conf, target);

            conf.Output = Configuration.OutputType.Dll;
        }
    }

    //******************************************************************************************
    //Extern projects
    //******************************************************************************************

    [Sharpmake.Generate]
    public abstract class IExternCompiledApplication : Sharpmake.Project
    {
        public readonly string ExternFilesFolderName;

        public readonly string ProjectGenerationPath = Puma.Utils.GetProjectsPath() + @"\extern\[project.Name]\";
        public readonly string TargetOutputPath = Puma.Utils.GetOutputPath() + @"\extern\[project.Name]\";

        private ICompiledProject m_compiledProject = new ICompiledProject();

        public IExternCompiledApplication(string _projectName, string _externFolder)
        {
            Name = _projectName;
            ExternFilesFolderName = _externFolder;
            SourceRootPath = Puma.Utils.GetExternPath() + @"\[project.ExternFilesFolderName]\";
            AddTargets(Puma.Utils.GetDefaultTarget());
        }

        [Sharpmake.Configure]
        public virtual void ConfigureAll(Configuration conf, Sharpmake.Target target)
        {
            m_compiledProject.ConfigureAll(conf, target);

            //Path were the project will be generated
            conf.ProjectPath = ProjectGenerationPath;

            //Path were the binaries will be generated on compilation
            conf.TargetPath = TargetOutputPath;

            conf.IncludePaths.Add(SourceRootPath);
        }
    }

    [Sharpmake.Generate]
    public abstract class IExternCompiledLib : IExternCompiledApplication
    {
        public IExternCompiledLib(string _projectName, string _externFolder)
            : base(_projectName, _externFolder)
        {}

        [Sharpmake.Configure]
        public override void ConfigureAll(Configuration conf, Sharpmake.Target target)
        {
            base.ConfigureAll(conf, target);
            conf.Output = Configuration.OutputType.Lib;
        }
    }

    [Sharpmake.Generate]
    public abstract class IExternCompiledDll : IExternCompiledApplication
    {
        public IExternCompiledDll(string _projectName, string _externFolder)
            : base(_projectName, _externFolder)
        {}

        [Sharpmake.Configure]
        public override void ConfigureAll(Configuration conf, Sharpmake.Target target)
        {
            base.ConfigureAll(conf, target);
            conf.Output = Configuration.OutputType.Dll;
        }
    }

    [Sharpmake.Export]
    abstract public class IExternBinaryLibrary : Sharpmake.Project
    {
        public readonly string ExternFilesFolderName;

        public IExternBinaryLibrary(string _projectName, string _externFolder)
        {
            Name = _projectName;
            ExternFilesFolderName = _externFolder;
            SourceRootPath = Puma.Utils.GetExternPath() + @"\[project.ExternFilesFolderName]\";
            AddTargets(Puma.Utils.GetDefaultTarget());
        }

        public abstract void ConfigureIncludes(Configuration conf, Sharpmake.Target target);
        public abstract void ConfigureLink(Configuration conf, Sharpmake.Target target);

        [Sharpmake.Configure]
        public virtual void ConfigureAll(Configuration conf, Sharpmake.Target target)
        {
            ConfigureIncludes(conf, target);
            ConfigureLink(conf, target);
        }
    }
}