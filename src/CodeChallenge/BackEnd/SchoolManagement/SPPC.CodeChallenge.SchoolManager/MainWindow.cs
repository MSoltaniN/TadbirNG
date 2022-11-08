using System;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;
using SPPC.CodeChallenge.ViewModel.Core;

namespace SPPC.CodeChallenge.SchoolManager
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            _apiClient = new HttpClient()
            {
                BaseAddress = new Uri(ServiceRoot)
            };
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadSchools();
        }

        private void Schools_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            var hiddenColumns = new string[]
            {
                "Id", "ProvinceId", "CityId"
            };

            if (hiddenColumns.Contains(e.Column.Name))
            {
                e.Column.Visible = false;
            }
        }

        private void Refresh_Click(object sender, EventArgs e)
        {
            LoadSchools();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            var editor = new SchoolEditor() { School = new SchoolViewModel() };
            if (editor.ShowDialog(this) == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;

                var addedSchool = editor.School;
                // TODO: Use HttpClient field to add new school using Api service...

                // Refresh school list to show new data...
                LoadSchools();

                Cursor = Cursors.Default;
            }
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            if (grdSchools.SelectedRows.Count > 0)
            {
                var selectedSchool = grdSchools.SelectedRows[0].DataBoundItem as SchoolViewModel;
                var editor = new SchoolEditor() { School = selectedSchool };
                if (editor.ShowDialog(this) == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;

                    var editedSchool = editor.School;
                    // TODO: Use HttpClient field to edit selected school using Api service...

                    // Refresh school list to show modified data...
                    LoadSchools();

                    Cursor = Cursors.Default;
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            if (grdSchools.SelectedRows.Count > 0)
            {
                var confirm = MessageBox.Show(
                    this, "Are you sure you want to delete selected school?", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (confirm == DialogResult.Yes)
                {
                    Cursor = Cursors.WaitCursor;

                    var selectedSchool = grdSchools.SelectedRows[0].DataBoundItem as SchoolViewModel;
                    // TODO: Use HttpClient field to delete selected school using Api service...

                    Cursor = Cursors.Default;
                }
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LoadSchools()
        {
            Cursor = Cursors.WaitCursor;

            // TODO: Use HttpClient field to load all schools from API service...
            // Uncomment the following lines to bind school list to grid view control.
            // grdSchools.DataSource = null;
            // grdSchools.DataSource = schools;

            Cursor = Cursors.Default;
        }

        private const string ServiceRoot = "http://localhost:5000";
        private readonly HttpClient _apiClient;
    }
}
