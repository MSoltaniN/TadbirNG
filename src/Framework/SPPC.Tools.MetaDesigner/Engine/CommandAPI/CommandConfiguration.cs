using System;
using System.Collections.Generic;
using System.Linq;
using SPPC.Tools.MetaDesigner.Common;
using SPPC.Framework.Common;
using SPPC.Tools.MetaDesigner.Configuration;
using SPPC.Tools.Model;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class CommandConfiguration
    {
        public CommandConfiguration()
        {
            var sectionHandler = new MetaDesignerSectionHandler();
            _configSection = sectionHandler.Section;
        }

        public Tree GetCommands(ICommandContext context)
        {
            var configTree = new Tree();
            if (context.IsItemContext)
            {
                AddItems(configTree, "item", context);
            }
            else if (context.IsCollectionContext)
            {
                AddItems(configTree, "collection", context);
            }
            else
            {
                AddItems(configTree, "object", context);
            }

            return configTree;
        }

        public CommandElement GetCommand(string commandName)
        {
            var command = GetCommandByName(commandName, _configSection.Commands);
            if (command == null)
            {
                command = GetCommandByName(commandName, _configSection.DefaultCommands);
            }

            return command;
        }

        public CommandParameterElement GetParameter(string commandName, string parameterName)
        {
            var command = GetCommand(commandName);
            return command.Parameters
                .Cast<CommandParameterElement>()
                .Where(para => para.Name == parameterName)
                .FirstOrDefault();
        }

        public string[] GetParameterNames(string commandName)
        {
            Verify.ArgumentNotNullOrWhitespace(commandName, "commandName");
            var command = GetCommand(commandName);
            var parameterNames = command.Parameters
                .Cast<CommandParameterElement>()
                .Select(cfg => cfg.Name)
                .ToList();
            if (command.NeedsInput)
            {
                parameterNames.Add("editor");
            }

            return parameterNames.ToArray();
        }

        public MetadataEditorElement GetEditor(ICommandContext context)
        {
            var type = GetItemType(context);
            var typeName = String.Format("{0},{1}", type.FullName, type.Namespace);
            return _configSection.MetadataEditors
                .Cast<MetadataEditorElement>()
                .Where(editor => editor.ItemType == typeName)
                .FirstOrDefault();
        }

        private CommandElement GetCommandByName(string commandName, CommandElementCollection collection)
        {
            return collection
                .Cast<CommandElement>()
                .Where(cmd => cmd.Name == commandName)
                .FirstOrDefault();
        }

        private void AddItems(Tree configTree, string commandType, ICommandContext context)
        {
            var itemType = String.Empty;
            if (context.IsItemContext)
            {
                itemType = context.Item.GetType().Name;
            }
            else if (context.IsObjectContext)
            {
                itemType = context.Object.GetType().Name;
            }
            else if (Reflector.IsGenericCollection(context.Collection))
            {
                itemType = context.Collection.GetType().GenericTypeArguments[0].Name;
            }

            var items = GetItems(_configSection.Commands, commandType, itemType);
            Array.ForEach(items.ToArray(), item => configTree.AddChild(item));
            items = GetItems(_configSection.DefaultCommands, commandType, String.Empty);
            Array.ForEach(items.ToArray(), item => configTree.AddChild(item));
        }

        private IEnumerable<Tree> GetItems(CommandElementCollection commands, string commandType, string itemType)
        {
            var treeItems = new List<Tree>();
            var configItems = commands
                .Cast<CommandElement>()
                .Where(cfg => cfg.CommandType == commandType && cfg.TargetType == itemType)
                .GroupBy(cfg => cfg.Parent);
            foreach (var configGroup in configItems)
            {
                if (String.IsNullOrWhiteSpace(configGroup.Key))
                {
                    Array.ForEach(configGroup.ToArray(), cfg => treeItems.Add(new Tree() { Data = cfg }));
                }
                else
                {
                    var configSubTree = new Tree() { Data = configGroup.Key };
                    Array.ForEach(configGroup.ToArray(), cfg => configSubTree.AddChild(new Tree() { Data = cfg }));
                    treeItems.Add(configSubTree);
                }
            }

            return treeItems;
        }

        private Type GetItemType(ICommandContext context)
        {
            Type type = null;
            if (context.IsItemContext)
            {
                type = context.Item.GetType();
            }
            else if (context.IsObjectContext)
            {
                type = context.Object.GetType();
            }
            else if (context.Collection != null && Reflector.IsGenericCollection(context.Collection))
            {
                type = context.Collection.GetType().GenericTypeArguments[0];
            }
            else
            {
                type = typeof(object);
            }

            return type;
        }

        private MetaDesignerSection _configSection;
    }
}
