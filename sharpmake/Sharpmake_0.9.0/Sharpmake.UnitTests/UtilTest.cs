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
using NUnit.Framework;

using System.IO;
using System.Linq;
using Sharpmake;
using System;

namespace SharpmakeUnitTests
{
    namespace NiceTypeNameTest
    {
        internal class DummyClass { };
        internal class DummyClass2 { };

        internal class DummyGeneric<T> { };

        internal class DummyGeneric2<T, U> { };

        public class NiceTypeName
        {
            [Test]
            public void NiceTypeNameOnSimpleType()
            {
                Assert.That(typeof(DummyClass).ToNiceTypeName(), Is.EqualTo("SharpmakeUnitTests.NiceTypeNameTest.DummyClass"));
                Assert.That(typeof(DummyClass2).ToNiceTypeName(), Is.EqualTo("SharpmakeUnitTests.NiceTypeNameTest.DummyClass2"));
            }

            [Test]
            public void NiceTypeNameOnGenericType()
            {
                Assert.That(typeof(DummyGeneric<DummyClass>).ToNiceTypeName(),
                            Is.EqualTo("SharpmakeUnitTests.NiceTypeNameTest.DummyGeneric<SharpmakeUnitTests.NiceTypeNameTest.DummyClass>"));
                Assert.That(typeof(DummyGeneric2<DummyClass, DummyClass2>).ToNiceTypeName(),
                            Is.EqualTo("SharpmakeUnitTests.NiceTypeNameTest.DummyGeneric2<SharpmakeUnitTests.NiceTypeNameTest.DummyClass,SharpmakeUnitTests.NiceTypeNameTest.DummyClass2>"));
            }
        }
    }

    public class PathMakeStandard
    {
        [Test]
        public void LeavesEmptyStringsUntouched()
        {
            Assert.That(Util.PathMakeStandard(string.Empty), Is.EqualTo(string.Empty));
            Assert.That(Util.PathMakeStandard(""), Is.EqualTo(string.Empty));
            Assert.That(Util.PathMakeStandard(""), Is.EqualTo(""));
        }

        [Test]
        public void LeavesVariablesUntouched()
        {
            string expectedResult = "$(Console_SdkPackagesRoot)";
            if (!Util.IsRunningInMono())
                expectedResult = expectedResult.ToLower();
            Assert.That(Util.PathMakeStandard("$(Console_SdkPackagesRoot)"), Is.EqualTo(expectedResult));
        }

