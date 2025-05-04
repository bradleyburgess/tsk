using Tsk.Domain.Validators.Utils;

namespace Tsk.Domain.Validators
{
    public static class InputValidators
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
            Strings.NoLineBreaks(input);
            Strings.MaxLength(input, 50);
            Strings.NotWhiteSpace(input);
        }

        public static void ValidateDateString(string input)
        {
            try
            {
                new DateOnly(
                    int.Parse(input.Substring(0, 4)),
                    int.Parse(input.Substring(4, 2)),
                    int.Parse(input.Substring(6, 2))
                );
            }
            catch (Exception)
            {
                throw new ArgumentException($"Date `{input}` is not in the correct format (yyyyMMdd)");
            }
        }
    }
}