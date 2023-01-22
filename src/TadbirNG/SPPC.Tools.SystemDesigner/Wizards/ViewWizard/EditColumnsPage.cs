using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Forms;
using SPPC.Framework.Common;
using SPPC.Tadbir.ViewModel.Finance;
using SPPC.Tadbir.ViewModel.Metadata;

namespace SPPC.Tools.SystemDesigner.Wizards.ViewWizard
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
        }

        private void InitColumns(bool isDefault)
        {
            if (isDefault)
            {
                if (!String.IsNullOrEmpty(ViewName))
                {
                    var type = typeof(AccountViewModel).Assembly
                        .GetTypes()
                        .Where(t => t.Name == ViewName + "ViewModel")
                        .FirstOrDefault();
                    if (type != null)
                    {
                        var propertyNames = Reflector.GetPropertyNames(type);
                        int itemCount = propertyNames.Count();
                        foreach (var name in propertyNames)
                        {
                            var column = GetColumn(name, Reflector.GetPropertyType(type, name), type);
                            Columns.Add(column);
                        }
                    }
                }
            }
        }

        private ColumnViewModel GetColumn(string name, Type type, Type TargetType)
        {
            var column = new ColumnViewModel()
            {
                AllowFiltering = name != "RowNo",
                AllowSorting = name != "RowNo",
                Type = SpecialTypeFromType(name, type),
                DotNetType = type.FullName,
                Name = name,
                ScriptType = ScriptTypeFromType(type),
                StorageType = StorageTypeFromType(type),
                Visibility = (name.Contains("Id") ? "AlwaysHidden" : (name=="RowNo" ? "AlwaysVisible" : "Visible"))
            };
            
            if (column.ScriptType == "string")
            {
                if (Reflector.GetPropertyAttribute(
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

        private string SpecialTypeFromType(string name, Type type)
        {
            string specialType;
            if (name.Contains("Date") || type == typeof(DateTime))
            {
                specialType = "Default";
            }
            else if (name.Contains("Currency"))
            {
                specialType = "Currency";
            }
            else if (type == typeof(decimal))
            {
                specialType = "Money";
            }
            else
            {
                specialType = null;
            }

            return specialType;
        }

        private void LoadColumns()
        {
            foreach (var column in Columns)
            {
                lbxColumns.Items.Add(column, false);
            }
        }

        private void Columns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxColumns.SelectedItem == null)
            {
                return;
            }
           
            int tmpSelectedIndex = Columns
                .IndexOf(Columns.Where(p => p.Name == lbxColumns.SelectedItem.ToString())
                .SingleOrDefault());

            if (tmpSelectedIndex != _columnSelectedIndex && lbxColumns.SelectedIndex != -1)
            {
                SaveColumnDetails(_columnSelectedIndex != -1);
                _columnSelectedIndex = tmpSelectedIndex;
                RetrieveColumnDetails();
            }
        }

        private void SaveColumnDetails(bool dataExist)
        {
            if(dataExist)
            {
                Columns[_columnSelectedIndex].Name = txtName.Text;
                Columns[_columnSelectedIndex].Type = cmbType.Text;
                Columns[_columnSelectedIndex].DotNetType = cmbDotNetType.Text;
                Columns[_columnSelectedIndex].StorageType = cmbStorageType.Text;
                Columns[_columnSelectedIndex].ScriptType = cmbScriptType.Text;
                Columns[_columnSelectedIndex].Visibility = cmbVisibility.Text;
                Columns[_columnSelectedIndex].Length = Convert.ToInt32(spnLength.Value);
                Columns[_columnSelectedIndex].MinLength = Convert.ToInt32(spnMinLength.Value);
                Columns[_columnSelectedIndex].IsFixedLength = chkIsFixedLength.Checked;
                Columns[_columnSelectedIndex].IsNullable = chkIsNullable.Checked;
                Columns[_columnSelectedIndex].AllowSorting = chkAllowSorting.Checked;
                Columns[_columnSelectedIndex].AllowFiltering = chkAllowFiltering.Checked;
                Columns[_columnSelectedIndex].GroupName = txtGroupName.Text;
                Columns[_columnSelectedIndex].Expression = txtExpression.Text;
            }
        }

        private void RetrieveColumnDetails()
        {
            txtName.Text = Columns[_columnSelectedIndex].Name;
            cmbType.Text = Columns[_columnSelectedIndex].Type == "" ? "(not set)" : Columns[_columnSelectedIndex].Type;
            cmbDotNetType.Text = Columns[_columnSelectedIndex].DotNetType;
            cmbStorageType.Text = Columns[_columnSelectedIndex].StorageType;
            cmbScriptType.Text = Columns[_columnSelectedIndex].ScriptType;
            cmbVisibility.Text = Columns[_columnSelectedIndex].Visibility;
            spnLength.Value = Columns[_columnSelectedIndex].Length;
            spnMinLength.Value = Columns[_columnSelectedIndex].MinLength;
            chkIsFixedLength.Checked = Columns[_columnSelectedIndex].IsFixedLength;
            chkIsNullable.Checked = Columns[_columnSelectedIndex].IsNullable;
            chkAllowSorting.Checked = Columns[_columnSelectedIndex].AllowSorting;
            chkAllowFiltering.Checked = Columns[_columnSelectedIndex].AllowFiltering;
            txtGroupName.Text = Columns[_columnSelectedIndex].GroupName;
            txtExpression.Text = Columns[_columnSelectedIndex].Expression;
        }

        private void MoveUp_Click(object sender, EventArgs e)
        {
            if(lbxColumns.SelectedIndex>0)
            {
                int itemIndex = lbxColumns.SelectedIndex;
                var tmpValue = lbxColumns.Items[itemIndex-1];
                bool tmpCheckState = lbxColumns.GetItemChecked(itemIndex - 1);
                lbxColumns.Items[itemIndex - 1] = lbxColumns.Items[itemIndex];
                lbxColumns.SetItemChecked(itemIndex - 1,lbxColumns.GetItemChecked(itemIndex));
                lbxColumns.Items[itemIndex] = tmpValue;
                lbxColumns.SetItemChecked(itemIndex, tmpCheckState);
                lbxColumns.SelectedIndex = itemIndex - 1;
            }
        }

        private void MoveDown_Click(object sender, EventArgs e)
        {
            if (lbxColumns.SelectedIndex < lbxColumns.Items.Count-1)
            {
                int itemIndex = lbxColumns.SelectedIndex;
                var tmpValue = lbxColumns.Items[itemIndex + 1];
                bool tmpCheckState = lbxColumns.GetItemChecked(itemIndex + 1);
                lbxColumns.Items[itemIndex + 1] = lbxColumns.Items[itemIndex];
                lbxColumns.SetItemChecked(itemIndex + 1, lbxColumns.GetItemChecked(itemIndex));
                lbxColumns.Items[itemIndex] = tmpValue;
                lbxColumns.SetItemChecked(itemIndex, tmpCheckState);
                lbxColumns.SelectedIndex = itemIndex + 1;
            }
        }
      
        private void DeselectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lbxColumns.Items.Count; i++)
            {
                lbxColumns.SetItemChecked(i, false);
            }
        }

        private void SelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lbxColumns.Items.Count; i++)
            {
                lbxColumns.SetItemChecked(i, true);
                lbxColumns.SelectedIndex = i;
            }
               
        }
        
        private void EditColumnsForm_Leave(object sender, EventArgs e)
        {
            SaveColumnDetails(_columnSelectedIndex != -1);
            int AlwaysVisibleCount = 0;
            foreach (var column in lbxColumns.CheckedItems.OfType<ColumnViewModel>())
            {
                if (column.Visibility == "AlwaysVisible")
                {
                    AlwaysVisibleCount++;
                }
            }
               
            if (AlwaysVisibleCount == 0)
            {
                MessageBox.Show("Please set at least one column as AlwaysVisible.", "Warnings");
                return;
            }

            Columns.Clear();
            foreach (var column in lbxColumns.CheckedItems.OfType<ColumnViewModel>())
            {
                Columns.Add(column);
            }
        }

        public int _columnSelectedIndex = -1;
    }
}
