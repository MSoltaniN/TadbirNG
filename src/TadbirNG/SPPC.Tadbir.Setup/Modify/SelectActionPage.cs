using System;
using System.Windows.Forms;
using SPPC.Tadbir.Utility;

namespace SPPC.Tadbir.Setup
{
    public partial class SelectActionPage : UserControl, ISetupWizardPage
    {
        public SelectActionPage()
        {
            InitializeComponent();
        }

        public bool IsModifying => radModify.Checked;

        public SetupWizardModel WizardModel { get; set; }

        public Func<bool> PageValidator => ConfirmAppRemove;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            radRemove.Checked = true;
        }

        private bool ConfirmAppRemove()
        {
            var result = MessageBox.Show(
                "آیا از حذف برنامه اطمینان دارید؟", "دریافت تأیید از کاربر",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2,
                MessageBoxOptions.RtlReading);
            bool confirmed = result == DialogResult.Yes;
            if (confirmed && SetupUtility.IsAppRunning())
            {
                MessageBox.Show(
                    "برنامه تدبیر در حال اجرا است. لطفاً برنامه را متوقف کرده و دوباره تلاش کنید.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                confirmed = false;
            }

            return confirmed;
        }
    }
}
