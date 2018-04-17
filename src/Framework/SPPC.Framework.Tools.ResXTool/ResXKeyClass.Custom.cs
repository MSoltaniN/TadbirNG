using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace SPPC.Framework.Tools.ResXTool
{
    public partial class ResXKeyClass
    {
        public ResXKeyClass(string resPath, string codeNamespace)
        {
            _class = Path.GetFileNameWithoutExtension(resPath);
            _namespace = codeNamespace;
            _keys = ExtractResourceKeys(resPath).ToArray();
            _version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        }

        private static IEnumerable<string> ExtractResourceKeys(string path)
        {
            var keys = new List<string>();
            using (var resReader = new ResXResourceReader(path))
            {
                foreach (DictionaryEntry entry in resReader)
                {
                    keys.Add(entry.Key.ToString());
                }
            }

            return keys;
        }

        private readonly string _namespace;
        private readonly string _class;
        private string[] _keys;
        private string _version;
    }
}
