using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Forms;
using BS = BabakSoft.Platform.Common;
using SPPC.Framework.Common;
using SPPC.Tadbir.Tools.SystemDesigner.Models;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tadbir.Tools.SystemDesigner.Wizards.ViewWizard
{
    public partial class EditColumnsPage : UserControl
    {
        public EditColumnsPage()
        {
            InitializeComponent();
            Info = "Edit Columns";
        }

        public string Info { get; set; }

        public string ViewName { get; set; }

        public List<ColumnViewModel> Columns { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitColumns(Columns.Count == 0);
            LoadColumns();
            SetupBinding();
        }

        private void SetupBinding()
        {
            txtName.DataBindings.Add("Text", Columns, "Name");
            cmbType.DataBindings.Add("Text", Columns, "Type");
            cmbDotNetType.DataBindings.Add("Text", Columns, "DotNetType");
            cmbStorageType.DataBindings.Add("Text", Columns, "StorageType");
            cmbScriptType.DataBindings.Add("Text", Columns, "ScriptType");
            spnLength.DataBindings.Add("Value", Columns, "Length");
            spnMinLength.DataBindings.Add("Value", Columns, "MinLength");
            chkIsFixedLength.DataBindings.Add("Checked", Columns, "IsFixedLength");
            chkIsNullable.DataBindings.Add("Checked", Columns, "IsNullable");
            chkAllowSorting.DataBindings.Add("Checked", Columns, "AllowSorting");
            chkAllowFiltering.DataBindings.Add("Checked", Columns, "AllowFiltering");
            txtExpression.DataBindings.Add("Text", Columns,  "Expression");
        }

        private void InitColumns(bool isDefault)
        {
            if (!String.IsNullOrEmpty(ViewName))
            {
                var type = typeof(AccountViewModel).Assembly
                    .GetTypes()
                    .Where(t => t.Name == ViewName + "ViewModel")
                    .FirstOrDefault();
                if (type != null)
                {
                    var propertyNames = BS::Reflector.GetPropertyNames(type);
                    int itemCount = propertyNames.Count();
                    foreach (var name in propertyNames)
                    {
                        var column = GetColumn(name, BS::Reflector.GetPropertyType(type, name), type);
                        Columns.Add(column);
                    }
                }
            }
        }

        private ColumnViewModel GetColumn(string name, Type type, Type TargetType)
        {
            var column = new ColumnViewModel()
            {
                AllowFiltering = true,
                AllowSorting = true,
                DotNetType = type.FullName,
                Name = name,
                ScriptType = ScriptTypeFromType(type),
                StorageType = StorageTypeFromType(type)
            };

            if (column.ScriptType == "string")
            {
                if (BS::Reflector.GetPropertyAttribute(
                    TargetType, name, typeof(StringLengthAttribute)) is StringLengthAttribute lengthAttribute)
                {
                    column.Length = lengthAttribute.MaximumLength;
                }
            }

            return column;
        }

        private string StorageTypeFromType(Type type)
        {
            string storageType = String.Empty;
            switch (type.FullName.Replace("System.", String.Empty))
            {
                case "Int16":
                    storageType = "smallint";
                    break;
                case "Int32":
                    storageType = "int";
                    break;
                case "Int64":
                    storageType = "bigint";
                    break;
                case "Single":
                case "Double":
                    storageType = "float";
                    break;
                case "Decimal":
                    storageType = "decimal";
                    break;
                case "String":
                    storageType = "nvarchar";
                    break;
                case "DateTime":
                    storageType = "datetime";
                    break;
                case "Boolean":
                    storageType = "bit";
                    break;
                case "Object":
                default:
                    break;
            }

            return storageType;
        }

        private string ScriptTypeFromType(Type type)
        {
            string scriptType = String.Empty;
            switch (type.FullName.Replace("System.", String.Empty))
            {
                case "Int16":
                case "Int32":
                case "Int64":
                case "Single":
                case "Double":
                case "Decimal":
                    scriptType = "number";
                    break;
                case "String":
                    scriptType = "string";
                    break;
                case "DateTime":
                    scriptType = "Date";
                    break;
                case "Boolean":
                    scriptType = "boolean";
                    break;
                case "Object":
                    scriptType = "object";
                    break;
                default:
                    break;
            }

            return scriptType;
        }

        private void LoadColumns()
        {
            int itemIndex = 0;
            foreach (var column in Columns)
            {
                lbxColumns.Items.Add(column);
                itemIndex++;
            }
        }

        private void Columns_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void EditColumnsForm_Leave(object sender, EventArgs e)
        {
        }

        public int _columnSelectedIndex = 0;
    }
}
