using System;
using System.Windows.Forms;

namespace SPPC.Tadbir.Setup
{
    public partial class ProgressPage : UserControl, ISetupWizardPage, ISetupProgressPage
    {
        public ProgressPage()
        {
            InitializeComponent();
        }

        public SetupWizardModel WizardModel { get; set; }

        public Func<bool> PageValidator
        {
            get { return null; }
        }

        public Label ElapsedLabel => lblElapsed;

        public Label ProgressLabel => lblProgress;

        public Label StatusLabel => lblStatus;

        public ProgressBar ProgressBar => progress;

        public TextBox ConsoleTextBox => txtConsole;
    }
}
