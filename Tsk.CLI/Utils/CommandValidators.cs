using Tsk.CLI.Application.Commands;

namespace Tsk.CLI.Utils
{
    public class CommandValidators
    {
        public static void ValidateTagOptions(UpdateCommand.Settings settings)
        {
            string?[] tagOptions = {
                settings.UpdateTags,
                settings.AddTags,
                settings.RemoveTags
            };
            int tagOptionsSet = 0;
            foreach (var t in tagOptions)
                if (!string.IsNullOrEmpty(t)) tagOptionsSet++;

            if (tagOptionsSet > 1)
                throw new ArgumentException("Please choose one of `--tags`, `--add-tags` or `--remove-tags`.");
        }
    }
}