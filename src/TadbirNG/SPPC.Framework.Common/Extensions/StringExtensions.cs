using System;
using SPPC.Framework.Common;

namespace SPPC.Framework.Extensions
{
    /// <summary>
    /// Provides additional operations for String class using standard .NET extension method syntax.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Truncates a long text value to a maximum character length and appends ellipsis (...) to the end of
        /// truncated text, indicating that the original text contains more characters. If the text is shorter than
        /// the specified maximum length, original text is returned without any modification.
        /// </summary>
        /// <param name="longText">Text that should be converted to auto-ellipsis form</param>
        /// <param name="maxChars">Maximum character length for truncated text (excluding ellipsis characters)</param>
        /// <returns>Auto-ellipsis form of the original text if it is longer than the specified maximum;
        /// otherwise the original text.</returns>
        /// <remarks>Truncation is done to the word boundaries (i.e. truncated text will not end with a broken word).
        /// </remarks>
        public static string AutoEllipsis(this string longText, int maxChars = 100)
        {
            var autoEllipsis = longText;
            if (longText != null && longText.Length > maxChars)
            {
                var whitespace = new char[] { ' ', '\t', '\r', '\n' };
                if (!Char.IsWhiteSpace(longText[maxChars]))
                {
                    int length = maxChars;
                    int previous = longText.Substring(0, maxChars).LastIndexOfAny(whitespace);
                    if (previous != -1)
                    {
                        length = previous;
                    }

                    autoEllipsis = String.Format("{0}...", longText.Substring(0, length));
                }
                else
                {
                    autoEllipsis = String.Format(
                        "{0}...", longText.Substring(0, maxChars));
                }
            }

            return autoEllipsis;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CamelCase(this string name)
        {
            Verify.ArgumentNotNullOrEmptyString(name, nameof(name));
            string camelCase = name;
            if (name.Length > 1)
            {
                camelCase = String.Format("{0}{1}", Char.ToLower(name[0]), name.Substring(1));
            }

            return camelCase;
        }
    }
}
