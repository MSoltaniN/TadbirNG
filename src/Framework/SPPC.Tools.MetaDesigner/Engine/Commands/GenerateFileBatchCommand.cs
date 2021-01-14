using System;
using System.Collections.Generic;
using SPPC.Tools.MetaDesigner.Common;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class GenerateFileBatchCommand : RepositoryCommand, IRepositoryCommand
    {
        public GenerateFileBatchCommand()
            : base()
        {
        }

        public override void Execute()
        {
            base.Execute();
            var repository = MetaDesignerContext.Current.Model.Repository;
            var command = new GenerateFileCommand();
            command.Parameters.Add("item", null);
            command.Parameters.Add("fileName", Parameters["fileName"]);
            command.Parameters.Add("template", Parameters["template"]);
            foreach (var entity in repository.Entities)
            {
                command.Parameters["item"] = entity;
                command.Execute();
            }
        }

        protected override IDictionary<string, string> GetRequiredParameters()
        {
            var requiredParams = new Dictionary<string, string>();
            requiredParams.Add("fileName", typeof(string).FullName);
            requiredParams.Add("template", typeof(string).FullName);
            return requiredParams;
        }
    }
}
