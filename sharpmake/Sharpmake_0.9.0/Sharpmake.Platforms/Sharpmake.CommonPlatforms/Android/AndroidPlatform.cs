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
using System.Collections.Generic;
using Sharpmake.Generators;
using Sharpmake.Generators.VisualStudio;

namespace Sharpmake
{
    public static partial class Android
    {
        [PlatformImplementation(Platform.android,
            typeof(IPlatformDescriptor),
            typeof(IPlatformVcxproj),
            typeof(Project.Configuration.IConfigurationTasks))]
        public sealed partial class AndroidPlatform : BasePlatform, Project.Configuration.IConfigurationTasks
        {
            #region IPlatformDescriptor implementation.
            public override string SimplePlatformString => "ARM";
            public override bool IsMicrosoftPlatform => false;
            public override bool IsPcPlatform => false;
            public override bool IsUsingClang => true;
            public override bool HasDotNetSupport => false;
            public override bool HasSharedLibrarySupport => true;
            public override bool HasPrecompiledHeaderSupport => true;
            #endregion

            #region Project.Configuration.IConfigurationTasks implementation
            public void SetupDynamicLibraryPaths(Project.Configuration configuration, DependencySetting dependencySetting, Project.Configuration dependency)
            {
                // Not tested. We may need to root the path like we do in SetupStaticLibraryPaths.
                DefaultPlatform.SetupLibraryPaths(configuration, dependencySetting, dependency);
            }

            public void SetupStaticLibraryPaths(Project.Configuration configuration, DependencySetting dependencySetting, Project.Configuration dependency)
            {
                if (!dependency.Project.GetType().IsDefined(typeof(Export), false))
                {
                    if (dependencySetting.HasFlag(DependencySetting.LibraryPaths))
                        configuration.AddDependencyBuiltTargetLibraryPath(dependency.TargetLibraryPath, dependency.TargetLibraryPathOrderNumber);

                    // Clang and GCC have trouble finding "Additional Dependencies" through "Additional Library Directories" when compiling
                    // for Android under VS. As a work around, we use rooted path for dependencies library files.
                    if (dependencySetting.HasFlag(DependencySetting.LibraryFiles))
                        configuration.AddDependencyBuiltTargetLibraryFile(dependency.TargetPath + "\\" + dependency.TargetFileFullName, dependency.TargetFileOrderNumber);
                }
                else
                {
                    if (dependencySetting.HasFlag(DependencySetting.LibraryPaths))
                        configuration.DependenciesOtherLibraryPaths.Add(dependency.TargetLibraryPath, dependency.TargetLibraryPathOrderNumber);

                    // Clang and GCC have trouble finding "Additional Dependencies" through "Additional Library Directories" when compiling
                    // for Android under VS. As a work around, we use rooted path for dependencies library files.
                    if (dependencySetting.HasFlag(DependencySetting.LibraryFiles))
                        configuration.DependenciesOtherLibraryFiles.Add(dependency.TargetPath + "\\" + dependency.TargetFileFullName, dependency.TargetFileOrderNumber);
                }
            }

            public string GetDefaultOutputExtension(Project.Configuration.OutputType outputType)
            {
                switch (outputType)
                {
                    case Project.Configuration.OutputType.Exe:
                        return string.Empty;
                    case Project.Configuration.OutputType.Dll:
                        return "so";
                    default:
                        return "a";
                }
            }

            public IEnumerable<string> GetPlatformLibraryPaths(Project.Configuration configuration)
            {
                yield break;
            }
            #endregion

            #region IPlatformVcxproj implementation
            public override string ProgramDatabaseFileExtension => string.Empty;
            public override string SharedLibraryFileExtension => "so";
            public override string ExecutableFileExtension => string.Empty;

            public override void GeneratePlatformSpecificProjectDescription(IVcxprojGenerationContext context, IFileGenerator generator)
            {
                string applicationTypeRevision = context.DevelopmentEnvironmentsRange.MinDevEnv == DevEnv.vs2017 ? "3.0" : "2.0";

                using (generator.Declare("applicationTypeRevision", applicationTypeRevision))
                {
                    generator.Write(_projectDescriptionPlatformSpecific);
                }
            }

            public override void GenerateProjectCompileVcxproj(IVcxprojGenerationContext context, IFileGenerator generator)
            {
                generator.Write(_projectConfigurationsCompileTemplate);
            }

            public override void GenerateProjectConfigurationGeneral(IVcxprojGenerationContext context, IFileGenerator generator)
            {
                generator.Write(_projectConfigurationsGeneralTemplate);
            }

