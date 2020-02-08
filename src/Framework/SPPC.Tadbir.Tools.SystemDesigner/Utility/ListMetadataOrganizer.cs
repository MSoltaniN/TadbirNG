using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using BabakSoft.Platform.Data;
using SPPC.Tadbir.Tools.SystemDesigner.Models;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Tools.SystemDesigner.Utility
{
    public class ListMetadataOrganizer
    {
        public static void OrganizeListMetadata()
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
                Console.WriteLine("Error occurred. More details are shown below.");
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
    }
}
