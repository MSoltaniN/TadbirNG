using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Framework.Common;
using SPPC.Tadbir.ViewModel.Metadata;
using System.ComponentModel.DataAnnotations;
using SPPC.Tadbir.Tools.SystemDesigner.Models;

namespace SPPC.Tadbir.Tools.SystemDesigner.Wizards.ViewWizard
{
    public partial class EditColumnsForm : UserControl
    {
        public EditColumnsForm()
        {
            InitializeComponent();
            Info="Edit Columns";
        }

        public ViewModelEntityModel ViewModel { get; set; }

        public string Info { get; set; }

        private void InitColumnProperties()
        {
            //cmbDotNetType.SelectedIndex = 0;
            //cmbScriptType.SelectedIndex = 0;
            //cmbStorageType.SelectedIndex = 0;
            //cmbType.SelectedIndex = 0;

            //if (lbxColumns.Items.Count != 0)
            //{
            //    txtName.Text = lbxColumns.Items[0].ToString();
            //    cmbType.Text = ViewModel.Columns[0].Type;
            //    cmbDotNetType.Text = ViewModel.Columns[0].DotNetType;
            //    cmbStorageType.Text = ViewModel.Columns[0].StorageType;
            //    cmbScriptType.Text = ViewModel.Columns[0].ScriptType;
            //    spnLength.Value = ViewModel.Columns[0].Length;
            //    spnMinLength.Value = ViewModel.Columns[0].MinLength;
            //    chkIsFixedLength.Checked = ViewModel.Columns[0].IsFixedLength;
            //    chkIsNullable.Checked = ViewModel.Columns[0].IsNullable;
            //    chkAllowSorting.Checked = ViewModel.Columns[0].AllowSorting;
            //    chkAllowFiltering.Checked = ViewModel.Columns[0].AllowFiltering;
            //    txtExpression.Text = ViewModel.Columns[0].Expression;
            //}

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (ViewModel.Columns.Count == 0)
            {
                InitViewModelColumns();
            }
            LoadColumns();
            InitColumnProperties();
            SetupBinding();
        }

        private void SetupBinding()
        {
            txtName.DataBindings.Add("Text", ViewModel.Columns, "Name");
            cmbType.DataBindings.Add("Text", ViewModel.Columns, "Type");
            cmbDotNetType.DataBindings.Add("Text", ViewModel.Columns, "DotNetType");
            cmbStorageType.DataBindings.Add("Text", ViewModel.Columns, "StorageType");
            cmbScriptType.DataBindings.Add("Text", ViewModel.Columns, "ScriptType");
            spnLength.DataBindings.Add("Value", ViewModel.Columns, "Length");
            spnMinLength.DataBindings.Add("Value", ViewModel.Columns, "MinLength");
            chkIsFixedLength.DataBindings.Add("Checked", ViewModel.Columns, "IsFixedLength");
            chkIsNullable.DataBindings.Add("Checked", ViewModel.Columns, "IsNullable");
            chkAllowSorting.DataBindings.Add("Checked", ViewModel.Columns, "AllowSorting");
            chkAllowFiltering.DataBindings.Add("Checked", ViewModel.Columns, "AllowFiltering");
            txtExpression.DataBindings.Add("Text", ViewModel.Columns,  "Expression");
        }

        private void InitViewModelColumns()
        {
            if (!String.IsNullOrEmpty(ViewModel.Name))
            {
                var type = typeof(AccountViewModel).Assembly
                    .GetTypes()
                    .Where(t => t.Name == ViewModel.Name + "ViewModel")
                    .FirstOrDefault();
                if (type != null)
                {
                    var propertyNames = Reflector.GetPropertyNames(type);
                    int ItemCount = propertyNames.Count();
                    ViewModel.ActiveColumns = Enumerable.Repeat(true, ItemCount).ToList();
                    int ItemIndex = 0;
                    foreach (var name in propertyNames)
                    {
                        var column = new ColumnViewModel();
                        column = GetColumn(name, Reflector.GetPropertyType(type, name), type);
                        ViewModel.Columns.Add(column);
                        ItemIndex++;
                    }
                }
            }
        }

