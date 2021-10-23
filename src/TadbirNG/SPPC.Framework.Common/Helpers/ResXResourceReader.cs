using System;
using System.Collections.Generic;
using System.Xml;

namespace SPPC.Framework.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class ResXResourceReader
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="path"></param>
        public ResXResourceReader(string path)
        {
            StringResources = ReadStringResources(path);
        }

        /// <summary>
        ///
        /// </summary>
        public IDictionary<string, string> StringResources { get; }

        private static IDictionary<string, string> ReadStringResources(string path)
        {
            var resources = new Dictionary<string, string>();
            using (var reader = new XmlTextReader(path))
            {
                SeekFirstResource(reader);
                var entry = GetNextEntry(reader);
                while (entry.Key != null)
                {
                    resources.Add(entry.Key, entry.Value);
                    entry = GetNextEntry(reader);
                }
            }

            return resources;
        }

        private static void SeekFirstResource(XmlReader reader)
        {
            while (reader.Read())
            {
                if (reader.Name == "data")
                {
                    break;
                }
            }
        }

        private static KeyValuePair<string, string> GetNextEntry(XmlReader reader)
        {
            var entry = new KeyValuePair<string, string>();
            string value;
            var key = reader.GetAttribute("name");
            SeekNextElement(reader);
            if (reader.ReadState != ReadState.EndOfFile && reader.Name == "value")
            {
                value = reader.ReadString();
                if (!String.IsNullOrEmpty(key))
                {
                    entry = new KeyValuePair<string, string>(key, value);
                }
            }

            SeekNextElement(reader);
            return entry;
        }

        private static void SeekNextElement(XmlReader reader)
        {
            while (reader.Read() && reader.NodeType != XmlNodeType.Element)
            {
            }
        }
    }
}
