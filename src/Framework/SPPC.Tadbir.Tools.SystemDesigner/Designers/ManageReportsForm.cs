﻿using SPPC.Framework.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BabakSoft.Platform.Data;
using SPPC.Tools.Model;

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
                SysConnection = _sysConnection ,
               
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
                _paramDictionary.Add(Convert.ToInt32(dr["ReportId"]), form.Parameters);
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
                    var designer = new ReportDesignerForm()
                    {
                        SysConnection = _sysConnection
                    };
                    designer.SetupControls();
                    designer.cmbParent.SelectedValue = Convert.ToInt32(dr["ParentID"]);
                    designer.cmbListViews.SelectedValue = Convert.ToInt32(dr["ViewID"]);
                    designer.cmbSubsystem.SelectedIndex= Convert.ToInt32( dr["SubsystemID"]) -1;
                    designer.txtServiceUrl.Text        = dr["ServiceUrl"].ToString();
                    designer.chkIsGroup.Checked        = Convert.ToBoolean(dr["IsGroup"]);
                    designer.chkSystemReport.Checked   = Convert.ToBoolean(dr["IsSystem"]);
                    designer.chkSetAsDefault.Checked   = Convert.ToBoolean(dr["IsDefault"]);
                    designer.chkQuickReport.Checked    = Convert.ToBoolean(dr["IsDynamic"]);
                    designer.txtEnglish.Text           = dr["EnCaption"].ToString();
                    designer.txtPersian.Text           = dr["FaCaption"].ToString();
                    designer.txtTemplateEn.Text        = dr["EnTemplatePath"].ToString();
                    designer.txtTemplateFa.Text        = dr["FaTemplatePath"].ToString();
                    designer.Parameters                = _paramDictionary[Convert.ToInt32(dr["ReportId"])];
                    designer.RefreshGrid();
                    if (designer.ShowDialog() == DialogResult.OK)
                    {
                        dr["ParentID"       ] = designer.cmbParent.SelectedValue;
                        dr["ViewID"         ] = designer.cmbListViews.SelectedValue;
                        dr["SubsystemID"    ] = designer.cmbSubsystem.SelectedIndex + 1;
                        dr["ServiceUrl"     ] = designer.txtServiceUrl.Text;
                        dr["IsGroup"        ] = designer.chkIsGroup.Checked;
                        dr["IsSystem"       ] = designer.chkSystemReport.Checked;
                        dr["IsDefault"      ] = designer.chkSetAsDefault.Checked;
                        dr["IsDynamic"      ] = designer.chkQuickReport.Checked;
                        dr["EnCaption"      ] = designer.txtEnglish.Text;
                        dr["FaCaption"      ] = designer.txtPersian.Text;
                        dr["EnTemplatePath" ] = designer.txtTemplateEn.Text;
                        dr["FaTemplatePath" ] = designer.txtTemplateFa.Text;
                        _paramDictionary[Convert.ToInt32(dr["ReportId"])] = designer.Parameters;
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
                _paramDictionary.Remove(Convert.ToInt32(dr["ReportId"]));
                LoadGridView();
            }
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            var sysConnection = GetSysConnectionString();
            var dal = new SqlDataLayer(sysConnection, ProviderType.SqlClient);
            int maxReportId = Convert.ToInt32(dal.QueryScalar("SELECT MAX([ReportID]) FROM [Reporting].[Report]"));
            int maxLocalReport = Convert.ToInt32(dal.QueryScalar("SELECT MAX([LocalReportID]) FROM [Reporting].[LocalReport]"));
            int maxParamId = Convert.ToInt32(dal.QueryScalar("SELECT MAX([ParamID]) FROM [Reporting].[Parameter]"));
            var builder = new StringBuilder();
            var solutionVersion = GetSolutionVersion();

            builder.AppendLine();
            builder.AppendFormat("-- {0}", solutionVersion);
            builder.AppendLine();

            builder.AppendLine("SET IDENTITY_INSERT [Reporting].[Report] ON");
            int reportId = maxReportId + 1;
            foreach (DataRow row in _reportTable.Rows)
            {
                builder.AppendLine("INSERT INTO [Reporting].[Report] " +
                     "([ReportID], [ParentID], [CreatedByID], [ViewID], [SubsystemID], [Code], [ServiceUrl], [IsGroup], [IsSystem], [IsDefault], [IsDynamic])");
                builder.AppendFormat("    VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10})"
                    , reportId++
                    , Convert.ToInt32(row["ParentID"])
                    , 1
                    , Convert.ToInt32(row["ViewID"])
                    , Convert.ToInt32(row["SubsystemID"])
                    , "''"
                    , GetNullableValue(row["ServiceUrl"].ToString())
                    , Convert.ToBoolean(row["IsGroup"]) == true ? 1 : 0
                    , Convert.ToBoolean(row["IsSystem"]) == true ? 1 : 0
                    , Convert.ToBoolean(row["IsDefault"]) == true ? 1 : 0
                    , Convert.ToBoolean(row["IsDynamic"]) == true ? 1 : 0);
                builder.AppendLine();
            }

            builder.AppendLine("SET IDENTITY_INSERT [Reporting].[Report] OFF");
            builder.AppendLine();

            builder.AppendLine("SET IDENTITY_INSERT [Reporting].[LocalReport] ON");
            int localReportId = maxLocalReport + 1;
            reportId = maxReportId + 1;
            foreach (DataRow row in _reportTable.Rows)
            {
                builder.AppendLine("INSERT INTO [Reporting].[LocalReport] " +
                    "([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])");
                builder.AppendFormat("    VALUES ({0}, {1}, {2}, '{3}', {4})"
                    , localReportId++
                    , 1
                    , reportId
                    , row["EnCaption"].ToString()
                    , Convert.ToBoolean(row["IsDynamic"]) ? "NULL" : GetNullableValue(row["EnTemplatePath"].ToString()));
                builder.AppendLine();
                builder.AppendLine("INSERT INTO [Reporting].[LocalReport] " +
                    "([LocalReportID], [LocaleID], [ReportID], [Caption], [Template])");
                builder.AppendFormat("    VALUES ({0}, {1}, {2}, N'{3}', {4})"
                    , localReportId++
                    , 2
                    , reportId++
                    , row["FaCaption"].ToString()
                    , Convert.ToBoolean(row["IsDynamic"]) ? "NULL" : GetNullableValue(row["FaTemplatePath"].ToString()));
                builder.AppendLine();
            }

            builder.AppendLine("SET IDENTITY_INSERT [Reporting].[LocalReport] OFF");
            builder.AppendLine();

            builder.AppendLine("SET IDENTITY_INSERT [Reporting].[Parameter] ON");
            int paramId = maxParamId + 1;
            var sortedKeys = _paramDictionary.Keys.OrderBy(key => key);
            foreach (int key in sortedKeys)
            {
                var parameters = _paramDictionary[key];
                foreach (DataRow row in parameters.Rows)
                {
                    builder.AppendLine("INSERT INTO [Reporting].[Parameter] " +
                     "([ParamID], [ReportID], [Name], [FieldName], [Operator], [DataType], [ControlType], [CaptionKey], [DescriptionKey])");
                    builder.AppendFormat("    VALUES ({0}, {1}, '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')"
                        , paramId++
                        , maxReportId + key
                        , row["Name"].ToString()
                        , row["FieldName"].ToString()
                        , row["Operator"].ToString()
                        , row["DataType"].ToString()
                        , row["ControlType"].ToString()
                        , row["CaptionKey"].ToString()
                        , row["CaptionKey"].ToString());
                    builder.AppendLine();
                }
            }

            builder.AppendLine("SET IDENTITY_INSERT [Reporting].[Parameter] OFF");
            builder.AppendLine();

            File.AppendAllText(_TadbirSysUpdateScript, builder.ToString());

            MessageBox.Show("The script was generated.");
            DialogResult = DialogResult.OK;
            Close();
        }

        private string GetNullableValue(string nullable)
        {
            return String.IsNullOrEmpty(nullable)
                ? "NULL"
                : String.Format("N'{0}'", nullable);
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
            return new Version(assemblyVersion.Major, assemblyVersion.Minor, assemblyVersion.Build);
        }

        private string _sysConnection;
        private DataTable _reportTable = new DataTable("ReportTable");
        private Dictionary<int, DataTable> _paramDictionary = new Dictionary<int, DataTable>();
        private const string _TadbirSysUpdateScript = @"..\..\res\TadbirSys_UpdateDbObjects.sql";
    }
}