        ColumnViewModel GetColumn(string name, Type type, Type TargetType)
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
                    TargetType, name, typeof(StringLengthAttribute)) as StringLengthAttribute;
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
        private void LoadColumns()
        {
            int ItemIndex = 0;
            foreach (var Item in ViewModel.Columns)
            {
                lbxColumns.Items.Add(Item, ViewModel.ActiveColumns[ItemIndex]);
                ItemIndex++;
            }
        }
      

    private void lbxColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (lbxColumns.SelectedItem != null && _ColumnSlectedIndex != lbxColumns.SelectedIndex)
            //{

            //    //save last items in model
            //    ViewModel.Columns[_ColumnSlectedIndex].Name =txtName.Text;
            //    ViewModel.Columns[_ColumnSlectedIndex].Type = cmbType.Text;
            //    ViewModel.Columns[_ColumnSlectedIndex].DotNetType = cmbDotNetType.Text;
            //    ViewModel.Columns[_ColumnSlectedIndex].StorageType = cmbStorageType.Text;
            //    ViewModel.Columns[_ColumnSlectedIndex].ScriptType = cmbScriptType.Text;
            //    ViewModel.Columns[_ColumnSlectedIndex].Length = Convert.ToInt32( spnLength.Value);
            //    ViewModel.Columns[_ColumnSlectedIndex].MinLength =Convert.ToInt32( spnMinLength.Value);
            //    ViewModel.Columns[_ColumnSlectedIndex].IsFixedLength = chkIsFixedLength.Checked;
            //    ViewModel.Columns[_ColumnSlectedIndex].IsNullable = chkIsNullable.Checked;
            //    ViewModel.Columns[_ColumnSlectedIndex].AllowSorting = chkAllowSorting.Checked;
            //    ViewModel.Columns[_ColumnSlectedIndex].AllowFiltering = chkAllowFiltering.Checked;
            //    ViewModel.Columns[_ColumnSlectedIndex].Expression = txtExpression.Text;


            //    _ColumnSlectedIndex = lbxColumns.SelectedIndex;

            //    //retrive items from model
            //    txtName.Text = lbxColumns.SelectedItem.ToString();
            //    cmbType.Text = ViewModel.Columns[_ColumnSlectedIndex].Type;
            //    cmbDotNetType.Text= ViewModel.Columns[_ColumnSlectedIndex].DotNetType;
            //    cmbStorageType.Text= ViewModel.Columns[_ColumnSlectedIndex].StorageType;
            //    cmbScriptType.Text= ViewModel.Columns[_ColumnSlectedIndex].ScriptType;
            //    spnLength.Value= ViewModel.Columns[_ColumnSlectedIndex].Length;
            //    spnMinLength.Value= ViewModel.Columns[_ColumnSlectedIndex].MinLength;
            //    chkIsFixedLength.Checked= ViewModel.Columns[_ColumnSlectedIndex].IsFixedLength;
            //    chkIsNullable.Checked= ViewModel.Columns[_ColumnSlectedIndex].IsNullable;
            //    chkAllowSorting.Checked= ViewModel.Columns[_ColumnSlectedIndex].AllowSorting;
            //    chkAllowFiltering.Checked = ViewModel.Columns[_ColumnSlectedIndex].AllowFiltering;
            //    txtExpression.Text= ViewModel.Columns[_ColumnSlectedIndex].Expression;
            //}
        }
            
        private void EditColumnsForm_Leave(object sender, EventArgs e)
        {
            for(int i=0;i< ViewModel.ActiveColumns.Count;i++)
            {
                ViewModel.ActiveColumns[i] = false;
                foreach (var item in lbxColumns.CheckedItems)
                {
                    if (item.ToString() == ViewModel.Columns[i].Name)
                        ViewModel.ActiveColumns[i] = true;
                   
                }
            }
          
        }

        public int _ColumnSlectedIndex = 0;

    }
}
