using System;
using System.Windows.Forms;
using SPPC.Tools.MetaDesigner.Common;
using SPPC.Tools.MetaDesigner.Engine;

namespace SPPC.Tools.MetaDesigner
{
    public partial class MetadataDesigner : Form
    {
        public MetadataDesigner()
        {
            InitializeComponent();
        }

        #region Repository Menu Commands

        private void NewRepositoryFromFileMenu_Click(object sender, EventArgs e)
        {
            RunFileCommand(RepositoryCommandType.New, true);
        }

        private void OpenRepositoryFromFileMenu_Click(object sender, EventArgs e)
        {
            RunFileCommand(RepositoryCommandType.Open, true);
        }

        private void SaveRepositoryMenu_Click(object sender, EventArgs e)
        {
            RunFileCommand(RepositoryCommandType.Save, false);
        }

        private void SaveRepositoryAsMenu_Click(object sender, EventArgs e)
        {
            RunFileCommand(RepositoryCommandType.SaveAs, false);
        }

        private void RunFileCommand(RepositoryCommandType commandType, bool refreshView)
        {
            var command = new FileRepositoryCommand();
            command.Parameters.Add("action", commandType);
            command.Execute();
            if (command.IsComplete && refreshView)
            {
                ShowRepository();
            }
        }

        #endregion

        private void Metadata_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var data = e.Node.Tag;
            ShowProperties(data);
        }

        private void Properties_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (e.ChangedItem.PropertyDescriptor.Name == "Name")
            {
                ShowRepository();
            }
        }

        private void ShowRepository()
        {
            tvMetadata.Nodes.Clear();
            tvMetadata.Nodes.Add(MetaDesignerContext.Current.View.RootNode.Node as TreeNode);
        }

        private void ShowProperties(object data)
        {
            prgProperties.SelectedObject = data;
            txtCurrentElement.Text = (data != null)
                ? String.Format("{0} ({1})", data, data.GetType().FullName)
                : String.Format("(No property available)");
        }
    }
}
