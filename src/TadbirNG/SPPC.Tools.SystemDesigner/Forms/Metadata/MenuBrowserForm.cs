using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SPPC.Framework.Extensions;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.ViewModel;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tools.Extensions;
using SPPC.Tools.Model;
using SPPC.Tools.Utility;

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
            this.GetActiveForm().Cursor = Cursors.WaitCursor;
            LoadPermissionData();
            _menus = GetMenus();
            LoadMenus();
            this.GetActiveForm().Cursor = Cursors.Default;
        }

        private void Menus_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            var node = tvMenus.SelectedNode;
            if (node != null)
            {
                var command = node.Tag as CommandViewModel;
                node.Text = txtTitleKey.Text;
                command.Title = txtTitleKey.Text;
                command.RouteUrl = txtRouteUrl.Text;
                command.IconName = txtIconName.Text;
                command.HotKey = txtHotKey.Text;
                command.PermissionId = cmbPermission.SelectedValue != null && (int)cmbPermission.SelectedValue > 0
                    ? (int)cmbPermission.SelectedValue
                    : null;
                if (command.State == RecordState.Unmodified)
                {
                    command.State = RecordState.Edited;
                }
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

            // Currently, menu tree is limited to 3 levels...
            var grandParent = tvMenus.SelectedNode?.Parent?.Parent;
            btnNewChild.Enabled = grandParent == null;
        }

        private void PermissionGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPermissionGroup.SelectedIndex != -1)
            {
                LoadPermissions((int)cmbPermissionGroup.SelectedValue);
            }
        }

        private void NewChild_Click(object sender, EventArgs e)
        {
            var selected = tvMenus.SelectedNode;
            if (selected != null)
            {
                var parentMenu = selected.Tag as CommandViewModel;
                var child = GetNewChildCommand(parentMenu);
                var newNode = new TreeNode(child.Title) { Tag = child };
                selected.Nodes.Add(newNode);
                tvMenus.SelectedNode = newNode;
            }
        }

        private void NewSibling_Click(object sender, EventArgs e)
        {
            var selected = tvMenus.SelectedNode;
            if (selected != null)
            {
                var parentMenu = selected.Parent?.Tag as CommandViewModel;
                var sibling = GetNewChildCommand(parentMenu);
                var newNode = new TreeNode(sibling.Title) { Tag = sibling };
                if (selected.Parent != null)
                {
                    selected.Parent.Nodes.Add(newNode);
                }
                else
                {
                    tvMenus.Nodes.Add(newNode);
                }

                tvMenus.SelectedNode = newNode;
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            var selected = tvMenus.SelectedNode;
            if (selected != null)
            {
                var response = MessageBox.Show(this, "Are you sure you want to delete this menu?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2);
                if (response == DialogResult.Yes)
                {
                    var deletedMenu = selected.Tag as CommandViewModel;
                    deletedMenu.State = RecordState.Deleted;
                    selected.Remove();
                }
            }
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            this.GetActiveForm().Cursor = Cursors.WaitCursor;
            var orderedMenus = GetOrderedCommands();
            GenerateCreateScript(orderedMenus);
            GenerateUpdateScript(orderedMenus);
            this.GetActiveForm().Cursor = Cursors.Default;
            MessageBox.Show(this, "The script was successfully generated.", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            LoadMenus();
        }

        private static CommandViewModel CommandFromRow(DataRow row)
        {
            int? permissionId = row.ValueOrDefault<int>("PermissionID");
            int? parentId = row.ValueOrDefault<int>("ParentID");
            return new CommandViewModel()
            {
                Id = row.ValueOrDefault<int>("CommandID"),
                ParentId = parentId > 0
                    ? parentId
                    : null,
                PermissionId = permissionId > 0
                    ? permissionId
                    : null,
                Title = row.ValueOrDefault("TitleKey"),
                RouteUrl = row.ValueOrDefault("RouteUrl"),
                IconName = row.ValueOrDefault("IconName"),
                HotKey = row.ValueOrDefault("HotKey")
            };
        }

        private static void GenerateCreateScript(IEnumerable<CommandViewModel> commands)
        {
            var scriptBuilder = new StringBuilder();
            var remaining = commands.Where(cmd => cmd.State != RecordState.Deleted);
            scriptBuilder.AppendLine(GetGroupInsertScript(remaining));
            ScriptUtility.ReplaceSysScript(scriptBuilder.ToString());
        }

        private static void GenerateUpdateScript(IEnumerable<CommandViewModel> commands)
        {
            var scriptBuilder = new StringBuilder();
            ScriptUtility.AddVersionMarker(scriptBuilder);
            var deletedIds = commands
                .Where(cmd => cmd.State == RecordState.Deleted)
                .Select(cmd => cmd.Id);
            if (deletedIds.Any())
            {
                scriptBuilder.AppendLine(
$@"DELETE FROM [Metadata].[Command]
WHERE [CommandID] IN({String.Join(", ", deletedIds.ToArray())})");
            }

            var edited = commands.Where(cmd => cmd.State == RecordState.Edited);
            foreach (var command in edited)
            {
                scriptBuilder.AppendLine(command.ToUpdateScript());
                scriptBuilder.AppendLine();
            }

            var added = commands.Where(cmd => cmd.State == RecordState.Added);
            if (added.Any())
            {
                scriptBuilder.AppendLine(GetGroupInsertScript(added));
            }

            int lineCount = scriptBuilder
                .ToString()
                .Count(ch => ch == '\r');
            if (lineCount > 2)
            {
                var path = Path.Combine(PathConfig.ResourceRoot, ScriptUtility.SysUpdateScriptName);
                File.AppendAllText(path, scriptBuilder.ToString(), Encoding.UTF8);
            }
        }

        public static string GetGroupInsertScript(IEnumerable<CommandViewModel> commands)
        {
            var scriptBuilder = new StringBuilder();
            if (commands.Count() == 1)
            {
                scriptBuilder.Append(commands.First().ToScript(true, true));
            }
            else if (commands.Count() > 1)
            {
                scriptBuilder.Append(commands.First().ToScript(true, false));
                foreach (var menu in commands
                    .Skip(1)
                    .Take(commands.Count() - 2))
                {
                    scriptBuilder.Append(menu.ToScript(false, false));
                }

                scriptBuilder.Append(commands.Last().ToScript(false, true));
            }

            return scriptBuilder.ToString();
        }

        private IEnumerable<CommandViewModel> GetOrderedCommands()
        {
            var ordered = new List<CommandViewModel>();
            Array.ForEach(_menus.ToArray(), parent =>
            {
                ordered.Add(parent);
                Array.ForEach(parent.Children.ToArray(), child =>
                {
                    ordered.Add(child);
                    ordered.AddRange(child.Children);
                });
            });

            return ordered.OrderBy(menu => menu.Id);
        }

        private CommandViewModel GetNewChildCommand(CommandViewModel parent)
        {
            int id = GetLastId() + 1;
            var child = new CommandViewModel()
            {
                Title = $"Node{id}",
                Id = id,
                ParentId = parent?.Id,
                State = RecordState.Added
            };
            if (parent != null)
            {
                parent.Children.Add(child);
            }
            else
            {
                _menus.Add(child);
            }

            return child;
        }

        private CommandViewModel GetParentCommand(IList<CommandViewModel> items, CommandViewModel command)
        {
            var parent = default(CommandViewModel);
            foreach (var item in items)
            {
                if (item.Id == command.ParentId)
                {
                    parent = item;
                    break;
                }
                else
                {
                    GetParentCommand(item.Children, command);
                }
            }

            return parent;
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
            LoadChildMenus(null, _menus);
        }

        private void LoadChildMenus(TreeNode node, IList<CommandViewModel> menus)
        {
            foreach (var menu in menus)
            {
                var childNode = new TreeNode(menu.Title) { Tag = menu };
                _ = node != null
                    ? node.Nodes.Add(childNode)
                    : tvMenus.Nodes.Add(childNode);
                LoadChildMenus(childNode, menu.Children);
            }
        }

        private int GetLastId()
        {
            var idValues = new List<int>();
            Array.ForEach(_menus.ToArray(), parent =>
            {
                idValues.Add(parent.Id);
                Array.ForEach(parent.Children.ToArray(), child =>
                {
                    idValues.Add(child.Id);
                    idValues.AddRange(child.Children.Select(c => c.Id));
                });
            });

            return idValues.Max();
        }

        private readonly SqlDataLayer _dal;
        private List<CommandViewModel> _menus;
        private DataTable _groups;
        private DataTable _permissions;
    }
}
