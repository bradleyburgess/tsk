using System.Text.RegularExpressions;
using Tsk.CLI.Utils;

namespace Tsk.Tests.CliUtils
{
    public class AppDataTests_GetDefaultTskPath
    {
        [Fact]
        public void ShouldReturnCorrectDefaultTskPath()
        {
            var result = AppData.GetDefaultTskPath();
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
            var result = AppData.GetDefaultTskPath(filename);
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
}