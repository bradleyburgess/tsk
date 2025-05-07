using Tsk.CLI.Utils;

namespace Tsk.CLI.Tests.Utils;

public class HelpersTests_EnsureTskFile
{
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
