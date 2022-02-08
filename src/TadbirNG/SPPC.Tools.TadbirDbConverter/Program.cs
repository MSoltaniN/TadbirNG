using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Helpers;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Security;

namespace SPPC.Tools.TadbirDbConverter
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //ExportTaxCurrencies();
            TestSessionProvider();
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }

        static void ExportTaxCurrencies()
        {
            try
            {
                var path = @"..\..\..\src\TadbirNG\SPPC.Tadbir.Web.Api\wwwroot\static\tax-currencies.json";
                var conn = "Server=BE-LAPTOP;Database=NGTadbir;User ID=NgTadbirUser;Password=Demo1234;Trusted_Connection=False";
                var dal = new SqlDataLayer(conn);
                var result = dal.Query("SELECT TaxCurrencyID, Code, Name FROM [Finance].[TaxCurrency]");
                var allItems = new List<TaxCurrency>();
                allItems.AddRange(result.Rows
                    .Cast<DataRow>()
                    .Select(row => new TaxCurrency()
                    {
                        Id = Int32.Parse(row["TaxCurrencyID"].ToString()),
                        Code = Int32.Parse(row["Code"].ToString()),
                        Name = row["Name"].ToString()
                    }));
                File.WriteAllText(path, JsonHelper.From(allItems, true, new string[] { "RowGuid", "ModifiedDate" }));
                MessageBox.Show("Tax currencies successfully exported!");
            }
            catch (Exception ex)
            {
                var errorBuilder = new StringBuilder(ex.Message);
                var currEx = ex;
                while (currEx.InnerException != null)
                {
                    errorBuilder.AppendLine(currEx.InnerException.Message);
                    currEx = currEx.InnerException;
                }

                MessageBox.Show("Shit happened, dude!" + Environment.NewLine + errorBuilder.ToString());
            }
        }

        static void TestSessionProvider()
        {
            int id = 1;
            var sessionProvider = new SessionProvider(
                new CryptoService(
                    new CertificateManager()));
            foreach (var userAgent in _userAgents)
            {
                var session = sessionProvider.GetSession(userAgent, "N/A");
                session.Id = id++;
                Debug.WriteLine(JsonHelper.From(session));
            }
        }

        private static readonly string[] _userAgents = new string[]
        {
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:95.0) Gecko/20100101 Firefox/95.0",
            "Mozilla/5.0 (Linux; Android 11; SM-A715F) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.104 Mobile Safari/537.36",
            "Mozilla/5.0 (iPad; CPU OS 9_3_5 like Mac OS X) AppleWebKit/601.1.46 (KHTML, like Gecko) Version/9.0 Mobile/13G36 Safari/601.1",
            "Mozilla/5.0 (Linux; Android 7.0; SM-P585) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.104 Mobile Safari/537.36",
            "Mozilla/5.0 (iPad; CPU OS 12_5_4 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/12.1.2 Mobile/15E148 Safari/604.1",
            "Mozilla/5.0 (iPhone; CPU iPhone OS 14_8 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) GSA/161.1.375499464 Mobile/15E148 Safari/604.1",
            "Mozilla/5.0 (iPhone; CPU iPhone OS 14_8 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/14.1.2 Mobile/15E148 Safari/604.1"
        };
    }
}
