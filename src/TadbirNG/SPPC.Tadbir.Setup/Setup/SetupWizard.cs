using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SPPC.Tadbir.Setup
{
    public partial class SetupWizard : Form
    {
        public SetupWizard()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupWizardDriver();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private static SetupWizardModel GetDefaultOptions()
        {
            return new SetupWizardModel()
            {
                InstallPath = @"C:\SPPC\TadbirNG",
                CreateShortcut = true,
                IsGlobal = true,
                IsLocal = false
            };
        }

        private void SetupWizardDriver()
        {
            var model = GetDefaultOptions();
            _driver = new WizardDriver()
            {
                PreviousButton = btnPrevious,
                NextButton = btnNext,
                PageContainer = pnlPage
            };
            _driver.Pages.Add(new SettingsPage() { WizardModel = model });
            _driver.Pages.Add(new AppAccessPage() { WizardModel = model });
            _driver.Pages.Add(new DbAccessPage() { WizardModel = model });
            _driver.Pages.Add(new ProgressPage() { WizardModel = model });
            _driver.InitWizard();
        }

        private WizardDriver _driver;
    }
}
