using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SPPC.Framework.Extensions;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.ViewModel.Metadata;
using SPPC.Tools.Extensions;
using SPPC.Tools.Model;

namespace SPPC.Tools.SystemDesigner.Forms
{
    public partial class ViewColumnsBrowserForm : Form
    {
        public ViewColumnsBrowserForm()
        {
            InitializeComponent();
            _allViews = new List<ViewViewModel>();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.GetActiveForm().Cursor = Cursors.WaitCursor;
            InitColumns();
            LoadViews();
            this.GetActiveForm().Cursor = Cursors.Default;
        }

        private void AddView_Click(object sender, EventArgs e)
        {
            var form = new ViewColumnsEditorForm();
            if (form.ShowDialog(this) == DialogResult.OK)
            {
                int maxId = _allViews.Max(vu => vu.Id);
                var addedView = form.View;
                addedView.Id = maxId + 1;
                _allViews.Add(addedView);
                grdViews.DataSource = _allViews;
            }
        }

        private void EditView_Click(object sender, EventArgs e)
        {

        }

        private void DeleteView_Click(object sender, EventArgs e)
        {

        }

        private void Views_SelectionChanged(object sender, EventArgs e)
        {
            if (grdViews.SelectedRows.Count == 1)
            {
                this.GetActiveForm().Cursor = Cursors.WaitCursor;
                var selectedView = grdViews.SelectedRows[0].DataBoundItem as ViewViewModel;
                LoadColumns(selectedView.Id);
                this.GetActiveForm().Cursor = Cursors.Default;
            }
        }

        private void AddColumn_Click(object sender, EventArgs e)
        {

        }

        private void EditColumn_Click(object sender, EventArgs e)
        {

        }

        private void DeleteColumn_Click(object sender, EventArgs e)
        {

        }

        private void Generate_Click(object sender, EventArgs e)
        {

        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InitColumns()
        {
            var dal = new SqlDataLayer(DbConnections.SystemConnection);
            var query = "SELECT * FROM [Metadata].[Column] ORDER BY [DisplayIndex]";
            _allColumns = dal.Query(query);
        }

        private void LoadViews()
        {
            var dal = new SqlDataLayer(DbConnections.SystemConnection);
            var query = "SELECT * FROM [Metadata].[View] ORDER BY [Name]";
            var result = dal.Query(query);
            _allViews.AddRange(result.Rows
                .Cast<DataRow>()
                .Select(row => new ViewViewModel()
                {
                    Id = row.ValueOrDefault<int>("ViewID"),
                    Name = row.ValueOrDefault("Name"),
                    EntityName = row.ValueOrDefault("EntityName"),
                    Entitytype = row.ValueOrDefault("Entitytype"),
                    FetchUrl = row.ValueOrDefault("FetchUrl"),
                    SearchUrl = row.ValueOrDefault("SearchUrl"),
                    IsHierarchy = row.ValueOrDefault<bool>("IsHierarchy"),
                    IsCartableIntegrated = row.ValueOrDefault<bool>("IsCartableIntegrated")
                }));
            grdViews.DataSource = _allViews;
        }

        private void LoadColumns(int viewId)
        {
            var columnRows = _allColumns.Select($"ViewID = {viewId}");
            grdColumns.DataSource = columnRows
                .Select(row => ColumnFromRow(row))
                .ToList();
        }

        private static ColumnViewModel ColumnFromRow(DataRow row)
        {
            var visibility = String.IsNullOrEmpty(row.ValueOrDefault("Visibility"))
                ? "Visible"
                : row.ValueOrDefault("Visibility");
            return new ColumnViewModel()
            {
                Id = row.ValueOrDefault<int>("ColumnID"),
                ViewId = row.ValueOrDefault<int>("ViewID"),
                Name = row.ValueOrDefault("Name"),
                GroupName = row.ValueOrDefault("GroupName"),
                Type = row.ValueOrDefault("Type"),
                DotNetType = row.ValueOrDefault("DotNetType"),
                StorageType = row.ValueOrDefault("StorageType"),
                ScriptType = row.ValueOrDefault("ScriptType"),
                Length = row.ValueOrDefault<int>("Length"),
                MinLength = row.ValueOrDefault<int>("MinLength"),
                IsFixedLength = row.ValueOrDefault<bool>("IsFixedLength"),
                IsDynamic = row.ValueOrDefault<bool>("IsDynamic"),
                IsNullable = row.ValueOrDefault<bool>("IsNullable"),
                AllowSorting = row.ValueOrDefault<bool>("AllowSorting"),
                AllowFiltering = row.ValueOrDefault<bool>("AllowFiltering"),
                Visibility = visibility,
                DisplayIndex = row.ValueOrDefault<short>("DisplayIndex"),
                Expression = row.ValueOrDefault("Expression")
            };
        }

        private readonly List<ViewViewModel> _allViews;
        private DataTable _allColumns;
    }
}
