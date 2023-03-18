using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SPPC.Framework.Extensions;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Reporting;
using SPPC.Tools.Extensions;
using SPPC.Tools.Model;
using SPPC.Tools.Utility;

namespace SPPC.Tools.SystemDesigner.Designers
{
    public partial class ReportBrowserForm : Form
    {
        public ReportBrowserForm()
        {
            InitializeComponent();
            _sysConnection = DbConnections.SystemConnection;
            _dal = new SqlDataLayer(_sysConnection);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.GetActiveForm().Cursor = Cursors.WaitCursor;
            LoadReports();
            LoadLocalReports();
            LoadParameters();
            SetNextIdValues();
            this.GetActiveForm().Cursor = Cursors.Default;
        }

        private void Reports_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            var hiddenColumns = new List<string> { "ResourceMap", "State" };
            var orderedColumns = new List<string>
            {
                "Id", "ParentId", "ViewId", "CreatedById", "Code", "ServiceUrl",
                "IsGroup", "IsSystem", "IsDefault", "IsDynamic", "SubsystemId"
            };
            e.Column.Visible = !hiddenColumns.Contains(e.Column.Name);
            var allColumns = e.Column.DataGridView.Columns;
            if (allColumns.Count == orderedColumns.Count)
            {
                foreach (DataGridViewColumn column in allColumns)
                {
                    int index = orderedColumns.IndexOf(column.Name);
                    if (index != -1)
                    {
                        column.DisplayIndex = index;
                    }
                }
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            var editor = new ReportEditorForm();
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                SetIdValues(editor);
                _allReports.Add(editor.Report);
                _allLocalReports.AddRange(editor.LocalReports);
                _allParameters.AddRange(editor.Parameters);
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
        }

