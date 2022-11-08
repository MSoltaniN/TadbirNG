using System;
using System.Net.Http;
using System.Windows.Forms;
using SPPC.CodeChallenge.ViewModel.Core;

namespace SPPC.CodeChallenge.SchoolManager
{
    public partial class SchoolEditor : Form
    {
        public SchoolEditor()
        {
            InitializeComponent();
            _apiClient = new HttpClient()
            {
                BaseAddress = new Uri(ServiceRoot)
            };
        }

        public SchoolViewModel School { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadProvinces();
            SetupBindings();
            LoadValues();
        }

        private void Province_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            // This method is called when current selection in Provinces combo box changes.
            // TODO: Use HttpClient field to load all cities for selected province from service.
            int selected = cmbProvince.SelectedIndex;
            if (selected != -1)
            {
                int provinceId = Int32.Parse(cmbProvince.SelectedValue.ToString());
                LoadCities(provinceId);
            }

            Cursor = Cursors.Default;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (SaveValues())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void LoadProvinces()
        {
            Cursor = Cursors.WaitCursor;

            // TODO: Use HttpClient field to load province list from service...
            // Uncomment following lines to bind province list to combo box.
            // cmbProvince.DataSource = provinces;
            // cmbProvince.DisplayMember = "Value";
            // cmbProvince.ValueMember = "Key";

            Cursor = Cursors.Default;
        }

        private void LoadCities(int provinceId)
        {
            // TODO: Use HttpClient field to load city list for given provinceId from service...
            // Uncomment following lines to bind city list to combo box.
            // cmbCity.DataSource = null;
            // cmbCity.DataSource = cities;
            // cmbCity.DisplayMember = "Value";
            // cmbCity.ValueMember = "Key";
        }

        private void SetupBindings()
        {
            txtName.DataBindings.Add("Text", School, "Name");
            txtManagerName.DataBindings.Add("Text", School, "Manager");
            spnTuition.DataBindings.Add("Value", School, "Tuition");
            spnCapacity.DataBindings.Add("Value", School, "Capacity");
            txtAddress.DataBindings.Add("Text", School, "Address");
            chkListed.DataBindings.Add("Checked", School, "IsListed");
            dtpEstablished.DataBindings.Add("Value", School, "FoundedDate");
        }

        private void LoadValues()
        {
            cmbAdminType.SelectedItem = School.AdminSystem;
            cmbProvince.SelectedValue = School.ProvinceId.ToString();
            cmbCity.SelectedValue = School.CityId.ToString();
        }

        private bool SaveValues()
        {
            if (!ValidateValues())
            {
                return false;
            }

            School.AdminSystem = cmbAdminType.SelectedItem.ToString();
            School.ProvinceId = Int32.Parse(cmbProvince.SelectedValue.ToString());
            School.CityId = Int32.Parse(cmbCity.SelectedValue.ToString());
            return true;
        }

        private bool ValidateValues()
        {
            if (String.IsNullOrEmpty(txtName.Text))
            {
                MessageBox.Show(
                    this, "Name is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (cmbAdminType.SelectedIndex == -1)
            {
                MessageBox.Show(
                    this, "Administration Type is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (cmbProvince.SelectedIndex == -1)
            {
                MessageBox.Show(
                    this, "Province is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            if (cmbCity.SelectedIndex == -1)
            {
                MessageBox.Show(
                    this, "City is required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return true;
        }

        private const string ServiceRoot = "http://localhost:5000";
        private readonly HttpClient _apiClient;
    }
}
