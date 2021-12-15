using System.Text;

namespace SPPC.Tools.TadbirDbConverter
{
    public static class ConverterStringExtensions
    {
        public static string FromTadbirText(this string value)
        {
            var unicodeValue = SubstituteDigits(value);
            return SubstituteArabicLetters(unicodeValue);
        }

        private static string SubstituteDigits(string value)
        {
            // NOTE: Here, we should replace character codes in SPPC proprietary font (Saman)
            // with standard latin digits
            var builder = new StringBuilder(value);
            return builder
                .Replace('\u00b9', '0')    // Digit 0 (new version)
                .Replace('\u00b1', '1')    // Digit 1 (new version)
                .Replace('\u201a', '2')    // Digit 2
                .Replace('\u0192', '3')    // Digit 3
                .Replace('\u201e', '4')    // Digit 4
                .Replace('\u2026', '5')    // Digit 5
                .Replace('\u2020', '6')    // Digit 6
                .Replace('\u2021', '7')    // Digit 7
                .Replace('\u02c6', '8')    // Digit 8
                .Replace('\u2030', '9')    // Digit 9
                .ToString();
        }

        private static string SubstituteArabicLetters(string value)
        {
            // NOTE: Here, we should replace specific Arabic character codes with equivalent
            // codes used in standard Persian keyboard
            var builder = new StringBuilder(value);
            return builder
                .Replace('\u0643', '\u06a9')    // Replace Arabic letter Kaf with Farsi Keh (Arabic letter Keheh)
                .Replace('\u064a', '\u06cc')    // Replace Arabic letter Yeh with Farsi Yeh
                .Replace('\u0649', '\u06cc')    // Replace Arabic letter Alef Maksura with Farsi Yeh
                .ToString();
        }
    }
}
