using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SPPC.Framework.Common;

namespace SPPC.Framework.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public class ResXResourceWriter
    {
        /// <summary>
        /// 
        /// </summary>
        public ResXResourceWriter()
        {
            StringResources = new Dictionary<string, string>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entries"></param>
        public ResXResourceWriter(IDictionary<string, string> entries)
        {
            StringResources = entries;
        }

        /// <summary>
        ///
        /// </summary>
        public IDictionary<string, string> StringResources { get; }

        /// <summary>
        /// ریسورس های متنی را در فایل داده شده ذخیره می کند
        /// </summary>
        /// <param name="path">مسیر فایل ریسورس ها با فرمت استاندارد ایکس ام ال</param>
        /// <remarks>در حال حاضر، این کلاس از ایجاد فایل ریسورس اولیه پشتیبانی نمی کند. بنابراین
        /// در مسیر داده شده باید فایل ریسورس استانداردی با فرمت ایکس ام ال وجود داشته باشد</remarks>
        public void Save(string path)
        {
            string entryMarker = $"\t</resheader>{Environment.NewLine}";
            string entryTemplate = $"\t<data name=\"{{0}}\" xml:space=\"preserve\">{Environment.NewLine}"
                + $"\t\t<value>{{1}}</value>{Environment.NewLine}\t</data>{Environment.NewLine}";
            string schemaAndHeaders = String.Empty;
            var xmlContents = File.ReadAllText(path, Encoding.UTF8);
            int index = xmlContents.LastIndexOf(entryMarker);
            if (index == -1)
            {
                entryMarker = entryMarker.Replace("\t", "  ");
                entryTemplate = entryTemplate.Replace("\t", "  ");
                index = xmlContents.LastIndexOf(entryMarker);
                if (index == -1)
                {
                    throw ExceptionBuilder.NewNotImplementedException(
                        "Creating a new ResX resource file is not supported.");
                }
            }

            schemaAndHeaders = xmlContents.Substring(0, index + entryMarker.Length);
            var resxBuilder = new StringBuilder(schemaAndHeaders);
            resxBuilder.Append(String.Join(String.Empty,
                StringResources.Select(entry => String.Format(entryTemplate, entry.Key, entry.Value))));
            resxBuilder.Append("</root>");
            File.WriteAllText(path, resxBuilder.ToString(), Encoding.UTF8);
        }
    }
}
