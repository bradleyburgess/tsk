using Tsk.CLI.Utils;
using Tsk.Tests.TestSupport;

namespace Tsk.Tests.CliUtils
{
    public class HelpersTests
    {
        [Fact]
        public void ShouldReturnQualifiedRelativeUserHomePath()
        {
            var result = Helpers.ExpandUserPath(
                OperatingSystem.IsWindows()
                ? "~\\tsk.txt"
                : "~/tsk.txt"
            );
            var expected = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "tsk.txt")
            ;
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldReturnQualifiedRelativePath_ParentDirectory()
        {
            Directory.SetCurrentDirectory(TestHelpers.ResolveFromSlnRoot());
            var result = Helpers.ExpandUserPath(
                OperatingSystem.IsWindows()
                ? "..\\tsk.txt"
                : "../tsk.txt"
            );
            var expected = Path.Combine(
                Directory.GetParent(TestHelpers.ResolveFromSlnRoot())!.FullName,
                "tsk.txt"
            );
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldReturnQualifiedRelativePath_CurrentDirectory()
        {
            Directory.SetCurrentDirectory(TestHelpers.ResolveFromSlnRoot());
            var result = Helpers.ExpandUserPath(
                OperatingSystem.IsWindows()
                ? ".\\tsk.txt"
                : "./tsk.txt"
            );
            var expected = Path.Combine(
                TestHelpers.ResolveFromSlnRoot(),
                "tsk.txt"
            );
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ShouldCreateTskFile()
        {
            var tmpDir = Path.GetTempPath();
            var tmpFile = Path.GetRandomFileName();
            var tmpPath = Path.Combine(tmpDir, tmpFile);
            Assert.False(Path.Exists(tmpPath));

            Helpers.EnsureTskFile(tmpPath);
            Thread.Sleep(1000);
            Assert.True(Path.Exists(tmpPath));
        }

        [Fact]
        public void ShouldErrorWithBadPath()
        {
            Assert.Throws<DirectoryNotFoundException>(
                () => Helpers.EnsureTskFile(
                    Path.Combine(Path.GetTempPath(), "asdfasdf", "asdfasdf.txt")
                )
            );
        }
    }
}