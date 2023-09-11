using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using SPPC.Tools.MetaDesigner.Common;
using SPPC.Tools.Presentation;
using SPPC.Tools.Transforms;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class AddItemCommand : RepositoryCommand, IRepositoryCommand
    {
        public AddItemCommand()
            : base()
        {
        }

        public override void Execute()
        {
            base.Execute();
            var wrapper = Parameters["wrapper"] as IRepositoryModel;
            var collection = Parameters["collection"] as IEnumerable;
            if (Parameters["editor"] is Form editor)
            {
                var inputCollector = new FormInputCollector() { InputForm = editor };
                inputCollector.GetInput();
                if (inputCollector.Output != null)
                {
                    wrapper.AddItem(inputCollector.Output, repo => collection);
                }
            }
            else
            {
                var itemType = collection.GetType().GenericTypeArguments[0];
                var generator = new BasicMetaGenerator();
                object item = generator.GenerateDefaultItem(itemType);
                wrapper.AddItem(item, repo => collection);
            }
        }

        protected override IDictionary<string, string> GetRequiredParameters()
        {
            var requiredParams = new Dictionary<string, string>
            {
                { "collection", String.Empty },
                { "wrapper", typeof(IRepositoryModel).AssemblyQualifiedName },
                { "editor", typeof(Form).AssemblyQualifiedName }
            };
            return requiredParams;
        }
    }
}
