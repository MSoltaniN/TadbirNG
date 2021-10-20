using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SPPC.Tools.MetaDesigner.Common;
using SPPC.Tools.MetaDesigner.Configuration;
using SPPC.Tools.Model;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class ContextMenuBuilder
    {
        public ContextMenuStrip Build(ICommandContext context)
        {
            var contextMenu = new ContextMenuStrip();
            var commandConfig = new CommandConfiguration();
            var configTree = commandConfig.GetCommands(context);
            AddChildMenuItems(contextMenu.Items, configTree.Children, context);
            return contextMenu;
        }

        private void AddChildMenuItems(
            ToolStripItemCollection menuItems, IEnumerable<Tree> treeItems, ICommandContext context)
        {
            foreach (var treeItem in treeItems)
            {
                var commandConfig = treeItem.Data as CommandElement;
                if (commandConfig != null)
                {
                    context.Command = commandConfig.Name;
                    menuItems.Add(BuildMenuItem(context));
                }
                else
                {
                    var popupMenu = new ToolStripMenuItem((string)treeItem.Data);
                    AddChildMenuItems(popupMenu.DropDownItems, treeItem.Children, context);
                    menuItems.Add(popupMenu);
                }
            }
        }

        private ToolStripMenuItem BuildMenuItem(ICommandContext context)
        {
            var menuItem = new ToolStripMenuItem(context.Title) { Name = context.Command };
            menuItem.Click += new EventHandler(MetaDesignerContext.Current.Controller.Handle);
            menuItem.Tag = _commandBuilder.Build(context);
            return menuItem;
        }

        private CommandBuilder _commandBuilder = new CommandBuilder();
    }
}
