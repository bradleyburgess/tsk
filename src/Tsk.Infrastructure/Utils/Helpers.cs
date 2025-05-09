

namespace Tsk.Infrastructure.Utils;

public class Helpers
{
    /// <summary>
    /// Returns the user's home directory. Compatible with Windows, MacOS, and Linux.
    /// </summary>
    /// <returns>The user's home directory</returns>
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

    /// <summary>
    /// Ensures that the tsk file exists
    /// </summary>
    /// <param name="path"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="DirectoryNotFoundException"></exception>
    /// <exception cref="UnauthorizedAccessException"></exception>
    public static void EnsureTskFile(string path)
    {
        string? _path = path;
        if (_path == GetDefaultTskPath())
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_path)!);
        }
        string? parentDir = Path.GetDirectoryName(_path);
        if (string.IsNullOrEmpty(parentDir))
            throw new ArgumentNullException(parentDir);
        if (!File.Exists(_path))
        {
            File.Create(_path!).Close();
            Console.WriteLine($"Creating {path}.");

        }
    }

    public static string GetDefaultTskPath(string tskFileName = "tsk.txt") =>
            Path.Combine(
                Helpers.GetUserHomeDirectory(),
                "tsk",
                tskFileName
            );

    public static string GetTskPath(string? userPath) =>
        string.IsNullOrEmpty(userPath)
            ? GetDefaultTskPath()
            : Helpers.ExpandUserPath(userPath);

    /// <summary>
    /// Expands a user-specified path, resolving to absolute path.
    /// </summary>
    /// <param name="path"></param>
    /// <returns>The fully-qualified absolute path</returns>
    /// <exception cref="ArgumentException"></exception>
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
}
