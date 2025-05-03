namespace Tsk.CLI.Utils;

public class Helpers
{
    public static string ExpandUserPath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be null or empty.", nameof(path));

        if (path.StartsWith("~/") || path.StartsWith("~\\"))
        {
            var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            return Path.Combine(home, path[2..]); // strips "~/"
        }

        return Path.GetFullPath(path); // normalize relative paths
    }

    public static string GetUserHomeDirectory() =>
        OperatingSystem.IsWindows()
        ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        : OperatingSystem.IsMacOS()
        ? Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "Library",
            "Application Support"
        )
        : Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
}
