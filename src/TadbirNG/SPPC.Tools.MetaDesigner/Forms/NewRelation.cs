using System;
using System.Windows.Forms;
using SPPC.Tools.MetaDesigner.Common;
using SPPC.Tools.Model;

namespace SPPC.Tools.MetaDesigner.Forms
{
    public partial class NewRelation : Form, IMetadataEditor
    {
        public NewRelation()
        {
            InitializeComponent();
        }

        public bool HasMetadata
        {
            get { return (Relation != null); }
        }

        public object Metadata
        {
            get { return Relation; }
        }

        public Relation Relation { get; private set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            cmbRelationType.SelectedIndex = 0;
            var repository = MetaDesignerContext.Current.Model.Repository;
            cmbEntities.DisplayMember = "Name";
            cmbEntities.DataSource = repository.Entities;
        }

        private void RelationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool showJoinTable = (cmbRelationType.SelectedIndex == 3);
            lblJoinTable.Visible = showJoinTable;
            txtJoinTable.Visible = showJoinTable;
        }

        private void AddRelation_Click(object sender, EventArgs e)
        {
            SetNewRelation();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetNewRelation()
        {
            var entity = cmbEntities.SelectedItem as Entity;
            var type = FromText(cmbRelationType.SelectedItem.ToString());
            Relation = new Relation()
            {
                Multiplicity = type,
                EntityName = entity.Name,
                EndpointName = entity.Name,
                HasKey = chkHasKey.Checked,
                JoinTable = txtJoinTable.Text,
                Description = "TODO: Add description..."
            };
        }

        private RelationMultiplicity FromText(string text)
        {
            RelationMultiplicity type = RelationMultiplicity.Undefined;
            switch (text)
            {
                case "One to Many":
                    type = RelationMultiplicity.OneToMany;
                    break;
                case "Many to One":
                    type = RelationMultiplicity.ManyToOne;
                    break;
                case "One to One":
                    type = RelationMultiplicity.OneToOne;
                    break;
                case "Many to Many":
                    type = RelationMultiplicity.ManyToMany;
                    break;
            }

            return type;
        }
    }
}
