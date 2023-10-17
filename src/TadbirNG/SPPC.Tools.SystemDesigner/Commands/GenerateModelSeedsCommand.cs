using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tools.Transforms.Templates;

namespace SPPC.Tools.SystemDesigner.Commands
{
    public class GenerateModelSeedsCommand<TModel> : ICommand
         where TModel : class, new()
    {
        public GenerateModelSeedsCommand(IEnumerable<TModel> seeds , bool modelIsDuplicateInSysDB = false)
        {
            _modelType = typeof(TModel);
            _seeds = seeds;
            _modelIsDuplicateInSysDB = modelIsDuplicateInSysDB;
        }

        public void Execute()
        {
            GenerateSeed();
        }

        private void GenerateSeed()
        {
            string csModelPath = ConfigurationManager.AppSettings["CsPersistSeedingPath"];
            string fileName = string.Format("{0}.config.Generated.cs", _modelType.Name);
            string path = GetInitializedFilePath(csModelPath, _modelType.Namespace.Split('.').Last(), fileName);

            var template = new CsModelSeeder<TModel>(_seeds , _modelIsDuplicateInSysDB);
            string transformed = template.TransformText();
            File.WriteAllText(path, transformed);
        }

        private  string GetInitializedFilePath(string root, string area, string fileName)
        {
            if (String.IsNullOrEmpty(area))
            {
                return Path.Combine(root, fileName);
            }
            else
            {
                string directory = Path.Combine(root, area);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                return Path.Combine(directory, fileName);
            }
        }

        private readonly IEnumerable<TModel> _seeds;
        private readonly PropertyInfo[] _properties;
        private readonly Type _modelType;
        private readonly bool _modelIsDuplicateInSysDB;
    }
}
