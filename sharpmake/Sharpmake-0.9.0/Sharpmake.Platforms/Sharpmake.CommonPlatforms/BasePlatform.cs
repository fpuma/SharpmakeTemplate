﻿// Copyright (c) 2017 Ubisoft Entertainment
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
using System;
using System.Collections.Generic;
using System.Linq;
using Sharpmake.Generators;
using Sharpmake.Generators.FastBuild;
using Sharpmake.Generators.VisualStudio;

namespace Sharpmake
{
    public abstract partial class BasePlatform : IPlatformDescriptor, IPlatformBff, IPlatformVcxproj
    {
        #region IPlatformDescriptor
        public abstract string SimplePlatformString { get; }
        public abstract bool IsMicrosoftPlatform { get; }
        public abstract bool IsPcPlatform { get; }
        public abstract bool IsUsingClang { get; }
        public abstract bool HasDotNetSupport { get; }
        public abstract bool HasSharedLibrarySupport { get; }
        public virtual bool HasPrecompiledHeaderSupport => true;

        public virtual EnvironmentVariableResolver GetPlatformEnvironmentResolver(params VariableAssignment[] parameters)
        {
            //
            // TODO: EnvironmentVariableResolver is not an actual environment variable resolver,
            //       and doesn't care about environment variables, just those passed to it in the
            //       argument. This causes it to attempt to resolve environment variable that were
            //       not passed to it, and throw an exception for it.
            //

            //return new EnvironmentVariableResolver(assignments);
            return null;
        }
        #endregion

        #region IPlatformBff implementation
        protected const string RemoveLineTag = FileGeneratorUtilities.RemoveLineTag;

        public virtual string BffPlatformDefine => null;

        public virtual string CConfigName(Configuration conf)
        {
            return string.Empty;
        }

        public virtual string CppConfigName(Configuration conf)
        {
            return string.Empty;
        }

        public virtual bool AddLibPrefix(Configuration conf)
        {
            return false;
        }

        [Obsolete("Use " + nameof(SetupExtraLinkerSettings) + " and pass the conf")]
        public virtual void SetupExtraLinkerSettings(IFileGenerator fileGenerator, Project.Configuration.OutputType outputType, string fastBuildOutputFile)
        {
            SetupExtraLinkerSettings(fileGenerator, outputType);
        }

        public virtual void SetupExtraLinkerSettings(IFileGenerator fileGenerator, Project.Configuration configuration, string fastBuildOutputFile)
        {
            SetupExtraLinkerSettings(fileGenerator, configuration.Output);
        }

        private void SetupExtraLinkerSettings(IFileGenerator fileGenerator, Project.Configuration.OutputType outputType)
        {
            using (fileGenerator.Resolver.NewScopedParameter("dllOption", outputType == Project.Configuration.OutputType.Dll ? " /DLL" : ""))
            {
                fileGenerator.Write(Bff.Template.ConfigurationFile.LinkerOptions);
            }
        }

        public virtual IEnumerable<Project.Configuration.BuildStepBase> GetExtraPostBuildEvents(Project.Configuration configuration, string fastBuildOutputFile)
        {
            return Enumerable.Empty<Project.Configuration.BuildStepBase>();
        }

        public virtual string GetOutputFilename(Project.Configuration.OutputType outputType, string fastBuildOutputFile) => fastBuildOutputFile;

        public virtual void AddCompilerSettings(IDictionary<string, CompilerSettings> masterCompilerSettings, Project.Configuration conf)
        {
        }
        #endregion

        #region IPlatformVcxproj implementation
        public abstract string ExecutableFileExtension { get; }
        public virtual string PackageFileExtension => ExecutableFileExtension;
        public abstract string SharedLibraryFileExtension { get; }
        public abstract string ProgramDatabaseFileExtension { get; }
        public virtual string StaticLibraryFileExtension => "lib";
        public virtual string StaticOutputLibraryFileExtension => StaticLibraryFileExtension;
        public virtual bool ExcludesPrecompiledHeadersFromBuild => false;
        public virtual bool HasUserAccountControlSupport => false;
        public virtual bool HasEditAndContinueDebuggingSupport => false;

