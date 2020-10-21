using BabakSoft.Platform.Data;
using SPPC.Framework.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SPPC.Tadbir.Tools.SystemDesigner.Designers
{
    public partial class ManageReportsForm : Form
    {
        public ManageReportsForm()
        {
            InitializeComponent();
            LoadDataTables();
        }

        private void ManageReportsForm_Load(object sender, EventArgs e)
        {
            _sysConnection = GetSysConnectionString();
            LoadGridView();
        }

        private void LoadGridView()
        {
            grdNewReports.DataSource = _reportTable;
                //from table in _reportTable.AsEnumerable()
                //                       select new { ReportID = (int)table["ReportID"]
                //                                   , ParentID = (int)table["ParentID"] 
                //                                   , ViewID = (int)table["ViewID"]
                //                                   , SubsystemID = (int)table["SubsystemID"]
                //                                   , ServiceUrl = (int)table["ServiceUrl"]
                //                                   , IsGroup = (int)table["IsGroup"]
                //                                   , IsSystem = (int)table["IsSystem"]
                //                                   , IsDefault = (int)table["IsDefault"]
                //                                   , IsDynamic = (int)table["IsDynamic"]
                //                                   , EnCaption = (int)table["EnCaption"]
                //                                   , FaCaption = (int)table["FaCaption"]
                //                                   , EnTemplatePath = (int)table["EnTemplatePath"]
                //                                   , FaTemplatePath = (int)table["FaTemplatePath"] };

        }

        private void LoadDataTables()
        {
            DataColumn workCol = _reportTable.Columns.Add("ReportID", typeof(int));
            workCol.AllowDBNull = false;
            workCol.Unique = true;
            workCol.AutoIncrement = true;
            workCol.AutoIncrementSeed = 1;
            _reportTable.Columns.Add("ParentID"     , typeof(int));
            _reportTable.Columns.Add("ViewID"       , typeof(int));
            _reportTable.Columns.Add("SubsystemID"  , typeof(int));
            _reportTable.Columns.Add("ServiceUrl"   , typeof(string));
            _reportTable.Columns.Add("IsGroup"      , typeof(bool));
            _reportTable.Columns.Add("IsSystem"     , typeof(bool));
            _reportTable.Columns.Add("IsDefault"    , typeof(bool));
            _reportTable.Columns.Add("IsDynamic"    , typeof(bool));
            _reportTable.Columns.Add("EnCaption"    , typeof(string));
            _reportTable.Columns.Add("FaCaption"    , typeof(string));
            _reportTable.Columns.Add("EnTemplatePath", typeof(string));
            _reportTable.Columns.Add("FaTemplatePath", typeof(string));

            DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            PrimaryKeyColumns[0] = _reportTable.Columns["ReportID"];
            _reportTable.PrimaryKey = PrimaryKeyColumns;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            var form = new ReportDesignerForm()
            {
                sysConnection = _sysConnection ,
               
            };
            form.SetupControls();
            if (form.ShowDialog() == DialogResult.OK)
            {
                DataRow dr;
                dr = _reportTable.NewRow();
                dr["ParentID"   ] = form.cmbParent.SelectedValue;
                dr["ViewID"     ] = form.cmbListViews.SelectedValue;
                dr["SubsystemID"] = form.cmbSubsystem.SelectedIndex + 1;
                dr["ServiceUrl" ] = form.txtServiceUrl.Text;
                dr["IsGroup"    ] = form.chkIsGroup.Checked;
                dr["IsSystem"   ] = form.chkSystemReport.Checked;
                dr["IsDefault"  ] = form.chkSetAsDefault.Checked;
                dr["IsDynamic"  ] = form.chkQuickReport.Checked;
                dr["EnCaption"  ] = form.txtEnglish.Text;
                dr["FaCaption"  ] = form.txtPersian.Text;
                dr["EnTemplatePath"] = form.txtTemplateEn.Text;
                dr["FaTemplatePath"] = form.txtTemplateFa.Text;
                _reportTable.Rows.Add(dr);
                _parmDtDic.Add(Convert.ToInt32(dr["ReportId"]), form.paramTable);
                LoadGridView();    
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            
            int reportId = 1;
            if (grdNewReports.SelectedRows.Count > 0)
            {
                reportId = Convert.ToInt32(grdNewReports.SelectedRows[0].Cells["ReportID"].Value.ToString());
                DataRow dr = _reportTable.Select(string.Format("ReportID={0}", reportId)).FirstOrDefault();
                if (dr != null)
                {
                    var form1 = new ReportDesignerForm()
                    {
                        sysConnection = _sysConnection
                    };
                    form1.SetupControls();
                    form1.cmbParent.SelectedValue = Convert.ToInt32(dr["ParentID"]);
                    form1.cmbListViews.SelectedValue = Convert.ToInt32(dr["ViewID"]);
                    form1.cmbSubsystem.SelectedIndex= Convert.ToInt32( dr["SubsystemID"]) -1;
                    form1.txtServiceUrl.Text        = dr["ServiceUrl"].ToString();
                    form1.chkIsGroup.Checked        = Convert.ToBoolean(dr["IsGroup"]);
                    form1.chkSystemReport.Checked   = Convert.ToBoolean(dr["IsSystem"]);
                    form1.chkSetAsDefault.Checked   = Convert.ToBoolean(dr["IsDefault"]);
                    form1.chkQuickReport.Checked    = Convert.ToBoolean(dr["IsDynamic"]);
                    form1.txtEnglish.Text           = dr["EnCaption"].ToString();
                    form1.txtPersian.Text           = dr["FaCaption"].ToString();
                    form1.txtTemplateEn.Text        = dr["EnTemplatePath"].ToString();
                    form1.txtTemplateFa.Text        = dr["FaTemplatePath"].ToString();
                    form1.paramTable                = _parmDtDic[Convert.ToInt32(dr["ReportId"])];
                    form1.RefreshGrid();
                    if (form1.ShowDialog() == DialogResult.OK)
                    {
                        dr["ParentID"       ] = form1.cmbParent.SelectedValue;
                        dr["ViewID"         ] = form1.cmbListViews.SelectedValue;
                        dr["SubsystemID"    ] = form1.cmbSubsystem.SelectedIndex + 1;
                        dr["ServiceUrl"     ] = form1.txtServiceUrl.Text;
                        dr["IsGroup"        ] = form1.chkIsGroup.Checked;
                        dr["IsSystem"       ] = form1.chkSystemReport.Checked;
                        dr["IsDefault"      ] = form1.chkSetAsDefault.Checked;
                        dr["IsDynamic"      ] = form1.chkQuickReport.Checked;
                        dr["EnCaption"      ] = form1.txtEnglish.Text;
                        dr["FaCaption"      ] = form1.txtPersian.Text;
                        dr["EnTemplatePath" ] = form1.txtTemplateEn.Text;
                        dr["FaTemplatePath" ] = form1.txtTemplateFa.Text;
                        _parmDtDic[Convert.ToInt32(dr["ReportId"])] = form1.paramTable;
                        LoadGridView();
                    }
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (grdNewReports.SelectedRows.Count > 0)
            {
                int reportId = Convert.ToInt32(grdNewReports.SelectedRows[0].Cells["ReportID"].Value.ToString());
                DataRow dr = _reportTable.Select(string.Format("ReportID={0}", reportId)).FirstOrDefault();
                dr.Delete();
                _parmDtDic.Remove(Convert.ToInt32(dr["ReportId"]));
                LoadGridView();
            }
        }

        private void Generate_Click(object sender, EventArgs e)
        {
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
            foreach (DataRow drRep in _reportTable.Rows)
            {
                builder.AppendLine("SET IDENTITY_INSERT [Reporting].[Report] ON ");
                builder.AppendLine("INSERT INTO [Reporting].[Report] " +
                     "([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])");
                builder.AppendFormat("    VALUES ({0}, {1}, {2}, {3}, {4}, {5}, '{6}', {7}, {8}, {9}, {10})"
                    , maxReportId + 1
                    , Convert.ToInt32(drRep["ParentID"])
                    , 1
                    , Convert.ToInt32(drRep["ViewID"])
                    , Convert.ToInt32(drRep["SubsystemID"])
                    , "''"
                    , drRep["ServiceUrl"].ToString()
                    , Convert.ToBoolean(drRep["IsGroup"]) == true ? 1 : 0
                    , Convert.ToBoolean(drRep["IsSystem"]) == true ? 1 : 0
                    , Convert.ToBoolean(drRep["IsDefault"]) == true ? 1 : 0
                    , Convert.ToBoolean(drRep["IsDynamic"]) == true ? 1 : 0);
                builder.AppendLine();
                builder.AppendLine("SET IDENTITY_INSERT [Reporting].[Report] OFF ");
                builder.AppendLine();

                builder.AppendLine();
                builder.AppendLine("SET IDENTITY_INSERT [Reporting].[LocalReport] ON ");
                builder.AppendLine("INSERT INTO [Reporting].[LocalReport] " +
                    "([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])");
                builder.AppendFormat("    VALUES ({0}, {1}, {2}, '{3}', {4})"
                    , maxLocalReport++
                    , 1
                    , maxReportId++
                    , drRep["EnCaption"].ToString()
                    , Convert.ToBoolean(drRep["IsDynamic"]) ? "NULL" : GetNullableValue(drRep["EnTemplatePath"].ToString()));
                builder.AppendLine();
                builder.AppendLine("INSERT INTO [Reporting].[LocalReport] " +
                    "([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])");
                builder.AppendFormat("    VALUES ({0}, {1}, {2}, N'{3}', {4})"
                    , maxLocalReport++
                    , 2
                    , maxReportId++
                    , drRep["FaCaption"].ToString()
                    , Convert.ToBoolean(drRep["IsDynamic"]) ? "NULL" : GetNullableValue(drRep["FaTemplatePath"].ToString()));
                builder.AppendLine();
                builder.AppendLine("SET IDENTITY_INSERT [Reporting].[LocalReport] OFF ");
                builder.AppendLine();

                builder.AppendLine();
                builder.AppendLine("SET IDENTITY_INSERT [Reporting].[Parameter] ON ");

                foreach (DataRow dr in _parmDtDic[Convert.ToInt32(drRep["ReportId"])].Rows )
                {
                    builder.AppendLine("INSERT INTO [Reporting].[Parameter] " +
                     "([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])");
                    builder.AppendFormat("    VALUES ({0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')"
                        , paramId++
                        , maxReportId + 1
                        , dr["Name"].ToString()
                        , dr["FieldName"].ToString()
                        , dr["Operator"].ToString()
                        , dr["DataType"].ToString()
                        , dr["ControlType"].ToString()
                        , dr["CaptionKey"].ToString()
                        , dr["CaptionKey"].ToString());
                    builder.AppendLine();
                }

                builder.AppendLine("SET IDENTITY_INSERT [Reporting].[Parameter] OFF ");
                builder.AppendLine();
            }

            File.AppendAllText(_TadbirSysUpdateScript, builder.ToString());

            MessageBox.Show("The script was generated.");
            DialogResult = DialogResult.OK;
            Close();
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

        private string GetSysConnectionString()
        {
            string path = @"..\..\src\Framework\SPPC.Tadbir.Web.Api\appsettings.Development.json";
            var appSettings = JsonHelper.To<AppSettingsModel>(File.ReadAllText(path));
            return appSettings.ConnectionStrings.TadbirSysApi;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
        private Version GetSolutionVersion()
        {
            var assemblyVersion = GetType().Assembly.GetName().Version;
            return new Version(assemblyVersion.ToString());
        }

        private string _sysConnection;
        private DataTable _reportTable = new DataTable("ReportTable");
        private Dictionary<int,DataTable> _parmDtDic = new Dictionary<int, DataTable>();
        private const string _TadbirSysUpdateScript = @"..\..\res\TadbirSys_UpdateDbObjects.sql";
    }
}
