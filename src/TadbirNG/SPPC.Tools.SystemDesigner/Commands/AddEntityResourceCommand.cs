using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            var resxRoot = Path.Combine(PathConfig.SolutionRoot, resNamespace);
            var resxFile = Path.Combine(resxRoot, $"{TypeName}.en.resx");
            AddLocalResource(resxFile, _model.Entity.Name);
            resxFile = Path.Combine(resxRoot, $"{TypeName}.resx");
            AddLocalResource(resxFile, _model.SingularName);
            var template = new ResXKeyClass(resxFile, resNamespace);
            var transformed = template.TransformText();
            File.WriteAllText(Path.Combine(resxRoot, $"{TypeName}.cs"), transformed);
        }

        private void AddLocalResource(string resxPath, string localized)
        {
            var reader = new ResXResourceReader(resxPath);
            var entries = reader.StringResources;
            var existing = entries
                .FirstOrDefault(entry => entry.Key == _model.Entity.Name);
            if (existing.Equals(default(KeyValuePair<string, string>)))
            {
                entries.Add(_model.Entity.Name, localized);
                var writer = new ResXResourceWriter(entries);
                writer.Save(resxPath);
            }
        }

        private const string TypeName = "AppStrings";
        private readonly EntityInfoModel _model;
    }
}
