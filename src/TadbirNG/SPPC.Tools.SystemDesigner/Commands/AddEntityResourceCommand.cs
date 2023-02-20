using System.Collections.Generic;
using System.IO;
using System.Linq;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms.Templates;

namespace SPPC.Tools.SystemDesigner.Commands
{
    public class AddEntityResourceCommand : ICommand
    {
        public AddEntityResourceCommand(EntityInfoModel model)
        {
            _model = model;
        }

        public void Execute()
        {
            string resNamespace = "SPPC.Tadbir.Resources";
            string entityName = _model.Entity.Name;
            var resxRoot = Path.Combine(PathConfig.SolutionRoot, resNamespace);
            var resxFile = Path.Combine(resxRoot, $"{TypeName}.en.resx");
            AddLocalResource(resxFile, entityName, entityName);
            AddLocalResource(resxFile, entityName.ToPlural(), entityName.ToPlural());
            resxFile = Path.Combine(resxRoot, $"{TypeName}.resx");
            AddLocalResource(resxFile, entityName, _model.SingularName);
            AddLocalResource(resxFile, entityName.ToPlural(), _model.PluralName);
            var template = new ResXKeyClass(resxFile, resNamespace);
            var transformed = template.TransformText();
            File.WriteAllText(Path.Combine(resxRoot, $"{TypeName}.cs"), transformed);
        }

        private static void AddLocalResource(string resxPath, string key, string localized)
        {
            var reader = new ResXResourceReader(resxPath);
            var entries = reader.StringResources;
            var existing = entries
                .FirstOrDefault(entry => entry.Key == key);
            if (existing.Equals(default(KeyValuePair<string, string>)))
            {
                entries.Add(key, localized);
                var writer = new ResXResourceWriter(entries);
                writer.Save(resxPath);
            }
        }

        private const string TypeName = "AppStrings";
        private readonly EntityInfoModel _model;
    }
}
