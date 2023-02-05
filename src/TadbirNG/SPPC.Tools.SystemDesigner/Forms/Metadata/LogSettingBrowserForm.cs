using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.ViewModel.Config;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tools.Extensions;
using SPPC.Tools.Model;
using SPPC.Tools.Utility;

namespace SPPC.Tools.SystemDesigner.Forms
{
    public partial class LogSettingBrowserForm : Form
    {
        public LogSettingBrowserForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadCurrentSettings();
        }

        private void SystemSettings_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSystemSettings.Checked)
            {
                LoadCurrentSysSettings();
            }
            else
            {
                LoadCurrentSettings();
            }
        }

        private void LogMetadata_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var selected = tvLogMetadata.SelectedNode;
            LoadProperty(selected, "Name", txtName);
            LoadProperty(selected, "Description", txtDescription);
            if (e.Node.Text == "Entities")
            {
                btnNew.Enabled = true;
                btnNew.Text = "Add Entity";
            }
            else if (e.Node.Text == "Sources")
            {
                btnNew.Enabled = true;
                btnNew.Text = "Add Source";
            }
            else if (e.Node.Tag is EntityTypeViewModel
                || e.Node.Tag is OperationSourceViewModel)
            {
                btnNew.Enabled = true;
                btnNew.Text = "Manage Log Settings";
            }
            else if (e.Node.Tag is SubsystemViewModel)
            {
                btnNew.Enabled = true;
                btnNew.Text = "Add Subsystem";
            }
            else
            {
                btnNew.Text = "Add";
                btnNew.Enabled = false;
            }
        }

        private void LogMetadata_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            var selected = tvLogMetadata.SelectedNode;
            if (selected != null)
            {
                SaveProperty(selected, "Name", txtName);
                SaveProperty(selected, "Description", txtDescription);
            }
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            if (chkSystemSettings.Checked)
            {
                GenerateSysScripts();
            }
            else
            {
                GenerateScripts();
            }

            MessageBox.Show(this, "Script was successfully generated.",
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void SaveProperty(TreeNode node, string propertyName, TextBox propertySource)
        {
            var tag = node.Tag;
            if (tag != null)
            {
                bool hasProperty = Reflector
                    .GetPropertyNames(tag)
                    .FirstOrDefault(name => name == propertyName) != null;
                if (hasProperty)
                {
                    Reflector.SetProperty(tag, propertyName, propertySource.Text);
                    //node.Text = propertySource.Text;
                }
            }
        }

        private static void LoadProperty(TreeNode node, string propertyName, TextBox propertyTarget)
        {
            var tag = node.Tag;
            if (tag != null)
            {
                bool hasProperty = Reflector
                    .GetPropertyNames(tag)
                    .FirstOrDefault(name => name == propertyName) != null;
                if (hasProperty)
                {
                    propertyTarget.Text = (string)Reflector.GetProperty(tag, propertyName);
                }
            }
        }

        private static void AddLogSettings(DataTable logSettings, IEnumerable<TreeNode> nodes)
        {
            var settingItems = new List<LogSettingViewModel>();
            foreach (var node in nodes)
            {
                if (node.Tag is EntityTypeViewModel entity)
                {
                    var entityRows = logSettings.Select($"EntityTypeID = {entity.Id}");
                    settingItems.AddRange(entityRows
                        .Select(row => SettingItemFromRow(row)));
                }
                else if (node.Tag is OperationSourceViewModel source)
                {
                    var sourceRows = logSettings.Select($"SourceID = {source.Id}");
                    settingItems.AddRange(sourceRows
                        .Select(row => SettingItemFromRow(row)));
                }

                node.Nodes.AddRange(settingItems
                    .Select(item => new TreeNode(item.OperationName)
                    {
                        Tag = item,
                        Checked = true
                    })
                    .ToArray());
                settingItems.Clear();
            }
        }

        private static LogSettingViewModel SettingItemFromRow(DataRow row)
        {
            int entityId = row.ValueOrDefault<int>("EntityTypeID");
            int sourceId = row.ValueOrDefault<int>("SourceID");
            return new LogSettingViewModel()
            {
                Id = Math.Max(row.ValueOrDefault<int>("LogSettingID"),
                    row.ValueOrDefault<int>("SysLogSettingID")),
                EntityTypeId = entityId > 0 ? entityId : null,
                SourceId = sourceId > 0 ? sourceId : null,
                OperationId = row.ValueOrDefault<int>("OperationID"),
                OperationName = row.ValueOrDefault("OperationName"),
                IsEnabled = row.ValueOrDefault<bool>("IsEnabled")
            };
        }

        #region Manage LogSettings

        private void LoadCurrentSettings()
        {
            this.GetActiveForm().Cursor = Cursors.WaitCursor;
            tvLogMetadata.Nodes.Clear();
            var dal = new SqlDataLayer(DbConnections.CompanyConnection);
            var logSettings = dal.Query(ToolsQuery.LogSettings);
            AddSubsystems(logSettings);
            AddSourceTypes(logSettings);
            AddEntitiesAndSources(logSettings);
            AddLogSettings(logSettings, GetAllSourceEntityItems());
            this.GetActiveForm().Cursor = Cursors.Default;
        }

        private void AddSubsystems(DataTable logSettings)
        {
            var subsystems = new List<SubsystemViewModel>();
            var groups = logSettings.Rows
                .Cast<DataRow>()
                .GroupBy(row => row.ValueOrDefault<int>("SubsystemID"));
            subsystems.AddRange(groups
                .Select(grp => new SubsystemViewModel()
                {
                    Id = grp.Key,
                    Name = grp.First().ValueOrDefault("SubsystemName")
                }));
            Array.ForEach(subsystems
                .OrderBy(sub => sub.Name)
                .ToArray(), subsystem =>
                {
                    var node = new TreeNode(subsystem.Name) { Tag = subsystem };
                    tvLogMetadata.Nodes.Add(node);
                });
        }

        private void AddSourceTypes(DataTable logSettings)
        {
            var sourceTypes = new List<OperationSourceTypeViewModel>();
            foreach (TreeNode node in tvLogMetadata.Nodes)
            {
                var subsystem = node.Tag as SubsystemViewModel;
                var rows = logSettings.Select($"SubsystemID = {subsystem.Id}");
                var groups = rows
                    .GroupBy(row => row.ValueOrDefault<int>("SourceTypeID"));
                sourceTypes.AddRange(groups
                    .Select(grp => new OperationSourceTypeViewModel()
                    {
                        Id = grp.Key,
                        Name = grp.First().ValueOrDefault("SourceTypeName")
                    }));
                Array.ForEach(sourceTypes.ToArray(), sourceType =>
                {
                    var typeNode = new TreeNode(sourceType.Name) { Tag = sourceType };
                    node.Nodes.Add(typeNode);
                });
                sourceTypes.Clear();
            }
        }

        private void AddEntitiesAndSources(DataTable logSettings)
        {
            foreach (TreeNode subsystemNode in tvLogMetadata.Nodes)
            {
                var subsystem = subsystemNode.Tag as SubsystemViewModel;
                foreach (TreeNode sourceTypeNode in subsystemNode.Nodes)
                {
                    var sourceType = sourceTypeNode.Tag as OperationSourceTypeViewModel;
                    var entitiesNode = new TreeNode("Entities");
                    sourceTypeNode.Nodes.Add(entitiesNode);
                    AddEntities(logSettings, subsystem, sourceType, entitiesNode);

                    var sourcesNode = new TreeNode("Sources");
                    sourceTypeNode.Nodes.Add(sourcesNode);
                    AddSources(logSettings, subsystem, sourceType, sourcesNode);
                }
            }
        }

        private static void AddEntities(
            DataTable logSettings, SubsystemViewModel subsystem, OperationSourceTypeViewModel sourceType,
            TreeNode entitiesNode)
        {
            var entities = new List<EntityTypeViewModel>();
            var entityRows = logSettings.Select(
                $"SubsystemID = {subsystem.Id} AND SourceTypeID = {sourceType.Id}");
            var entityGroups = entityRows
                .GroupBy(row => row.ValueOrDefault<int>("EntityTypeID"));
            entities.AddRange(entityGroups
                .Select(grp => new EntityTypeViewModel()
                {
                    Id = grp.Key,
                    Name = grp.First().ValueOrDefault("EntityName"),
                    Description = grp.First().ValueOrDefault("EntityDescription")
                }));
            Array.ForEach(entities
                .Where(ent => !String.IsNullOrEmpty(ent.Name))
                .OrderBy(ent => ent.Name)
                .ToArray(), entity =>
                {
                    var node = new TreeNode(entity.Name) { Tag = entity };
                    entitiesNode.Nodes.Add(node);
                });
        }

        private static void AddSources(
            DataTable logSettings, SubsystemViewModel subsystem, OperationSourceTypeViewModel sourceType,
            TreeNode sourcesNode)
        {
            var sources = new List<OperationSourceViewModel>();
            var sourceRows = logSettings.Select(
                $"SubsystemID = {subsystem.Id} AND SourceTypeID = {sourceType.Id}");
            var sourceGroups = sourceRows
                .GroupBy(row => row.ValueOrDefault<int>("SourceID"));
            sources.AddRange(sourceGroups
                .Select(grp => new OperationSourceViewModel()
                {
                    Id = grp.Key,
                    Name = grp.First().ValueOrDefault("SourceName"),
                    Description = grp.First().ValueOrDefault("SourceDescription")
                }));
            Array.ForEach(sources
                .Where(src => !String.IsNullOrEmpty(src.Name))
                .OrderBy(src => src.Name)
                .ToArray(), source =>
                {
                    var node = new TreeNode(source.Name) { Tag = source };
                    sourcesNode.Nodes.Add(node);
                });
        }

        private IEnumerable<TreeNode> GetAllSourceEntityItems()
        {
            var allItems = new List<TreeNode>();
            Array.ForEach(tvLogMetadata.Nodes
                .Cast<TreeNode>()
                .ToArray(), subsystem =>
                {
                    Array.ForEach(subsystem.Nodes
                        .Cast<TreeNode>()
                        .ToArray(), sourceType =>
                        {
                            allItems.AddRange(sourceType.FirstNode.Nodes.Cast<TreeNode>());
                            allItems.AddRange(sourceType.LastNode.Nodes.Cast<TreeNode>());
                        });
                });
            return allItems;
        }

        private IEnumerable<LogSettingViewModel> GetAllSettings()
        {
            var allSettings = new List<LogSettingViewModel>();
            var allItems = GetAllSourceEntityItems();
            foreach (var item in allItems)
            {
                var subsystem = item.Parent.Parent.Parent.Tag as SubsystemViewModel;
                var sourceType = item.Parent.Parent.Tag as OperationSourceTypeViewModel;
                if (item.Tag is EntityTypeViewModel entity)
                {
                    var entitySettings = item.Nodes
                        .Cast<TreeNode>()
                        .Select(node => node.Tag as LogSettingViewModel)
                        .ToArray();
                    Array.ForEach(entitySettings, setting =>
                    {
                        setting.SubsystemId = subsystem.Id;
                        setting.SourceTypeId = sourceType.Id;
                    });
                    allSettings.AddRange(entitySettings);
                }
                else if (item.Tag is OperationSourceViewModel source)
                {
                    var sourceSettings = item.Nodes
                        .Cast<TreeNode>()
                        .Select(node => node.Tag as LogSettingViewModel)
                        .ToArray();
                    Array.ForEach(sourceSettings, setting =>
                    {
                        setting.SubsystemId = subsystem.Id;
                        setting.SourceTypeId = sourceType.Id;
                    });
                    allSettings.AddRange(sourceSettings);
                }
            }

            return allSettings;
        }

        private void GenerateScripts()
        {
            var orderedSettings = GetAllSettings()
                .OrderBy(setting => setting.Id);
            var scriptBuilder = new StringBuilder();
            scriptBuilder.Append(orderedSettings.First().ToScript(true, false));

            foreach (var setting in orderedSettings
                .Skip(1)
                .Take(orderedSettings.Count() - 2))
            {
                scriptBuilder.Append(setting.ToScript(false, false));
            }

            scriptBuilder.Append(orderedSettings.Last().ToScript(false, true));
            ScriptUtility.ReplaceScript(scriptBuilder.ToString());
        }

        #endregion

        #region Manage SysLogSettings

        private void LoadCurrentSysSettings()
        {
            this.GetActiveForm().Cursor = Cursors.WaitCursor;
            tvLogMetadata.Nodes.Clear();
            var dal = new SqlDataLayer(DbConnections.SystemConnection);
            var logSettings = dal.Query(ToolsQuery.SysLogSettings);
            AddSysEntitiesAndSources(logSettings);
            AddLogSettings(logSettings, GetAllSysSourceEntityItems());
            this.GetActiveForm().Cursor = Cursors.Default;
        }

        private void AddSysEntitiesAndSources(DataTable logSettings)
        {
            var entitiesNode = new TreeNode("Entities");
            tvLogMetadata.Nodes.Add(entitiesNode);
            AddSysEntities(logSettings, entitiesNode);

            var sourcesNode = new TreeNode("Sources");
            tvLogMetadata.Nodes.Add(sourcesNode);
            AddSysSources(logSettings, sourcesNode);
        }

        private static void AddSysEntities(DataTable logSettings, TreeNode entitiesNode)
        {
            var entities = new List<EntityTypeViewModel>();
            var entityRows = logSettings.Select("EntityTypeID IS NOT NULL");
            var entityGroups = entityRows
                .GroupBy(row => row.ValueOrDefault<int>("EntityTypeID"));
            entities.AddRange(entityGroups
                .Select(grp => new EntityTypeViewModel()
                {
                    Id = grp.Key,
                    Name = grp.First().ValueOrDefault("EntityName"),
                    Description = grp.First().ValueOrDefault("EntityDescription")
                }));
            Array.ForEach(entities
                .Where(ent => !String.IsNullOrEmpty(ent.Name))
                .OrderBy(ent => ent.Name)
                .ToArray(), entity =>
                {
                    var node = new TreeNode(entity.Name) { Tag = entity };
                    entitiesNode.Nodes.Add(node);
                });
        }

        private static void AddSysSources(DataTable logSettings, TreeNode sourcesNode)
        {
            var sources = new List<OperationSourceViewModel>();
            var sourceRows = logSettings.Select("SourceID IS NOT NULL");
            var sourceGroups = sourceRows
                .GroupBy(row => row.ValueOrDefault<int>("SourceID"));
            sources.AddRange(sourceGroups
                .Select(grp => new OperationSourceViewModel()
                {
                    Id = grp.Key,
                    Name = grp.First().ValueOrDefault("SourceName"),
                    Description = grp.First().ValueOrDefault("SourceDescription")
                }));
            Array.ForEach(sources
                .Where(src => !String.IsNullOrEmpty(src.Name))
                .OrderBy(src => src.Name)
                .ToArray(), source =>
                {
                    var node = new TreeNode(source.Name) { Tag = source };
                    sourcesNode.Nodes.Add(node);
                });
        }

        private IEnumerable<TreeNode> GetAllSysSourceEntityItems()
        {
            var allItems = new List<TreeNode>();
            allItems.AddRange(tvLogMetadata.Nodes[0].Nodes.Cast<TreeNode>());
            allItems.AddRange(tvLogMetadata.Nodes[1].Nodes.Cast<TreeNode>());
            return allItems;
        }

        private IEnumerable<LogSettingViewModel> GetAllSysSettings()
        {
            var allSettings = new List<LogSettingViewModel>();
            Array.ForEach(GetAllSysSourceEntityItems().ToArray(), node =>
            {
                allSettings.AddRange(node.Nodes
                    .Cast<TreeNode>()
                    .Select(node => node.Tag as LogSettingViewModel));
            });
            return allSettings;
        }

        private void GenerateSysScripts()
        {
            var orderedSettings = GetAllSysSettings()
                .OrderBy(setting => setting.Id);
            var scriptBuilder = new StringBuilder();
            scriptBuilder.Append(orderedSettings.First().ToSysScript(true, false));

            foreach (var setting in orderedSettings
                .Skip(1)
                .Take(orderedSettings.Count() - 2))
            {
                scriptBuilder.Append(setting.ToSysScript(false, false));
            }

            scriptBuilder.Append(orderedSettings.Last().ToSysScript(false, true));
            ScriptUtility.ReplaceSysScript(scriptBuilder.ToString());
        }

        #endregion
    }
}
