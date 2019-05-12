using System;
using System.Linq;
using System.Windows.Forms;
using SPPC.Tadbir.ViewModel.Finance;

namespace SPPC.Tadbir.Tools.SystemDesigner.Wizards.ViewWizard
{
    public partial class SelectViewModelForm : UserControl
    {
        public SelectViewModelForm()
        {
            InitializeComponent();
            SelectedViewModel = "View Models";
        }

        public string SelectedViewModel { get; private set; }

        public void SetSelectedViewModel(string name)
        {
            SelectedViewModel = !String.IsNullOrEmpty(name)
                ? name
                : "View Models";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadViewModels();
        }

        private void tvViewModels_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count == 0)
            {
                SelectedViewModel = e.Node.Name;
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

        private const string _defaultAssembly = "SPPC.Tadbir.ViewModel";
    }
}
