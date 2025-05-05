namespace Tsk.CLI.Utils
{
    public class AppData
    {
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

        public const string TskVersion = "0.1.0";
    }
}