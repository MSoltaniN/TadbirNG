using System;
using System.ComponentModel;
using System.Windows.Forms;
using SPPC.Tools.MetaDesigner.Common;

namespace SPPC.Tools.MetaDesigner.Engine
{
    public class CommandHandler : IMetaDesignerController
    {
        public IRepositoryView View
        {
            get
            {
                return _view;
            }

            set
            {
                if (value != null && _view != value)
                {
                    _view = value;
                    _view.NodeAdded += View_NodeAdded;
                }
            }
        }

        public void Handle(object sender, EventArgs args)
        {
            var cancelled = RaiseExecutingEvent();
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem != null && !cancelled.Cancel)
            {
                try
                {
                    var command = menuItem.Tag as IRepositoryCommand;
                    command.Execute();
                    RaiseExecutedEvent();
                }
                catch (Exception exception)
                {
                    var errorMessage = String.Format(_errorMessageFormat, exception.Message);
                    MessageBox.Show(Form.ActiveForm, errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RaiseErrorEvent(exception);
                }
            }
        }

        public event EventHandler<CancelEventArgs> Executing;
        public event EventHandler<EventArgs> Executed;
        public event EventHandler<ErrorEventArgs> Error;

        public void SetNodeContextMenu(TreeNode node)
        {
            var menuBuilder = new ContextMenuBuilder();
            var contextBuilder = new CommandContextBuilder();
            var context = contextBuilder.Build(node);
            var contextMenu = menuBuilder.Build(context);
            if (contextMenu.Items.Count > 0)
            {
                node.ContextMenuStrip = contextMenu;
            }
        }

        private void View_NodeAdded(object sender, TreeNodeEventArgs e)
        {
            SetNodeContextMenu(e.Node.Node as TreeNode);
        }

        private CancelEventArgs RaiseExecutingEvent()
        {
            CancelEventArgs cancelArgs = new CancelEventArgs(false);
            if (Executing != null)
            {
                Executing(this, cancelArgs);
            }

            return cancelArgs;
        }

        private void RaiseExecutedEvent()
        {
            if (Executed != null)
            {
                Executed(this, EventArgs.Empty);
            }
        }

        private void RaiseErrorEvent(Exception exception)
        {
            if (Error != null)
            {
                Error(this, new ErrorEventArgs(exception));
            }
        }

        private IRepositoryView _view;
        private string _errorMessageFormat = "An error occured while executing current command.\r\nError details : {0}";
    }
}
