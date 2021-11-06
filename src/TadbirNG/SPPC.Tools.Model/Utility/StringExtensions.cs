using System;
using SPPC.Framework.Common;

namespace SPPC.Tools.Utility
{
    public static class StringExtensions
    {
        public static string ToPlural(this string name)
        {
            Verify.ArgumentNotNullOrEmptyString(name, nameof(name));
            char lastChar = name[name.Length - 1];
            string plural;
            switch (lastChar)
            {
                case 'h':
                case 's':
                case 'x':
                case 'z':
                    plural = String.Format("{0}es", name);
                    break;
                case 'y':
                    plural = String.Format("{0}ies", name.Substring(0, name.Length - 1));
                    break;
                default:
                    plural = String.Format("{0}s", name);
                    break;
            }

            return plural;
        }
    }
}
