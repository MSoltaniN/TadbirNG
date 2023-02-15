using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SPPC.Tools.Model;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Framework.Persistence;
using SPPC.Framework.Extensions;
using SPPC.Tools.Extensions;
using SPPC.Tools.Utility;

namespace SPPC.Tools.SystemDesigner.Forms
{
    public partial class PermissionEditorForm : Form
    {
        public PermissionEditorForm()
        {
            InitializeComponent();
            Model = new PermissionGroupViewModel();
            _sysConnection = DbConnections.SystemConnection;
        }

        public PermissionGroupViewModel Model { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupBindings();
            SeedIdentities();
            LoadPermissions();
            lstPermissions.DisplayMember = "Name";
        }

        private void GroupName_Enter(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtGroupName.Text) && !String.IsNullOrEmpty(txtEntityName.Text))
            {
                txtGroupName.Text = String.Format("ManageEntities,{0}", txtEntityName.Text.ToPlural());
            }
        }

        private void AddDefaultPermissions_Click(object sender, EventArgs e)
        {
            if(!ValidateGroup())
            {
                return;
            }

            ApplyPermissionGroup();
            LoadDefaultPermissions();
            lstPermissions.SelectedIndex = 0;
        }

        private void Permissions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lstPermissions.SelectedIndex != _lastselectedIndex)
            {
                SavePermission(_lastselectedIndex);
                _lastselectedIndex = lstPermissions.SelectedIndex;
                LoadPermission(_lastselectedIndex);
                ShowFlagLabel();
            }
        }

        private void AddPermission_Click(object sender, EventArgs e)
        {
            var newItemIndex = lstPermissions.Items.Count;
            var permission = GetPermission(String.Format("(Pemission {0})", newItemIndex + 1), newItemIndex);
            lstPermissions.Items.Add(permission);
            lstPermissions.SelectedIndex = newItemIndex;
        }

        private void DeletePermission_Click(object sender, EventArgs e)
        {
            if(lstPermissions.SelectedIndex != -1)
            {
                int deletedIndex = lstPermissions.SelectedIndex;
                int newCount = lstPermissions.Items.Count - 1;
                int selectedIndex = (deletedIndex < newCount)
                    ? deletedIndex
                    : deletedIndex - 1;
                _lastselectedIndex = -1;
                lstPermissions.Items.RemoveAt(deletedIndex);
                lstPermissions.SelectedIndex = selectedIndex;
            }
        }

        private void GenarateScript_Click(object sender, EventArgs e)
        {
            if (!ValidateAndSaveModel())
            {
                return;
            }

            var builder = new StringBuilder();
            ScriptUtility.AddSysVersionMarker(builder);
            builder.AppendLine(Model.ToScript());

            if (Model.Permissions.Count == 1)
            {
                builder.Append(Model.Permissions.First().ToScript());
            }
            else
            {
                builder.Append(Model.Permissions.First().ToScript(true, false));
                foreach (var permission in Model.Permissions
                    .Skip(1)
                    .Take(Model.Permissions.Count - 2))
                {
                    builder.Append(permission.ToScript(false, false));
                }

                builder.Append(Model.Permissions.Last().ToScript(false, true));
            }

            var path = Path.Combine(PathConfig.ResourceRoot, ScriptUtility.SysUpdateScriptName);
            File.AppendAllText(path, builder.ToString());
            MessageBox.Show(this, "The script was successfully generated.", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Flag_Scroll(object sender, EventArgs e)
        {
            ShowFlagLabel();
        }

        private static PermissionViewModel GetPermission(string name, int index)
        {
            var Permission = new PermissionViewModel()
            {
                Id = index,
                Name = name,
                Flag = (int)Math.Pow(2, index)
            };
            return Permission;
        }

        private void SetupBindings()
        {
            txtGroupName.DataBindings.Add("Text", Model, "Name");
            txtEntityName.DataBindings.Add("Text", Model, "EntityName");
            txtGroupDescription.DataBindings.Add("Text", Model, "Description");
        }

        private void SeedIdentities()
        {
            var dal = new SqlDataLayer(_sysConnection);
            _maxGroupId = Convert.ToInt32(
                dal.QueryScalar("SELECT MAX(PermissionGroupID) FROM [Auth].[PermissionGroup]"));
            _maxPermissionId = Convert.ToInt32(
                dal.QueryScalar("SELECT MAX(PermissionID) FROM [Auth].[Permission]"));
        }

        private void ApplyPermissionGroup()
        {
            var dal = new SqlDataLayer(_sysConnection);
            var lastId = dal.QueryScalar("SELECT MAX(pg.PermissionGroupID) FROM [Auth].[PermissionGroup] pg");
            Model.Id = Convert.ToInt32(lastId) + 1;
        }

        private bool ValidateGroup()
        {
            bool validated = true;
            if (String.IsNullOrWhiteSpace(txtGroupName.Text))
            {
                MessageBox.Show("Please enter a name for permission group.");
                validated = false;
            }

            return validated;
        }

        private bool ValidatePermission()
        {
            bool validated = true;
            if (String.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter a name for permission.");
                validated = false;
            }

            return validated;
        }

        private void LoadPermissions()
        {
            foreach (var permission in Model.Permissions)
            {
                lstPermissions.Items.Add(permission);
            }
        }

        private void LoadDefaultPermissions()
        {
            if (Model.Permissions.Any())
            {
                var result = MessageBox.Show(this, "There are existing permissions. Do you want to overwrite them?",
                    "Confirm Overwrite", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            Model.Permissions.Clear();
            var permissionNames = new List<string>
            {
                "View", "Filter", "Print", "Export", "Create", "Edit", "Delete"
            };
            int itemIndex = 0;
            foreach (var name in permissionNames)
            {
                lstPermissions.Items.Add(GetPermission(name, itemIndex++));
            }
        }

        private void SavePermission(int index)
        {
            if (index != -1 && ValidatePermission())
            {
                var permission = lstPermissions.Items[index] as PermissionViewModel;
                permission.Name = txtName.Text;
                permission.Flag = Convert.ToInt32(Math.Pow(2, trbFlag.Value));
                permission.Description = txtDescription.Text;
                lstPermissions.Items.RemoveAt(index);
                lstPermissions.Items.Insert(index, permission);
            }
        }

        private void LoadPermission(int index)
        {
            if(index != -1)
            {
                var permission = lstPermissions.Items[index] as PermissionViewModel;
                txtName.Text = permission.Name;
                trbFlag.Value = Convert.ToInt32(Math.Log(permission.Flag, 2));
                txtDescription.Text = permission.Description;
            }
        }

        private void ShowFlagLabel()
        {
            var flagValue = Math.Pow(2, trbFlag.Value).ToString();
            lblFlag.Text = $"Flag: {flagValue}";
        }

        private bool ValidateAndSaveModel()
        {
            if (lstPermissions.Items.Count == 0)
            {
                MessageBox.Show("Please enter at least one permission.");
                return false;
            }

            int invalidItemCount = lstPermissions.Items
                .Cast<PermissionViewModel>()
                .Count(perm => String.IsNullOrWhiteSpace(perm.Name));
            if (invalidItemCount > 0)
            {
                MessageBox.Show("One or more permissions have blank name.");
                return false;
            }

            Model.Permissions.Clear();
            Model.Permissions.AddRange(lstPermissions.Items.Cast<PermissionViewModel>());
            Model.Id = _maxGroupId + 1;
            int permissionId = _maxPermissionId + 1;
            foreach (var permission in Model.Permissions)
            {
                permission.GroupId = Model.Id;
                permission.Id = permissionId++;
            }

            return true;
        }

        private readonly string _sysConnection;
        private int _maxGroupId;
        private int _maxPermissionId;
        private int _lastselectedIndex = -1;
    }
}
