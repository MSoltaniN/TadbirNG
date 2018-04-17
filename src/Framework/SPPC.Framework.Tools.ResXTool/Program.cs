using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;

namespace SPPC.Framework.Tools.ResXTool
{
    class Program
    {
        static int Main(string[] args)
        {
            DisplayBanner();

            if (args.Length == 0)
            {
                Console.WriteLine("No parameter specified.");
            }

            string param = args
                .Where(arg => arg.StartsWith(_resFileParam))
                .FirstOrDefault();
            string resFile = param?.Replace(_resFileParam, String.Empty).Trim('"', '\'');
            if (String.IsNullOrEmpty(resFile))
            {
                Console.WriteLine("ERROR: Resource File (-res:) parameter is missing.");
                return -1;
            }

            string dirName = Path.GetDirectoryName(resFile);
            string className = Path.GetFileNameWithoutExtension(resFile);
            Console.WriteLine("Generating ResX key class '{0}' in '{1}' ...", className, dirName);

            param = args
                .Where(arg => arg.StartsWith(_namespaceParam))
                .FirstOrDefault();
            string codeNamespace = param.Replace(_namespaceParam, String.Empty) ?? "Resources";

            string path = String.Format(@"{0}\{1}.cs", dirName, className);
            var template = new ResXKeyClass(resFile, codeNamespace);
            string transformed = template.TransformText();
            File.WriteAllText(path, transformed);
            Console.WriteLine("Done.");
            Console.WriteLine();
            return 0;
        }

        private static void DisplayBanner()
        {
            Console.WriteLine();
            Console.WriteLine("============================================================");
            Console.WriteLine("SPPC Framework : ResX Key Class Generator tool (v1.1)");
            Console.WriteLine("(c) Copyright 2018, SPPC, All Rights Reserved");
            Console.WriteLine("============================================================");
            Console.WriteLine();
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

        private static readonly string _resFileParam = "-res:";
        private static readonly string _namespaceParam = "-ns:";
    }
}
