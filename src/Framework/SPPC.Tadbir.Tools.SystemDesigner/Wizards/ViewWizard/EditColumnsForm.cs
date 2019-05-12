using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Framework.Common;
using SPPC.Tadbir.ViewModel.Metadata;
using System.ComponentModel.DataAnnotations;

namespace SPPC.Tadbir.Tools.SystemDesigner.Wizards.ViewWizard
{
    public partial class EditColumnsForm : UserControl
    {
        public EditColumnsForm()
        {
            InitializeComponent();
        }

        public string ViewModel { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadColumns();
        }

        private void LoadColumns()
        {
            var columns = new List<ColumnViewModel>();
            if (!String.IsNullOrEmpty(ViewModel))
            {
                var type = typeof(AccountViewModel).Assembly
                    .GetTypes()
                    .Where(t => t.Name == ViewModel + "ViewModel")
                    .FirstOrDefault();
                if (type != null)
                {
                    var propertyNames = Reflector.GetPropertyNames(type);
                    foreach (var name in propertyNames)
                    {
                        lbxColumns.Items.Add(GetColumn(name, Reflector.GetPropertyType(type, name)), true);
                    }
                }
            }
        }

        ColumnViewModel GetColumn(string name, Type type)
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
                var lengthAttribute = Reflector.GetPropertyAttribute(
                    type, name, typeof(StringLengthAttribute)) as StringLengthAttribute;
                if (lengthAttribute != null)
                {
                    column.Length = lengthAttribute.MaximumLength;
                }
            }

            return column;
        }

        string StorageTypeFromType(Type type)
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

        string ScriptTypeFromType(Type type)
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

        private void lbxColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxColumns.SelectedItem != null)
            {
            }
        }

        private void SetColumn(ColumnViewModel column)
        {
        }
    }
}
