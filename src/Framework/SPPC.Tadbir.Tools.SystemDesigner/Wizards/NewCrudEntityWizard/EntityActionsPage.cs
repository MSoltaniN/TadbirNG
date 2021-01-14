using System;
using System.Windows.Forms;
using SPPC.Tools.Model;

namespace SPPC.Tadbir.Tools.SystemDesigner.Wizards.NewCrudEntityWizard
{
    public partial class EntityActionsPage : UserControl
    {
        public EntityActionsPage()
        {
            InitializeComponent();
            Info = "Select entity actions";
        }

        public EntityActionsModel Actions { get; set; }

        public string Info { get; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupBindings();
            UpdateActionsList();
        }

        private void Actions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstActions.SelectedItem != null)
            {
                txtName.Text = lstActions.SelectedItem.ToString();
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            var action = txtName.Text;
            if (!String.IsNullOrWhiteSpace(action) && !lstActions.Items.Contains(action))
            {
                Actions.CustomActions.Add(txtName.Text);
                UpdateActionsList();
                txtName.Text = String.Empty;
            }
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            if (lstActions.SelectedItem != null)
            {
                Actions.CustomActions.Remove(lstActions.SelectedItem.ToString());
                UpdateActionsList();
            }
        }

        private void SetupBindings()
        {
            chkView.DataBindings.Add("Checked", Actions, "HasView");
            chkCreate.DataBindings.Add("Checked", Actions, "HasCreate");
            chkModify.DataBindings.Add("Checked", Actions, "HasModify");
            chkDelete.DataBindings.Add("Checked", Actions, "HasDelete");
        }

        private void UpdateActionsList()
        {
            lstActions.DataSource = null;
            lstActions.DataSource = Actions.CustomActions;
        }
    }
}
