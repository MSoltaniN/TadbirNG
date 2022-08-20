using System;
using System.Windows.Forms;
using SPPC.Tools.Utility;

namespace SPPC.Tadbir.Setup
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool isInstalled = SetupUtility.IsAppRegistered();
            if (!DockerUtility.IsDockerEngineRunning())
            {
                MessageBox.Show("لطفاً پیش از اجرای این برنامه، ابتدا برنامه داکر دسکتاپ را اجرا کنید.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                Application.Exit();
                return;
            }

            Form startForm = isInstalled
                ? new ModifyWizard()
                : new SetupWizard();
            Application.Run(startForm);
        }
    }
}
