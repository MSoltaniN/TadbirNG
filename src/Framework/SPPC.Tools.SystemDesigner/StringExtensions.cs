using System;
using SPPC.Framework.Common;

namespace SPPC.Tools.SystemDesigner
{
    public static class StringExtensions
    {
        public static string CamelCase(this string name)
        {
            Verify.ArgumentNotNullOrEmptyString(name, "name");
            string camelCase = name;
            if (name.Length > 1)
            {
                camelCase = String.Format("{0}{1}", Char.ToLower(name[0]), name.Substring(1));
            }

            return camelCase;
        }
    }
}
