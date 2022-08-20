using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SPPC.Framework.Helpers;
using SPPC.Framework.Persistence;
using SPPC.Tools.Model;
using SPPC.Tools.Utility;

namespace SPPC.Tools.SystemDesigner.Forms
{
    public partial class LogSettingBrowserForm : Form
    {
        public LogSettingBrowserForm()
        {
            InitializeComponent();
            _connection = DbConnections.CompanyConnection;
            _sysConnection = DbConnections.SystemConnection;
            _dal = new SqlDataLayer(_connection);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ActiveForm.Cursor = Cursors.WaitCursor;
            LoadLookups();
            LoadSourceEntities();
            ActiveForm.Cursor = Cursors.Default;
        }

        private void SourceEntity_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (int index in Enumerable.Range(0, lbxOperations.Items.Count))
            {
                lbxOperations.SetItemChecked(index, false);
            }

            if (lbxSourceEntity.SelectedIndex != -1)
            {
                var selected = lbxSourceEntity.SelectedItem as LogSettingModel;
                cmbSubsystem.SelectedValue = selected.SubsystemId.ToString();
                cmbSourceType.SelectedValue = selected.SourceTypeId.ToString();
                var allOps = lbxOperations.Items
                    .Cast<KeyValue>()
                    .Select(item => Int32.Parse(item.Key))
                    .ToList();
                foreach (var id in selected.Operations)
                {
                    int index = allOps.IndexOf(id);
                    lbxOperations.SetItemChecked(index, true);
                }
            }
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            int id = 1;
            var script = new StringBuilder();
            script.AppendLine("SET IDENTITY_INSERT [Config].[LogSetting] ON");
            var all = lbxSourceEntity.DataSource as List<LogSettingModel>;
            foreach (var item in all)
            {
                foreach (int operationId in item.Operations.OrderBy(opId => opId))
                {
                    script.AppendFormat(@"INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES ({0}, {1}, {2}, {3}, {4}, {5}, {6})",
                    id++, item.SubsystemId, item.SourceTypeId, item.SourceId.HasValue ? item.SourceId.Value.ToString() : "NULL",
                    item.EntityTypeId.HasValue ? item.EntityTypeId.Value.ToString() : "NULL", operationId, 1);
                    script.AppendLine();
                }
            }

            script.AppendLine("SET IDENTITY_INSERT [Config].[LogSetting] OFF");
            File.WriteAllText("script.sql", script.ToString());
            MessageBox.Show("SQL script successfully generated.");
        }

