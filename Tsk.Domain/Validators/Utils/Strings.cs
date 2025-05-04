namespace Tsk.Domain.Validators.Utils
{
    public static class Strings
    {
        public static void NoCommas(string input)
        {
            if (input.Contains(","))
                throw new ArgumentException("Cannot contain a comma", nameof(input));
        }

        public static void NotWhiteSpace(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException("Cannot by null or white space", nameof(input));
        }

        public static void NoLineBreaks(string input)
        {
            string[] invalid = { "\r", "\n" };
            if (invalid.Any(t => input.Contains(t)))
                throw new ArgumentException("Cannot contain line breaks", nameof(input));
        }

        public static void MaxLength(string input, int maxLength = 50)
        {
            if (input.Length > maxLength)
                throw new ArgumentException($"Cannot be longer than {maxLength}", nameof(input));
        }

        public static void IsInteger(string input)
        {
            try
            {
                int.Parse(input);
            }
            catch (Exception)
            {
                throw new ArgumentException("Input is not a valid integer");
            }
        }
    }
}