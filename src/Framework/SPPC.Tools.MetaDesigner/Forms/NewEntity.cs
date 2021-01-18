using System;
using System.Windows.Forms;
using SPPC.Tools.MetaDesigner.Common;
using SPPC.Tools.MetaDesigner.Transforms;
using SPPC.Tools.Model;

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
                Entity = generator.GenerateAsIEntity(entityName, MetaDesignerContext.Current.Model.Repository);
            }
            else
            {
                Entity = generator.GenerateEntity(entityName, MetaDesignerContext.Current.Model.Repository);
            }
        }
    }
}
