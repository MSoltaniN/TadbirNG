using System;
using System.IO;
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

        void GenerateScript()
        {

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

        private int _currentStepNo = 1;
        private string _sysConnection;
    }
}
