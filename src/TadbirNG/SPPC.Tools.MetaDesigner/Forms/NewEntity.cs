using System;
using System.Windows.Forms;
using SPPC.Tools.Transforms;
using SPPC.Tools.Model;
using SPPC.Tools.Presentation;

namespace SPPC.Tools.MetaDesigner.Forms
{
    public partial class NewEntity : Form, IMetadataEditor
    {
        public NewEntity()
        {
            InitializeComponent();
        }

        public bool HasMetadata
        {
            get { return (Entity != null); }
        }

        public object Metadata
        {
            get { return Entity; }
        }

        public Entity Entity { get; private set; }

        private void Add_Click(object sender, EventArgs e)
        {
            SetNewEntity();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetNewEntity()
        {
            var generator = new BasicMetaGenerator();
            var entityName = !String.IsNullOrWhiteSpace(txtEntityName.Text) ? txtEntityName.Text : "NewEntity";
            if (chkDerive.Checked)
            {
                Entity = generator.GenerateAsIEntity(entityName);
            }
            else
            {
                Entity = generator.GenerateEntity(entityName);
            }
        }
    }
}
