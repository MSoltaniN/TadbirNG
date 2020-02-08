using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using SPPC.Framework.Helpers;
using SPPC.Tadbir.Tools.SystemDesigner.Models;
using HAP = HtmlAgilityPack;

namespace SPPC.Tadbir.Tools.SystemDesigner.Utility
{
    public class CurrencyDatabaseBuilder
    {
        public static void BuildCurrencyDatabase()
        {
            string url = "https://en.wikipedia.org/wiki/List_of_circulating_currencies";
            var currencies = ScrapeWikipediaPage(url);
            File.WriteAllText("currencies.json", JsonHelper.From(currencies));
            Console.WriteLine("Currency database is now ready.");
            Console.ReadLine();
        }

        public static void AddResources()
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
    }
}
