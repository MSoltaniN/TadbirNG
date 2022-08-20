using System;
using System.Collections.Generic;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;

namespace SPPC.Tools.Utility
{
    public class PasswordGenerator
    {
        public static string Generate(int length = 15)
        {
            Verify.ArgumentNotOutOfRange(length, MinLength, MaxLength, nameof(length));
            var chars = new List<char>(GetStarterValue());
            for (int count = MinLength; count <= length; count++)
            {
                if (count % 5 == 0)
                {
                    chars.Add(GetRandomChar(Numbers));
                }
                else if (count % 2 == 0)
                {
                    chars.Add(GetRandomChar(SmallLetters));
                }
                else
                {
                    chars.Add(GetRandomChar(CapitalLetters));
                }
            }

            var passwordChars = chars.ToArray();
            passwordChars.Shuffle();
            return new string(passwordChars);
        }

        private static IEnumerable<char> GetStarterValue()
        {
            return new char[]
            {
                GetRandomChar(CapitalLetters),
                GetRandomChar(SmallLetters),
                GetRandomChar(Numbers)
            };
        }

        private static char GetRandomChar(string word)
        {
            var random = new Random((int)DateTime.Now.Ticks);
            return word[random.Next() % word.Length];
        }

        private const int MinLength = 4;
        private const int MaxLength = 200;
        private const string CapitalLetters = "QWERTYUIOPASDFGHJKLZXCVBNM";
        private const string SmallLetters = "mnbvcxzlkjhgfdsapoiuytrewq";
        private const string Numbers = "1234567890";
    }
}
