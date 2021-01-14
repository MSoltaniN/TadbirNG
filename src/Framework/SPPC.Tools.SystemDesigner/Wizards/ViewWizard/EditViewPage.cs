using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tools.SystemDesigner.Wizards.ViewWizard
{
    public partial class EditViewPage : UserControl
    {
        public EditViewPage()
        {
            InitializeComponent();
            Info = "Select View Model";
        }

        public string Info { get; set; }

        public ViewViewModel View { get; set; }
        public List<ColumnViewModel> Columns { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupBindings();
            cmbEntityType.SelectedIndex = 0;
            LoadViewModels();
        }

        private void ViewModels_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var lastViewName= View.Name;
            if (e.Node.Nodes.Count == 0)
            {
                txtName.Text = e.Node.Name;
                View.Name = e.Node.Name;
                View.IsHierarchy = false;
                View.IsCartableIntegrated = false;
            }
            if(lastViewName != View.Name)
            {
                Columns.Clear();
            }
        }

        private void LoadViewModels()
        {
            var assembly = typeof(AccountViewModel).Assembly;
            var all = assembly
                .GetTypes()
                .GroupBy(type => type.Namespace.Replace(_defaultAssembly, String.Empty));
            var root = tvViewModels.Nodes.Add("View Models", "View Models");
            foreach (var grp in all)
            {
                string schemaName = String.IsNullOrEmpty(grp.Key)
                    ? "(no schema)"
                    : grp.Key.TrimStart('.');
                var schema = root.Nodes.Add(schemaName, schemaName);
                foreach (var vmType in grp)
                {
                    string typeName = (vmType.Name.IndexOf("ViewModel") != -1)
                        ? vmType.Name.Replace("ViewModel", String.Empty)
                        : vmType.Name;
                    schema.Nodes.Add(typeName, typeName);
                }
            }

            var node = tvViewModels.Nodes.Find(View.Name, true);
            tvViewModels.SelectedNode = node.FirstOrDefault() ?? tvViewModels.Nodes[0];
        }

        private void SetupBindings()
        {
            txtName.DataBindings.Add("Text", View, "Name");
            txtFetchUrl.DataBindings.Add("Text", View, "FetchUrl");
            txtSearchUrl.DataBindings.Add("Text", View, "SearchUrl");
            cmbEntityType.DataBindings.Add("Text", View, "EntityType");
            chkIsHierarchy.DataBindings.Add("Checked", View, "IsHierarchy");
            chkEnableCartable.DataBindings.Add("Checked", View, "IsCartableIntegrated");
        }

        private const string _defaultAssembly = "SPPC.Tadbir.ViewModel";
    }
}
