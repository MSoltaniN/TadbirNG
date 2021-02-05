﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using SPPC.Tools.MetaDesigner.Common;
using SPPC.Tools.MetaDesigner.Transforms;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class AddItemCommand : RepositoryCommand, IRepositoryCommand
    {
        public AddItemCommand() : base()
        {
        }

        public override void Execute()
        {
            base.Execute();
            var wrapper = Parameters["wrapper"] as IRepositoryModel;
            var collection = Parameters["collection"] as IEnumerable;
            var editor = Parameters["editor"] as Form;
            if (editor != null)
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
            var requiredParams = new Dictionary<string, string>();
            requiredParams.Add("collection", String.Empty);
            requiredParams.Add("wrapper", typeof(IRepositoryModel).AssemblyQualifiedName);
            requiredParams.Add("editor", typeof(Form).AssemblyQualifiedName);
            return requiredParams;
        }
    }
}