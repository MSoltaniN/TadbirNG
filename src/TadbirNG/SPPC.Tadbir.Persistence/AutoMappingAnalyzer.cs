using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AutoMapper;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Mapper;
using SPPC.Tadbir.Mapper;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    ///
    /// </summary>
    public class AutoMappingAnalyzer
    {
        /// <summary>
        ///
        /// </summary>
        public AutoMappingAnalyzer()
        {
            _autoMapper = new DomainMapper(new CryptoService());
            _mapperConfig = _autoMapper.Configuration as MapperConfiguration;
        }

        /// <summary>
        ///
        /// </summary>
        public void AnalyzeMappings()
        {
            var builder = new StringBuilder();
            var usageList = GetCurrentUsage();
            var mappingList = GetCurrentMappings();
            WriteCurrentMappings(mappingList, builder);
            WriteMissingMappings(mappingList, usageList, builder);
            File.WriteAllText(Result, builder.ToString());
        }

        private static IList<KeyValuePair<string, string>> GetCurrentUsage()
        {
            var usages = new List<KeyValuePair<string, string>>();
            var source = File.ReadAllText(Source);
            var regex = new Regex(MapperRegex);
            usages.AddRange(regex.Matches(source)
                .Cast<Match>()
                .Select(m => new KeyValuePair<string, string>(m.Groups[2].Value, m.Groups[1].Value)));
            return usages;
        }

        private IList<KeyValuePair<string, string>> GetCurrentMappings()
        {
            var maps = _mapperConfig.GetAllTypeMaps()
                .Select(map => new KeyValuePair<string, string>(
                    map.SourceType.FullName, map.DestinationType.FullName))
                .ToList();
            return maps;
        }

        private static void WriteCurrentMappings(IList<KeyValuePair<string, string>> mappings, StringBuilder builder)
        {
            builder.AppendLine("=======================================================================");
            builder.AppendLine("An exhaustive list of all mapping currently defined in AutoMapper.");
            builder.AppendLine("=======================================================================");
            builder.AppendLine();
            foreach (var group in mappings
                .GroupBy(map => map.Key)
                .OrderBy(map => map.Key))
            {
                var srcItems = group.Key.Split('.');
                builder.AppendFormat(">{0} Mappings :{1}", srcItems.Last(), Environment.NewLine);
                Array.ForEach(group.ToArray(), item =>
                {
                    var destItems = item.Value.Split('.');
                    builder.AppendFormat("    {0} => {1} ({2} => {3}){4}",
                        srcItems.Last(), destItems.Last(), item.Key, item.Value, Environment.NewLine);
                });
                builder.AppendLine();
            }
        }

        private static void WriteMissingMappings(IList<KeyValuePair<string, string>> mappings,
            IList<KeyValuePair<string, string>> usages, StringBuilder builder)
        {
            builder.AppendLine("==========================================================================");
            builder.AppendLine("An estimation of missing mappings in current AutoMapper configuration");
            builder.AppendLine("==========================================================================");
            builder.AppendLine();
            foreach (var usage in usages)
            {
                var destination = usage.Value;
                var doneMappings = mappings
                    .Where(m => m.Value.Split('.').Last() == destination);
                if (!doneMappings.Any())
                {
                    builder.AppendFormat("[Probably missing] {0} => {1}{2}",
                        usage.Key, usage.Value, Environment.NewLine);
                }
            }
        }

        private readonly IDomainMapper _autoMapper;
        private readonly MapperConfiguration _mapperConfig;
        private const string MapperRegex = @"[mM]apper.Map<(\w{1,})>\((\w{1,})\)";
        private const string Source = @"D:\Temp\mapper-usage.txt";
        private const string Result = @"D:\Temp\analysis-result.txt";
    }
}
