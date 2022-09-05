using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Tools.Transforms.Templates;

namespace SPPC.Tools.SystemDesignerCli
{
    public class GenerateTsModelCommand : ICliCommand
    {
        public GenerateTsModelCommand(params string[] args)
        {
            if (args.Length < 1)
            {
                throw ExceptionBuilder.NewArgumentException(
                    "Insufficient arguments were provided (needed 1).", "paramValues");
            }

            _typeNames = args[0].Split(',');
            _area = args.Length > 1 ? args[1] : null;
        }

        public void Execute()
        {
            string tsAppPath = ConfigurationManager.AppSettings["TsAppPath"];
            string csAssembly = ConfigurationManager.AppSettings["CsViewModelAssemblies"];
            var assembly = Assembly.Load(csAssembly);
            if (assembly == null)
            {
                Console.WriteLine(
                    "ERROR: Could not load view model assembly (Error code : {0}).", CliResult.TypeLoadError);
                return;
            }

            foreach (string typeName in _typeNames)
            {
                string tsTypeName = typeName
                    .Split('.')
                    .Last()
                    .Replace("ViewModel", String.Empty)
                    .CamelCase();
                string fullName = String.Format("{0}{2}.{1}", csAssembly, typeName,
                    _area != null ? $".{_area}" : String.Empty);
                string generatedPath = String.Format(@"{0}\{1}.ts", tsAppPath, tsTypeName);
                var csType = assembly.GetType(fullName);

                Console.WriteLine("Generating TypeScript model '{0}' for '{1}'...", tsTypeName, typeName);
                var template = new TsModelFromCsViewModel(csType);
                File.WriteAllText(generatedPath, template.TransformText());
            }
        }

        private readonly string[] _typeNames;
        private readonly string _area;
    }
}
