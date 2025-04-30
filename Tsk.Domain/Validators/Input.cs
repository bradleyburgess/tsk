using Tsk.Domain.Validators.Utils;

namespace Tsk.Domain.Validators
{
    public static class Input
    {
        public static void ValidateTag(string input)
        {
            Strings.NoCommas(input);
            Strings.NoLineBreaks(input);
            Strings.MaxLength(input, 30);
            Strings.NotWhiteSpace(input);
        }

        public static void ValidateLocation(string input)
        {
            Strings.NoCommas(input);
            Strings.NoLineBreaks(input);
            Strings.MaxLength(input, 30);
            Strings.NotWhiteSpace(input);
        }

        public static void ValidateDescription(string input)
        {
            // Strings.NoCommas(input);
            Strings.NoLineBreaks(input);
            Strings.MaxLength(input, 50);
            Strings.NotWhiteSpace(input);
        }
    }
}