        public virtual void SetupDeleteExtensionsOnCleanOptions(IGenerationContext context)
        {
        }

        public virtual IEnumerable<string> GetImplicitlyDefinedSymbols(IGenerationContext context)
        {
            yield break;
        }

        public virtual IEnumerable<string> GetLibraryPaths(IGenerationContext context)
        {
            yield break;
        }

        public virtual IEnumerable<string> GetLibraryFiles(IGenerationContext context)
        {
            yield break;
        }

        public virtual IEnumerable<string> GetPlatformLibraryFiles(IGenerationContext context)
        {
            yield break;
        }

        public IEnumerable<string> GetIncludePaths(IGenerationContext context)
        {
            return GetIncludePathsImpl(context);
        }

        public IEnumerable<string> GetPlatformIncludePaths(IGenerationContext context)
        {
            return GetPlatformIncludePathsWithPrefixImpl(context).Select(x => x.Path);
        }

        public IEnumerable<IncludeWithPrefix> GetPlatformIncludePathsWithPrefix(IGenerationContext context)
        {
            return GetPlatformIncludePathsWithPrefixImpl(context);
        }

        public IEnumerable<string> GetResourceIncludePaths(IGenerationContext context)
        {
            return GetResourceIncludePathsImpl(context);
        }

        public virtual IEnumerable<string> GetCxUsingPath(IGenerationContext context)
        {
            yield break;
        }

        public virtual IEnumerable<VariableAssignment> GetEnvironmentVariables(IGenerationContext context)
        {
            yield break;
        }

        public virtual string GetOutputFileNamePrefix(IGenerationContext context, Project.Configuration.OutputType outputType)
        {
            return string.Empty;
        }

        public virtual void SetupSdkOptions(IGenerationContext context)
        {
        }

        public virtual void SetupPlatformToolsetOptions(IGenerationContext context)
        {
        }

        public virtual void SetupPlatformTargetOptions(IGenerationContext context)
        {
            context.Options["TargetMachine"] = FileGeneratorUtilities.RemoveLineTag;
            context.CommandLineOptions["TargetMachine"] = FileGeneratorUtilities.RemoveLineTag;
        }

        public virtual void SelectCompilerOptions(IGenerationContext context)
        {
        }

        public virtual void SelectLinkerOptions(IGenerationContext context)
        {
        }

        public virtual void SelectPlatformAdditionalDependenciesOptions(IGenerationContext context)
        {
        }

        public virtual void SelectApplicationFormatOptions(IGenerationContext context)
        {
        }

        public virtual void SelectBuildType(IGenerationContext context)
        {
        }

        public virtual void GenerateSdkVcxproj(IVcxprojGenerationContext context, IFileGenerator generator)
        {
        }

        public virtual void GenerateMakefileConfigurationVcxproj(IVcxprojGenerationContext context, IFileGenerator generator)
        {
        }

        public virtual void GenerateProjectCompileVcxproj(IVcxprojGenerationContext context, IFileGenerator generator)
        {
            generator.Write(_projectConfigurationsCompileTemplate);
        }

        public virtual void GenerateProjectLinkVcxproj(IVcxprojGenerationContext context, IFileGenerator generator)
        {
            var simpleOutput = Project.Configuration.SimpleOutputType(context.Configuration.Output);

            if (simpleOutput == Project.Configuration.OutputType.Lib)
                generator.Write(GetProjectStaticLinkVcxprojTemplate());
            else if (simpleOutput == Project.Configuration.OutputType.Dll)
                generator.Write(GetProjectLinkSharedVcxprojTemplate());
            else if (simpleOutput == Project.Configuration.OutputType.Exe)
                generator.Write(GetProjectLinkExecutableVcxprojTemplate());
        }

        public virtual void GenerateUserConfigurationFile(Project.Configuration conf, IFileGenerator generator)
        {
            generator.Write(_userFileConfigurationGeneralTemplate);
        }