        private void Delete_Click(object sender, EventArgs e)
        {
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            GenerateCreateScripts();
            GenerateUpdateScripts();
            MessageBox.Show(this, "Scripts were successfully generated.", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private static ReportViewModel ReportFromRow(DataRow row)
        {
            int nullable = row.ValueOrDefault<int>("ParentID");
            int? parentId = nullable > 0 ? nullable : null;
            nullable = row.ValueOrDefault<int>("ViewID");
            int? viewId = nullable > 0 ? nullable : null;
            return new ReportViewModel()
            {
                Id = row.ValueOrDefault<int>("ReportID"),
                ParentId = parentId,
                CreatedById = row.ValueOrDefault<int>("CreatedByID"),
                ViewId = viewId,
                SubsystemId = row.ValueOrDefault<int>("SubsystemID"),
                Code = row.ValueOrDefault("Code"),
                ServiceUrl = row.ValueOrDefault("ServiceUrl"),
                IsGroup = row.ValueOrDefault<bool>("IsGroup"),
                IsSystem = row.ValueOrDefault<bool>("IsSystem"),
                IsDefault = row.ValueOrDefault<bool>("IsDefault"),
                IsDynamic = row.ValueOrDefault<bool>("IsDynamic")
            };
        }

        private static LocalReportViewModel LocalReportFromRow(DataRow row)
        {
            return new LocalReportViewModel()
            {
                Id = row.ValueOrDefault<int>("LocalReportID"),
                LocaleId = row.ValueOrDefault<int>("LocaleId"),
                ReportId = row.ValueOrDefault<int>("ReportID"),
                Caption = row.ValueOrDefault("Caption"),
                Template = row.ValueOrDefault("Template")
            };
        }

        private static ParameterViewModel ParameterFromRow(DataRow row)
        {
            // TODO: Extract parameter data from row later...
            return new ParameterViewModel()
            {
            };
        }

        private void LoadReports()
        {
            var result = _dal.Query("SELECT * FROM [Reporting].[Report]");
            _allReports.AddRange(result.Rows
                .Cast<DataRow>()
                .Select(row => ReportFromRow(row)));
            grdReports.DataSource = _allReports;
        }

        private void LoadLocalReports()
        {
            var result = _dal.Query("SELECT * FROM [Reporting].[LocalReport]");
            _allLocalReports.AddRange(result.Rows
                .Cast<DataRow>()
                .Select(row => LocalReportFromRow(row)));
        }

        private void LoadParameters()
        {
            var result = _dal.Query("SELECT * FROM [Reporting].[Parameter]");
            _allParameters.AddRange(result.Rows
                .Cast<DataRow>()
                .Select(row => ParameterFromRow(row)));
        }

        private void SetNextIdValues()
        {
            _nextReportId = _allReports.Max(rpt => rpt.Id) + 1;
            _nextLocalReportId =
                (int)_dal.QueryScalar("SELECT MAX([LocalReportID]) FROM [Reporting].[LocalReport]") + 1;
            _nextParameterId =
                (int)_dal.QueryScalar("SELECT MAX([ParamID]) FROM [Reporting].[Parameter]") + 1;
        }

        private void SetIdValues(ReportEditorForm editor)
        {
            int viewId = 0;
            var value = _dal.QueryScalar(@$"
SELECT [ViewID]
FROM [Metadata].[View]
WHERE [EntityName] = '{editor.SelectedViewModel}'");
            if (value == null)
            {
                viewId = (int)_dal.QueryScalar("SELECT MAX([ViewID]) FROM [Metadata].[View]") + 1;
            }

            editor.Report.Id = _nextReportId;
            editor.Report.ViewId = viewId;
            Array.ForEach(editor.LocalReports.ToArray(), report =>
            {
                report.ReportId = _nextReportId;
                report.Id = _nextLocalReportId++;
            });
            Array.ForEach(editor.Parameters.ToArray(), param =>
            {
                param.ReportId = _nextReportId;
                param.Id = _nextParameterId++;
            });
            _nextReportId++;
        }

        private void GenerateCreateScripts()
        {
            var orderedReports = _allReports.OrderBy(rpt => rpt.Id);
            var generated = ScriptUtility.GetInsertScripts(orderedReports, ReportExtensions.ToScript);
            ScriptUtility.ReplaceSysScript(generated);

            var orderedLocals = _allLocalReports.OrderBy(local => local.Id);
            generated = ScriptUtility.GetInsertScripts(_allLocalReports, LocalReportExtensions.ToScript);
            ScriptUtility.ReplaceSysScript(generated);

            // TODO: Add scripting support to ParameterViewModel, then uncomment the following lines...
            ////var orderedParameters = _allParameters.OrderBy(param => param.Id);
            ////generated = ScriptUtility.GetInsertScripts(_allParameters, ParameterExtensions.ToScript);
            ////ScriptUtility.ReplaceSysScript(generated);
        }

        private void GenerateUpdateScripts()
        {
            var addedReports = _allReports.Where(rpt => rpt.State == RecordState.Added);
            var addedLocals = _allLocalReports.Where(local => local.State == RecordState.Added);
            var scriptBuilder = new StringBuilder();
            ScriptUtility.AddSysVersionMarker(scriptBuilder);
            scriptBuilder.Append(
                ScriptUtility.GetInsertScripts(addedReports, ReportExtensions.ToScript));
            scriptBuilder.Append(
                ScriptUtility.GetInsertScripts(addedLocals, LocalReportExtensions.ToScript));

            // TODO: Add scripting support to ParameterViewModel, then uncomment the following lines...
            ////var addedParameters = _allParameters.Where(param => param.State == RecordState.Added);
            ////scriptBuilder.AppendLine(
            ////    ScriptUtility.GetInsertScripts(addedParameters, ParameterExtensions.ToScript));

            var path = Path.Combine(PathConfig.ApiScriptRoot, ScriptUtility.SysUpdateScriptName);
            File.AppendAllText(path, scriptBuilder.ToString(), Encoding.UTF8);
        }

        private readonly List<ReportViewModel> _allReports = new();
        private readonly List<LocalReportViewModel> _allLocalReports = new();
        private readonly List<ParameterViewModel> _allParameters = new();
        private readonly DataLayerBase _dal;
        private readonly string _sysConnection;
        private int _nextReportId;
        private int _nextLocalReportId;
        private int _nextParameterId;
    }
}
