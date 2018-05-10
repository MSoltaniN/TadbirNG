using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;

namespace SPPC.Framework.Tools.ProjectCLI
{
    public partial class ResXKeyClass
    {
        public ResXKeyClass(string resPath, string codeNamespace)
        {
            _class = Path.GetFileNameWithoutExtension(resPath);
            _namespace = codeNamespace;
            _resources = ExtractResourceKeys(resPath);
            _version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        }

        private static IDictionary<string, string> ExtractResourceKeys(string path)
        {
            var resources = new Dictionary<string, string>();
            using (var resReader = new ResXResourceReader(path))
            {
                foreach (DictionaryEntry entry in resReader)
                {
                    resources.Add(entry.Key.ToString(), entry.Value.ToString());
                }
            }

            return resources;
        }

        private readonly string _namespace;
        private readonly string _class;
        private IDictionary<string, string> _resources;
        private string _version;
    }
}