        public virtual void GenerateRunFromPcDeployment(IVcxprojGenerationContext context, IFileGenerator generator)
        {
        }

        public virtual void GenerateProjectPlatformSdkDirectoryDescription(IVcxprojGenerationContext context, IFileGenerator generator)
        {
        }

        public virtual void GenerateProjectConfigurationGeneral(IVcxprojGenerationContext context, IFileGenerator generator)
        {
            generator.Write(_projectConfigurationsGeneral);
        }

        public virtual void GenerateProjectConfigurationGeneral2(IVcxprojGenerationContext context, IFileGenerator generator)
        {
            generator.Write(_projectConfigurationsGeneral2);
        }

        public virtual void GenerateProjectConfigurationFastBuildMakeFile(IVcxprojGenerationContext context, IFileGenerator generator)
        {
            generator.Write(_projectConfigurationsFastBuildMakefile);
        }
        public virtual void GenerateProjectConfigurationCustomMakeFile(IVcxprojGenerationContext context, IFileGenerator generator)
        {
            generator.Write(_projectConfigurationsCustomMakefile);
        }

        public virtual void GenerateProjectPlatformImportSheet(IVcxprojGenerationContext context, IFileGenerator generator)
        {
        }

        public virtual void GeneratePlatformResourceFileList(IVcxprojGenerationContext context, IFileGenerator generator, Strings alreadyWrittenPriFiles, IList<Vcxproj.ProjectFile> resourceFiles, IList<Vcxproj.ProjectFile> imageResourceFiles)
        {
        }

        public virtual void GeneratePlatformReferences(IVcxprojGenerationContext context, IFileGenerator generator)
        {
        }

        public virtual void GeneratePlatformSpecificProjectDescription(IVcxprojGenerationContext context, IFileGenerator generator)
        {
        }

        public virtual void SetupPlatformLibraryOptions(ref string platformLibExtension, ref string platformOutputLibExtension, ref string platformPrefixExtension)
        {
            platformLibExtension = ".lib";
            platformOutputLibExtension = "";
            platformPrefixExtension = string.Empty;
        }

        protected virtual string GetProjectLinkExecutableVcxprojTemplate()
        {
            return GetProjectLinkSharedVcxprojTemplate();
        }

        protected virtual string GetProjectLinkSharedVcxprojTemplate()
        {
            return _projectConfigurationsLinkTemplate;
        }

        protected virtual string GetProjectStaticLinkVcxprojTemplate()
        {
            return _projectConfigurationsStaticLinkTemplate;
        }

        protected IEnumerable<string> EnumerateSemiColonSeparatedString(string str)
        {
            string[] dirs = str.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var dir in dirs)
                yield return dir;
        }

        protected virtual IEnumerable<string> GetIncludePathsImpl(IGenerationContext context)
        {
            var includePaths = new OrderableStrings();
            includePaths.AddRange(context.Configuration.IncludePrivatePaths);
            includePaths.AddRange(context.Configuration.IncludePaths);
            includePaths.AddRange(context.Configuration.DependenciesIncludePaths);

            includePaths.Sort();
            return includePaths;
        }

        protected virtual IEnumerable<IncludeWithPrefix> GetPlatformIncludePathsWithPrefixImpl(IGenerationContext context)
        {
            yield break;
        }

        [Obsolete("Implement GetPlatformIncludePathsWithPrefixImpl instead")]
        protected virtual IEnumerable<string> GetPlatformIncludePathsImpl(IGenerationContext context)
        {
            yield break;
        }

        protected virtual IEnumerable<string> GetResourceIncludePathsImpl(IGenerationContext context)
        {
            var resourceIncludePaths = new OrderableStrings();
            resourceIncludePaths.AddRange(context.Configuration.ResourceIncludePrivatePaths);
            resourceIncludePaths.AddRange(context.Configuration.ResourceIncludePaths);
            resourceIncludePaths.AddRange(context.Configuration.DependenciesResourceIncludePaths);

            return resourceIncludePaths;
        }

        #endregion
    }
}