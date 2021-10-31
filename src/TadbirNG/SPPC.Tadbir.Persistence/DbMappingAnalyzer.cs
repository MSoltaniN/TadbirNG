using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SPPC.Framework.Persistence;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    ///
    /// </summary>
    public class DbMappingAnalyzer
    {
        /// <summary>
        ///
        /// </summary>
        public DbMappingAnalyzer()
        {
            _tadbir = new TadbirContext() { ConnectionString = _connection };
            _system = new SystemContext(new DbContextOptionsBuilder<SystemContext>()
                .UseSqlServer(_sysConnection)
                .Options);
            _sql = new SqlServerConsole();
        }

        /// <summary>
        ///
        /// </summary>
        public void Analyze()
        {
            var builder = new StringBuilder();
            var dbItems = GetDbNullables(_connection, true);
            var modelItems = GetModelNullables(_tadbir, true);
            ReportProblems("Nullable", "Tadbir", GetInconsistencies(dbItems, modelItems), builder);

            dbItems = GetDbNullables(_connection, false);
            modelItems = GetModelNullables(_tadbir, false);
            ReportProblems("Not Nullable", "Tadbir", GetInconsistencies(dbItems, modelItems), builder);

            dbItems = GetDbNullables(_sysConnection, true);
            modelItems = GetModelNullables(_system, true);
            ReportProblems("Nullable", "System", GetInconsistencies(dbItems, modelItems), builder);

            dbItems = GetDbNullables(_sysConnection, false);
            modelItems = GetModelNullables(_system, false);
            ReportProblems("Not Nullable", "System", GetInconsistencies(dbItems, modelItems), builder);
            File.WriteAllText(Result, builder.ToString());
        }

        private List<KeyValuePair<string, string>> GetDbNullables(string connection, bool nullable)
        {
            var dbNullables = new List<KeyValuePair<string, string>>();
            _sql.ConnectionString = connection;
            string query = String.Format(_template, nullable ? "YES" : "NO");
            var result = _sql.ExecuteQuery(query);
            dbNullables.AddRange(result.Rows
                .Cast<DataRow>()
                .Select(row => new KeyValuePair<string, string>(
                    row["TABLE_NAME"].ToString(), row["COLUMN_NAME"].ToString().Replace("ID", "Id")))
                .OrderBy(kv => kv.Key)
                    .ThenBy(kv => kv.Value));
            return dbNullables;
        }

        private static List<KeyValuePair<string, string>> GetModelNullables(DbContext dbContext, bool nullable)
        {
            var modelNullables = new List<KeyValuePair<string, string>>();
            var entities = dbContext.Model
                .GetEntityTypes()
                .ToArray();
            Func<IProperty, bool> criteria = nullable
                ? prop => prop.IsNullable
                : prop => !prop.IsNullable;
            Array.ForEach(entities, entity =>
            {
                modelNullables.AddRange(entity
                    .GetProperties()
                    .Where(criteria)
                    .Select(prop => new KeyValuePair<string, string>(entity.ShortName(), prop.Name)));
            });
            return modelNullables
                .OrderBy(kv => kv.Key)
                    .ThenBy(kv => kv.Value)
                .ToList();
        }

        private static IList<KeyValuePair<string, string>> GetInconsistencies(
            IList<KeyValuePair<string, string>> dbItems,
            IList<KeyValuePair<string, string>> modelItems)
        {
            return dbItems
                .Except(modelItems)
                .ToList();
        }

        private static void ReportProblems(string criteria, string dbType,
            IList<KeyValuePair<string, string>> problems, StringBuilder builder)
        {
            builder.AppendFormat("{0} fields in {1} database that are NOT properly mapped in context.{2}",
                criteria, dbType, Environment.NewLine);
            builder.AppendLine("============================================================================================");
            builder.AppendLine();
            foreach (var problem in problems)
            {
                builder.AppendFormat("[{0}].[{1}]{2}", problem.Key, problem.Value, Environment.NewLine);
            }

            builder.AppendLine();
        }

        private readonly TadbirContext _tadbir;
        private readonly SystemContext _system;
        private readonly ISqlConsole _sql;
        private const string _connection = "Server=BE-LAPTOP;Database=NGTadbir;User ID=NgTadbirUser;Password=Demo1234;Trusted_Connection=False";
        private const string _sysConnection = "Server=BE-LAPTOP;Database=NGTadbirSys;User ID=NgTadbirUser;Password=Demo1234;Trusted_Connection=False";
        private const string Result = @"D:\Temp\db-analysis-result.txt";
        private const string _template = @"
SELECT [TABLE_NAME], [COLUMN_NAME]
FROM [INFORMATION_SCHEMA].[COLUMNS]
WHERE [TABLE_NAME] <> 'sysdiagrams' AND [COLUMN_NAME] NOT IN('rowguid', 'ModifiedDate') AND [IS_NULLABLE] = '{0}'";
    }
}
