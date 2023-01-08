using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SPPC.Framework.Extensions;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tools.Model;

namespace SPPC.Tools.SystemDesigner.Forms
{
    public partial class MenuBrowserForm : Form
    {
        public MenuBrowserForm()
        {
            InitializeComponent();
            _dal = new SqlDataLayer(DbConnections.SystemConnection);
        }

        private class PermissionItem
        {
            public int? Id { get; set; }

            public string Name { get; set; }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ActiveForm.Cursor = Cursors.WaitCursor;
            LoadPermissionData();
            _menus = GetMenus();
            LoadMenus();
            ActiveForm.Cursor = Cursors.Default;
        }

        private void Menus_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            var node = tvMenus.SelectedNode;
            if (node != null)
            {
                var command = node.Tag as CommandViewModel;
                command.Title = txtTitleKey.Text;
                command.RouteUrl = txtRouteUrl.Text;
                command.IconName = txtIconName.Text;
                command.HotKey = txtHotKey.Text;
                command.PermissionId = cmbPermission.SelectedValue != null && (int)cmbPermission.SelectedValue > 0
                    ? (int)cmbPermission.SelectedValue
                    : null;
            }
        }

        private void Menus_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var command = e.Node.Tag as CommandViewModel;
            txtTitleKey.Text = command.Title;
            txtRouteUrl.Text = command.RouteUrl;
            txtIconName.Text = command.IconName;
            txtHotKey.Text = command.HotKey;
            SetPermissionGroup(command.PermissionId ?? 0);
            cmbPermission.SelectedValue = command.PermissionId ?? 0;
        }

        private void PermissionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPermissionGroup.SelectedIndex != -1)
            {
                LoadPermissions((int)cmbPermissionGroup.SelectedValue);
            }
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            LoadMenus();
        }

        private static CommandViewModel CommandFromRow(DataRow row)
        {
            int? permissionId = row.ValueOrDefault<int>("PermissionID");
            return new CommandViewModel()
            {
                Id = row.ValueOrDefault<int>("CommandID"),
                ParentId = row.ValueOrDefault<int>("ParentID"),
                PermissionId = permissionId > 0
                    ? permissionId
                    : null,
                Title = row.ValueOrDefault("TitleKey"),
                RouteUrl = row.ValueOrDefault("RouteUrl"),
                IconName = row.ValueOrDefault("IconName"),
                HotKey = row.ValueOrDefault("HotKey")
            };
        }

        private void LoadPermissionData()
        {
            _permissions= _dal.Query(@$"
SELECT PermissionID, GroupID, Name
FROM [Auth].[Permission]");

            _groups = _dal.Query(@"
SELECT PermissionGroupID, Name
FROM [Auth].[PermissionGroup]");
            cmbPermissionGroup.DisplayMember = "Name";
            cmbPermissionGroup.ValueMember = "Id";
            cmbPermissionGroup.DataSource = _groups.Rows
                .Cast<DataRow>()
                .Select(row => new PermissionItem()
                {
                    Id = row.ValueOrDefault<int>("PermissionGroupID"),
                    Name = row.ValueOrDefault("Name")
                })
                .Prepend(new PermissionItem()
                {
                    Id = 0,
                    Name = "(None)"
                })
                .ToList();
        }

        private void SetPermissionGroup(int permissionId)
        {
            int groupId = _permissions
                .Select($"PermissionID = {permissionId}")
                .Select(row => row.ValueOrDefault<int>("GroupID"))
                .SingleOrDefault();
            cmbPermissionGroup.SelectedValue = groupId;
        }

        private void LoadPermissions(int groupId)
        {
            ActiveForm.Cursor = Cursors.WaitCursor;
            var permissions = _permissions.Select($"GroupID = {groupId}")
                .Select(row => new PermissionItem()
                {
                    Id = row.ValueOrDefault<int>("PermissionID"),
                    Name = row.ValueOrDefault("Name")
                })
                .Prepend(new PermissionItem()
                {
                    Id = 0,
                    Name = "(None)"
                })
                .ToList();
            cmbPermission.DataSource = null;
            cmbPermission.DataSource = permissions;
            cmbPermission.DisplayMember = "Name";
            cmbPermission.ValueMember = "Id";
            ActiveForm.Cursor = Cursors.Default;
        }

        private List<CommandViewModel> GetMenus()
        {
            // Current implementation supports 3 menu levels, which is all the levels we have now
            // To support unlimited child levels, a recursive method will be required.
            var result = _dal.Query("SELECT * FROM [Metadata].[Command]");
            var menus = new List<CommandViewModel>();
            menus.AddRange(result
                .Select("ParentID IS NULL")
                .Where(row => row.ValueOrDefault("TitleKey") != "Profile")
                .Select(row => CommandFromRow(row)));
            foreach (var menu in menus)
            {
                var children = result.Select($"ParentID = {menu.Id}");
                Array.ForEach(children, row => menu.Children.Add(CommandFromRow(row)));
                foreach (var child in menu.Children)
                {
                    var grandChildren = result.Select($"ParentID = {child.Id}");
                    Array.ForEach(grandChildren, row => child.Children.Add(CommandFromRow(row)));
                }
            }

            return menus;
        }

        private void LoadMenus()
        {
            tvMenus.Nodes.Clear();
            foreach (var menu in _menus)
            {
                var node = new TreeNode(menu.Title) { Tag = menu };
                tvMenus.Nodes.Add(node);
                LoadChildMenus(node, menu.Children);
            }
        }

        private void LoadChildMenus(TreeNode node, IList<CommandViewModel> menus)
        {
            foreach (var menu in menus)
            {
                var childNode = new TreeNode(menu.Title) { Tag = menu };
                node.Nodes.Add(childNode);
                LoadChildMenus(childNode, menu.Children);
            }
        }

        private readonly SqlDataLayer _dal;
        private List<CommandViewModel> _menus;
        private DataTable _groups;
        private DataTable _permissions;
    }
}
