using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using SPPC.Framework.Common;
using SPPC.Tools.MetaDesigner.Common;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class DeleteItemCommand : RepositoryCommand, IRepositoryCommand
    {
        public DeleteItemCommand() : base()
        {
        }

        public override void Execute()
        {
            base.Execute();
            var item = Parameters["item"];
            var confirmMessage = String.Format(
                "Are you sure you want to delete this {0}?", item.GetType().Name.ToLower());
            if (MessageBox.Show(Form.ActiveForm, confirmMessage, "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                == DialogResult.Yes)
            {
                var collection = Parameters["collection"] as IEnumerable;
                var wrapper = Parameters["wrapper"] as IRepositoryModel;
                wrapper.RemoveItem(item, repo => collection);
            }
        }

        protected override IDictionary<string, string> GetRequiredParameters()
        {
            var requiredParams = new Dictionary<string, string>();
            requiredParams.Add("item", String.Empty);
            requiredParams.Add("collection", String.Empty);
            requiredParams.Add("wrapper", typeof(IRepositoryModel).AssemblyQualifiedName);
            return requiredParams;
        }

        protected override bool ValidateParameters()
        {
            return (base.ValidateParameters()
                && IsCompatibleCollection(Parameters["collection"], Parameters["item"]));
        }

        private bool IsCompatibleCollection(object collection, object item)
        {
            var collectionType = collection.GetType();
            var itemType = item.GetType();
            return (Reflector.IsGenericCollection(collection)
                && collectionType.GenericTypeArguments[0].IsAssignableFrom(itemType));
        }
    }
}
