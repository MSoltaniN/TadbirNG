﻿using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using BabakSoft.Platform.Data;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Tools.SystemDesigner.Models;

namespace SPPC.Tadbir.Tools.SystemDesigner.Wizards.ViewWizard
{
    public partial class ViewWizardForm : Form
    {
        public ViewWizardForm()
        {
            InitializeComponent();
            WizardModel = new ViewWizardModel();
        }

        public ViewWizardModel WizardModel { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadViews();
            LoadFirstPage();
        }

        private void LoadViews()
        {
            _sysConnection = GetSysConnectionString();
            var dal = new SqlDataLayer(_sysConnection, ProviderType.SqlClient);
            WizardModel.ViewItems = dal.Query("SELECT ViewID, Name FROM [Metadata].[View]");
        }

        private void Back_Click(object sender, EventArgs e)
        {
            if (_currentStepNo == 2)
            {
                _currentStepNo--;
                LoadFirstPage();
            }
            else if (_currentStepNo == 3)
            {
                _currentStepNo--;
                LoadSecondPage();
            }
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (_currentStepNo == 1)
            {
                _currentStepNo++;
                LoadSecondPage();
            }
            else if (_currentStepNo == 2)
            {
                _currentStepNo++;
                LoadThirdPage();
            }
            else
            {
                GenerateScript();
                MessageBox.Show("The script was generated.");
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void LoadFirstPage()
        {
            var page = new BrowseViewsPage()
            {
                Dock = DockStyle.Fill,
                ViewItems = WizardModel.ViewItems,
                SysConnection = _sysConnection
            };
            pnlPage.Controls.Clear();
            pnlPage.Controls.Add(page);
            SetCurrentStepInfo(page.Info);
            btnNext.Text = "Add View";
        }

        private void LoadSecondPage()
        {
            var page = new EditViewPage()
            {
                Dock = DockStyle.Fill,
                View = WizardModel.View,
                Columns = WizardModel.Columns
            };
            pnlPage.Controls.Clear();
            pnlPage.Controls.Add(page);
            SetCurrentStepInfo(page.Info);
            btnNext.Text = "Next";
        }

        private void LoadThirdPage()
        {
            var page = new EditColumnsPage()
            {
                Dock = DockStyle.Fill,
                Columns = WizardModel.Columns,
                ViewName = WizardModel.View.Name
            };
            pnlPage.Controls.Clear();
            pnlPage.Controls.Add(page);
            SetCurrentStepInfo(page.Info);
            btnNext.Text = "Finish";
        }

        private void SetCurrentStepInfo(string task)
        {
            lblStepInfo.Text = String.Format("Step {0} : {1}", _currentStepNo, task);
            btnBack.Enabled = (_currentStepNo > 1);
        }

        private void GenerateScript()
        {
            var sysConnection = GetSysConnectionString();
            var dal = new SqlDataLayer(sysConnection, ProviderType.SqlClient);
            int maxViewId = Convert.ToInt32(dal.QueryScalar("SELECT MAX([ViewID]) FROM [Metadata].[View]"));
            int maxColumnId = Convert.ToInt32( dal.QueryScalar("SELECT MAX([ColumnID]) FROM [Metadata].[Column]"));
            var view = WizardModel.View;
            var columns = WizardModel.Columns;
            var builder = new StringBuilder();
            var result = GetDatabaseVersion(sysConnection);
            
            builder.AppendFormat("--{0} \n", result.ToString());
            builder.AppendLine("SET IDENTITY_INSERT [Metadata].[View] ON ");
            builder.AppendFormat("INSERT INTO [Metadata].[View] " +
                "([ViewID], [Name], [IsHierarchy], [IsCartableIntegrated], [EntityType], [FetchUrl], [SearchUrl]) " +
                "VALUES ({0}, '{1}', {2}, {3}, {4}, '{5}', {6}) \n"
                , maxViewId+1
                , view.Name
                , view.IsHierarchy == true ? 1 : 0
                , view.IsCartableIntegrated == true ? 1 : 0 
                , view.Entitytype == "(not set)" ? "NULL" : "'" + view.Entitytype + "'"
                , view.FetchUrl
                , view.SearchUrl == "" ? "NULL" : "'" + view.SearchUrl + "'");
            builder.AppendLine("SET IDENTITY_INSERT[Metadata].[View] OFF ");
            builder.AppendLine();
            builder.AppendLine("SET IDENTITY_INSERT[Metadata].[Column] ON ");
            short rowCount = 0;
            foreach (var item in columns)
                builder.AppendFormat("INSERT INTO [Metadata].[Column]" +
                    "([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType]," +
                    " [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], " +
                    "[AllowFiltering], [Visibility], [DisplayIndex], [Expression]) \n" +
                    "VALUES ({0}, {1}, '{2}', {3}, {4}, '{5}', '{6}', '{7}', {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16}) \n"
                    , maxColumnId + 1
                    , maxViewId + 1
                    , item.Name
                    , item.GroupName == "" ? "NULL" : "'" + item.GroupName + "'"
                    , item.Type == "(not set)" ? "NULL" : "'" + item.Type + "'"
                    , item.DotNetType
                    , item.StorageType
                    , item.ScriptType
                    , item.Length
                    , item.MinLength
                    , item.IsFixedLength == true ? 1 : 0
                    , item.IsNullable == true ? 1 : 0
                    , item.AllowSorting == true ? 1 : 0
                    , item.AllowFiltering == true ? 1 : 0
                    , item.Visibility == "(not set)" ? "NULL" : "'"+item.Visibility+"'"
                    , item.DisplayIndex = (short)(item.Visibility != "AlwaysHidden" ? rowCount++ : -1)
                    , item.Expression == "" ? "NULL" : "'" + item.Expression + "'");
            
            builder.AppendLine("SET IDENTITY_INSERT [Metadata].[Column] OFF ");
            builder.AppendLine();

            File.AppendAllText(_tempScript, builder.ToString());
        }

        private string GetSysConnectionString()
        {
            string path = @"..\..\src\Framework\SPPC.Tadbir.Web.Api\appsettings.Development.json";
            var appSettings = JsonHelper.To<AppSettingsModel>(File.ReadAllText(path));
            return appSettings.ConnectionStrings.TadbirSysApi;
        }
        private Version GetDatabaseVersion(string connection)
        {
            var dal = new SqlDataLayer(connection, ProviderType.SqlClient);
            var result = dal.QueryScalar("SELECT Number FROM [Core].[Version]");
            return new Version(result.ToString());
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private int _currentStepNo = 1;
        private string _sysConnection;
        private const string _tempScript = "Update.sql";
    }
}
