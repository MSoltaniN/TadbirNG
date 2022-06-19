using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SPPC.Framework.Persistence;
using SPPC.Tools.Model;
using SPPC.Tools.Utility;

namespace SPPC.Tools.SystemDesigner.Wizards.ViewWizard
{
    public partial class ViewWizardForm : Form
    {
        public ViewWizardForm()
        {
            InitializeComponent();
            WizardModel = new ViewWizardModel();
            _sysConnection = DbConnections.SystemConnection;
        }

        public ViewWizardModel WizardModel { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ActiveForm.Cursor = Cursors.WaitCursor;
            LoadViews();
            LoadFirstPage();
            ActiveForm.Cursor = Cursors.Default;
        }

        private void LoadViews()
        {
            var dal = new SqlDataLayer(_sysConnection);
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

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
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
            var dal = new SqlDataLayer(_sysConnection);
            int maxViewId = Convert.ToInt32(dal.QueryScalar("SELECT MAX([ViewID]) FROM [Metadata].[View]"));
            int maxColumnId = Convert.ToInt32( dal.QueryScalar("SELECT MAX([ColumnID]) FROM [Metadata].[Column]"));
            var view = WizardModel.View;
            var columns = WizardModel.Columns;
            var builder = new StringBuilder();
            var solutionVersion = GetSolutionVersion();

            int columnId = maxColumnId + 1;
            builder.AppendLine();
            builder.AppendFormat("-- {0}", solutionVersion);
            builder.AppendLine();
            builder.AppendLine("SET IDENTITY_INSERT [Metadata].[View] ON ");
            builder.AppendLine("INSERT INTO [Metadata].[View] " +
                "([ViewID], [Name], [IsHierarchy], [IsCartableIntegrated], [EntityType], [FetchUrl], [SearchUrl])");
            builder.AppendFormat("    VALUES ({0}, '{1}', {2}, {3}, '{4}', {5}, {6})"
                , maxViewId + 1
                , view.Name
                , view.IsHierarchy == true ? 1 : 0
                , view.IsCartableIntegrated == true ? 1 : 0 
                , view.Entitytype 
                , GetNullableValue(view.FetchUrl)
                , GetNullableValue(view.SearchUrl));
            builder.AppendLine();
            builder.AppendLine("SET IDENTITY_INSERT [Metadata].[View] OFF ");
            builder.AppendLine();
            builder.AppendLine("SET IDENTITY_INSERT [Metadata].[Column] ON ");
            short rowCount = 0;
            foreach (var item in columns)
            {
                builder.AppendLine("INSERT INTO [Metadata].[Column] " +
                    "([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], " +
                    "[ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], " +
                    "[AllowFiltering], [Visibility], [DisplayIndex], [Expression])");
                builder.AppendFormat(
                    "    VALUES ({0}, {1}, '{2}', {3}, {4}, '{5}', '{6}', '{7}', {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16})"
                    , columnId++
                    , maxViewId + 1
                    , item.Name
                    , GetNullableValue(item.GroupName)
                    , GetNullableValue(item.Type, "(not set)")
                    , item.DotNetType
                    , item.StorageType
                    , item.ScriptType
                    , item.Length
                    , item.MinLength
                    , item.IsFixedLength == true ? 1 : 0
                    , item.IsNullable == true ? 1 : 0
                    , item.AllowSorting == true ? 1 : 0
                    , item.AllowFiltering == true ? 1 : 0
                    , GetNullableValue(item.Visibility, "Visible")
                    , item.DisplayIndex = (short)(item.Visibility != "AlwaysHidden" ? rowCount++ : -1)
                    , GetNullableValue(item.Expression));
                builder.AppendLine();
            }

            builder.AppendLine("SET IDENTITY_INSERT [Metadata].[Column] OFF ");
            builder.AppendLine();

            File.AppendAllText(_TadbirSysUpdateScript, builder.ToString());
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

        private Version GetSolutionVersion()
        {
            var assemblyVersion= GetType().Assembly.GetName().Version;
            return new Version(assemblyVersion.ToString(3));
        }

        private int _currentStepNo = 1;
        private readonly string _sysConnection;
        private const string _TadbirSysUpdateScript = @"..\..\..\res\TadbirSys_UpdateDbObjects.sql";
    }
}
