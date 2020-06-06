using System;
using System.Linq;
using System.Windows.Forms;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.Tools.SystemDesigner.Models;

namespace SPPC.Tadbir.Tools.SystemDesigner.Wizards.ViewWizard
{
    public partial class SelectViewModelForm : UserControl
    {
        public SelectViewModelForm()
        {
            InitializeComponent();
            Info = "Select View Model";
            SelectedViewModel = "View Models";
        }

        public string Info { get; set; }

        public string SelectedViewModel { get; private set; }

        public ViewModelEntityModel View { get; set; }
        public ViewModelWizard ViewModelWizard { get; set; }

        public void SetSelectedViewModel(string name)
        {
            SelectedViewModel = !String.IsNullOrEmpty(name)
                ? name
                : "View Models";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SelectedViewModel = ViewModelWizard.SelectedViewModelOnTreeView ;
            cmbEntityType.SelectedIndex = 0;
            LoadViewModels();
            SetupBindings();
        }

        private void TViewModels_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count == 0)
            {
                SelectedViewModel = e.Node.Name;
                txtName.Text = SelectedViewModel;
                View.Name = SelectedViewModel;
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

            var node = tvViewModels.Nodes.Find(SelectedViewModel, true);
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

        private void SelectViewModelForm_Leave(object sender, EventArgs e)
        {
            ViewModelWizard.SelectedViewModelOnTreeView = SelectedViewModel;
        }

    }
}
