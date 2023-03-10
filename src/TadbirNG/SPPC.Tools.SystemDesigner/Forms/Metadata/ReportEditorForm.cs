using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SPPC.Framework.Extensions;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tools.Extensions;
using SPPC.Tools.Model;

namespace SPPC.Tools.SystemDesigner.Designers
{
    public partial class ReportEditorForm : Form
    {
        public ReportEditorForm()
        {
            InitializeComponent();
            _sysConnection = DbConnections.SystemConnection;
            Report = new ReportViewModel()
            {
                CreatedById = AppConstants.AdminUserId,
                Code = String.Empty,
                State = RecordState.Added,
                IsDefault = true,
                IsDynamic = true,
                IsSystem = true
            };
            LocalReports = new List<LocalReportViewModel>();
            Parameters = new List<ParameterViewModel>();
        }

        public string SelectedViewModel { get; set; }

        public ReportViewModel Report { get; set; }

        public List<LocalReportViewModel> LocalReports { get; }

        public List<ParameterViewModel> Parameters { get; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupBindings();
            SetupControls();
            LoadViewModels();
        }

        private void Select_Click(object sender, EventArgs e)
        {
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                InitialDirectory = PathConfig.GitRepoRoot,
                RestoreDirectory = true,
                Title = "Browse Template File",
                DefaultExt = "mrt",
                Filter = "mrt files (*.mrt)|*.mrt|All files (*.*)|*.*"
            };
            if ( fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtTemplateEn.Text = fileDialog.FileName;
            }
        }

        private void BrowseFa_Click(object sender, EventArgs e)
        {
            var fileDialog = new OpenFileDialog
            {
                InitialDirectory = PathConfig.GitRepoRoot,
                RestoreDirectory = true,
                Title = "Browse Template File",
                DefaultExt = "mrt",
                Filter = "mrt files (*.mrt)|*.mrt|All files (*.*)|*.*"
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtTemplateFa.Text = fileDialog.FileName;
            }
        }

        private void QuickReport_CheckedChanged(object sender, EventArgs e)
        {
            txtTemplateEn.Enabled = !chkIsDynamic.Checked;
            btnBrowseEn.Enabled = !chkIsDynamic.Checked;
            txtTemplateFa.Enabled = !chkIsDynamic.Checked;
            btnBrowseFa.Enabled = !chkIsDynamic.Checked;
        }

        private void Add_Click(object sender, EventArgs e)
        {
        }

        private void Edit_Click(object sender, EventArgs e)
        {
        }

        private void Delete_Click(object sender, EventArgs e)
        {
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if(!Validate())
            {
                return;
            }

            SelectedViewModel = tvViewModels.SelectedNode.Text;
            Report.SubsystemId = cmbSubsystem.SelectedIndex + 1;
            Report.ParentId = cmbParent.SelectedIndex >= 0
                ? (int)cmbParent.SelectedValue
                : null;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetupBindings()
        {
            chkIsGroup.DataBindings.Add("Checked", Report, "IsGroup");
            chkIsDefault.DataBindings.Add("Checked", Report, "IsDefault");
            chkIsDynamic.DataBindings.Add("Checked", Report, "IsDynamic");
            chkIsSystem.DataBindings.Add("Checked", Report, "IsSystem");
            txtServiceUrl.DataBindings.Add("Text", Report, "ServiceUrl");
            var enReport = new LocalReportViewModel()
            {
                LocaleId = 1,
                State = RecordState.Added
            };
            var faReport = new LocalReportViewModel()
            {
                LocaleId = 2,
                State = RecordState.Added
            };
            txtEnglish.DataBindings.Add("Text", enReport, "Caption");
            txtPersian.DataBindings.Add("Text", faReport, "Caption");
            LocalReports.Add(enReport);
            LocalReports.Add(faReport);
        }

        private void LoadViewModels()
        {
            this.GetActiveForm().Cursor = Cursors.WaitCursor;
            var assembly = typeof(ColumnViewModel).Assembly;
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
                    string typeName = vmType.Name.EndsWith("ViewModel")
                        ? vmType.Name.Replace("ViewModel", String.Empty)
                        : String.Empty;
                    if (!String.IsNullOrEmpty(typeName))
                    {
                        schema.Nodes.Add(typeName, typeName);
                    }
                }
            }

            this.GetActiveForm().Cursor = Cursors.Default;
        }

        private void LoadSubsystems()
        {
            this.GetActiveForm().Cursor = Cursors.WaitCursor;
            var dal = new SqlDataLayer(_sysConnection);
            var result = dal.Query(@"
SELECT [SubsystemID], [Name]
FROM [Metadata].[Subsystem]
ORDER BY [Name]");
            cmbSubsystem.ValueMember = "Id";
            cmbSubsystem.DisplayMember = "Name";
            cmbSubsystem.DataSource = result.Rows
                .Cast<DataRow>()
                .Select(row => new SubsystemViewModel()
                {
                    Id = row.ValueOrDefault<int>("SubsystemID"),
                    Name = row.ValueOrDefault("Name")
                })
                .ToList();
            this.GetActiveForm().Cursor = Cursors.Default;
        }

        private void LoadParents()
        {
            this.GetActiveForm().Cursor = Cursors.WaitCursor;
            var dal = new SqlDataLayer(_sysConnection);
            var result = dal.Query(@"
SELECT [ReportID],[Code]
FROM [Reporting].[Report]
WHERE [IsGroup] = 1
ORDER BY [ReportID]");
            cmbParent.ValueMember = "Id";
            cmbParent.DisplayMember = "Code";
            cmbParent.DataSource = result.Rows
                .Cast<DataRow>()
                .Select(row => new ReportViewModel()
                {
                    Id = row.ValueOrDefault<int>("ReportID"),
                    Code = row.ValueOrDefault("Code")
                })
                .ToList();
            this.GetActiveForm().Cursor = Cursors.Default;
        }

        private void SetupControls()
        {
            LoadSubsystems();
            LoadParents();
            grdParameters.DataSource = Parameters;
            cmbSubsystem.SelectedIndex = Report.SubsystemId > 0
                ? Report.SubsystemId - 1
                : -1;
            if (Report.ParentId.HasValue)
            {
                cmbParent.SelectedValue = Report.ParentId.Value;
            }
            else
            {
                cmbParent.SelectedIndex = -1;
            }
        }

        private new bool Validate()
        {
            if (tvViewModels.SelectedNode.Nodes.Count > 0)
            {
                MessageBox.Show(this, "Please select a view model.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (chkIsDynamic.Checked)
            {
                return true;
            }

            if (String.IsNullOrWhiteSpace(txtEnglish.Text))
            {
                MessageBox.Show(this, "Please fill English Caption.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (String.IsNullOrWhiteSpace(txtPersian.Text))
            {
                MessageBox.Show(this, "Please fill Persian Caption.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
        }

        private const string _defaultAssembly = "SPPC.Tadbir.ViewModel";
        private readonly string _sysConnection;
    }
}