            public override void GenerateProjectConfigurationGeneral2(IVcxprojGenerationContext context, IFileGenerator generator)
            {
                generator.Write(_projectConfigurationsGeneral2Template);
            }

            protected override string GetProjectLinkSharedVcxprojTemplate()
            {
                return _projectConfigurationsSharedLinkTemplate;
            }

            protected override string GetProjectStaticLinkVcxprojTemplate()
            {
                return _projectConfigurationsStaticLinkTemplate;
            }

            public override void SelectCompilerOptions(IGenerationContext context)
            {
                var options = context.Options;
                var cmdLineOptions = context.CommandLineOptions;
                var conf = context.Configuration;

                // Although we add options to cmdLineOptions, FastBuild isn't supported yet for Android projects.

                context.SelectOption
                (
                Options.Option(Options.Android.General.AndroidAPILevel.Default, () => { options["AndroidAPILevel"] = RemoveLineTag; }),
                Options.Option(Options.Android.General.AndroidAPILevel.Android19, () => { options["AndroidAPILevel"] = "android-19"; }),
                Options.Option(Options.Android.General.AndroidAPILevel.Android21, () => { options["AndroidAPILevel"] = "android-21"; }),
                Options.Option(Options.Android.General.AndroidAPILevel.Android22, () => { options["AndroidAPILevel"] = "android-22"; }),
                Options.Option(Options.Android.General.AndroidAPILevel.Android23, () => { options["AndroidAPILevel"] = "android-23"; }),
                Options.Option(Options.Android.General.AndroidAPILevel.Android24, () => { options["AndroidAPILevel"] = "android-24"; })
                );

                context.SelectOption
                (
                Options.Option(Options.Android.General.PlatformToolset.Default, () => { options["PlatformToolset"] = RemoveLineTag; }),
                Options.Option(Options.Android.General.PlatformToolset.Clang_3_8, () => { options["PlatformToolset"] = "Clang_3_8"; }),
                Options.Option(Options.Android.General.PlatformToolset.Gcc_4_9, () => { options["PlatformToolset"] = "Gcc_4_9"; })
                );

                context.SelectOption
                (
                Options.Option(Options.Android.General.UseOfStl.System, () => { options["UseOfStl"] = "system"; }),
                Options.Option(Options.Android.General.UseOfStl.GAbiPP_Static, () => { options["UseOfStl"] = "gabi++_static"; }),
                Options.Option(Options.Android.General.UseOfStl.GAbiPP_Shared, () => { options["UseOfStl"] = "gabi++_shared"; }),
                Options.Option(Options.Android.General.UseOfStl.StlPort_Static, () => { options["UseOfStl"] = "stlport_static"; }),
                Options.Option(Options.Android.General.UseOfStl.StlPort_Shared, () => { options["UseOfStl"] = "stlport_shared"; }),
                Options.Option(Options.Android.General.UseOfStl.GnuStl_Static, () => { options["UseOfStl"] = "gnustl_static"; }),
                Options.Option(Options.Android.General.UseOfStl.GnuStl_Shared, () => { options["UseOfStl"] = "gnustl_shared"; }),
                Options.Option(Options.Android.General.UseOfStl.LibCpp_Static, () => { options["UseOfStl"] = "c++_static"; }),
                Options.Option(Options.Android.General.UseOfStl.LibCpp_Shared, () => { options["UseOfStl"] = "c++_shared"; })
                );

                context.SelectOption
                (
                Options.Option(Options.Android.General.ThumbMode.Thumb, () => { options["ThumbMode"] = "Thumb"; }),
                Options.Option(Options.Android.General.ThumbMode.ARM, () => { options["ThumbMode"] = "ARM"; }),
                Options.Option(Options.Android.General.ThumbMode.Disabled, () => { options["ThumbMode"] = "Disabled"; })
                );

                context.SelectOption
                (
                Options.Option(Options.Android.General.WarningLevel.TurnOffAllWarnings, () => { options["WarningLevel"] = "TurnOffAllWarnings"; cmdLineOptions["WarningLevel"] = "-w"; }),
                Options.Option(Options.Android.General.WarningLevel.EnableAllWarnings, () => { options["WarningLevel"] = "EnableAllWarnings"; cmdLineOptions["WarningLevel"] = "-Wall"; })
                );

                context.SelectOption
                (
                Options.Option(Options.Android.Compiler.CppLanguageStandard.Cpp98, () => { options["CppLanguageStandard"] = "c++98"; cmdLineOptions["CppLanguageStandard"] = "-std=c++98"; }),
                Options.Option(Options.Android.Compiler.CppLanguageStandard.Cpp11, () => { options["CppLanguageStandard"] = "c++11"; cmdLineOptions["CppLanguageStandard"] = "-std=c++11"; }),
                Options.Option(Options.Android.Compiler.CppLanguageStandard.Cpp1y, () => { options["CppLanguageStandard"] = "c++1y"; cmdLineOptions["CppLanguageStandard"] = "-std=c++1y"; }),
                Options.Option(Options.Android.Compiler.CppLanguageStandard.GNU_Cpp98, () => { options["CppLanguageStandard"] = "gnu++98"; cmdLineOptions["CppLanguageStandard"] = "-std=gnu++98"; }),
                Options.Option(Options.Android.Compiler.CppLanguageStandard.GNU_Cpp11, () => { options["CppLanguageStandard"] = "gnu++11"; cmdLineOptions["CppLanguageStandard"] = "-std=gnu++11"; }),
                Options.Option(Options.Android.Compiler.CppLanguageStandard.GNU_Cpp1y, () => { options["CppLanguageStandard"] = "gnu++1y"; cmdLineOptions["CppLanguageStandard"] = "-std=gnu++1y"; })
                );

                context.SelectOption
                (
                Options.Option(Options.Android.Compiler.DataLevelLinking.Disable, () => { options["EnableDataLevelLinking"] = "false"; cmdLineOptions["EnableDataLevelLinking"] = RemoveLineTag; }),
                Options.Option(Options.Android.Compiler.DataLevelLinking.Enable, () => { options["EnableDataLevelLinking"] = "true"; cmdLineOptions["EnableDataLevelLinking"] = "-fdata-sections"; })
                );

                context.SelectOption
                (
                Options.Option(Options.Android.Compiler.DebugInformationFormat.None, () => { options["DebugInformationFormat"] = "None"; cmdLineOptions["DebugInformationFormat"] = "-g0"; }),
                Options.Option(Options.Android.Compiler.DebugInformationFormat.FullDebug, () => { options["DebugInformationFormat"] = "FullDebug"; cmdLineOptions["DebugInformationFormat"] = "-g2 -gdwarf-2"; }),
                Options.Option(Options.Android.Compiler.DebugInformationFormat.LineNumber, () => { options["DebugInformationFormat"] = "LineNumber"; cmdLineOptions["DebugInformationFormat"] = "-gline-tables-only"; })
                );

                context.SelectOption
                (
                Options.Option(Options.Android.Compiler.Exceptions.Disable, () => { options["ExceptionHandling"] = "Disabled"; cmdLineOptions["ExceptionHandling"] = "-fno-exceptions"; }),
                Options.Option(Options.Android.Compiler.Exceptions.Enable, () => { options["ExceptionHandling"] = "Enabled"; cmdLineOptions["ExceptionHandling"] = "-fexceptions"; }),
                Options.Option(Options.Android.Compiler.Exceptions.UnwindTables, () => { options["ExceptionHandling"] = "UnwindTables"; cmdLineOptions["ExceptionHandling"] = "-funwind-tables"; })
                );

                context.SelectOption
                (
                Options.Option(Options.Vc.General.TreatWarningsAsErrors.Disable, () => { options["TreatWarningAsError"] = "false"; cmdLineOptions["TreatWarningAsError"] = RemoveLineTag; }),
                Options.Option(Options.Vc.General.TreatWarningsAsErrors.Enable, () => { options["TreatWarningAsError"] = "true"; cmdLineOptions["TreatWarningAsError"] = "-Werror"; })
                );

                context.SelectOption
                (
                Options.Option(Options.Vc.Compiler.BufferSecurityCheck.Enable, () => { options["BufferSecurityCheck"] = "true"; cmdLineOptions["BufferSecurityCheck"] = RemoveLineTag; }),
                Options.Option(Options.Vc.Compiler.BufferSecurityCheck.Disable, () => { options["BufferSecurityCheck"] = "false"; cmdLineOptions["BufferSecurityCheck"] = "-fstack-protector"; })
                );

                context.SelectOption
                (
                Options.Option(Options.Vc.Compiler.FunctionLevelLinking.Disable, () => { options["EnableFunctionLevelLinking"] = "false"; cmdLineOptions["EnableFunctionLevelLinking"] = RemoveLineTag; }),
                Options.Option(Options.Vc.Compiler.FunctionLevelLinking.Enable, () => { options["EnableFunctionLevelLinking"] = "true"; cmdLineOptions["EnableFunctionLevelLinking"] = "-ffunction-sections"; })
                );

                context.SelectOption
                (
                Options.Option(Options.Vc.Compiler.OmitFramePointers.Disable, () => { options["OmitFramePointers"] = "false"; cmdLineOptions["OmitFramePointers"] = "-fno-omit-frame-pointer"; }),
                Options.Option(Options.Vc.Compiler.OmitFramePointers.Enable, () => { options["OmitFramePointers"] = "true"; cmdLineOptions["OmitFramePointers"] = "-fomit-frame-pointer"; })
                );

                context.SelectOption
                (
                Options.Option(Options.Vc.Compiler.Optimization.Disable, () => { options["Optimization"] = "Disabled"; cmdLineOptions["Optimization"] = "-O0"; }),
                Options.Option(Options.Vc.Compiler.Optimization.MinimizeSize, () => { options["Optimization"] = "MinSize"; cmdLineOptions["Optimization"] = "-Os"; }),
                Options.Option(Options.Vc.Compiler.Optimization.MaximizeSpeed, () => { options["Optimization"] = "MaxSpeed"; cmdLineOptions["Optimization"] = "-O2"; }),
                Options.Option(Options.Vc.Compiler.Optimization.FullOptimization, () => { options["Optimization"] = "Full"; cmdLineOptions["Optimization"] = "-O3"; })
                );

                context.SelectOption
                (
                Options.Option(Options.Vc.Compiler.MultiProcessorCompilation.Enable, () => { options["UseMultiToolTask"] = "true"; }),
                Options.Option(Options.Vc.Compiler.MultiProcessorCompilation.Disable, () => { options["UseMultiToolTask"] = "false"; })
                );

                context.SelectOption
                (
                Options.Option(Options.Vc.Compiler.RTTI.Disable, () => { options["RuntimeTypeInfo"] = "false"; cmdLineOptions["RuntimeTypeInfo"] = "-fno-rtti"; }),
                Options.Option(Options.Vc.Compiler.RTTI.Enable, () => { options["RuntimeTypeInfo"] = "true"; cmdLineOptions["RuntimeTypeInfo"] = "-frtti"; })
                );
            }

