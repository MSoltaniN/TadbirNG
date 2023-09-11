using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SPPC.Framework.Common;
using SPPC.Tools.Model;
using SPPC.Tools.Presentation;
using SPPC.Tools.Transforms;

namespace SPPC.Tools.MetaDesigner.Forms
{
    public partial class NewProperty : Form, IMetadataEditor
    {
        public NewProperty()
        {
            InitializeComponent();
        }

        public bool HasMetadata
        {
            get { return (Property != null); }
        }

        public object Metadata
        {
            get { return Property; }
        }

        public Property Property { get; private set; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            chkAutoGenerate.Checked = true;
            LoadBuiltinTypes();
        }

        private void Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            var typeName = cmbType.SelectedItem.ToString().Replace("System.", String.Empty);
            var type = (BuiltinType)Enum.Parse(typeof(BuiltinType), typeName);
            SetValidation(ValidationRuleFactory.CreateDefault(type));
        }

        private void Add_Click(object sender, EventArgs e)
        {
            SetNewProperty();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadBuiltinTypes()
        {
            var values = new List<string>();
            Array.ForEach(Enum.GetNames(typeof(BuiltinType)), n => values.Add(String.Format("System.{0}", n)));
            cmbType.DataSource = values;
        }

        private void SetNewProperty()
        {
            string typeName = cmbType.SelectedItem.ToString().Replace("System.", String.Empty);
            var type = (BuiltinType)Enum.Parse(typeof(BuiltinType), typeName);
            var generator = new BasicMetaGenerator();
            _ = Int32.TryParse(txtMaximum.Text, out int length);
            Property = generator.GenerateProperty(txtName.Text, type, length);
            Property.Storage.Nullable = !chkRequired.Checked;
            GetValidation(Property.ValidationRule);
        }

        private void SetValidation(ValidationRule rule)
        {
            txtMinimum.Text = rule.Minimum;
            txtMaximum.Text = rule.Maximum;
            txtFormat.Text = rule.Format;
            chkRequired.Checked = rule.Required;
        }

        private void GetValidation(ValidationRule rule)
        {
            rule.Minimum = txtMinimum.Text;
            rule.Maximum = txtMaximum.Text;
            rule.Format = txtFormat.Text;
            rule.Required = chkRequired.Checked;
        }
    }
}
