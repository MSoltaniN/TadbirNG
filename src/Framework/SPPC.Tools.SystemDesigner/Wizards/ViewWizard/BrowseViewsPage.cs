using System;
using System.Data;
using System.Windows.Forms;
using BabakSoft.Platform.Data;

namespace SPPC.Tools.SystemDesigner.Wizards.ViewWizard
{
    public partial class BrowseViewsPage : UserControl
    {
        public BrowseViewsPage()
        {
            InitializeComponent();
            Info = "Browse Existing Views";
        }

        public string Info { get; set; }

        public string SysConnection { get; set; }

        public DataTable ViewItems { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            cmbViewSelector.ValueMember = "ViewID";
            cmbViewSelector.DisplayMember = "Name";
            cmbViewSelector.DataSource = ViewItems;
        }

        private void ViewSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbViewSelector.SelectedIndex != -1)
            {
                int selectedViewId = (int)cmbViewSelector.SelectedValue;
                var query = string.Format(
                    @"SELECT
                    c.ColumnID
                    ,c.Name
                    ,c.Type
                    ,c.Length
                    ,c.IsNullable
                    ,ISNULL(c.Visibility,'Visible') AS Visibility
                    ,c.DisplayIndex
                  FROM [Metadata].[Column] c WHERE c.ViewID = {0}", selectedViewId);

                var dal = new SqlDataLayer(SysConnection, ProviderType.SqlClient);
                var result = dal.Query(query);
                gvColumns.DataSource = result;
            }
        }
    }
}
