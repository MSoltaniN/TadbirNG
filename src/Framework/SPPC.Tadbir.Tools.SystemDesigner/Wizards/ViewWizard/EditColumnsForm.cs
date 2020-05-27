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
            info="Edit Columns";
        }

        public ViewModelClass ViewModel { get; set; }
        public List<ColumnViewModel> ColumnView { get; set; }
        public List<bool> ActiveColumns { get; set; }

        public string info { get; set; }

        private void InitColumnProperties()
        {
            cmbDotNetType.SelectedIndex = 0;
            cmbScriptType.SelectedIndex = 0;
            cmbStorageType.SelectedIndex = 0;
            cmbType.SelectedIndex = 0;

            if(lbxColumns.Items.Count!=0)
            {
                txtName.Text = lbxColumns.Items[0].ToString();
                cmbType.Text = ColumnView[0].Type;
                cmbDotNetType.Text = ColumnView[0].DotNetType;
                cmbStorageType.Text = ColumnView[0].StorageType;
                cmbScriptType.Text = ColumnView[0].ScriptType;
                spnLength.Value = ColumnView[0].Length;
                spnMinLength.Value = ColumnView[0].MinLength;
                chkIsFixedLength.Checked = ColumnView[0].IsFixedLength;
                chkIsNullable.Checked = ColumnView[0].IsNullable;
                chkAllowSorting.Checked = ColumnView[0].AllowSorting;
                chkAllowFiltering.Checked = ColumnView[0].AllowFiltering;
                txtExpression.Text = ColumnView[0].Expression;
            }
            
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadColumns();
            InitColumnProperties();
            SetupBinding();
        }

        private void SetupBinding()
        {
            txtName.DataBindings.Add("Text", ColumnView[_ColumnSlectedIndex], "Name");
            cmbType.DataBindings.Add("Text", ColumnView[_ColumnSlectedIndex], "Type");
            cmbDotNetType.DataBindings.Add("Text", ColumnView[_ColumnSlectedIndex], "DotNetType");
            cmbStorageType.DataBindings.Add("Text", ColumnView[_ColumnSlectedIndex], "StorageType");
            cmbScriptType.DataBindings.Add("Text", ColumnView[_ColumnSlectedIndex], "ScriptType");
            spnLength.DataBindings.Add("Value", ColumnView[_ColumnSlectedIndex], "Length");
            spnMinLength.DataBindings.Add("Value", ColumnView[_ColumnSlectedIndex], "MinLength");
            chkIsFixedLength.DataBindings.Add("Checked", ColumnView[_ColumnSlectedIndex], "IsFixedLength");
            chkIsNullable.DataBindings.Add("Checked", ColumnView[_ColumnSlectedIndex], "IsNullable");
            chkAllowSorting.DataBindings.Add("Checked", ColumnView[_ColumnSlectedIndex], "AllowSorting");
            chkAllowFiltering.DataBindings.Add("Checked", ColumnView[_ColumnSlectedIndex], "AllowFiltering");
            txtExpression.DataBindings.Add("Text", ColumnView[_ColumnSlectedIndex],  "Expression");
        }

        private void LoadColumns()
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
                    if(ActiveColumns.Count==0)
                    {
                        int ItemCount = propertyNames.Count();
                        ActiveColumns = Enumerable.Repeat(true, ItemCount).ToList();
                    }
                    int ItemIndex = 0;
                    foreach (var name in propertyNames)
                    {
                        var column = new ColumnViewModel();
                        column = GetColumn(name, Reflector.GetPropertyType(type, name), type);
                        lbxColumns.Items.Add(column, ActiveColumns[ItemIndex]) ;
                        ItemIndex++;
                    }
                    ItemIndex = 0;
                    if (ColumnView.Count==0)
                    {
                        foreach (var name in propertyNames)
                        {
                            var column = new ColumnViewModel();
                            column = GetColumn(name, Reflector.GetPropertyType(type, name), type);
                            ColumnView.Add(column);
                            ItemIndex++;
                        }
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

        private void lbxColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxColumns.SelectedItem != null && _ColumnSlectedIndex != lbxColumns.SelectedIndex)
            {

                //save last items in model
                ColumnView[_ColumnSlectedIndex].Name =txtName.Text;
                ColumnView[_ColumnSlectedIndex].Type = cmbType.Text;
                ColumnView[_ColumnSlectedIndex].DotNetType = cmbDotNetType.Text;
                ColumnView[_ColumnSlectedIndex].StorageType = cmbStorageType.Text;
                ColumnView[_ColumnSlectedIndex].ScriptType = cmbScriptType.Text;
                ColumnView[_ColumnSlectedIndex].Length = Convert.ToInt32( spnLength.Value);
                ColumnView[_ColumnSlectedIndex].MinLength =Convert.ToInt32( spnMinLength.Value);
                ColumnView[_ColumnSlectedIndex].IsFixedLength = chkIsFixedLength.Checked;
                ColumnView[_ColumnSlectedIndex].IsNullable = chkIsNullable.Checked;
                ColumnView[_ColumnSlectedIndex].AllowSorting = chkAllowSorting.Checked;
                ColumnView[_ColumnSlectedIndex].AllowFiltering = chkAllowFiltering.Checked;
                ColumnView[_ColumnSlectedIndex].Expression = txtExpression.Text;


                _ColumnSlectedIndex = lbxColumns.SelectedIndex;

                //retrive items from model
                txtName.Text = lbxColumns.SelectedItem.ToString();
                cmbType.Text = ColumnView[_ColumnSlectedIndex].Type;
                cmbDotNetType.Text=ColumnView[_ColumnSlectedIndex].DotNetType;
                cmbStorageType.Text=ColumnView[_ColumnSlectedIndex].StorageType;
                cmbScriptType.Text=ColumnView[_ColumnSlectedIndex].ScriptType;
                spnLength.Value=ColumnView[_ColumnSlectedIndex].Length;
                spnMinLength.Value=ColumnView[_ColumnSlectedIndex].MinLength;
                chkIsFixedLength.Checked=ColumnView[_ColumnSlectedIndex].IsFixedLength;
                chkIsNullable.Checked=ColumnView[_ColumnSlectedIndex].IsNullable;
                chkAllowSorting.Checked=ColumnView[_ColumnSlectedIndex].AllowSorting;
                chkAllowFiltering.Checked =ColumnView[_ColumnSlectedIndex].AllowFiltering;
                txtExpression.Text=ColumnView[_ColumnSlectedIndex].Expression;
            }
        }
            
        private void EditColumnsForm_Leave(object sender, EventArgs e)
        {
            for(int i=0;i<ActiveColumns.Count;i++)
            {
                ActiveColumns[i] = false;
                foreach (var item in lbxColumns.CheckedItems)
                {
                    if (item.ToString() == ColumnView[i].Name)
                        ActiveColumns[i] = true;
                   
                }
            }
          
        }

        public int _ColumnSlectedIndex = 0;

    }
}
