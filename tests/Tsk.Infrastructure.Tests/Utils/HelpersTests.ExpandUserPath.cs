using Tsk.Infrastructure.Utils;
using Tsk.TestSupport;

namespace Tsk.CLI.Tests.Utils;

public class HelpersTests_ExpandUserPath
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
}
