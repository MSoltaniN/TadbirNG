using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SPPC.Framework.Common;
using SPPC.Tools.Model;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Framework.Persistence;
using SPPC.Tools.Utility;

namespace SPPC.Tools.SystemDesigner.Designers
{
    public partial class PermissionEditorForm : Form
    {
        public PermissionEditorForm()
        {
            InitializeComponent();
            Model = new PermissionDesignerModel();
            _sysConnection = DbConnections.SystemConnection;
        }

        public PermissionDesignerModel Model { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupBindings();
            lboxPermissions.DisplayMember = "Name";
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
            lboxPermissions.SelectedIndex = 0;
        }

        private void Permissions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lboxPermissions.SelectedIndex != _lastselectedIndex)
            {
                SavePermission(_lastselectedIndex);
                _lastselectedIndex = lboxPermissions.SelectedIndex;
                LoadPermission(_lastselectedIndex);
                ShowFlagLabel();
            }
        }

        private void AddPermission_Click(object sender, EventArgs e)
        {
            var newItemIndex = lboxPermissions.Items.Count;
            var permission = GetPermission(String.Format("(Pemission {0})", newItemIndex + 1), newItemIndex);
            lboxPermissions.Items.Add(permission);
            lboxPermissions.SelectedIndex = newItemIndex;
        }

        private void DeletePermission_Click(object sender, EventArgs e)
        {
            if(lboxPermissions.SelectedIndex != -1)
            {
                int deletedIndex = lboxPermissions.SelectedIndex;
                int newCount = lboxPermissions.Items.Count - 1;
                int selectedIndex = (deletedIndex < newCount)
                    ? deletedIndex
                    : deletedIndex - 1;
                _lastselectedIndex = -1;
                lboxPermissions.Items.RemoveAt(deletedIndex);
                lboxPermissions.SelectedIndex = selectedIndex;
            }
        }

        private void GenarateScript_Click(object sender, EventArgs e)
        {
            if (!SaveAndValidateModel())
            {
                return;
            }

            var dal = new SqlDataLayer(_sysConnection);
            int maxGroupId = Convert.ToInt32(
                dal.QueryScalar("SELECT MAX(PermissionGroupID) FROM [Auth].[PermissionGroup]"));
            int maxPermissionId = Convert.ToInt32(
                dal.QueryScalar("SELECT MAX(PermissionID) FROM [Auth].[Permission]"));
            var group = Model.PermissionGroup;
            var builder = new StringBuilder();
            var solutionVersion = GetSolutionVersion();

            int permissionId = maxPermissionId + 1;
            builder.AppendLine();
            builder.AppendLine();
            builder.AppendFormat("-- {0}", solutionVersion);
            builder.AppendLine();
            builder.AppendLine("SET IDENTITY_INSERT [Auth].[PermissionGroup] ON");
            builder.AppendFormat(
                @"INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName] , [Description])
    VALUES ({0}, N'{1}', {2} , {3})"
                    , maxGroupId + 1
                    , group.Name
                    , GetNullableValue(group.EntityName)
                    , GetNullableValue(group.Description));

            builder.AppendLine();
            builder.AppendLine("SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF");
            builder.AppendLine();
            builder.AppendLine("SET IDENTITY_INSERT [Auth].[Permission] ON");

            foreach (var item in Model.Permissions)
            {
                builder.AppendFormat(
                    @"INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag] , [Description])
    VALUES ({0}, {1}, N'{2}', {3}, {4})"
                        , permissionId++
                        , maxGroupId + 1
                        , item.Name
                        , item.Flag
                        , GetNullableValue(item.Description));
                builder.AppendLine();
            }

            builder.AppendLine("SET IDENTITY_INSERT[Auth].[Permission] OFF");
            builder.AppendLine();

            File.AppendAllText(_TadbirSysUpdateScript, builder.ToString());
            MessageBox.Show("The script was successfully generated.");
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

        private static string GetNullableValue(string nullable)
        {
            return String.IsNullOrEmpty(nullable)
                ? "NULL"
                : String.Format("N'{0}'", nullable);
        }

        private void SetupBindings()
        {
            txtGroupName.DataBindings.Add("Text", Model.PermissionGroup, "Name");
            txtEntityName.DataBindings.Add("Text", Model.PermissionGroup, "EntityName");
            txtGroupDescription.DataBindings.Add("Text", Model.PermissionGroup, "Description");
        }

        private void ApplyPermissionGroup()
        {
            var dal = new SqlDataLayer(_sysConnection);
            var lastId = dal.QueryScalar("SELECT MAX(pg.PermissionGroupID) FROM [Auth].[PermissionGroup] pg");
            Model.PermissionGroup.Id = Convert.ToInt32(lastId)+1;
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

        private void LoadDefaultPermissions()
        {
            var permissionNames = new List<string> { "View", "Create", "Edit", "Delete", "Filter", "Print" };
            int itemIndex = 0;
            foreach (var name in permissionNames)
            {
                lboxPermissions.Items.Add(GetPermission(name, itemIndex++));
            }
        }

        private void SavePermission(int index)
        {
            if (index != -1 && ValidatePermission())
            {
                var permission = lboxPermissions.Items[index] as PermissionViewModel;
                permission.Name = txtName.Text;
                permission.Flag = Convert.ToInt32(Math.Pow(2, trbFlag.Value));
                permission.Description = txtDescription.Text;
                lboxPermissions.Items.RemoveAt(index);
                lboxPermissions.Items.Insert(index, permission);
            }
        }

        private void LoadPermission(int index)
        {
            if(index != -1)
            {
                var permission = lboxPermissions.Items[index] as PermissionViewModel;
                txtName.Text = permission.Name;
                trbFlag.Value = Convert.ToInt32(Math.Log(permission.Flag, 2));
                txtDescription.Text = permission.Description;
            }
        }

        private void ShowFlagLabel()
        {
            var flagValue = Math.Pow(2, trbFlag.Value).ToString();
            lblFlag.Text = "Flag:" + flagValue;
        }

        private bool SaveAndValidateModel()
        {
            if (lboxPermissions.Items.Count == 0)
            {
                MessageBox.Show("Please enter at least one permission.");
                return false;
            }

            int invalidItemCount = lboxPermissions.Items
                .Cast<PermissionViewModel>()
                .Where(perm => String.IsNullOrWhiteSpace(perm.Name))
                .Count();
            if (invalidItemCount > 0)
            {
                MessageBox.Show("One or more permissions have blank name.");
                return false;
            }

            Model.Permissions.AddRange(lboxPermissions.Items.Cast<PermissionViewModel>());
            return true;
        }

        private Version GetSolutionVersion()
        {
            var assemblyVersion = GetType().Assembly.GetName().Version;
            return new Version(assemblyVersion.ToString(3));
        }

        private readonly string _sysConnection;
        private int _lastselectedIndex = -1;
        private const string _TadbirSysUpdateScript = @"..\..\res\TadbirSys_UpdateDbObjects.sql";
    }
}
