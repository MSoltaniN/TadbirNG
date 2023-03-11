using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SPPC.Framework.Extensions;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Config;
using SPPC.Tools.Extensions;
using SPPC.Tools.Model;

namespace SPPC.Tools.SystemDesigner.Forms
{
    public partial class LogSelectOperationsForm : Form
    {
        public LogSelectOperationsForm()
        {
            InitializeComponent();
            SelectedItems = new List<LogSettingViewModel>();
        }

        public bool IsSystemSetting { get; set; }

        public List<LogSettingViewModel> SelectedItems { get; }

        protected override void OnLoad(EventArgs e)
        {
            this.GetActiveForm().Cursor = Cursors.WaitCursor;
            base.OnLoad(e);
            LoadOperations();
            HighlightSelectedItems();
            this.GetActiveForm().Cursor = Cursors.Default;
        }

        private void AddOperation_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show(this, "Operation name cannot be empty or whitespace.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var operation = new LogSettingViewModel()
            {
                OperationName = txtName.Text,
                IsEnabled = true,
                State = RecordState.Added
            };
            var allOperations = lstOperations.DataSource as List<LogSettingViewModel>;
            SaveOperations();
            allOperations.Add(operation);
            SelectedItems.Add(operation);
            lstOperations.DataSource = null;
            lstOperations.DataSource = allOperations
                .OrderBy(item => item.OperationName)
                .ToList();
            lstOperations.DisplayMember = "OperationName";
            HighlightSelectedItems();
            txtName.Text = String.Empty;
        }

        private void Select_Click(object sender, EventArgs e)
        {
            if (lstOperations.SelectedIndices.Count == 0)
            {
                MessageBox.Show(this, "No item is selected.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            SaveOperations();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void LoadOperations()
        {
            var connection = IsSystemSetting
                ? DbConnections.SystemConnection
                : DbConnections.CompanyConnection;
            var dal = new SqlDataLayer(connection);
            var result = dal.Query(@"
SELECT [OperationID], [Name]
FROM [Metadata].[Operation]
ORDER BY [Name]");
            lstOperations.DisplayMember = "OperationName";
            lstOperations.DataSource = result.Rows
                .Cast<DataRow>()
                .Select(row => new LogSettingViewModel()
                {
                    OperationId = row.ValueOrDefault<int>("OperationID"),
                    OperationName = row.ValueOrDefault("Name"),
                    IsEnabled = true
                })
                .ToList();
        }

        private void SaveOperations()
        {
            SelectedItems.Clear();
            SelectedItems.AddRange(lstOperations.SelectedIndices
                .Cast<int>()
                .Select(index => lstOperations.Items[index] as LogSettingViewModel)
                .OrderBy(setting => setting.OperationId));
        }

        private void HighlightSelectedItems()
        {
            lstOperations.SelectedIndices.Clear();
            if (SelectedItems.Count > 0)
            {
                var selectedIds = SelectedItems.Select(item => item.OperationId);
                for (int index = 0; index < lstOperations.Items.Count; index++)
                {
                    var setting = lstOperations.Items[index] as LogSettingViewModel;
                    if (selectedIds.Contains(setting.OperationId))
                    {
                        lstOperations.SelectedIndices.Add(index);
                    }
                }
            }
            else
            {
                var defaultOps = new string[]
                {
                    "View", "Create", "Edit", "Delete", "Filter", "Print",
                    "GroupDelete", "Export", "PrintPreview"
                };
                for (int index = 0; index < lstOperations.Items.Count; index++)
                {
                    var setting = lstOperations.Items[index] as LogSettingViewModel;
                    if (defaultOps.Contains(setting.OperationName))
                    {
                        lstOperations.SelectedIndices.Add(index);
                    }
                }
            }
        }
    }
}
