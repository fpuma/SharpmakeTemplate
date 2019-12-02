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
using System.IO;

namespace Sharpmake
{
    public static partial class Durango
    {
        public static class GlobalSettings
        {
            public static bool OverridenDurangoXDK { get; set; } = false;

            private static string s_durangoXDK = null;
            public static string DurangoXDK
            {
                get
                {
                    if (s_durangoXDK == null)
                    {
                        Sharpmake.Util.GetEnvironmentVariable(
                            "DurangoXDK",
                            @"c:\Program Files (x86)\Microsoft Durango XDK\",
                            ref s_durangoXDK,
                            true
                        );
                    }
                    return s_durangoXDK;
                }

                set
                {
                    s_durangoXDK = value;
                    OverridenDurangoXDK = true;
                }
            }

            private static string s_xboxOneExtensionSDK = null;
            public static string XboxOneExtensionSDK
            {
                get
                {
                    if (s_xboxOneExtensionSDK == null)
                    {
                        string xboxOneExtensionSDKLatest = null;

                        // get the XboxOneExtensionSDKLatest, which looks like
                        // C:\Program Files (x86)\Microsoft SDKs\Durango.160300\v8.0\
                        // we just want the path to Microsoft SDKs, the version number is irrelevant
                        if (Sharpmake.Util.GetEnvironmentVariable("XboxOneExtensionSDKLatest", null, ref xboxOneExtensionSDKLatest, true) != null)
                            s_xboxOneExtensionSDK = Sharpmake.Util.SimplifyPath(Path.Combine(xboxOneExtensionSDKLatest, "..", ".."));
                        else
                            s_xboxOneExtensionSDK = @"C:\Program Files (x86)\Microsoft SDKs\"; // maybe should throw a warning
                    }
                    return s_xboxOneExtensionSDK;
                }

                set
                {
                    s_xboxOneExtensionSDK = value;
                }
            }

            /// <summary>
            /// Ignore when the path for the XdkTargetEdition is missing.
            /// In the TG we do a SDK Switch (which creates a symlink to the SDK needed) before building
            /// but after generation so it's possible that the path does not exist during generation
            /// </summary>
            public static bool IgnoreMissingXdkEditionTargetPath { get; set; }

            /// <summary>
            /// Xdk Edition Target
            /// Build project against a specific XDK Edition.
            /// Do not set to build against the XDK with the highest version
            /// </summary>
            private static string s_xdkEditionTarget = null;
            public static string XdkEditionTarget
            {
                get
                {
                    if (s_xdkEditionTarget == null)
                    {
                        if (!Util.IsDurangoSideBySideXDK())
                            throw new Error("XdkEdition is not available with this XDK!");

                        s_xdkEditionTarget = Util.GetLatestDurangoSideBySideXDK();
                    }
                    return s_xdkEditionTarget;
                }

                set
                {
                    s_xdkEditionTarget = value;
                    if (!IgnoreMissingXdkEditionTargetPath)
                    {
                        if (!Sharpmake.Util.DirectoryExists(Path.Combine(DurangoXDK, s_xdkEditionTarget)))
                            throw new Error("Cannot find required files for XDK edition " + s_xdkEditionTarget);
                    }
                }
            }

            /// <summary>
            /// EnableLegacyXdkHeaders to use VS2015 include and libraries on VS2017.
            /// </summary>
            internal const int _feb2018XdkEditionTarget = 180200;
            private static bool? s_enableLegacyXdkHeaders = null;
            public static bool EnableLegacyXdkHeaders
            {
                get
                {
                    if (s_enableLegacyXdkHeaders == null)
                    {
                        int xdkEdition;
                        s_enableLegacyXdkHeaders = Util.IsDurangoSideBySideXDK() && Util.TryParseXdkEditionTarget(XdkEditionTarget, out xdkEdition) && xdkEdition < _feb2018XdkEditionTarget;
                    }

                    return s_enableLegacyXdkHeaders.Value;
                }

                set
                {
                    int xdkEdition;
                    if (!Util.TryParseXdkEditionTarget(XdkEditionTarget, out xdkEdition) || xdkEdition < _feb2018XdkEditionTarget)
                        throw new NotSupportedException(nameof(EnableLegacyXdkHeaders) + $" is not yet supported with '{xdkEdition}'");

                    s_enableLegacyXdkHeaders = value;
                }
            }

            [Obsolete("Please use MSBuildGlobalSettings.GetCppPlatformFolder(DevEnv.vs2015, Platform.durango) and MSBuildGlobalSettings.SetCppPlatformFolder(DevEnv.vs2015, Platform.durango, value) instead")]
            public static string XdkEditionRootVS2015
            {
                get { return MSBuildGlobalSettings.GetCppPlatformFolder(DevEnv.vs2015, Platform.durango); }
                set { MSBuildGlobalSettings.SetCppPlatformFolder(DevEnv.vs2015, Platform.durango, value); }
            }
        }
    }
}
