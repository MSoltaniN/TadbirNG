using System;
using System.Windows.Forms;
using SPPC.Tools.Model;

namespace SPPC.Tadbir.Tools.SystemDesigner.Wizards.NewCrudEntityWizard
{
    public partial class NewCrudEntityWizardForm : Form
    {
        public NewCrudEntityWizardForm()
        {
            InitializeComponent();
            WizardModel = new CrudWizardModel();
            WizardModel.Options.Controller.HasCrudMethods = false;
            WizardModel.Options.Controller.HasCrudImpl = false;
        }

        public CrudWizardModel WizardModel { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadFirstPage();
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
            else if(_currentStepNo == 2)
            {
                if (WizardModel.Options.HasPermissionEnum)
                {
                    _currentStepNo++;
                    LoadThirdPage();
                }
                else
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void LoadFirstPage()
        {
            var page = new EntityInfoPage() { Dock = DockStyle.Fill, EntityInfo = WizardModel.EntityInfo };
            pnlPage.Controls.Clear();
            pnlPage.Controls.Add(page);
            SetCurrentStepInfo(page.Info);
            btnNext.Text = "Next";
        }

        private void LoadSecondPage()
        {
            var page = new WizardOptionsPage() { Dock = DockStyle.Fill, Options = WizardModel.Options };
            page.PermissionsChecked += Page_PermissionsChecked;
            pnlPage.Controls.Clear();
            pnlPage.Controls.Add(page);
            SetCurrentStepInfo(page.Info);
            btnNext.Text = WizardModel.Options.HasPermissionEnum ? "Next" : "Finish";
        }

        private void Page_PermissionsChecked(object sender, PermissionCheckedEventArgs e)
        {
            btnNext.Text = e.Checked ? "Next" : "Finish";
        }

        private void LoadThirdPage()
        {
            var page = new EntityActionsPage() { Dock = DockStyle.Fill, Actions = WizardModel.EntityActions };
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

        private int _currentStepNo = 1;
    }
}
