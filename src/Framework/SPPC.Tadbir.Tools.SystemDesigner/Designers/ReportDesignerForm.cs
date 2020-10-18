using BabakSoft.Platform.Data;
using SPPC.Framework.Helpers;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SPPC.Tadbir.Tools.SystemDesigner.Designers
{
    public partial class ReportDesignerForm : Form
    {
        public ReportDesignerForm()
        {
            InitializeComponent();
        }

        private void ReportDesignerForm_Load(object sender, EventArgs e)
        {
            _sysConnection = GetSysConnectionString();
            SetupControls();
        }

        private void SetupControls()
        {
            LoadListViews();
            LoadSubSystems();
            LoadParents();
            LoadParameters();
        }

        #region General tab
        private void LoadListViews()
        {
            var dal = new SqlDataLayer(_sysConnection, ProviderType.SqlClient);
            cmbListViews.ValueMember = "ViewID";
            cmbListViews.DisplayMember = "Name";
            cmbListViews.DataSource = dal.Query(@"SELECT [ViewID], [Name] FROM [Metadata].[View] 
                                                 WHERE [ViewID] NOT IN ( SELECT DISTINCT COALESCE(ViewID,0) FROM [Reporting].[Report] )
                                                 ORDER BY ViewID");
        }

        private void LoadSubSystems()
        {
            cmbSubsystem.SelectedIndex = 0;
        }

        private void LoadParents()
        {
            var dal = new SqlDataLayer(_sysConnection, ProviderType.SqlClient);
            cmbParent.ValueMember = "ReportID";
            cmbParent.DisplayMember = "Code";
            cmbParent.DataSource = dal.Query(@"SELECT [ReportID],[Code] FROM [Reporting].[Report] 
                                                      WHERE [IsGroup] = 1 ORDER BY [ReportID]");
        }
       
        private void Select_Click(object sender, EventArgs e)
        {

        }

        private void Browse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.InitialDirectory = @"C:\";
            fileDialog.RestoreDirectory = true;
            fileDialog.Title = "Browse Template File";
            fileDialog.DefaultExt = "mrt";
            fileDialog.Filter = "mrt files (*.mrt)|*.mrt|All files (*.*)|*.*";
            if( fileDialog.ShowDialog() == DialogResult.OK)
            {
                txtTemplateEn.Text = fileDialog.FileName;
            }
        }

        private void QuickReport_CheckedChanged(object sender, EventArgs e)
        {
           if( chkQuickReport.Checked == true )
            {
                txtTemplateEn.Enabled = false;
                btnBrowseEn.Enabled = false;
            }
           else
            {
                txtTemplateEn.Enabled = true;
                btnBrowseEn.Enabled = true;
            }
        }

        #endregion
        #region Parameter tab
        private void LoadParameters()
        {
            MakeDataTableParameter();
            RefreshGrid();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            var paramForm = new ParameterEditorForm();
            if(paramForm.ShowDialog() == DialogResult.OK )
            {
                DataRow dr;
                dr = _table.NewRow();
                dr["Name"] = paramForm.txtName.Text;
                dr["FieldName"] = paramForm.txtFieldName.Text;
                dr["CaptionKey"] = paramForm.txtCaptionKey.Text;
                dr["Operator"] = paramForm.cmbOperator.SelectedItem.ToString();
                dr["DataType"] = paramForm.cmbDataType.SelectedItem.ToString();
                dr["ControlType"] = paramForm.cmbControlType.SelectedItem.ToString();
                _table.Rows.Add(dr);
                RefreshGrid();
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            var paramForm = new ParameterEditorForm();
            int paramId=1;
            if (grdParameters.SelectedRows.Count > 0)
            {
                paramId = Convert.ToInt32(grdParameters.SelectedRows[0].Cells["ParamId"].Value.ToString());
                DataRow dr = _table.Select(string.Format("ParamId={0}", paramId)).FirstOrDefault();
                if (dr != null)
                {
                    paramForm.txtName.Text = dr["Name"].ToString();
                    paramForm.txtFieldName.Text = dr["FieldName"].ToString();
                    paramForm.txtCaptionKey.Text = dr["CaptionKey"].ToString();
                    paramForm.cmbOperator.SelectedItem = dr["Operator"].ToString();
                    paramForm.cmbDataType.SelectedItem = dr["DataType"].ToString();
                    paramForm.cmbControlType.SelectedItem = dr["ControlType"].ToString();

                    if (paramForm.ShowDialog() == DialogResult.OK)
                    {
                        dr["Name"] = paramForm.txtName.Text;
                        dr["FieldName"] = paramForm.txtFieldName.Text;
                        dr["CaptionKey"] = paramForm.txtCaptionKey.Text;
                        dr["Operator"] = paramForm.cmbOperator.SelectedItem.ToString();
                        dr["DataType"] = paramForm.cmbDataType.SelectedItem.ToString();
                        dr["ControlType"] = paramForm.cmbControlType.SelectedItem.ToString();
                        RefreshGrid();
                    }
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (grdParameters.SelectedRows.Count > 0)
            {
                int paramId = Convert.ToInt32(grdParameters.SelectedRows[0].Cells["ParamId"].Value.ToString());
                DataRow dr = _table.Select(string.Format("ParamId={0}", paramId)).FirstOrDefault();
                dr.Delete();
                RefreshGrid();
            }
        }

        private void MakeDataTableParameter()
        {
            
            DataColumn column;

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.Int32");
            column.ColumnName = "ParamId";
            column.AutoIncrement = true;
            column.AutoIncrementSeed = 1;
            column.ReadOnly = true;
            column.Unique = true;
            _table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Name";
            column.AutoIncrement = false;
            column.Caption = "Name";
            column.ReadOnly = false;
            column.Unique = false;
            _table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "FieldName";
            column.AutoIncrement = false;
            column.Caption = "FieldName";
            column.ReadOnly = false;
            column.Unique = false;
            _table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "CaptionKey";
            column.AutoIncrement = false;
            column.Caption = "CaptionKey";
            column.ReadOnly = false;
            column.Unique = false;
            _table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Operator";
            column.AutoIncrement = false;
            column.Caption = "Operator";
            column.ReadOnly = false;
            column.Unique = false;
            _table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "DataType";
            column.AutoIncrement = false;
            column.Caption = "DataType";
            column.ReadOnly = false;
            column.Unique = false;
            _table.Columns.Add(column);

            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "ControlType";
            column.AutoIncrement = false;
            column.Caption = "ControlType";
            column.ReadOnly = false;
            column.Unique = false;
            _table.Columns.Add(column);

            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = _table.Columns["id"];
            _table.PrimaryKey = PrimaryKeyColumns;
        }

        private void RefreshGrid()
        {
            grdParameters.DataSource = _table;
        }

        #endregion

        private string GetSysConnectionString()
        {
            string path = @"..\..\src\Framework\SPPC.Tadbir.Web.Api\appsettings.Development.json";
            var appSettings = JsonHelper.To<AppSettingsModel>(File.ReadAllText(path));
            return appSettings.ConnectionStrings.TadbirSysApi;
        }

        private Version GetSolutionVersion()
        {
            var assemblyVersion = GetType().Assembly.GetName().Version;
            return new Version(assemblyVersion.ToString());
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            if(!Validate())
            {
                return;
            }
           
            var sysConnection = GetSysConnectionString();
            var dal = new SqlDataLayer(sysConnection, ProviderType.SqlClient);
            int maxReportId = Convert.ToInt32(dal.QueryScalar("SELECT MAX([ReportID]) FROM [Reporting].[Report]"));
            int maxLocalReport = Convert.ToInt32(dal.QueryScalar("SELECT MAX([LocalReportID]) FROM [Reporting].[LocalReport]"));
            int maxPramId = Convert.ToInt32(dal.QueryScalar("SELECT MAX([ParamID]) FROM [Reporting].[Parameter]"));
            int paramId = maxPramId + 1;
            var builder = new StringBuilder();
            var solutionVersion = GetSolutionVersion();

            builder.AppendLine();
            builder.AppendFormat("-- {0}", solutionVersion);
            builder.AppendLine();
            builder.AppendLine("SET IDENTITY_INSERT [Reporting].[Report] ON ");
            builder.AppendLine("INSERT INTO [Reporting].[Report] " +
                 "([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])");
            builder.AppendFormat("    VALUES ({0}, {1}, {2}, {3}, {4}, {5}, '{6}', {7}, {8}, {9}, {10})"
                , maxReportId + 1
                , cmbParent.SelectedValue
                , 1
                , cmbListViews.SelectedValue
                , cmbSubsystem.SelectedIndex + 1
                , ""
                , txtServiceUrl.Text
                , chkIsGroup.Checked == true ? 1 : 0
                , chkSystemReport.Checked == true ? 1 : 0
                , chkSetAsDefault.Checked == true ? 1 : 0
                , chkQuickReport.Checked == true ? 1 : 0);
            builder.AppendLine();
            builder.AppendLine("SET IDENTITY_INSERT [Reporting].[Report] OFF ");
            builder.AppendLine();

            builder.AppendLine();
            builder.AppendLine("SET IDENTITY_INSERT [Reporting].[LocalReport] ON ");
            builder.AppendLine("INSERT INTO [Reporting].[LocalReport] " +
                "([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])");
            builder.AppendFormat("    VALUES ({0}, {1}, {2}, '{3}', {4})"
                , maxLocalReport + 1
                , 1
                , maxReportId + 1
                , txtEnglish.Text
                , chkQuickReport.Checked ? "''" : GetNullableValue(txtTemplateEn.Text));
            builder.AppendLine();
            builder.AppendLine("INSERT INTO [Reporting].[LocalReport] " +
                "([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])");
            builder.AppendFormat("    VALUES ({0}, {1}, {2}, '{3}', {4})"
                , maxLocalReport + 1
                , 2
                , maxReportId + 1
                , txtPersian.Text
                , chkQuickReport.Checked ? "''" : GetNullableValue(txtTemplateEn.Text));
            builder.AppendLine();
            builder.AppendLine("SET IDENTITY_INSERT [Reporting].[LocalReport] OFF ");
            builder.AppendLine();

            builder.AppendLine();
            builder.AppendLine("SET IDENTITY_INSERT [Reporting].[Parameter] ON ");
            foreach ( DataRow dr in _table.Rows)
            {
                builder.AppendLine("INSERT INTO [Reporting].[Parameter] " +
                 "([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey])");
                builder.AppendFormat("    VALUES ({0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')"
                    , paramId++
                    , maxReportId + 1
                    , dr["Name"].ToString()
                    , dr["FieldName"].ToString()
                    , dr["Operator"].ToString()
                    , dr["CaptionKey"].ToString()
                    , dr["DataType"].ToString()
                    , dr["ControlType"].ToString()
                    , dr["CaptionKey"].ToString());
                builder.AppendLine();
            }
            
            builder.AppendLine("SET IDENTITY_INSERT [Reporting].[Parameter] OFF ");
            builder.AppendLine();

            File.AppendAllText(_TadbirSysUpdateScript, builder.ToString());

            MessageBox.Show("The script was generated.");
            DialogResult = DialogResult.OK;
            Close();
        }

        private new bool Validate()
        {
            if (txtEnglish.Text == "")
            {
                MessageBox.Show("Please fill English Caption.");
                return false;
            }
            if (txtPersian.Text == "")
            {
                MessageBox.Show("Please fill Persian Caption.");
                return false;
            }
            if (txtTemplateEn.Text == "" && chkQuickReport.Checked == false)
            {
                MessageBox.Show("Please fill template file path.");
                return false;
            }
            return true;
        }

        private string GetNullableValue(string nullable, string nullValue = null)
        {
            string output;
            if (nullValue == null)
            {
                output = String.IsNullOrEmpty(nullable)
                    ? "NULL"
                    : String.Format("'{0}'", nullable);
            }
            else
            {
                output = nullable == nullValue
                    ? "NULL"
                    : String.Format("'{0}'", nullable);
            }

            return output;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string _sysConnection;
        System.Data.DataTable _table = new DataTable("ParameterTable");
        private const string _TadbirSysUpdateScript = @"..\..\res\TadbirSys_UpdateDbObjects.sql";
    }
}
