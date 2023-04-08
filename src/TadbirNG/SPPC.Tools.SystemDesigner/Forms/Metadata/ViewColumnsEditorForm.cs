using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SPPC.Framework.Common;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Common;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tools.Extensions;
using SPPC.Tools.Model;
using SPPC.Tools.Utility;

namespace SPPC.Tools.SystemDesigner.Forms
{
    public partial class ViewColumnsEditorForm : Form
    {
        public ViewColumnsEditorForm()
        {
            InitializeComponent();
            View = new ViewViewModel();
        }

        public ViewViewModel View { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.GetActiveForm().Cursor = Cursors.WaitCursor;
            LoadViewModels();
            this.GetActiveForm().Cursor = Cursors.Default;
        }

        private void ViewModels_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Nodes.Count == 0)
            {
                LoadViewProperties(e.Node.Name);
                InitColumns();
            }
            else
            {
                View.Columns.Clear();
                lbxColumns.Items.Clear();
            }
        }

        private void ViewModels_DoubleClick(object sender, EventArgs e)
        {
            tabViewColumns.SelectedTab = tabColumnEditor;
        }

        private void ViewColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabViewColumns.SelectedTab == tabViewEditor && View.Columns.Any())
            {
                short index = 0;
                foreach (var column in View.Columns)
                {
                    column.DisplayIndex = (short)(column.Visibility != "AlwaysHidden"
                        ? index++
                        : -1);
                }
            }
        }

        private void MoveUp_Click(object sender, EventArgs e)
        {
            int selectedIndex = lbxColumns.SelectedIndex;
            if (selectedIndex > 0)
            {
                _moving = true;
                var selection = lbxColumns.Items
                    .OfType<ColumnViewModel>()
                    .Select(col => lbxColumns.GetItemChecked(lbxColumns.Items.IndexOf(col)))
                    .ToArray();
                var current = View.Columns[selectedIndex];
                View.Columns.Remove(current);
                View.Columns.Insert(selectedIndex - 1, current);
                lbxColumns.Items.Clear();
                lbxColumns.Items.AddRange(View.Columns.ToArray());
                int index = 0;
                Array.ForEach(selection, sel =>
                {
                    lbxColumns.SetItemChecked(index++, sel);
                });
                lbxColumns.SelectedIndex = selectedIndex - 1;
                _moving = false;
            }
        }

        private void MoveDown_Click(object sender, EventArgs e)
        {
            int selectedIndex = lbxColumns.SelectedIndex;
            if (selectedIndex < lbxColumns.Items.Count - 1)
            {
                _moving = true;
                var selection = lbxColumns.Items
                    .OfType<ColumnViewModel>()
                    .Select(col => lbxColumns.GetItemChecked(lbxColumns.Items.IndexOf(col)))
                    .ToArray();
                var current = View.Columns[selectedIndex];
                View.Columns.Remove(current);
                View.Columns.Insert(selectedIndex + 1, current);
                lbxColumns.Items.Clear();
                lbxColumns.Items.AddRange(View.Columns.ToArray());
                int index = 0;
                Array.ForEach(selection, sel =>
                {
                    lbxColumns.SetItemChecked(index++, sel);
                });
                lbxColumns.SelectedIndex = selectedIndex + 1;
                _moving = false;
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
            }
        }

        private void Columns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_currentIndex != -1 && !_moving)
            {
                SaveColumnDetails();
            }

            _currentIndex = lbxColumns.SelectedIndex;

            if (!_moving)
            {
                LoadColumnDetails();
            }
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            SaveViewProperties();
            SetIdValues();
            var scriptBuilder = new StringBuilder();
            ScriptUtility.AddSysVersionMarker(scriptBuilder);
            scriptBuilder.AppendLine(View.ToScript());
            scriptBuilder.Append(View.Columns
                .First()
                .ToScript(true, false));
            foreach (var column in View.Columns
                .Skip(1)
                .Take(View.Columns.Count - 2))
            {
                scriptBuilder.Append(column.ToScript(false, false));
            }

            scriptBuilder.AppendLine(View.Columns
                .Last()
                .ToScript(false, true));
            var path = Path.Combine(PathConfig.ApiScriptRoot, ScriptConstants.SysDbUpdateScript);
            File.AppendAllText(path, scriptBuilder.ToString(), Encoding.UTF8);
            MessageBox.Show(this, "Scripts were successfully generated.", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private static ColumnViewModel GetColumn(string name, Type type, Type TargetType)
        {
            string visibility = "Visible";
            if (name.Contains("Id"))
            {
                visibility = "AlwaysHidden";
            }
            else if (name == "RowNo")
            {
                visibility = "AlwaysVisible";
            }

            var column = new ColumnViewModel()
            {
                AllowFiltering = name != "RowNo",
                AllowSorting = name != "RowNo",
                Type = SpecialTypeFromType(name, type),
                DotNetType = type.FullName,
                Name = name,
                ScriptType = ScriptTypeFromType(type),
                StorageType = StorageTypeFromType(type),
                Visibility = visibility
            };

            if (column.DotNetType == typeof(string).FullName)
            {
                var attribute = Reflector.GetPropertyAttribute(TargetType, name, typeof(StringLengthAttribute));
                if (attribute is StringLengthAttribute lengthAttribute)
                {
                    column.Length = lengthAttribute.MaximumLength;
                }
            }

            return column;
        }

        private static string StorageTypeFromType(Type type)
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
                    storageType = "money";
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

        private static string ScriptTypeFromType(Type type)
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

        private static string SpecialTypeFromType(string name, Type type)
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
                specialType = "(not set)";
            }

            return specialType;
        }

        private void LoadViewModels()
        {
            var assembly = typeof(ColumnViewModel).Assembly;
            var all = assembly
                .GetTypes()
                .GroupBy(type => type.Namespace.Replace(_defaultAssembly, String.Empty));
            var root = tvViewModels.Nodes.Add("View Models", "View Models");
            foreach (var grp in all)
            {
                string schemaName = String.IsNullOrEmpty(grp.Key)
                    ? "(no schema)"
                    : grp.Key.TrimStart('.');
                var schema = root.Nodes.Add(schemaName, schemaName);
                foreach (var vmType in grp)
                {
                    string typeName = vmType.Name.EndsWith("ViewModel")
                        ? vmType.Name.Replace("ViewModel", String.Empty)
                        : String.Empty;
                    if (!String.IsNullOrEmpty(typeName))
                    {
                        schema.Nodes.Add(typeName, typeName);
                    }
                }
            }

            if (!String.IsNullOrEmpty(View.Name))
            {
                var nodes = tvViewModels.Nodes.Find(View.Name, true);
                tvViewModels.SelectedNode = nodes.FirstOrDefault() ?? tvViewModels.Nodes[0];
            }
            else
            {
                tvViewModels.SelectedNode = tvViewModels.Nodes[0];
            }
        }

        private void LoadViewProperties(string name)
        {
            txtName.Text = name;
            txtEntityName.Text = name;
            txtFetchUrl.Text = String.Empty;
            txtSearchUrl.Text = String.Empty;
            cmbEntityType.SelectedIndex = 1;
            chkIsHierarchy.Checked = false;
            chkEnableCartable.Checked = false;
        }

        private bool SaveViewProperties()
        {
            if (String.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show(this, "Name is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            View.Name = txtName.Text;
            View.EntityName = txtName.Text;
            View.FetchUrl = txtFetchUrl.Text;
            View.SearchUrl = txtSearchUrl.Text;
            View.Entitytype = cmbEntityType.SelectedIndex > 0
                ? cmbEntityType.SelectedItem.ToString()
                : null;
            View.IsHierarchy = chkIsHierarchy.Checked;
            View.IsCartableIntegrated = chkEnableCartable.Checked;
            RemoveUncheckedColumns();
            return true;
        }

        private void InitColumns()
        {
            var selectedNode = tvViewModels.SelectedNode;
            if (selectedNode != null && selectedNode.Nodes.Count == 0)
            {
                this.GetActiveForm().Cursor = Cursors.WaitCursor;
                var type = typeof(ColumnViewModel).Assembly
                    .GetTypes()
                    .Where(t => t.Name == $"{txtName.Text}ViewModel")
                    .FirstOrDefault();
                if (type != null)
                {
                    View.Columns.Clear();
                    View.Columns.AddRange(Reflector
                        .GetPropertyNames(type)
                        .Select(prop => GetColumn(prop, Reflector.GetPropertyType(type, prop), type)));
                }

                lbxColumns.Items.Clear();
                lbxColumns.Items.AddRange(View.Columns.ToArray());
                this.GetActiveForm().Cursor = Cursors.Default;
            }
        }

        private void SaveColumnDetails()
        {
            var columns = View.Columns;
            columns[_currentIndex].Name = txtColumnName.Text;
            columns[_currentIndex].Type = cmbType.Text;
            columns[_currentIndex].DotNetType = cmbDotNetType.Text;
            columns[_currentIndex].StorageType = cmbStorageType.Text;
            columns[_currentIndex].ScriptType = cmbScriptType.Text;
            columns[_currentIndex].Visibility = cmbVisibility.Text;
            columns[_currentIndex].Length = Convert.ToInt32(spnLength.Value);
            columns[_currentIndex].MinLength = Convert.ToInt32(spnMinLength.Value);
            columns[_currentIndex].IsFixedLength = chkIsFixedLength.Checked;
            columns[_currentIndex].IsNullable = chkIsNullable.Checked;
            columns[_currentIndex].AllowSorting = chkAllowSorting.Checked;
            columns[_currentIndex].AllowFiltering = chkAllowFiltering.Checked;
            columns[_currentIndex].GroupName = txtGroupName.Text;
            columns[_currentIndex].Expression = txtExpression.Text;
        }

        private void LoadColumnDetails()
        {
            var columns = View.Columns;
            txtColumnName.Text = columns[_currentIndex].Name;
            cmbType.Text = columns[_currentIndex].Type;
            cmbDotNetType.Text = columns[_currentIndex].DotNetType;
            cmbStorageType.Text = columns[_currentIndex].StorageType;
            cmbScriptType.Text = columns[_currentIndex].ScriptType;
            cmbVisibility.Text = columns[_currentIndex].Visibility;
            spnLength.Value = columns[_currentIndex].Length;
            spnMinLength.Value = columns[_currentIndex].MinLength;
            chkIsFixedLength.Checked = columns[_currentIndex].IsFixedLength;
            chkIsNullable.Checked = columns[_currentIndex].IsNullable;
            chkAllowSorting.Checked = columns[_currentIndex].AllowSorting;
            chkAllowFiltering.Checked = columns[_currentIndex].AllowFiltering;
            txtGroupName.Text = columns[_currentIndex].GroupName;
            txtExpression.Text = columns[_currentIndex].Expression;
        }

        private void RemoveUncheckedColumns()
        {
            var columns = new ColumnViewModel[View.Columns.Count];
            View.Columns.CopyTo(columns);
            View.Columns.Clear();
            foreach (int index in lbxColumns.CheckedIndices)
            {
                View.Columns.Add(columns[index]);
            }
        }

        private void SetIdValues()
        {
            var dal = new SqlDataLayer(DbConnections.SystemConnection);
            View.Id = (int)dal.QueryScalar("SELECT MAX([ViewID]) + 1 FROM [Metadata].[View]");
            int nextId = (int)dal.QueryScalar("SELECT MAX([ColumnID]) + 1 FROM [Metadata].[Column]");
            Array.ForEach(View.Columns.ToArray(), column =>
            {
                column.Id = nextId++;
                column.ViewId = View.Id;
            });
        }

        private const string _defaultAssembly = "SPPC.Tadbir.ViewModel";
        public int _currentIndex = -1;
        public bool _moving = false;
    }
}
