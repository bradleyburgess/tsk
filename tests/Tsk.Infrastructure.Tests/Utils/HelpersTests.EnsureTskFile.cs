using Tsk.Infrastructure.Utils;

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

    [Fact]
    public void ShouldReturnCorrectDefaultTskPath()
    {
        var result = Helpers.GetDefaultTskPath();
        if (OperatingSystem.IsWindows())
        {
            Assert.EndsWith(@"\AppData\Roaming\tsk\tsk.txt", result);
        }
        else if (OperatingSystem.IsMacOS())
        {
            Assert.EndsWith(@"Library/Application Support/tsk/tsk.txt", result);
        }
        else
        {
            Assert.EndsWith(@".local/share/tsk/tsk.txt", result);
        }
    }

    [Fact]
    public void ShouldReturnCustomTskFilename()
    {
        var filename = "custom-filename.txt";
        var result = Helpers.GetDefaultTskPath(filename);
        if (OperatingSystem.IsWindows())
        {
            Assert.EndsWith($"\\AppData\\Roaming\\tsk\\{filename}", result);
        }
        else if (OperatingSystem.IsMacOS())
        {
            Assert.EndsWith($"Library/Application Support/tsk/{filename}", result);
        }
        else
        {
            Assert.EndsWith($".local/share/tsk/{filename}", result);
        }
    }
}