        [Test]
        public void ProcessesPathWithTrailingBackslash()
        {
            string expectedResult = Path.Combine("rd", "project", "dev", "projects", "sharpmake", "..", "..", "extern", "Geometrics");
            if (!Util.IsRunningInMono())
                expectedResult = expectedResult.ToLower();
            Assert.That(Util.PathMakeStandard(@"rd\project\dev\projects\sharpmake\..\..\extern\Geometrics\"), Is.EqualTo(expectedResult));
        }

        [Test]
        public void ProcessesPathWithTrailingBackslashAndADot()
        {
            var expectedResult = Path.Combine("rd", "project", "dev", "projects", "sharpmake", "..", "..", "extern", "Microsoft.CNG", "Lib");
            if (!Util.IsRunningInMono())
                expectedResult = expectedResult.ToLower();
            Assert.That(Util.PathMakeStandard(@"rd\project\dev\projects\sharpmake\..\..\extern\Microsoft.CNG\Lib\"), Is.EqualTo(expectedResult));
        }

        [Test]
        public void ProcessesPathWithMultipleTrailingBackslashes()
        {
            var expectedResult = Path.Combine("rd", "project", "dev", "projects", "sharpmake", "..", "..", "extern", "Microsoft.CNG", "Lib");
            if (!Util.IsRunningInMono())
                expectedResult = expectedResult.ToLower();
            Assert.That(Util.PathMakeStandard(@"rd\project\dev\projects\sharpmake\..\..\extern\Microsoft.CNG\Lib\\\"), Is.EqualTo(expectedResult));
        }
    }

    public class SimplifyPath
    {
        [Test]
        public void LeavesEmptyStringsUntouched()
        {
            Assert.That(Util.SimplifyPath(string.Empty), Is.EqualTo(string.Empty));
            Assert.That(Util.SimplifyPath(""), Is.EqualTo(string.Empty));
            Assert.That(Util.SimplifyPath(""), Is.EqualTo(""));
        }

        [Test]
        public void HandlesPathRelativeToCurrentFolder()
        {
            Assert.That(Util.SimplifyPath(@".\project\test.cpp"),
                Is.EqualTo(Path.Combine("project", "test.cpp")));

            Assert.That(Util.SimplifyPath(@".\.\.\.\project\.\test.cpp"),
                Is.EqualTo(Path.Combine("project", "test.cpp")));
        }

        [Test]
        public void HandlesReturningToParentFolder()
        {
            Assert.That(Util.SimplifyPath(@"test\..\test.cpp"),
                Is.EqualTo("test.cpp"));
        }

        [Test]
        public void HandlesReturningToParentFolderRelativeToCurrentFolder()
        {
            Assert.That(Util.SimplifyPath(@".\project\..\test.cpp"),
                Is.EqualTo("test.cpp"));

            Assert.That(Util.SimplifyPath(@".\.\.\.\project\..\test.cpp"),
                Is.EqualTo("test.cpp"));
        }

        [Test]
        public void CollapsesMultipleFolderSeparators()
        {
            Assert.That(Util.SimplifyPath(@".\\\project\..\test.cpp"),
                Is.EqualTo("test.cpp"));

            Assert.That(Util.SimplifyPath(@"\\\folder"),
                Is.EqualTo("folder"));
        }

        [Test]
        public void HandlesSlashesInFullPath()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            Assert.That(Util.SimplifyPath(currentDirectory + "\\main/test//t.cpp"),
                Is.EqualTo(Path.Combine(currentDirectory, "main", "test", "t.cpp")));
        }

        [Test]
        public void HandlesFolderParentsAtTheEnd()
        {
            Assert.That(Util.SimplifyPath(@"alpha\beta\gamma\sigma\omega\zeta\..\.."),
                Is.EqualTo(Path.Combine("alpha", "beta", "gamma", "sigma")));
        }

        [Test]
        public void LeavesCleanPathUntouched()
        {
            // Check that we do not change dot and dot dot
            Assert.That(".", Is.EqualTo(Util.SimplifyPath(".")));
            Assert.That("..", Is.EqualTo(Util.SimplifyPath("..")));

            Assert.That(Util.SimplifyPath(Util.PathMakeStandard(@"alpha\beta\gamma\sigma\omega\zeta\lambda\phi\")),
                Is.EqualTo(Path.Combine("alpha", "beta", "gamma", "sigma", "omega", "zeta", "lambda", "phi")));
        }
    }

    public class FakeTree
    {
        [SetUp]
        public void Init()
        {
            Util.FakePathPrefix = Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath);

            string[] files =
            {
                "./data/mod.el",
                "./code/test.h",
                "code/test.cpp",
                ".\\code\\main\\main.cpp",
                "./code/test/stuff.cpp"
            };

            foreach (string filePath in files.Select(Util.PathMakeStandard))
            {
                Util.AddNewFakeFile(filePath, 0);
            }
        }

        [TearDown]
        public void Shutdown()
        {
            Util.ClearFakeTree();
        }

        [Test, Repeat(2)]
        public void KeepsACountOfFakeFiles()
        {
            // Repetition is to ensure Shutdown() is restoring the global context
            // and not adding each time the Setup() is done
            Assert.That(Util.CountFakeFiles(), Is.EqualTo(5));
        }

        [Test]
        public void CanEmulateDirectories()
        {
            var directory = Path.Combine(Util.FakePathPrefix, "code");
            Assert.That(Util.DirectoryExists(directory), Is.True);

            var subDirectory = Path.Combine(Util.FakePathPrefix, "code", "main");
            Assert.That(Util.DirectoryExists(subDirectory), Is.True);

            var missingDirectory = Path.Combine(Util.FakePathPrefix, "doesnotexist");
            Assert.That(Util.DirectoryExists(missingDirectory), Is.False);
        }

        [Test]
        public void IsCaseInsensitive()
        {
            var directoryLower = Path.Combine(Util.FakePathPrefix, "code");
            var directoryUpper = Path.Combine(Util.FakePathPrefix, "CODE");

            Assert.That(Util.DirectoryExists(directoryLower), Is.True);
            Assert.That(Util.DirectoryExists(directoryUpper), Is.True);
        }

        [Test]
        public void CanListDirectories()
        {
            string[] expected =
            {
                Path.Combine(Util.FakePathPrefix, "code", "test"),
                Path.Combine(Util.FakePathPrefix, "code", "main")
            };

            var result = Util.DirectoryGetDirectories(Path.Combine(Util.FakePathPrefix, "code"));
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void CanListDirectoriesWithFilter()
        {
            string[] expected =
            {
                Path.Combine(Util.FakePathPrefix, "data"),
                Path.Combine(Util.FakePathPrefix, "code")
            };

            var result = Util.DirectoryGetDirectories(Path.Combine(Util.FakePathPrefix), "*d*");
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void CanListDirectoriesWithFilterAndSearchOption()
        {
            string[] expected =
            {
                Path.Combine(Util.FakePathPrefix, "data"),
                Path.Combine(Util.FakePathPrefix, "code"),
                Path.Combine(Util.FakePathPrefix, "code", "main"),
                Path.Combine(Util.FakePathPrefix, "code", "test")
            };

            var result = Util.DirectoryGetDirectories(Path.Combine(Util.FakePathPrefix), "????", SearchOption.AllDirectories);
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void CanListFiles()
        {
            string[] expected =
            {
                Path.Combine(Util.FakePathPrefix, "code", "test.h"),
                Path.Combine(Util.FakePathPrefix, "code", "test.cpp"),
                Path.Combine(Util.FakePathPrefix, "code", "main", "main.cpp"),
                Path.Combine(Util.FakePathPrefix, "code", "test", "stuff.cpp")
            };

            var result = Util.DirectoryGetFiles(Path.Combine(Util.FakePathPrefix, "code"));
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void CanListFilesWithFilter()
        {
            string[] expected =
            {
                Path.Combine(Util.FakePathPrefix, "code", "test.cpp"),
                Path.Combine(Util.FakePathPrefix, "code", "main", "main.cpp"),
                Path.Combine(Util.FakePathPrefix, "code", "test", "stuff.cpp")
            };

            var result = Util.DirectoryGetFiles(Path.Combine(Util.FakePathPrefix, "code"), "*.cpp");
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void CanListFilesWithFilterAndSearchOption()
        {
            string[] expected =
            {
                Path.Combine(Util.FakePathPrefix, "code", "test.cpp"),
            };

            var result = Util.DirectoryGetFiles(Path.Combine(Util.FakePathPrefix, "code"), "*.cpp", SearchOption.TopDirectoryOnly);
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void CanListFilesInSubDirectory()
        {
            string[] expected =
            {
                Path.Combine(Util.FakePathPrefix, "code", "main", "main.cpp")
            };

            var result = Util.DirectoryGetFiles(Path.Combine(Util.FakePathPrefix, "code", "main"));
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void TestPathWithWildcards()
        {
            Assert.IsTrue(Util.IsPathWithWildcards(Path.Combine("test", "*test", "test")));
            Assert.IsTrue(Util.IsPathWithWildcards(Path.Combine("test", "*test**", "test")));
            Assert.IsTrue(Util.IsPathWithWildcards(Path.Combine("test", "tes?t", "test")));
            Assert.IsTrue(Util.IsPathWithWildcards(Path.Combine("test", "tes??t", "test")));

            Assert.IsFalse(Util.IsPathWithWildcards(Path.Combine("test", "test", "test")));
        }

        [Test]
        public void ErrorListFileWithWildcards()
        {
            Assert.Catch<ArgumentException>(() => Util.DirectoryGetFilesWithWildcards(Path.Combine(Util.FakePathPrefix, "test")));
        }

        [Test]
        public void CanListFileWithWildcards_WithDotDot1()
        {
            string[] expected =
            {
                Path.Combine(Util.FakePathPrefix, "code", "test.h"),
                Path.Combine(Util.FakePathPrefix, "code", "test.cpp")
            };

            var result = Util.DirectoryGetFilesWithWildcards(Path.Combine(Util.FakePathPrefix, "code", "inexistantFolder", "..", "test*"));
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void CanListFileWithWildcards_WithDotDot2()
        {
            string[] expected =
            {
                Path.Combine(Util.FakePathPrefix, "code", "test.cpp")
            };

            var result1 = Util.DirectoryGetFilesWithWildcards(Path.Combine(Util.FakePathPrefix, "code", "????", "..", "*.cpp"));
            Assert.That(result1, Is.EquivalentTo(expected));

            var result2 = Util.DirectoryGetFilesWithWildcards(Path.Combine(Util.FakePathPrefix, "code", "????", "..", "test.cpp"));
            Assert.That(result2, Is.EquivalentTo(expected));
        }

        [Test]
        public void CanListFileWithWildcards1()
        {
            string[] expected =
            {
                Path.Combine(Util.FakePathPrefix, "code", "test.h"),
                Path.Combine(Util.FakePathPrefix, "code", "test.cpp")
            };

            var result = Util.DirectoryGetFilesWithWildcards(Path.Combine(Util.FakePathPrefix, "code", "test*"));
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void CanListFileWithWildcards2()
        {
            string[] expected =
            {
                Path.Combine(Util.FakePathPrefix, "code", "main", "main.cpp"),
                Path.Combine(Util.FakePathPrefix, "code", "test", "stuff.cpp")
            };

            var result = Util.DirectoryGetFilesWithWildcards(Path.Combine(Util.FakePathPrefix, "code", "*", "*.cpp"));
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void CanListFileWithWildcards3()
        {
            string[] expected =
            {
                Path.Combine(Util.FakePathPrefix, "code", "main", "main.cpp"),
                Path.Combine(Util.FakePathPrefix, "code", "test", "stuff.cpp")
            };

            var result = Util.DirectoryGetFilesWithWildcards(Path.Combine(Util.FakePathPrefix, "c*de", "????", "*.cpp"));
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void CanListFileWithWildcards4()
        {
            string[] expected =
            {
                Path.Combine(Util.FakePathPrefix, "code", "main", "main.cpp")
            };

            var result = Util.DirectoryGetFilesWithWildcards(Path.Combine(Util.FakePathPrefix, "code", "*", "main.cpp"));
            Assert.That(result, Is.EquivalentTo(expected));
        }

        [Test]
        public void CanListFileWithWildcards_NoMatch()
        {
            // Last file doesn't exist in test folder
            Assert.IsEmpty(Util.DirectoryGetFilesWithWildcards(Path.Combine(Util.FakePathPrefix, "c?de", "test", "main.cpp")));

            // No folder with only one character exist
            Assert.IsEmpty(Util.DirectoryGetFilesWithWildcards(Path.Combine(Util.FakePathPrefix, "code", "?", "main.cpp")));

            // No file with only one character exist
            Assert.IsEmpty(Util.DirectoryGetFilesWithWildcards(Path.Combine(Util.FakePathPrefix, "code", "*", "?")));
        }
    }

    [TestFixture]
    public class ReferencePath
    {
        [Test]
        public void CanBeComputedFromOutputPath()
        {
            const string outputFileFullPath = @"F:\OnePath\With\Output\with\a\file.cs";
            const string outputPath = @"F:\OnePath\With\Output\";
            const string referencePath = @"F:\OnePath\With\Reference\";

            var referenceFileFullPath = outputFileFullPath.ReplaceHeadPath(outputPath, referencePath);

            Assert.That(referenceFileFullPath, Is.EqualTo(
                Util.PathMakeStandard(@"F:\OnePath\With\Reference\with\a\file.cs", false)));
        }

        [Test]
        public void IsCaseInsensitiveButPreservesCase()
        {
            const string outputFileFullPath = @"F:\OnePath\With\Output\with\a\File.cs";
            const string outputPath = @"f:\OnePath\with\output\";
            const string referencePath = @"F:\OnePath\with\Reference\";

            var referenceFileFullPath = outputFileFullPath.ReplaceHeadPath(outputPath, referencePath);

            Assert.That(referenceFileFullPath, Is.EqualTo(
                Util.PathMakeStandard(@"F:\OnePath\with\Reference\with\a\File.cs", false)));
        }

        [Test]
        public void AcceptsOutputPathWithoutTrailingSlash()
        {
            const string outputFileFullPath = @"F:\OnePath\With\Output\with\a\file.cs";
            const string outputPath = @"F:\OnePath\With\Output";
            const string referencePath = @"F:\OnePath\With\Reference\";

            var referenceFileFullPath = outputFileFullPath.ReplaceHeadPath(outputPath, referencePath);

            Assert.That(referenceFileFullPath, Is.EqualTo(
                Util.PathMakeStandard(@"F:\OnePath\With\Reference\with\a\file.cs", false)));
        }
    }

    public class StringsOperations
    {
        [Test]
        public void ContainsIsCaseInsensitive()
        {
            Strings list = new Strings();
            list.Add("SOMETHING");
            Assert.That(list.Contains("SOMETHING"), Is.EqualTo(list.Contains("something")));

            list.Add("something");
            Assert.That(list.Count, Is.EqualTo(1));
        }
    }
}
