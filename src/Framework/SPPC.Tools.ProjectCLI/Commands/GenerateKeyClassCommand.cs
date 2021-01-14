using System;
using System.IO;
using SPPC.Framework.Common;
using SPPC.Tools.Transforms.Templates;

namespace SPPC.Tools.ProjectCLI
{
    public class GenerateKeyClassCommand : ICliCommand
    {
        public GenerateKeyClassCommand(params string[] args)
        {
            if (args.Length < 1)
            {
                throw ExceptionBuilder.NewArgumentException(
                    "Insufficient arguments were provided (needed 1).", "paramValues");
            }

            _resFilePath = args[0];
            _csNamespace = (args.Length > 1) ? args[1] : "Resources";
        }

        public void Execute()
        {
            string dirName = Path.GetDirectoryName(_resFilePath);
            string className = Path.GetFileNameWithoutExtension(_resFilePath);
            string path = String.Format(@"{0}\{1}.cs", dirName, className);
            if (!NotExistsOrOutOfDate(path))
            {
                Console.WriteLine("ResX key class '{0}' is up-to-date. Nothing was generated.", className);
                return;
            }

            Console.WriteLine("Generating ResX key class '{0}' in '{1}' ...", className, dirName);
            var template = new ResXKeyClass(_resFilePath, _csNamespace);
            string transformed = template.TransformText();
            File.WriteAllText(path, transformed);
        }

        private bool NotExistsOrOutOfDate(string path)
        {
            if (!File.Exists(path))
            {
                return true;
            }

            DateTime resxLastModified = File.GetLastWriteTime(_resFilePath);
            DateTime csLastModified = File.GetLastWriteTime(path);
            return (resxLastModified > csLastModified);
        }

        private string _resFilePath;
        private string _csNamespace;
    }
}