        private void UpdateDb_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This operation is not currently available.");
        }

        private void Subsystem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSubsystem.SelectedIndex != -1 && lbxSourceEntity.SelectedIndex != -1)
            {
                var setting = lbxSourceEntity.SelectedItem as LogSettingModel;
                setting.SubsystemId = Int32.Parse(cmbSubsystem.SelectedValue.ToString());
            }
        }

        private void SourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSourceType.SelectedIndex != -1 && lbxSourceEntity.SelectedIndex != -1)
            {
                var setting = lbxSourceEntity.SelectedItem as LogSettingModel;
                setting.SourceTypeId = Int32.Parse(cmbSourceType.SelectedValue.ToString());
            }
        }

        private void Operations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxOperations.SelectedIndex != -1 && lbxSourceEntity.SelectedIndex != -1)
            {
                var op = lbxOperations.SelectedItem as KeyValue;
                int opId = Int32.Parse(op.Key.ToString());
                var setting = lbxSourceEntity.SelectedItem as LogSettingModel;
                bool isChecked = lbxOperations.GetItemChecked(lbxOperations.SelectedIndex);
                if (!isChecked && setting.Operations.Contains(opId))
                {
                    setting.Operations.Remove(opId);
                }

                if (isChecked && !setting.Operations.Contains(opId))
                {
                    setting.Operations.Add(opId);
                }
            }
        }

        private static void MergeSettings(List<LogSettingModel> all, List<LogSettingModel> existing)
        {
            var entities = existing
                .Where(log => log.EntityTypeId != null)
                .GroupBy(log => log.EntityTypeId);
            foreach (var group in entities)
            {
                var first = group.First();
                var entityLog = all
                    .Where(log => log.EntityTypeId == group.Key)
                    .First();
                entityLog.SubsystemId = first.SubsystemId;
                entityLog.SourceTypeId = first.SourceTypeId;
                entityLog.Operations.AddRange(group.Select(log => log.OperationId));
            }

            var sources = existing
                .Where(log => log.SourceId != null)
                .GroupBy(log => log.SourceId);
            foreach (var group in sources)
            {
                var first = group.First();
                var entityLog = all
                    .Where(log => log.SourceId == group.Key)
                    .First();
                entityLog.SubsystemId = first.SubsystemId;
                entityLog.SourceTypeId = first.SourceTypeId;
                entityLog.Operations.AddRange(group.Select(log => log.OperationId));
            }
        }

        private static int IdFromRowItem(object item)
        {
            return Int32.Parse(item.ToString());
        }

        private static int? NullableIdFromRowItem(object item)
        {
            return (item != DBNull.Value)
                ? Int32.Parse(item.ToString())
                : (int?)null;
        }

        private List<LogSettingModel> LoadCurrentSetting()
        {
            string command =
                @"SELECT * FROM [Config].[LogSetting]";
            var result = _dal.Query(command);
            return result.Rows
                .Cast<DataRow>()
                .Select(row => new LogSettingModel()
                {
                    Id = IdFromRowItem(row[0]),
                    SubsystemId = IdFromRowItem(row[1]),
                    SourceTypeId = IdFromRowItem(row[2]),
                    SourceId = NullableIdFromRowItem(row[3]),
                    EntityTypeId = NullableIdFromRowItem(row[4]),
                    OperationId = IdFromRowItem(row[5]),
                    IsEnabled = Boolean.Parse(row[6].ToString())
                })
                .ToList();
        }

        private void LoadLookups()
        {
            var list = GetMetadataLookup("Subsystem");
            cmbSubsystem.DisplayMember = "Value";
            cmbSubsystem.ValueMember = "Key";
            cmbSubsystem.DataSource = list;

            list = GetMetadataLookup("OperationSourceType");
            cmbSourceType.DisplayMember = "Value";
            cmbSourceType.ValueMember = "Key";
            cmbSourceType.DataSource = list;

            list = GetMetadataLookup("Operation");
            lbxOperations.DisplayMember = "Value";
            lbxOperations.ValueMember = "Key";
            lbxOperations.DataSource = list;
        }

        private void LoadSourceEntities()
        {
            var settingItems = new List<LogSettingModel>();
            settingItems.AddRange(GetMetadataLookup("EntityType")
                .Select(row => new LogSettingModel()
                {
                    SubsystemId = 1,
                    SourceTypeId = 1,
                    EntityTypeId = Int32.Parse(row.Key),
                    Name = row.Value
                }));
            settingItems.AddRange(GetMetadataLookup("OperationSource")
                .Select(row => new LogSettingModel()
                {
                    SubsystemId = 1,
                    SourceTypeId = 1,
                    SourceId = Int32.Parse(row.Key),
                    Name = row.Value
                }));
            var existing = LoadCurrentSetting();
            MergeSettings(settingItems, existing);
            settingItems = settingItems
                .OrderBy(item => item.Name)
                .ToList();
            lbxSourceEntity.DisplayMember = "Name";
            lbxSourceEntity.DataSource = settingItems;
        }

        private List<KeyValue> GetMetadataLookup(string table)
        {
            string command = String.Format("SELECT {0}ID,Name FROM [Metadata].[{0}]", table);
            var result = _dal.Query(command);
            return result.Rows
                .Cast<DataRow>()
                .Select(row => new KeyValue(row[0].ToString(), row[1].ToString()))
                .ToList();
        }

        private readonly string _connection;
        private readonly string _sysConnection;
        private readonly SqlDataLayer _dal;
    }
}
