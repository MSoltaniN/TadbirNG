using System.Collections.Generic;
using System.IO;
using System.Reflection;
using SPPC.Framework.Helpers;

namespace SPPC.Tools.Transforms.Templates
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

        private static SortedDictionary<string, string> ExtractResourceKeys(string path)
        {
            var resources = new SortedDictionary<string, string>();
            var resReader = new ResXResourceReader(path);
            foreach (var entry in resReader.StringResources)
            {
                resources.Add(entry.Key, entry.Value);
            }

            return resources;
        }

        private readonly string _namespace;
        private readonly string _class;
        private readonly SortedDictionary<string, string> _resources;
        private readonly string _version;
    }
}
