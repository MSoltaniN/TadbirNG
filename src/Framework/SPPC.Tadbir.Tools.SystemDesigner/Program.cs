using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Windows.Forms;
using BabakSoft.Platform.Data;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Tools.SystemDesigner.Models;
using SPPC.Tadbir.ViewModel.Metadata;
using HAP = HtmlAgilityPack;

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
            //BuildCurrencyDatabase();
            //OrganizeListMetadata();
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

        #region Local Currency Database Builder

        private static void BuildCurrencyDatabase()
        {
            string url = "https://en.wikipedia.org/wiki/List_of_circulating_currencies";
            var currencies = ScrapeWikipediaPage(url);
            File.WriteAllText("currencies.json", JsonHelper.From(currencies));
            Console.WriteLine("Currency database is now ready.");
            Console.ReadLine();
        }

        private static void AddResources()
        {
            var currencies = JsonHelper.To<List<CurrencyInfo>>(File.ReadAllText("currencies.json"));
            AddOtherResources(currencies, "AppStrings.en.resx");
            AddOtherResources(currencies, "AppStrings.resx");
            Console.WriteLine("Currency and minor unit resources added.");
            Console.ReadLine();
        }

        private static List<CurrencyInfo> ScrapeWikipediaPage(string url)
        {
            var currencies = new List<CurrencyInfo>();
            var html = HtmlHelper.LoadHtml(url);
            var body = GetBody(html);
            var tableRows = GetTableRows(body);
            foreach (var row in tableRows)
            {
                var currency = GetCurrencyFromRow(row);
                if (currency != null)
                {
                    currencies.Add(currency);
                }
            }

            return currencies;
        }

        private static HAP::HtmlNode GetBody(HAP::HtmlDocument html)
        {
            var root = html.DocumentNode.ChildNodes
                .Where(n => n.Name == "html")
                .FirstOrDefault();
            var body = root.ChildNodes
                .Where(n => n.Name == "body")
                .FirstOrDefault();
            return body;
        }

        private static List<HAP::HtmlNode> GetTableRows(HAP::HtmlNode body)
        {
            string path = "content/bodyContent/mw-content-text";
            var container = body;
            foreach (var part in path.Split('/'))
            {
                container = container.ChildNodes
                    .Where(n => n.Name == "div" && n.Id == part)
                    .First();
            }

            container = container.ChildNodes
                .Where(n => n.Name == "div" && n.ChildAttributes("class") != null)
                .First();
            var table = container.ChildNodes
                .Where(n => n.Name == "table" && n.Attributes["class"].Value.Contains("sortable"))
                .FirstOrDefault();
            return table.ChildNodes[1].ChildNodes
                .Where(n => n.Name == "tr")
                .Cast<HAP::HtmlNode>()
                .Skip(1)            // Skip header row
                .ToList();
        }

        private static IList<HAP::HtmlNode> GetNonTextChildren(HAP::HtmlNode node)
        {
            return node.ChildNodes
                .Where(n => n.Name != "#text")
                .ToList();
        }

        private static CurrencyInfo GetCurrencyFromRow(HAP::HtmlNode row)
        {
            CurrencyInfo info = null;
            if (IsCurrencyRow(row))
            {
                info = new CurrencyInfo();
                var children = GetNonTextChildren(row);
                info.Country = children[0].ChildNodes
                    .Where(n => n.Name == "a")
                    .First()
                    .InnerText;
                info.Currency.Name = children[1].ChildNodes[0]
                    .InnerText;
                info.Currency.NameKey = "CUnit_" + ToIdentifier(info.Currency.Name);
                info.Currency.Code = children[3].InnerText.Trim();
                info.Currency.MinorUnit = children[4].ChildNodes[0]
                    .InnerText;
                info.Currency.MinorUnitKey = "CMUnit_" + ToIdentifier(info.Currency.MinorUnit);
                string decimalPlaces = children[5].InnerText.Trim();
                if (decimalPlaces != "(none)")
                {
                    info.Currency.DecimalCount = (int)Math.Log10(Double.Parse(decimalPlaces));
                }
            }

            return info;
        }

        private static bool IsCurrencyRow(HAP::HtmlNode row)
        {
            int cellCount = row.ChildNodes
                .Where(n => n.Name == "td")
                .Count();
            var style = row.Attributes
                .Where(att => att.Name == "style")
                .FirstOrDefault();
            var children = GetNonTextChildren(row);
            var rowSpan = children[0].Attributes
                .Where(att => att.Name == "rowspan")
                .FirstOrDefault();
            int row_span = (rowSpan != null) ? Int32.Parse(rowSpan.Value) : 1;
            return (cellCount == 6 && style == null)
                || (style != null && rowSpan == null && cellCount == 6);
        }

        private static void AddCountryResources(List<CurrencyInfo> currencies, string resx)
        {
            var existing = new List<DictionaryEntry>();
            using (var reader = new ResXResourceReader(resx))
            {
                foreach (DictionaryEntry entry in reader)
                {
                    existing.Add(new DictionaryEntry(entry.Key, entry.Value));
                }
            }

            using (var writer = new ResXResourceWriter(resx))
            {
                foreach (var entry in existing)
                {
                    writer.AddResource(entry.Key.ToString(), entry.Value.ToString());
                }

                foreach (var currency in currencies)
                {
                    writer.AddResource(String.Format("Country_{0}", ToIdentifier(currency.Country)), currency.Country);
                }
            }
        }

        private static void AddOtherResources(List<CurrencyInfo> currencies, string resx)
        {
            var units = currencies
                .Select(curr => curr.Currency.Name)
                .Distinct()
                .Select(curr => new KeyValue(String.Format("CUnit_{0}", ToIdentifier(curr)), curr))
                .ToList();
            var minorUnits = currencies
                .Select(curr => curr.Currency.MinorUnit)
                .Distinct()
                .Select(unit => new KeyValue(String.Format("CMUnit_{0}", ToIdentifier(unit)), unit))
                .ToList();
            var existing = new List<DictionaryEntry>();
            using (var reader = new ResXResourceReader(resx))
            {
                foreach (DictionaryEntry entry in reader)
                {
                    existing.Add(new DictionaryEntry(entry.Key, entry.Value));
                }
            }

            using (var writer = new ResXResourceWriter(resx))
            {
                foreach (var entry in existing)
                {
                    writer.AddResource(entry.Key.ToString(), entry.Value.ToString());
                }

                foreach (var unit in units)
                {
                    writer.AddResource(unit.Key, unit.Value);
                }

                foreach (var minorUnit in minorUnits)
                {
                    writer.AddResource(minorUnit.Key, minorUnit.Value);
                }
            }
        }

        private static string ToIdentifier(string name)
        {
            var items = name
                .Split(new string[] { " ", ",", "-", "'" }, StringSplitOptions.RemoveEmptyEntries)
                .Where(str => str.Length >= 2)
                .Select(str => String.Format("{0}{1}", Char.ToUpper(str[0]), str.Substring(1)))
                .ToArray();
            return String.Join(String.Empty, items);
        }

        #endregion

        #region List Metadata Organizer

        private static void OrganizeListMetadata()
        {
            try
            {
                var all = new List<ColumnMetaModel>();
                var metadata = GetListColumnsMetadata();
                foreach (var group in metadata.GroupBy(col => col.ViewId))
                {
                    all.AddRange(UpdateListColumnsMetadata(group.ToList()));
                }

                int id = 1;
                var builder = new StringBuilder("SET IDENTITY_INSERT [Metadata].[Column] ON");
                builder.AppendLine(Environment.NewLine);
                foreach (var column in all)
                {
                    column.Column.Id = id++;
                    builder.AppendLine(ScriptColumnAsInsert(column));
                }

                builder.AppendLine();
                builder.AppendLine("SET IDENTITY_INSERT [Metadata].[Column] OFF");
                File.WriteAllText("metadata.sql", builder.ToString());
                Console.WriteLine("List metadata successfully scripted to file.");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured. More details are shown below.");
                Console.WriteLine(ex);
            }
        }

        private static IEnumerable<ColumnMetaModel> GetListColumnsMetadata()
        {
            var result = _dal.Query("SELECT * FROM [Metadata].[Column]");
            return result.Rows
                .Cast<DataRow>()
                .Select(row => AsColumnModel(row));
        }

        private static ColumnMetaModel AsColumnModel(DataRow row)
        {
            return new ColumnMetaModel()
            {
                ViewId = Int32.Parse(row["ViewID"].ToString()),
                Column = new ColumnViewModel()
                {
                    AllowFiltering = Boolean.Parse(row["AllowFiltering"].ToString()),
                    AllowSorting = Boolean.Parse(row["AllowSorting"].ToString()),
                    DisplayIndex = Int16.Parse(row["DisplayIndex"].ToString()),
                    DotNetType = row["DotNetType"].ToString(),
                    Expression = AsNullable(row["Expression"]),
                    GroupName = AsNullable(row["GroupName"]),
                    Id = Int32.Parse(row["ColumnID"].ToString()),
                    IsFixedLength = Boolean.Parse(row["IsFixedLength"].ToString()),
                    IsNullable = Boolean.Parse(row["IsNullable"].ToString()),
                    Length = Int32.Parse(row["Length"].ToString()),
                    MinLength = Int32.Parse(row["MinLength"].ToString()),
                    Name = row["Name"].ToString(),
                    ScriptType = row["ScriptType"].ToString(),
                    StorageType = row["StorageType"].ToString(),
                    Type = AsNullable(row["Type"]),
                    Visibility = AsNullable(row["Visibility"])
                }
            };
        }

        private static string AsNullable(object value)
        {
            return value != DBNull.Value
                ? value.ToString()
                : null;
        }

        private static IList<ColumnMetaModel> UpdateListColumnsMetadata(IList<ColumnMetaModel> columns)
        {
            // TODO: Add RowNo column (if not present) here...
            if (HasRowNumberColumn(columns))
            {
                return columns;
            }

            var rowNo = new ColumnMetaModel()
            {
                ViewId = columns.First().ViewId,
                Column = new ColumnViewModel()
                {
                    AllowFiltering = true,
                    AllowSorting = true,
                    DisplayIndex = 0,
                    DotNetType = "System.Int32",
                    Name = "RowNo",
                    ScriptType = "number",
                    StorageType = "int",
                    Visibility = "AlwaysVisible"
                }
            };

            foreach (var column in columns.Where(col => col.Column.DisplayIndex != -1))
            {
                column.Column.DisplayIndex++;
            }

            columns.Add(rowNo);
            return columns
                .OrderBy(col => col.Column.DisplayIndex)
                .ToList();
        }

        private static bool HasRowNumberColumn(IEnumerable<ColumnMetaModel> columns)
        {
            var rowNo = columns
                .Where(col => col.Column.Name == "RowNo")
                .FirstOrDefault();
            return rowNo != null;
        }

        private static string ScriptColumnAsInsert(ColumnMetaModel column)
        {
            var col = column.Column;
            return String.Format(
                @"INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
    VALUES ({0}, {1}, '{2}', {3}, {4}, '{5}', '{6}', '{7}', {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}, {16})",
                col.Id, column.ViewId, col.Name, AsNullable(col.GroupName), AsNullable(col.Type, false),
                col.DotNetType, col.StorageType, col.ScriptType, col.Length, col.MinLength,
                AsBit(col.IsFixedLength), AsBit(col.IsNullable), AsBit(col.AllowSorting),
                AsBit(col.AllowFiltering), AsNullable(col.Visibility), col.DisplayIndex,
                AsNullable(col.Expression, false));
        }

        private static string AsNullable(string value, bool isUnicode = true)
        {
            if (isUnicode)
            {
                return !String.IsNullOrEmpty(value)
                    ? String.Format("N'{0}'", value)
                    : "NULL";
            }
            else
            {
                return !String.IsNullOrEmpty(value)
                    ? String.Format("'{0}'", value)
                    : "NULL";
            }
        }

        private static int AsBit(bool value)
        {
            return value ? 1 : 0;
        }

        private static readonly DataLayerBase _dal = new SqlDataLayer(_connection, ProviderType.SqlClient);
        private const string _connection = "Server=.;Database=NGTadbirSys;Trusted_Connection=True;MultipleActiveResultSets=true";

        #endregion
    }
}
