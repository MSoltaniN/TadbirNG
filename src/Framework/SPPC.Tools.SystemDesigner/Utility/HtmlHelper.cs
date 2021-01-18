using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace SPPC.Tools.SystemDesigner
{
    public class HtmlHelper
    {
        public static string RetrieveContent(string url)
        {
            var content = String.Empty;
            var client = new HttpClient();
            var response = client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                content = response.Content.ReadAsStringAsync().Result;
            }

            return content;
        }

        public static HtmlDocument LoadHtml(string url)
        {
            var html = new HtmlDocument();
            var htmlString = RetrieveContent(url);
            if (!String.IsNullOrEmpty(htmlString))
            {
                html.LoadHtml(htmlString);
            }

            return html;
        }

        public static async Task<string> RetrieveContentAsync(string url)
        {
            var content = String.Empty;
            var client = new HttpClient();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                content = await response.Content.ReadAsStringAsync();
                var bytes = await response.Content.ReadAsByteArrayAsync();
            }

            return content;
        }

        public static string RetrieveViaRequest(string url)
        {
            var content = String.Empty;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));
            request.Headers.UserAgent.Add(new ProductInfoHeaderValue("Firefox", "40.0"));
            var response = client.SendAsync(request).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        public static async Task<HtmlDocument> LoadHtmlAsync(string url)
        {
            var html = new HtmlDocument();
            var htmlString = await RetrieveContentAsync(url);
            if (!String.IsNullOrEmpty(htmlString))
            {
                html.LoadHtml(htmlString);
            }

            return html;
        }

        public static HtmlNode GetRootElement(HtmlDocument html)
        {
            return html.DocumentNode.ChildNodes
                .Where(n => n.NodeType == HtmlNodeType.Element && n.Name.ToLower() == "html")
                .FirstOrDefault();
        }

        public static string GetRootElementName(HtmlDocument html)
        {
            var node = html.DocumentNode.ChildNodes
                .Where(cn => !cn.Name.StartsWith("#") && !cn.Name.StartsWith("?"))
                .FirstOrDefault();
            return (node != null) ? node.Name : String.Empty;
        }

        public static string GetMarkupText(string text)
        {
            var markupText = text;
            if (!String.IsNullOrWhiteSpace(text))
            {
                var htmlNode = HtmlNode.CreateNode(text);
                markupText = htmlNode.InnerText;
                if (String.IsNullOrWhiteSpace(markupText))
                {
                    var sb = new StringBuilder();
                    var nodes = new List<HtmlNode>();
                    nodes.AddRange(htmlNode.Descendants("p"));
                    var sibling = htmlNode.NextSibling;
                    while(sibling != null)
                    {
                        if (IsTextNode(sibling))
                            nodes.Add(sibling);
                        sibling = sibling.NextSibling;
                    }

                    Array.ForEach(nodes.ToArray(), node => sb.AppendLine(node.InnerText));
                    markupText = sb.ToString().Trim();
                }
            }

            return markupText;
        }

        private static bool IsTextNode(HtmlNode node)
        {
            return (node.Name == "p");
        }
    }
}
