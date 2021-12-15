using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Helpers;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Model.Finance;
using SPPC.Tadbir.Persistence;

namespace SPPC.Tools.TadbirDbConverter
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            _map = JsonHelper.To<IDictionary<string, TableMapping>>(File.ReadAllText(_mappingPath));
            _dalFrom = new SqlDataLayer(_sourceConnection);
            _dbConsole = new SqlServerConsole() { ConnectionString = _targetConnection };
            _categoryMap = GetTadbirCategoryMap();
        }

        private void Convert_Click(object sender, EventArgs e)
        {
            try
            {
                int companyId = 8;          // Temporarily hardcoded value
                ConvertFiscalPeriods(companyId);
                InsertDefaultBranch(companyId);
                ConvertAccountGroups();
            }
            catch (Exception ex)
            {
                var message = String.Format($"Error occured during conversion.{Environment.NewLine}{ex.Message}");
                MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InsertDefaultBranch(int companyId)
        {
            var queryBuilder = new StringBuilder();
            queryBuilder.AppendLine("SET IDENTITY_INSERT [Corporate].[Branch] ON");
            queryBuilder.AppendFormat(Scripts.InsertDefaultBranch, companyId);
            queryBuilder.AppendLine();
            queryBuilder.AppendLine("SET IDENTITY_INSERT [Corporate].[Branch] OFF");
            _dbConsole.ExecuteNonQuery(queryBuilder.ToString());
        }

        private void ConvertFiscalPeriods(int companyId)
        {
            var query = GetSelectQuery<FiscalPeriod>();
            var result = _dalFrom.Query(query);
            string insertFunc(FiscalPeriod fp) => String.Format(
                Scripts.InsertFiscalPeriod, fp.Id, companyId, fp.Name.FromTadbirText(),
                fp.StartDate, fp.EndDate, FromTadbirInventoryMode(fp.InventoryMode),
                fp.Description.FromTadbirText());
            query = GetInsertCommands<FiscalPeriod>(result, insertFunc);
            _dbConsole.ExecuteNonQuery(query);
        }

        private void ConvertAccountGroups()
        {
            var query = GetSelectQuery<AccountGroup>();
            var result = _dalFrom.Query(query);

            string insertFunc(AccountGroup grp) => String.Format(
                Scripts.InsertAccountGroup, grp.Id, grp.Name.FromTadbirText(),
                FromTadbirInventoryMode(grp.InventoryMode), _categoryMap[grp.Category],
                grp.Description.FromTadbirText());
            query = GetInsertCommands<AccountGroup>(result, insertFunc);
            _dbConsole.ExecuteNonQuery(query);
        }

        private string GetSelectQuery<TModel>()
            where TModel : class, new()
        {
            var selectQuery = String.Empty;
            var typeName = typeof(TModel).Name.CamelCase();
            if (_map.ContainsKey(typeName))
            {
                var mapping = _map[typeName];
                var queryBuilder = new StringBuilder();
                queryBuilder.AppendFormat($"SELECT * FROM [dbo].[{mapping.SourceTable}]");
                queryBuilder.AppendFormat($" WHERE Id > 0");
                if (mapping.HadFpId)
                {
                    queryBuilder.AppendFormat($" AND FPId > 0");
                }

                selectQuery = queryBuilder.ToString();
            }

            return selectQuery;
        }

        private string GetInsertCommands<TModel>(DataTable result, Func<TModel, string> insertItem)
            where TModel : class, new()
        {
            var insertCommends = String.Empty;
            var typeName = typeof(TModel).Name.CamelCase();
            if (_map.ContainsKey(typeName))
            {
                var mapping = _map[typeName];
                var queryBuilder = new StringBuilder("BEGIN TRANSACTION;");
                queryBuilder.AppendLine();
                queryBuilder.AppendLine($"SET IDENTITY_INSERT [{mapping.TargetSchema}].[{typeof(TModel).Name}] ON");
                foreach (DataRow row in result.Rows)
                {
                    var model = MapModel<TModel>(row);
                    queryBuilder.AppendLine(insertItem(model));
                }

                queryBuilder.AppendLine($"SET IDENTITY_INSERT [{mapping.TargetSchema}].[{typeof(TModel).Name}] OFF");
                queryBuilder.AppendLine("COMMIT;");
                insertCommends = queryBuilder.ToString();
            }

            return insertCommends;
        }

        private TModel MapModel<TModel>(DataRow row)
            where TModel : class, new()
        {
            var model = new TModel();
            var typeName = typeof(TModel).Name.CamelCase();
            if (_map.ContainsKey(typeName))
            {
                var tableMapping = _map[typeName];
                var mainColumns = row.Table.Columns
                    .Cast<DataColumn>()
                    .Where(col => !tableMapping.IgnoreFields.Contains(col.ColumnName));
                foreach (DataColumn column in mainColumns)
                {
                    var mapping = tableMapping.Fields
                        .Where(fld => fld.FromName == column.ColumnName)
                        .FirstOrDefault();
                    var toName = mapping != null ? mapping.ToName : column.ColumnName;
                    var value = ValueOrDefault(column.DataType, row, column.ColumnName);
                    var targetType = Reflector.GetPropertyType(model, toName);
                    if (targetType == typeof(string))
                    {
                        Reflector.SetProperty(model, toName, value.ToString());
                    }
                    else
                    {
                        Reflector.SetProperty(model, toName, value);
                    }
                }
            }

            return model;
        }

        public static object ValueOrDefault(Type type, DataRow row, string field)
        {
            object value = null;
            if (row.Table.Columns.Contains(field) && row[field] != DBNull.Value)
            {
                value = Convert.ChangeType(row[field], type);
            }

            return value;
        }

        private static IDictionary<string, string> GetTadbirCategoryMap()
        {
            return new Dictionary<string, string>
            {
                { "1", "CategoryAsset" },
                { "2", "CategoryLiability" },
                { "4", "CategoryCapital" },
                { "5", "CategoryCoordination" },
                { "6", "CategoryIncome" },
                { "7", "CategoryExpense" },
                { "8", "CategorySales" },
                { "9", "CategoryPurchase" },
                { "10", "CategoryAssociation" }
            };
        }

        private static int FromTadbirInventoryMode(int tadbirMode)
        {
            int mode;
            if (tadbirMode == -1)
            {
                mode = (int)InventoryMode.Periodic;
            }
            else if (tadbirMode == 0)
            {
                mode = (int)InventoryMode.Both;
            }
            else
            {
                mode = (int)InventoryMode.Perpetual;
            }

            return mode;
        }

        private const string _mappingPath = @"..\..\..\src\TadbirNG\SPPC.Tools.TadbirDbConverter\convert-mapping.json";
        private const string _sourceConnection = "Server=BE-LAPTOP;Database=AVICHI2;Trusted_Connection=True";
        private const string _targetConnection = "Server=BE-LAPTOP;Database=NGVichi;Trusted_Connection=True";
        private readonly SqlDataLayer _dalFrom;
        private readonly ISqlConsole _dbConsole;
        private readonly IDictionary<string, TableMapping> _map;
        private readonly IDictionary<string, string> _categoryMap;
    }
}
