using System;
using System.Configuration;
using System.Windows.Forms;

namespace SPPC.Tadbir.Tools.SystemDesigner
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DoXferTadbirDb();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }

        private static void DoXferTadbirDb()
        {
            string src = ConfigurationManager.ConnectionStrings["SourceDb"].ConnectionString;
            string target = ConfigurationManager.ConnectionStrings["TargetDb"].ConnectionString;

            int fpId = GetFiscalPeriodId();
            try
            {
                var repo = new XferRepository(src, target);
                repo.XferFiscalPeriods();
                repo.XferCurrencies();
                repo.XferAccountGroups();
                repo.XferAccounts(fpId);
                repo.XferDetailAccounts(fpId);
                repo.XferCostCenters(fpId);
                repo.XferProjects(fpId);
                repo.XferAccountRelations(fpId);
                repo.XferVouchers(fpId);
                repo.XferVoucherLines(fpId);
                MessageBox.Show("Transfer completed successfully!", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private static int GetFiscalPeriodId()
        {
            string fpIdConfig = ConfigurationManager.AppSettings["FPId"];
            return !String.IsNullOrEmpty(fpIdConfig) ? Int32.Parse(fpIdConfig) : 1;
        }
    }
}
