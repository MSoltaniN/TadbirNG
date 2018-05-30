using System;
using System.Linq;
using BabakSoft.Platform.Metadata;

namespace SPPC.Framework.Tools.ProjectCLI
{
    public class NamespaceUtil
    {
        public static string GetNamespace(Repository repository, string component, string area = null)
        {
            var parts = new string[4];
            if (!String.IsNullOrWhiteSpace(repository.CompanyName))
            {
                parts[0] = repository.CompanyName;
            }

            if (!String.IsNullOrWhiteSpace(repository.ProductName))
            {
                parts[1] = repository.ProductName;
            }

            parts[2] = component;
            if (!String.IsNullOrWhiteSpace(area))
            {
                parts[3] = area;
            }

            return String.Join(".", parts.Where(p => p != null).ToArray());
        }
    }
}
