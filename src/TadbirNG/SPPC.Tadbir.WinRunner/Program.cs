﻿using System;
using System.Windows.Forms;
using SPPC.Tadbir.WinRunner.Utility;

namespace SPPC.Tadbir.WinRunner
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

            if (!InstallerUtility.IsDockerEngineRunning())
            {
                MessageBox.Show("لطفاً پیش از اجرای این برنامه، ابتدا برنامه داکر دسکتاپ را اجرا کنید و وارد حساب کاربری سازمان شوید.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                Application.Exit();
                return;
            }

            if (!InstallerUtility.VerifyChecksums())
            {
                MessageBox.Show("برنامه به دلیل دستکاری احتمالی فایل ها قابل اجرا نیست. لطفاً نسخه سالم برنامه را دوباره تهیه نمایید.",
                    "خطا", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.RtlReading);
                Application.Exit();
                return;
            }

            Application.Run(new MainWindow());
        }
    }
}
