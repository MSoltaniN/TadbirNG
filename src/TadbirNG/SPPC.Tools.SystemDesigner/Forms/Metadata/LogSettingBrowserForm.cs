using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.ViewModel;
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
            txtName.Text = null;
            txtDescription.Text = null;
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
                if (selected.Tag != null && !String.IsNullOrEmpty(txtName.Text))
                {
                    selected.Text = txtName.Text;
                }
            }
        }

        private void New_Click(object sender, EventArgs e)
        {
            var selected = tvLogMetadata.SelectedNode;
            if (selected != null)
            {
                string nodeText = selected.Text;
                if (nodeText == "Entities")
                {
                    AddNewEntity(selected);
                }
                else if (nodeText == "Sources")
                {
                    AddNewSource(selected);
                }
                else if (selected.Tag is EntityTypeViewModel
                    || selected.Tag is OperationSourceViewModel)
                {
                    UpdateLogSettings(selected);
                }
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

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
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

        private void SetLastIdValues(DataTable logSettings)
        {
            _nextSettingId = logSettings.Rows
                .Cast<DataRow>()
                .Max(row => row.ValueOrDefault<int>("LogSettingID")) + 1;
            _nextEntityId = logSettings.Rows
                .Cast<DataRow>()
                .Max(row => row.ValueOrDefault<int>("EntityTypeID")) + 1;
            _nextSourceId = logSettings.Rows
                .Cast<DataRow>()
                .Max(row => row.ValueOrDefault<int>("SourceID")) + 1;
        }

        private void AddNewEntity(TreeNode parent)
        {
            string entityName = $"Entity{parent.Nodes.Count}";
            var entity = new EntityTypeViewModel()
            {
                Id = _nextEntityId++,
                Name = entityName,
                State = RecordState.Added
            };
            var newNode = new TreeNode(entityName) { Tag = entity };
            parent.Nodes.Add(newNode);
            tvLogMetadata.SelectedNode = newNode;
            txtName.Focus();
            txtName.SelectAll();
        }

        private void AddNewSource(TreeNode parent)
        {
            string sourceName = $"Source{parent.Nodes.Count}";
            var source = new OperationSourceViewModel()
            {
                Id = _nextSourceId++,
                Name = sourceName,
                State = RecordState.Added
            };
            var newNode = new TreeNode(sourceName) { Tag = source };
            parent.Nodes.Add(newNode);
            tvLogMetadata.SelectedNode = newNode;
            txtName.Focus();
            txtName.SelectAll();
        }

        private void UpdateLogSettings(TreeNode parent)
        {
            var selector = new LogSelectOperationsForm();
            selector.SelectedItems.AddRange(parent.Nodes
                .Cast<TreeNode>()
                .Select(node => node.Tag as LogSettingViewModel));
            if (selector.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            this.GetActiveForm().Cursor = Cursors.WaitCursor;
            var subsystem = parent?.Parent?.Parent?.Tag as SubsystemViewModel;
            var sourceType = parent?.Parent?.Tag as OperationSourceTypeViewModel;
            var entity = parent?.Tag as EntityTypeViewModel;
            var source = parent?.Tag as OperationSourceViewModel;
            foreach (var setting in selector.SelectedItems)
            {
                setting.Id = _nextSettingId++;
                if (chkSystemSettings.Checked)
                {
                    setting.SubsystemId = subsystem.Id;
                    setting.SourceTypeId = sourceType.Id;
                }

                setting.EntityTypeId = entity?.Id;
                setting.SourceId = source?.Id;
                setting.State = RecordState.Added;
            }

            parent.Nodes.Clear();
            parent.Nodes.AddRange(selector.SelectedItems
                .Select(item => new TreeNode(item.OperationName) { Tag = item })
                .ToArray());
            tvLogMetadata.SelectedNode = parent.Nodes[0];
            this.GetActiveForm().Cursor = Cursors.Default;
        }

        #region Manage LogSettings

        private void LoadCurrentSettings()
        {
            this.GetActiveForm().Cursor = Cursors.WaitCursor;
            tvLogMetadata.Nodes.Clear();
            var dal = new SqlDataLayer(DbConnections.CompanyConnection);
            var logSettings = dal.Query(ToolsQuery.LogSettings);
            SetLastIdValues(logSettings);
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
            var all = GetAllSourceEntityItems();
            var entities = all
                .Where(node => node.Tag is EntityTypeViewModel)
                .Select(node => node.Tag as EntityTypeViewModel)
                .OrderBy(entity => entity.Id);
            var sources = all
                .Where(node => node.Tag is OperationSourceViewModel)
                .Select(node => node.Tag as OperationSourceViewModel)
                .OrderBy(source => source.Id);

            var orderedSettings = GetAllSettings()
                .OrderBy(setting => setting.Id);
            GenerateCreateScripts(orderedSettings, entities, sources);
            GenerateUpdateScripts(orderedSettings, entities, sources);
        }

        private static void GenerateCreateScripts(IEnumerable<LogSettingViewModel> allSettings,
            IEnumerable<EntityTypeViewModel> allEntities, IEnumerable<OperationSourceViewModel> allSources)
        {
            var generated = ScriptUtility.GetInsertScripts(allEntities, EntityTypeExtensions.ToScript);
            ScriptUtility.ReplaceScript(generated);

            generated = ScriptUtility.GetInsertScripts(allSources, OperationSourceExtensions.ToScript);
            ScriptUtility.ReplaceScript(generated);

            generated = ScriptUtility.GetInsertScripts(allSettings, LogSettingExtensions.ToScript);
            ScriptUtility.ReplaceScript(generated);
        }

        private static void GenerateUpdateScripts(IEnumerable<LogSettingViewModel> allSettings,
            IEnumerable<EntityTypeViewModel> allEntities, IEnumerable<OperationSourceViewModel> allSources)
        {
            var scriptBuilder = new StringBuilder();
            ScriptUtility.AddVersionMarker(scriptBuilder);
            var addedEntities = allEntities
                .Where(entity => entity.State == RecordState.Added)
                .OrderBy(entity => entity.Id);
            var addedSources = allSources
                .Where(source => source.State == RecordState.Added)
                .OrderBy(source => source.Id);
            var addedSettings = allSettings
                .Where(setting => setting.State == RecordState.Added)
                .OrderBy(setting => setting.Id);
            foreach (var entityGroup in addedSettings
                .Where(setting => setting.EntityTypeId != null)
                .GroupBy(setting => setting.EntityTypeId))
            {
                scriptBuilder.AppendLine(entityGroup.First().ToDeleteScript());
            }

            foreach (var sourceGroup in addedSettings
                .Where(setting => setting.SourceId != null)
                .GroupBy(setting => setting.SourceId))
            {
                scriptBuilder.AppendLine(sourceGroup.First().ToDeleteScript());
            }

            var generated = ScriptUtility.GetInsertScripts(addedEntities, EntityTypeExtensions.ToScript);
            scriptBuilder.Append(generated);
            generated = ScriptUtility.GetInsertScripts(addedSources, OperationSourceExtensions.ToScript);
            scriptBuilder.Append(generated);
            generated = ScriptUtility.GetInsertScripts(addedSettings, LogSettingExtensions.ToScript);
            scriptBuilder.Append(generated);
            var path = Path.Combine(PathConfig.ResourceRoot, ScriptUtility.UpdateScriptName);
            File.AppendAllText(path, scriptBuilder.ToString());
        }

        #endregion

        #region Manage SysLogSettings

        private void LoadCurrentSysSettings()
        {
            this.GetActiveForm().Cursor = Cursors.WaitCursor;
            tvLogMetadata.Nodes.Clear();
            var dal = new SqlDataLayer(DbConnections.SystemConnection);
            var logSettings = dal.Query(ToolsQuery.SysLogSettings);
            SetLastIdValues(logSettings);
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
            GenerateSysCreateScripts(orderedSettings);
            GenerateSysUpdateScripts(orderedSettings);
        }

        private static void GenerateSysCreateScripts(IEnumerable<LogSettingViewModel> allSettings)
        {
            var generated = GetSysInsertScripts(allSettings);
            ScriptUtility.ReplaceSysScript(generated);
        }

        private static void GenerateSysUpdateScripts(IEnumerable<LogSettingViewModel> allSettings)
        {
            var scriptBuilder = new StringBuilder();
            ScriptUtility.AddVersionMarker(scriptBuilder);
            var addedSettings = allSettings
                .Where(setting => setting.State == RecordState.Added)
                .OrderBy(setting => setting.Id);
            foreach (var entityGroup in addedSettings
                .Where(setting => setting.EntityTypeId != null)
                .GroupBy(setting => setting.EntityTypeId))
            {
                scriptBuilder.AppendLine(entityGroup.First().ToSysDeleteScript());
            }

            foreach (var sourceGroup in addedSettings
                .Where(setting => setting.SourceId != null)
                .GroupBy(setting => setting.SourceId))
            {
                scriptBuilder.AppendLine(sourceGroup.First().ToSysDeleteScript());
            }

            var generated = GetSysInsertScripts(addedSettings);
            scriptBuilder.Append(generated);
            var path = Path.Combine(PathConfig.ResourceRoot, ScriptUtility.SysUpdateScriptName);
            File.AppendAllText(path, scriptBuilder.ToString());
        }

        private static string GetSysInsertScripts(IEnumerable<LogSettingViewModel> newSettings)
        {
            var scriptBuilder = new StringBuilder();
            scriptBuilder.Append(newSettings.First().ToSysScript(true, false));

            foreach (var setting in newSettings
                .Skip(1)
                .Take(newSettings.Count() - 2))
            {
                scriptBuilder.Append(setting.ToSysScript(false, false));
            }

            scriptBuilder.Append(newSettings.Last().ToSysScript(false, true));
            return scriptBuilder.ToString();
        }

        #endregion

        private int _nextSettingId;
        private int _nextEntityId;
        private int _nextSourceId;
    }
}
