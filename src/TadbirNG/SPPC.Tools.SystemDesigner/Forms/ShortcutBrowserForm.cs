using System;
using System.Windows.Forms;
using SPPC.Framework.Persistence;
using SPPC.Tools.Utility;

namespace SPPC.Tools.SystemDesigner.Forms
{
    public partial class ShortcutBrowserForm : Form
    {
        public ShortcutBrowserForm()
        {
            InitializeComponent();
            _dal = new SqlDataLayer(_sysConnection);
            _sysConnection = DbConnections.SystemConnection;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ActiveForm.Cursor = Cursors.WaitCursor;
            LoadShortcuts();
            ActiveForm.Cursor = Cursors.Default;
        }

        private void New_Click(object sender, EventArgs e)
        {

        }

        private void Edit_Click(object sender, EventArgs e)
        {

        }

        private void Delete_Click(object sender, EventArgs e)
        {

        }

        private void Generate_Click(object sender, EventArgs e)
        {

        }

        private void LoadShortcuts()
        {
            string query = @"
SELECT [PermissionID], [Name], [Scope], [HotKey], [Method]
FROM [Metadata].[ShortcutCommand]";
            var result = _dal.Query(query);
            grdShortcuts.DataSource = result;
        }

        private readonly SqlDataLayer _dal;
        private readonly string _sysConnection;
    }
}
