using System;
using System.Collections.Generic;
using SPPC.Tools.MetaDesigner.Persistence;
using SPPC.Tools.MetaDesigner.Transforms;
using SPPC.Tools.Model;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class FileRepositoryCommand : RepositoryCommand, IRepositoryCommand
    {
        public FileRepositoryCommand()
            : base()
        {
        }

        public FileRepositoryCommand(IUserInputCollector inputCollector)
            : base()
        {
            this.InputCollector = inputCollector;
        }

        public RepositoryCommandType Action
        {
            get { return (RepositoryCommandType)Parameters["action"]; }
        }

        public override void Execute()
        {
            base.Execute();
            InputCollector = GetInputCollector();
            if (InputCollector != null)
            {
                InputCollector.GetInput();
                if (InputCollector.Output != null)
                {
                    if (Action == RepositoryCommandType.New || Action == RepositoryCommandType.Open)
                    {
                        Load((string)InputCollector.Output);
                    }
                    else
                    {
                        Save((string)InputCollector.Output);
                    }

                    IsComplete = true;
                }
            }
            else
            {
                Save();
            }
        }

        protected override IDictionary<string, string> GetRequiredParameters()
        {
            var requiredParams = new Dictionary<string, string>();
            requiredParams.Add("action", typeof(RepositoryCommandType).AssemblyQualifiedName);
            return requiredParams;
        }

        private void Load(string path)
        {
            if (Action == RepositoryCommandType.New)
            {
                var metaGenerator = new BasicMetaGenerator();
                ContextManager.Model = new RepositoryModel(
                    metaGenerator.GenerateFileRepository("Repository", path));
            }
            else
            {
                var repositoryStorage = RepositoryStorageFactory.GetStorage(StorageFactory.CreateFile(path));
                ContextManager.Model = new RepositoryModel(repositoryStorage.Load());
            }
        }

        private void Save(string path = null)
        {
            var repository = ContextManager.Model.Repository;
            if (!String.IsNullOrEmpty(path))
            {
                repository.Store = StorageFactory.CreateFile(path);
            }

            var storage = RepositoryStorageFactory.GetStorage(repository.Store);
            storage.Save(repository);
        }

        private IUserInputCollector GetInputCollector()
        {
            var inputCollector = InputCollector;
            if (inputCollector == null)
            {
                if (Action == RepositoryCommandType.New || Action == RepositoryCommandType.SaveAs)
                {
                    inputCollector = new FileNameCollector(false, FileFilters.XmlRepository);
                }
                else if (Action == RepositoryCommandType.Open)
                {
                    inputCollector = new FileNameCollector(true, FileFilters.XmlRepository);
                }
            }

            return inputCollector;
        }
    }
}
