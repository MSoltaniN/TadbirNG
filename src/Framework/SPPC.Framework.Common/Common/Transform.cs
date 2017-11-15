using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SPPC.Framework.Common
{
    /// <summary>
    /// Converts raw data between different representations.
    /// </summary>
    public static class Transform
    {
        /// <summary>
        /// Converts a byte array to an equivalent sequence of hexadecimal strings. In this
        /// transformation, each byte of data is mapped to two hex characters (0-9, A-F).
        /// </summary>
        /// <param name="input">An array of 8-bit unsigned integers which should be converted
        /// to hexadecimal form.</param>
        /// <returns>A string containing hexadecimal equivalent of input byte array.</returns>
        /// <exception cref="System.ArgumentNullException">input is null reference.</exception>
        /// <remarks>Hexadecimal strings are formed in uppercase letters.</remarks>
        public static string ToHexString(byte[] input)
        {
            Verify.ArgumentNotNull(input, "input");

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
                builder.Append(input[i].ToString("X2"));

            return (builder.ToString());
        }

        /// <summary>
        /// Converts a byte array to the equivalent base64 string.
        /// </summary>
        /// <param name="input">An array of 8-bit unsigned integers which should be converted
        /// to base64 form.</param>
        /// <returns>A string containing base64 equivalent of input byte array.</returns>
        /// <exception cref="System.ArgumentNullException">input is null reference.</exception>
        /// <remarks>Base64 strings are formatted without line breaks.</remarks>
        public static string ToBase64String(byte[] input)
        {
            Verify.ArgumentNotNull(input, "input");

            return (Convert.ToBase64String(input));
        }

        /// <summary>
        /// Converts a sequence of hexadecimal characters (0-9, A-F) to an equivalent
        /// array of bytes.
        /// </summary>
        /// <param name="input">A string containing a sequence of valid hexadecimal
        /// characters (0-9, A-F).</param>
        /// <returns>An array of 8-bit unsigned integers which is equivalent to the
        /// input sequence of hexadecimal characters.</returns>
        /// <exception cref="System.ArgumentNullException">input is null reference.</exception>
        /// <exception cref="System.ArgumentException">input string contains invalid
        /// hex characters, or the count of characters in the given string is not even.
        /// </exception>
        /// <remarks>Input string is read from left to right.</remarks>
        public static byte[] FromHexString(string input)
        {
            Verify.ArgumentNotNull(input, "input");

            if ((input.Length % 2) != 0)
            {
                throw (ExceptionBuilder.NewArgumentException("Input string cannot be " +
                    "converted to byte array. Character count should be an even number."));
            }

            List<byte> bytes = new List<byte>();
            for (int i = 0; i < input.Length; i += 2)
            {
                string part = input.Substring(i, 2);
                byte result;
                if (Byte.TryParse(part, NumberStyles.AllowHexSpecifier, null, out result))
                    bytes.Add(result);
                else
                {
                    string message = String.Format("string \"{0}\" does not represent " +
                        "a valid hex number.", part);
                    throw (ExceptionBuilder.NewArgumentException(message));
                }
            }

            return (bytes.ToArray());
        }

        /// <summary>
        /// Converts a sequence of base64 characters to an equivalent array of bytes.
        /// </summary>
        /// <param name="input">A base64 string that should be converted.</param>
        /// <returns>An array of 8-bit unsigned integers which is equivalent to the
        /// base64 string.</returns>
        /// <exception cref="System.ArgumentNullException">input is null reference.</exception>
        public static byte[] FromBase64String(string input)
        {
            return (Convert.FromBase64String(input));
        }

        /// <summary>
        /// Converts a sequence of hexadecimal characters (0-9, A-F) to an equivalent UTF8 string.
        /// </summary>
        /// <param name="input">A string containing a sequence of valid hexadecimal characters (0-9, A-F)</param>
        /// <returns>A unicode (UTF8) string whose byte sequence is equivalent to the hexadecimal input</returns>
        public static string StringFromHexString(string input)
        {
            Verify.ArgumentNotNull(input, "input");

            var hexBytes = FromHexString(input);
            return Encoding.UTF8.GetString(hexBytes);
        }
    }
}
