using System;
using System.IO;
using System.Windows.Forms;
using SPPC.Framework.Persistence;
using SPPC.Tools.Model;

namespace SPPC.Tools.SystemDesigner.Wizards.EnvSetupWizard
{
    public partial class EnvSetupWizardForm : Form
    {
        public EnvSetupWizardForm()
        {
            InitializeComponent();
            WizardModel = new EnvSetupWizardModel();
        }

        public EnvSetupWizardModel WizardModel { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadFirstPage();
        }

        private void Next_Click(object sender, EventArgs e)
        {
            if (_currentStepNo == 1)
            {
                if (!ValidateFirstPageData())
                {
                    return;
                }

                _currentStepNo++;
                LoadSecondPage();
            }
            else if (_currentStepNo == 2)
            {
                if (!ValidateSecondPageData())
                {
                    return;
                }

                _currentStepNo++;
                LoadThirdPage();
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
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

        private void ThirdPage_Started(object sender, EventArgs e)
        {
            btnBack.Enabled = false;
        }

        private void ThirdPage_Stopped(object sender, EventArgs e)
        {
            btnBack.Enabled = true;
            btnNext.Enabled = true;
        }

        private void LoadFirstPage()
        {
            var page = new GeneralSettingsPage() { Dock = DockStyle.Fill, WizardModel = WizardModel };
            pnlPage.Controls.Clear();
            pnlPage.Controls.Add(page);
            SetCurrentStepInfo(page.Info);
            btnNext.Text = "Next";
            btnNext.Enabled = true;
        }

        private void LoadSecondPage()
        {
            var page = new DbSettingsPage() { Dock = DockStyle.Fill, WizardModel = WizardModel };
            pnlPage.Controls.Clear();
            pnlPage.Controls.Add(page);
            SetCurrentStepInfo(page.Info);
            btnNext.Text = "Next";
            btnNext.Enabled = true;
        }

        private void LoadThirdPage()
        {
            var page = new ActionOutputPage() { Dock = DockStyle.Fill, WizardModel = WizardModel };
            page.Started += ThirdPage_Started;
            page.Stopped += ThirdPage_Stopped;
            pnlPage.Controls.Clear();
            pnlPage.Controls.Add(page);
            SetCurrentStepInfo(page.Info);
            btnNext.Enabled = false;
            btnNext.Text = "Finish";
        }

        private bool ValidateFirstPageData()
        {
            if (!EnsureRootFolderIsCorrect())
            {
                return false;
            }

            if (!EnsureLicenseeInfoIsCorrect())
            {
                return false;
            }

            if (!EnsureWinLoginInfoIsCorrect())
            {
                return false;
            }

            return true;
        }

        private bool ValidateSecondPageData()
        {
            if (!EnsureConnectionInfoIsValid())
            {
                return false;
            }

            return true;
        }

        private void SetCurrentStepInfo(string task)
        {
            lblStepInfo.Text = String.Format("Step {0} : {1}", _currentStepNo, task);
            btnBack.Enabled = _currentStepNo > 1;
        }

        private bool EnsureRootFolderIsCorrect()
        {
            bool validated = true;
            if (!Directory.Exists(WizardModel.RootFolder))
            {
                MessageBox.Show(this, "Specified folder does not exist.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                validated = false;
            }
            else
            {
                var solutionFolder = Path.Combine(WizardModel.RootFolder, "src", "TadbirNG");
                if (!Directory.Exists(solutionFolder))
                {
                    MessageBox.Show(this, "You must specify the root folder of your local git repository (containing src, doc, res, etc).",
                        "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    validated = false;
                }
            }

            return validated;
        }

        private bool EnsureLicenseeInfoIsCorrect()
        {
            bool validated = true;
            if (String.IsNullOrWhiteSpace(WizardModel.LicenseeFirstName)
                || String.IsNullOrWhiteSpace(WizardModel.LicenseeLastName))
            {
                MessageBox.Show(this, "You must enter a first name and last name for your development license.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                validated = false;
            }

            return validated;
        }

        private bool EnsureWinLoginInfoIsCorrect()
        {
            bool validated = true;
            if (String.IsNullOrWhiteSpace(WizardModel.WinUserName)
                || String.IsNullOrWhiteSpace(WizardModel.WinPassword))
            {
                MessageBox.Show(this, @"Your local Windows login credentials are required for activation.
This information stays on your local machine and is not committed to source control.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                validated = false;
            }

            return validated;
        }

        private bool EnsureConnectionInfoIsValid()
        {
            bool validated = true;
            if (String.IsNullOrWhiteSpace(WizardModel.DbServerName)
                || String.IsNullOrWhiteSpace(WizardModel.DbServerName)
                || String.IsNullOrWhiteSpace(WizardModel.DbServerName))
            {
                MessageBox.Show(this, "Creating TadbirNG databases requires information about your local database server.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                validated = false;
            }
            else
            {
                string connection = String.Format(_csTemplate,
                    WizardModel.DbServerName, WizardModel.DbUserName, WizardModel.DbPassword);
                var dal = new SqlDataLayer(connection);
                try
                {
                    Cursor = Cursors.WaitCursor;
                    var result = dal.QueryScalar("SELECT COUNT(*) FROM sys.databases");
                }
                catch (Exception ex)
                {
                    string message = String.Format(
                        @"Could not connect to server using provided login information.

Error info :
{0}", ex.Message);
                    MessageBox.Show(this, message, "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    validated = false;
                }

                Cursor = Cursors.Default;
            }

            return validated;
        }

        private int _currentStepNo = 1;
        private static readonly string _csTemplate =
            "Server={0};Database=master;User ID={1};Password={2};Trusted_Connection=False";
    }
}
