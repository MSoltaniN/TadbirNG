using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using SPPC.Framework.Helpers;
using SPPC.Framework.Persistence;
using SPPC.Tools.Model;

namespace SPPC.Tools.SystemDesigner.Commands
{
    public class GenerateFixScriptCommand : ICommand
    {
        public GenerateFixScriptCommand(string path)
        {
            _path = path;
        }

        public void Execute()
        {
            string sysConnection = GetSysConnectionString();
            if (!sysConnection.Contains("130.185.76.7"))
            {
                var connectionBuilder = new SqlConnectionStringBuilder(sysConnection)
                {
                    InitialCatalog = "NGTadbir"
                };

                // Generate script for company databases...
                _dal = new SqlDataLayer(connectionBuilder.ConnectionString);
                var textColumns = GetTextColumns();
                var clauses = GetUpdateClauses(textColumns);
                File.WriteAllText(_path, String.Join(Environment.NewLine, clauses));

                // Generate script for system database...
                _dal = new SqlDataLayer(sysConnection);
                textColumns = GetTextColumns();
                clauses = GetUpdateClauses(textColumns);
                string sysScriptPath = Path.Combine(Path.GetDirectoryName(_path), "TadbirSys_FixArabicLetters.sql");
                File.WriteAllText(sysScriptPath, String.Join(Environment.NewLine, clauses));
            }
        }

        private List<TextColumnModel> GetTextColumns()
        {
            var textColumns = new List<TextColumnModel>();
            string command = @"
  SELECT TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME
  FROM [INFORMATION_SCHEMA].[COLUMNS]
  WHERE DATA_TYPE = 'nvarchar' AND TABLE_SCHEMA <> 'dbo'
  ORDER BY TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME";
            var result = _dal.Query(command);
            foreach (DataRow row in result.Rows)
            {
                textColumns.Add(new TextColumnModel()
                {
                    Schema = row["TABLE_SCHEMA"].ToString(),
                    Table = row["TABLE_NAME"].ToString(),
                    Column = row["COLUMN_NAME"].ToString()
                });
            }

            return textColumns;
        }

        private List<string> GetUpdateClauses(List<TextColumnModel> textColumns)
        {
            var clauses = new List<string>();
            foreach (var tblGroup in textColumns
                .OrderBy(col => col.Table)
                .GroupBy(col => col.Table))
            {
                clauses.Add(GetUpdateClause(tblGroup.Key, tblGroup, ArabicKeh, FarsiKeh));
                clauses.Add(GetUpdateClause(tblGroup.Key, tblGroup, ArabicYeh, FarsiYeh));
                clauses.Add(GetUpdateClause(tblGroup.Key, tblGroup, ArabicYehAlt, FarsiYeh));
            }

            return clauses;
        }

        private string GetUpdateClause(string table, IEnumerable<TextColumnModel> textColumns, int from, int to)
        {
            var expressions = new List<string>();
            var clauseBuilder = new StringBuilder();
            string schema = textColumns.First().Schema;
            clauseBuilder.AppendFormat(@"
UPDATE [{0}].[{1}]
SET ", schema, table);
            foreach (var textColumn in textColumns)
            {
                expressions.Add(String.Format("[{0}] = REPLACE([{0}], NCHAR({1}), NCHAR({2}))",
                    textColumn.Column, from, to));
            }

            clauseBuilder.Append(String.Join(", ", expressions));
            return clauseBuilder.ToString();
        }

        private string GetSysConnectionString()
        {
            string path = @"..\..\src\Framework\SPPC.Tadbir.Web.Api\appsettings.Development.json";
            var appSettings = JsonHelper.To<AppSettingsModel>(File.ReadAllText(path));
            return appSettings.ConnectionStrings.TadbirSysApi;
        }

        private const int ArabicKeh = 1603;
        private const int FarsiKeh = 1705;
        private const int ArabicYehAlt = 1609;
        private const int ArabicYeh = 1610;
        private const int FarsiYeh = 1740;
        private SqlDataLayer _dal;
        private readonly string _path;
    }
}