            public override void SelectLinkerOptions(IGenerationContext context)
            {
                var options = context.Options;
                var cmdLineOptions = context.CommandLineOptions;
                var conf = context.Configuration;

                context.SelectOption
                (
                Options.Option(Options.Android.Linker.DebuggerSymbolInformation.IncludeAll, () => { options["DebuggerSymbolInformation"] = "true"; }),
                Options.Option(Options.Android.Linker.DebuggerSymbolInformation.OmitUnneededSymbolInformation, () => { options["DebuggerSymbolInformation"] = "OmitUnneededSymbolInformation"; cmdLineOptions["DebuggerSymbolInformation"] = "-Wl,--strip-unneeded"; }),
                Options.Option(Options.Android.Linker.DebuggerSymbolInformation.OmitDebuggerSymbolInformation, () => { options["DebuggerSymbolInformation"] = "OmitDebuggerSymbolInformation"; cmdLineOptions["DebuggerSymbolInformation"] = "-Wl,--strip-debug"; }),
                Options.Option(Options.Android.Linker.DebuggerSymbolInformation.OmitAllSymbolInformation, () => { options["DebuggerSymbolInformation"] = "OmitAllSymbolInformation"; cmdLineOptions["DebuggerSymbolInformation"] = "-Wl,--strip-all"; })
                );

                context.SelectOption
                (
                Options.Option(Options.Vc.Linker.Incremental.Default, () => { options["IncrementalLink"] = RemoveLineTag; cmdLineOptions["LinkIncremental"] = RemoveLineTag; }),
                Options.Option(Options.Vc.Linker.Incremental.Disable, () => { options["IncrementalLink"] = "false"; cmdLineOptions["LinkIncremental"] = RemoveLineTag; }),
                Options.Option(Options.Vc.Linker.Incremental.Enable, () => { options["IncrementalLink"] = "true"; cmdLineOptions["LinkIncremental"] = "-Wl,--incremental"; })
                );
            }

            public override void SetupPlatformLibraryOptions(ref string platformLibExtension, ref string platformOutputLibExtension, ref string platformPrefixExtension)
            {
                platformLibExtension = ".a";
                platformOutputLibExtension = ".a";
                platformPrefixExtension = string.Empty;
            }
            #endregion
        }
    }
}
