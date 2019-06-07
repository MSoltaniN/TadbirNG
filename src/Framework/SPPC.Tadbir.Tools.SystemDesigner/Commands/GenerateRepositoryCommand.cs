using System;
using System.Configuration;
using System.IO;
using BabakSoft.Platform.Metadata;
using SPPC.Tadbir.Tools.SystemDesigner.Models;
using SPPC.Tadbir.Tools.SystemDesigner.Templates;

namespace SPPC.Tadbir.Tools.SystemDesigner.Commands
{
    public class GenerateRepositoryCommand : ICommand
    {
        public GenerateRepositoryCommand(CrudWizardModel model)
        {
            _model = model;
        }

        public void Execute()
        {
            var entity = _model.EntityInfo.Entity;
            if (_model.Options.HasRepoInterface)
            {
                GenerateRepositoryInterface(entity);
            }
            if (_model.Options.HasRepoImplementation)
            {
                GenerateRepositoryImplementation(entity);
            }
        }

        private void GenerateRepositoryInterface(Entity entity)
        {
            string repoInterfacePath = ConfigurationManager.AppSettings["RepoInterfacePath"];
            string fileName = String.Format("I{0}Repository.Generated.cs", entity.Name);
            string path = Path.Combine(repoInterfacePath, fileName);

            var template = new RepoInterfaceFromMetadata(_model.EntityInfo);
            string transformed = template.TransformText();
            File.WriteAllText(path, transformed);
        }

        private void GenerateRepositoryImplementation(Entity entity)
        {
            string persistPath = ConfigurationManager.AppSettings["CsPersistPath"];
            string fileName = String.Format("{0}Repository.Generated.cs", entity.Name);
            string path = Path.Combine(persistPath, "Repository", fileName);

            var template = new RepoImplementationFromMetadata(_model.EntityInfo);
            string transformed = template.TransformText();
            File.WriteAllText(path, transformed);
        }

        private readonly CrudWizardModel _model;
    }
}
