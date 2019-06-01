using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            string src = @"Server=(localdb)\ProjectsV13;Database=COLORAN;Trusted_Connection=True;MultipleActiveResultSets=true";
            string target = @"Server=(localdb)\ProjectsV13;Database=ColoranNG;Trusted_Connection=True;MultipleActiveResultSets=true";

            try
            {
                var repo = new XferRepository(src, target);
                repo.XferFiscalPeriods();
                repo.XferCurrencies();
                repo.XferAccountGroups();
                repo.XferAccounts();
                repo.XferDetailAccounts();
                repo.XferCostCenters();
                repo.XferProjects();
                repo.XferAccountRelations();
                repo.XferVouchers();
                repo.XferVoucherLines();
                MessageBox.Show("Transfer completed successfully!", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}
