namespace Tsk.CLI.Utils
{
    public class AppData
    {
        public static string GetDefaultTskPath(string tskFileName = "tsk.txt") =>
            Path.Combine(
                OperatingSystem.IsWindows()
                ? Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
                : OperatingSystem.IsMacOS()
                ? Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    "Library",
                    "Application Support"
                )
                : Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "tsk",
                tskFileName
            );
    }
}