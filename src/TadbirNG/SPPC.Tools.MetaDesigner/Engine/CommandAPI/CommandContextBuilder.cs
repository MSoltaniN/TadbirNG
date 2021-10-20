using System;
using System.Collections;
using System.Windows.Forms;
using SPPC.Framework.Common;
using SPPC.Tools.MetaDesigner.Common;
using SPPC.Tools.Model;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class CommandContextBuilder
    {
        public ICommandContext Build(TreeNode node)
        {
            var context = new CommandContext
            {
                Model = MetaDesignerContext.Current.Model
            };
            if (IsMetadataItemNode(node))
            {
                context.Item = node.Tag;
                context.Collection = node.Parent.Tag as IList;
            }
            else if (IsMetadataCollectionNode(node))
            {
                context.Collection = node.Tag as IList;
                context.Object = node.Parent.Tag;
            }
            else
            {
                context.Object = node.Tag;
            }

            return context;
        }

        private bool IsMetadataCollectionNode(TreeNode node)
        {
            var isCollection = Reflector.IsGenericCollection(node.Tag);
            if (isCollection)
            {
                var result = isCollection && IsMetadataType(node.Tag.GetType().GenericTypeArguments[0]);
                return result;
            }

            return false;
        }

        private bool IsMetadataItemNode(TreeNode node)
        {
            return (node.Parent != null && IsMetadataCollectionNode(node.Parent));
        }

        private bool IsMetadataType(Type type)
        {
            var fullNameItems = typeof(Entity).FullName.Split('.');
            var metadataNamespace = String.Join(".", fullNameItems, 0, fullNameItems.Length - 1);
            return type.FullName.StartsWith(metadataNamespace);
        }
    }
}
