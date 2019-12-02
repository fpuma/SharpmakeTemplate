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

namespace Sharpmake.Generators.Apple
{
    public partial class XCodeProj
    {
        private class Template
        {
            public static string GlobalHeader =
@"// !$*UTF8*$!
{
	archiveVersion = [archiveVersion];
	classes = {
	};
	objectVersion = [objectVersion];
	objects = {

";
            public static string GlobalFooter =
@"	};
	rootObject = [RootObject.Uid] /* Project object */;
}
";

            public static string SectionBegin = @"/* Begin [item.SectionString] section */
";
            public static string SectionEnd = @"/* End [item.SectionString] section */

";

            public static string SectionSubItem =
@"				[item.Uid] /* [item.Identifier] */,
";
            public static string ProjectReferenceSubItem =
@"				{
					ProductGroup = [group.Uid] /* Products */;
					ProjectRef = [project.Uid] /* [project.Name] */;
				},
";
            public static string ProjectTargetAttribute =
@"					[item.Uid] /* [item.Identifier] */ =
					{
						DevelopmentTeam = [project.DevelopmentTeam];
						SystemCapabilities =
						{
							com.apple.iCloud =
							{
								enabled = [project.ICloudSupport];
							};
						};
					};
";

            public static Dictionary<ItemSection, string> Section = new Dictionary<ItemSection, string>
            {
                { ItemSection.PBXBuildFile,
@"		[item.Uid] /* [item.File.Name] in [item.File.Type] */ =
		{
			isa = PBXBuildFile;
			fileRef = [item.File.Uid] /* [item.File.Name] */;
		};
"               },

                { ItemSection.PBXContainerItemProxy,
@"		[item.Uid] /* PBXContainerItemProxy */ =
		{
			isa = PBXContainerItemProxy;
			containerPortal = [item.ProjectReference.Uid];
			proxyType = [item.ProxyType];
			remoteGlobalIDString = [item.ProxyItem.Uid];
			remoteInfo = [item.ProjectReference.ProjectName];
		};
"               },

                { ItemSection.PBXFileReference,
@"		[item.Uid] /* [item.Name] */ =
		{
			isa = PBXFileReference;
			explicitFileType = [item.ExplicitFileType];
			lastKnownFileType = [item.FileType];
			includeInIndex = [item.IncludeInIndex];
			name = ""[item.Name]"";
			path = ""[item.Path]"";
			sourceTree = [item.SourceTree];
		};
"               },

                { ItemSection.PBXFrameworksBuildPhase,
@"		[item.Uid] /* Frameworks */ =
		{
			isa = PBXFrameworksBuildPhase;
			buildActionMask = [item.BuildActionMask];
			files = (
[itemChildren]
			);
			runOnlyForDeploymentPostprocessing = [item.RunOnlyForDeploymentPostprocessing];
		};
"               },

                { ItemSection.PBXGroup,
@"		[item.Uid] /* [item.Identifier] */ =
		{
			isa = PBXGroup;
			children = (
[itemChildren]
			);
			name = ""[item.Name]"";
			path = ""[item.Path]"";
			sourceTree = [item.SourceTree];
		};
"               },

                { ItemSection.PBXNativeTarget,
@"		[item.Uid] /* [item.Identifier] */ =
		{
			isa = PBXNativeTarget;
			buildConfigurationList = [item.ConfigurationList.Uid] /* Build configuration list for PBXNativeTarget ""[item.Identifier]"" */;
			buildPhases =
			(
				[item.ResourcesBuildPhase.Uid] /* Resources */,
				[item.SourcesBuildPhase.Uid] /* Sources */,
				[item.FrameworksBuildPhase.Uid] /* Frameworks */,
			);
			buildRules = (
			);
			dependencies = (
[itemChildren]
			);
			name = ""[item.Identifier]"";
			productInstallPath = ""[item.ProductInstallPath]"";
			productName = ""[item.Identifier]"";
			productReference = [item.OutputFile.Uid] /* [item.OutputFile.Name] */;
			productType = ""[item.ProductType]"";
		};
"               },

                { ItemSection.PBXProject,
@"		[item.Uid] /* Project object */ =
		{
			isa = PBXProject;
			attributes =
			{
				TargetAttributes =
				{
[itemTargetAttributes]
				};
			};
			buildConfigurationList = [item.ConfigurationList.Uid] /* Build configuration list for PBXProject ""[item.Identifier]"" */;
			compatibilityVersion = ""[item.CompatibilityVersion]"";
			developmentRegion = English;
			hasScannedForEncodings = 0;
			knownRegions = (
				en,
			);
			mainGroup = [item.MainGroup.Uid] /* [item.MainGroup.Name] */;
			projectDirPath = """";
			projectReferences = (
[itemProjectReferences]
			);
			projectRoot = """";
			targets = (
[itemTargets]
			);
		};
"               },

                { ItemSection.PBXReferenceProxy,
@"		[item.Uid] /* [item.OutputFile.Name] */ =
		{
			isa = PBXReferenceProxy;
			fileType = [item.FileType];
			path = [item.OutputFile.Name];
			remoteRef = [item.Proxy.Uid] /* [item.Proxy.Identifier] */;
			sourceTree = [item.SourceTree];
		};
"               },

                { ItemSection.PBXResourcesBuildPhase,
@"		[item.Uid] /* Resources */ =
		{
			isa = PBXResourcesBuildPhase;
			buildActionMask = [item.BuildActionMask];
			files = (
[itemChildren]
			);
			runOnlyForDeploymentPostprocessing = [item.RunOnlyForDeploymentPostprocessing];
		};
"               },

                { ItemSection.PBXSourcesBuildPhase,
@"		[item.Uid] /* Sources */ =
		{
			isa = PBXSourcesBuildPhase;
			buildActionMask = [item.BuildActionMask];
			files = (
[itemChildren]
			);
			runOnlyForDeploymentPostprocessing = [item.RunOnlyForDeploymentPostprocessing];
		};
"               },

                { ItemSection.PBXVariantGroup,
@"
"               },

                { ItemSection.PBXTargetDependency,
@"		[item.Uid] /* PBXTargetDependency */ =
		{
			isa = PBXTargetDependency;
			name = [item.ProjectReference.Name];
			targetProxy = [item.Proxy.Uid];
			target = [item.TargetIdentifier];
		};
"               },

                { ItemSection.XCBuildConfiguration_NativeTarget,
@"		[item.Uid] /* [item.Optimization] */ =
		{
			isa = XCBuildConfiguration;
			buildSettings =
			{
				ALWAYS_SEARCH_USER_PATHS = [item.Options.AlwaysSearchUserPaths];
				CODE_SIGN_IDENTITY = ""[item.Options.CodeSigningIdentity]"";
				""CODE_SIGN_IDENTITY[sdk=iphoneos*]"" = ""[item.Options.CodeSigningIdentity]"";
				DEBUG_INFORMATION_FORMAT = [item.Options.DebugInformationFormat];
				DEAD_CODE_STRIPPING = [item.Options.DeadStripping];
				CONFIGURATION_BUILD_DIR = ""[item.Configuration.TargetPath]"";
				FRAMEWORK_SEARCH_PATHS = (
[item.Options.FrameworkPaths]
				);
				IPHONEOS_DEPLOYMENT_TARGET = ""[item.Options.IPhoneOSDeploymentTarget]"";
				MACH_O_TYPE = ""[item.Options.MachOType]"";
				COPY_PHASE_STRIP = [item.Options.StripDebugSymbolsDuringCopy];
				GCC_ENABLE_CPP_EXCEPTIONS = [item.Options.CppExceptionHandling];
				GCC_ENABLE_OBJC_EXCEPTIONS = [item.Options.ObjCExceptionHandling];
				GCC_ENABLE_CPP_RTTI = [item.Options.RuntimeTypeInfo];
				GCC_GENERATE_DEBUGGING_SYMBOLS = [item.Options.GenerateDebuggingSymbols];
				GCC_INLINES_ARE_PRIVATE_EXTERN = [item.Options.PrivateInlines];
				DEPLOYMENT_POSTPROCESSING = [item.Options.DeploymentPostProcessing];
				GCC_SYMBOLS_PRIVATE_EXTERN = [item.Options.PrivateSymbols];
				MACOSX_DEPLOYMENT_TARGET = [item.Options.MacOSDeploymentTarget];
				GCC_DYNAMIC_NO_PIC = [item.Options.DynamicNoPic];
				GCC_MODEL_TUNING = [item.Options.ModelTuning];
				INFOPLIST_FILE = ""[item.Options.InfoPListFile]"";
				CODE_SIGN_ENTITLEMENTS = ""[item.Options.CodeSignEntitlements]"";
				INSTALL_PATH = ""[item.NativeTarget.ProductInstallPath]"";
				PRESERVE_DEAD_CODE_INITS_AND_TERMS = [item.Options.PreserveDeadCodeInitsAndTerms];
				PRODUCT_NAME = ""[item.Configuration.TargetFileFullName]"";
				PROVISIONING_PROFILE = ""[item.Options.ProvisioningProfile]"";
				VALID_ARCHS = ""[item.Options.ValidArchs]"";
				SKIP_INSTALL = [item.Options.SkipInstall];
			};
			name = [item.Optimization];
		};
"               },

                { ItemSection.XCBuildConfiguration_UnitTestTarget,
@"		[item.Uid] /* [item.Optimization] */ =
		{
			isa = XCBuildConfiguration;
			buildSettings =
			{
				ALWAYS_SEARCH_USER_PATHS = [item.Options.AlwaysSearchUserPaths];
				BUNDLE_LOADER = ""[testHost]"";
				CODE_SIGN_IDENTITY = ""[item.Options.CodeSigningIdentity]"";
				""CODE_SIGN_IDENTITY[sdk=iphoneos*]"" = ""[item.Options.CodeSigningIdentity]"";
				CONFIGURATION_BUILD_DIR = ""[item.Configuration.TargetPath]"";
				FRAMEWORK_SEARCH_PATHS = (
[item.Options.FrameworkPaths]
				);
				GCC_DYNAMIC_NO_PIC = [item.Options.DynamicNoPic];
				GCC_ENABLE_CPP_RTTI = [item.Options.RuntimeTypeInfo];
				GCC_SYMBOLS_PRIVATE_EXTERN = [item.Options.PrivateSymbols];
				INFOPLIST_FILE = ""[item.Options.InfoPListFile]"";
				PRODUCT_NAME = ""[item.Configuration.TargetFileFullName]"";
				TEST_HOST = ""[testHost]"";
				WRAPPER_EXTENSION = xctest;
			};
			name = [item.Optimization];
		};
"               },

                { ItemSection.XCBuildConfiguration_Project,
@"		[item.Uid] /* [item.Optimization] */ =
		{
			isa = XCBuildConfiguration;
			buildSettings =
			{
				ARCHS = [item.Options.Archs];
				SYMROOT = ""[item.Configuration.TargetPath]"";
				CONFIGURATION_BUILD_DIR = ""$(SYMROOT)"";
				CONFIGURATION_TEMP_DIR = ""$(OBJROOT)"";
				CLANG_CXX_LANGUAGE_STANDARD = ""[item.Options.CppStandard]"";
				CLANG_CXX_LIBRARY = ""[item.Options.LibraryStandard]"";
				CLANG_ENABLE_OBJC_ARC = [item.Options.AutomaticReferenceCounting];
				CLANG_WARN_BOOL_CONVERSION = [item.Options.WarningBooleanConversion];
				CLANG_WARN_CONSTANT_CONVERSION = [item.Options.WarningConstantConversion];
				CLANG_WARN_DIRECT_OBJC_ISA_USAGE = [item.Options.WarningDirectIsaUsage];
				CLANG_WARN_EMPTY_BODY = [item.Options.WarningEmptyBody];
				CLANG_WARN_ENUM_CONVERSION = [item.Options.WarningEnumConversion];
				CLANG_WARN_INT_CONVERSION = [item.Options.WarningIntConversion];
				CLANG_WARN_OBJC_ROOT_CLASS = [item.Options.WarningRootClass];
				CLANG_WARN__DUPLICATE_METHOD_MATCH = [item.Options.WarningDuplicateMethodMatch];
				GCC_C_LANGUAGE_STANDARD = ""[item.Options.CStandard]"";
				GCC_PRECOMPILE_PREFIX_HEADER = [item.Options.UsePrecompiledHeader];
				GCC_PREFIX_HEADER = ""[item.Options.PrecompiledHeader]"";
				GCC_OPTIMIZATION_LEVEL = [item.Options.OptimizationLevel];
				GCC_PREPROCESSOR_DEFINITIONS = (
[item.Options.PreprocessorDefinitions]
				);
				GCC_WARN_64_TO_32_BIT_CONVERSION = [item.Options.Warning64To32BitConversion];
				GCC_WARN_ABOUT_RETURN_TYPE = [item.Options.WarningReturnType];
				GCC_WARN_UNDECLARED_SELECTOR = [item.Options.WarningUndeclaredSelector];

				GCC_WARN_UNINITIALIZED_AUTOS = [item.Options.WarningUniniatializedAutos];
				GCC_WARN_UNUSED_FUNCTION = [item.Options.WarningUnusedFunction];
				GCC_WARN_UNUSED_VARIABLE = [item.Options.WarningUnusedVariable];
				GCC_TREAT_WARNINGS_AS_ERRORS = [item.Options.TreatWarningsAsErrors];
				HEADER_SEARCH_PATHS = (
[item.Options.IncludePaths]
				);
				LIBRARY_SEARCH_PATHS = ( [item.Options.RemoveLibraryPaths]
[item.Options.LibraryPaths]
				); [item.Options.RemoveLibraryPaths]
				""LIBRARY_SEARCH_PATHS[sdk=iphoneos*]"" = ( [item.Options.RemoveSpecificDeviceLibraryPaths]
[item.Options.SpecificDeviceLibraryPaths]
				); [item.Options.RemoveSpecificDeviceLibraryPaths]
				""LIBRARY_SEARCH_PATHS[sdk=iphonesimulator*]"" = ( [item.Options.RemoveSpecificSimulatorLibraryPaths]
[item.Options.SpecificSimulatorLibraryPaths]
				); [item.Options.RemoveSpecificSimulatorLibraryPaths]
				OBJROOT = ""[item.Configuration.IntermediatePath]"";
				SDKROOT = ""[item.Options.SDKRoot]"";
				TARGETED_DEVICE_FAMILY = ""[item.Options.TargetedDeviceFamily]"";
				OTHER_CPLUSPLUSFLAGS = (
[item.Options.CompilerOptions]
				);
				OTHER_LDFLAGS = (
[item.Options.LinkerOptions]
				);
			};
			name = [item.Optimization];
		};
"               },

                { ItemSection.XCConfigurationList,
@"		[item.Uid] /* Build configuration list for [item.ConfigurationType] ""[item.RelatedItem.Identifier]"" */ =
		{
			isa = XCConfigurationList;
			buildConfigurations = (
[itemChildren]
			);
			defaultConfigurationIsVisible = 0;
			defaultConfigurationName = ""[item.DefaultConfiguration.Identifier]"";
		};
"               }
            };

            public static string SchemeTestableReference =
@"
         <TestableReference
            skipped = ""NO"">
            <BuildableReference
               BuildableIdentifier = ""primary""
               BlueprintIdentifier = ""[item.Uid]""
               BuildableName = ""[item.OutputFile.BuildableName]""
               BlueprintName = ""[item.Identifier]""
               ReferencedContainer = ""container:[projectFile].xcodeproj"">
            </BuildableReference>
         </TestableReference>";

            public static string SchemeFileTemplate =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<Scheme
   LastUpgradeVersion = ""0460""
   version = ""1.3"">
   <BuildAction
      parallelizeBuildables = ""YES""
      buildImplicitDependencies = ""YES"">
      <BuildActionEntries>
         <BuildActionEntry
            buildForTesting = ""YES""
            buildForRunning = ""YES""
            buildForProfiling = ""YES""
            buildForArchiving = ""YES""
            buildForAnalyzing = ""YES"">
            <BuildableReference
               BuildableIdentifier = ""primary""
               BlueprintIdentifier = ""[item.Uid]""
               BuildableName = ""[item.OutputFile.BuildableName]""
               BlueprintName = ""[item.Identifier]""
               ReferencedContainer = ""container:[projectFile].xcodeproj"">
            </BuildableReference>
         </BuildActionEntry>
      </BuildActionEntries>
   </BuildAction>
   <TestAction
      selectedDebuggerIdentifier = ""Xcode.DebuggerFoundation.Debugger.LLDB""
      selectedLauncherIdentifier = ""Xcode.DebuggerFoundation.Launcher.LLDB""
      shouldUseLaunchSchemeArgsEnv = ""YES""
      buildConfiguration = ""Debug"">
      <Testables>[testableElements]
      </Testables>
   </TestAction>
   <LaunchAction
      selectedDebuggerIdentifier = ""Xcode.DebuggerFoundation.Debugger.LLDB""
      selectedLauncherIdentifier = ""Xcode.DebuggerFoundation.Launcher.LLDB""
      launchStyle = ""0""
      useCustomWorkingDirectory = ""NO""
      buildConfiguration = ""Debug""
      ignoresPersistentStateOnLaunch = ""NO""
      debugDocumentVersioning = ""YES""
      allowLocationSimulation = ""YES"">
      <BuildableProductRunnable>
          <BuildableReference
              BuildableIdentifier = ""primary""
              BlueprintIdentifier = ""[item.Uid]""
              BuildableName = ""[item.OutputFile.BuildableName]""
              BlueprintName = ""[item.Identifier]""
              ReferencedContainer = ""container:[projectFile].xcodeproj"">
          </BuildableReference>
      </BuildableProductRunnable>
      <AdditionalOptions>
      </AdditionalOptions>
   </LaunchAction>
   <ProfileAction
      shouldUseLaunchSchemeArgsEnv = ""YES""
      savedToolIdentifier = """"
      useCustomWorkingDirectory = ""NO""
      buildConfiguration = ""Debug""
      debugDocumentVersioning = ""YES"">
   </ProfileAction>
   <AnalyzeAction
      buildConfiguration = ""Debug"">
   </AnalyzeAction>
   <ArchiveAction
      buildConfiguration = ""Debug""
      revealArchiveInOrganizer = ""YES"">
   </ArchiveAction>
</Scheme>
";
        }
    }
}
