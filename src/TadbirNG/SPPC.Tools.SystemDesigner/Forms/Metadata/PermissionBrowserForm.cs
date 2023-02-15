using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SPPC.Framework.Extensions;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tools.Extensions;
using SPPC.Tools.Model;
using SPPC.Tools.Utility;

namespace SPPC.Tools.SystemDesigner.Forms
{
    public partial class PermissionBrowserForm : Form
    {
        public PermissionBrowserForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.GetActiveForm().Cursor = Cursors.WaitCursor;
            _allGroups = GetAllPermissionGroups();
            ReloadGroups();
            this.GetActiveForm().Cursor = Cursors.Default;
        }

        private void Permissions_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            if (e.Column.Name == "EntityName")
            {
                e.Column.Name = "Entity Name";
            }
        }

        private void New_Click(object sender, EventArgs e)
        {
            var editor = new PermissionEditorForm();
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                _allGroups.Add(editor.Model);
                ReloadGroups();
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (grdPermissions.SelectedRows.Count == 1)
            {
                var editor = new PermissionEditorForm
                {
                    Model = grdPermissions.SelectedRows[0].DataBoundItem as PermissionGroupViewModel
                };
                if (editor.ShowDialog(this) == DialogResult.OK)
                {
                    var old = _allGroups.Single(grp => grp.Id == editor.Model.Id);
                    int index = _allGroups.IndexOf(old);
                    _allGroups.RemoveAt(index);
                    _allGroups.Insert(index, editor.Model);
                    ReloadGroups();
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (grdPermissions.SelectedRows.Count == 1)
            {
                var result = MessageBox.Show(
                    this, "Are you sure?", "Confirm Delete", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    var itemToDelete = grdPermissions.SelectedRows[0].DataBoundItem
                        as PermissionGroupViewModel;
                    _allGroups.Remove(itemToDelete);
                    ReloadGroups();
                }
            }
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            var scriptBuilder = new StringBuilder();
            var allGroups = grdPermissions.DataSource as List<PermissionGroupViewModel>;
            if (allGroups.Any())
            {
                scriptBuilder.AppendLine(GeneratePermissionGroupScripts(allGroups));

                var allPermissions = new List<PermissionViewModel>();
                foreach (var group in allGroups)
                {
                    allPermissions.AddRange(group.Permissions);
                }

                scriptBuilder.Append(GeneratePermissionScripts(allPermissions));
                ScriptUtility.ReplaceSysScript(scriptBuilder.ToString());
                MessageBox.Show(this, "Scripts were successfully generated.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private static List<PermissionGroupViewModel> GetAllPermissionGroups()
        {
            var allGroups = new List<PermissionGroupViewModel>();
            var dal = new SqlDataLayer(DbConnections.SystemConnection);
            var groups = dal.Query("SELECT * FROM [Auth].[PermissionGroup]");
            var permissions = dal.Query($"SELECT * FROM [Auth].[Permission]");
            foreach (var row in groups.Rows.Cast<DataRow>())
            {
                var group = new PermissionGroupViewModel()
                {
                    Id = row.ValueOrDefault<int>("PermissionGroupID"),
                    Name = row.ValueOrDefault("Name"),
                    EntityName = row.ValueOrDefault("EntityName"),
                    Description = row.ValueOrDefault("Description")
                };
                var filtered = permissions.Select($"GroupID = {group.Id}");
                group.Permissions.AddRange(filtered
                    .Select(row => new PermissionViewModel()
                    {
                        Id = row.ValueOrDefault<int>("PermissionID"),
                        GroupId = row.ValueOrDefault<int>("GroupID"),
                        Name = row.ValueOrDefault("Name"),
                        Flag = row.ValueOrDefault<int>("Flag"),
                        Description = row.ValueOrDefault("Description"),
                        GroupName = group.Name
                    }));
                allGroups.Add(group);
            }

            return allGroups;
        }

        private static string GeneratePermissionGroupScripts(IEnumerable<PermissionGroupViewModel> groups)
        {
            var scriptBuilder = new StringBuilder();
            scriptBuilder.Append(groups.First().ToScript(true, false));
            foreach (var group in groups
                .Skip(1)
                .Take(groups.Count() - 2))
            {
                scriptBuilder.Append(group.ToScript(false, false));
            }

            scriptBuilder.Append(groups.Last().ToScript(false, true));
            return scriptBuilder.ToString();
        }

        private static string GeneratePermissionScripts(IEnumerable<PermissionViewModel> permissions)
        {
            var scriptBuilder = new StringBuilder();
            scriptBuilder.Append(permissions.First().ToScript(true, false));
            foreach (var group in permissions
                .Skip(1)
                .Take(permissions.Count() - 2))
            {
                scriptBuilder.Append(group.ToScript(false, false));
            }

            scriptBuilder.Append(permissions.Last().ToScript(false, true));
            return scriptBuilder.ToString();
        }

        private void ReloadGroups()
        {
            grdPermissions.DataSource = null;
            grdPermissions.DataSource = _allGroups;
        }

        private List<PermissionGroupViewModel> _allGroups;
    }
}
