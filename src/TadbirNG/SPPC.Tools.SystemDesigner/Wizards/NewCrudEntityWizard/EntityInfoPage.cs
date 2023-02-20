using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using SPPC.Tools.MetaDesigner.Persistence;
using SPPC.Tools.Model;

namespace SPPC.Tools.SystemDesigner.Wizards.NewCrudEntityWizard
{
    public partial class EntityInfoPage : UserControl
    {
        public EntityInfoPage()
        {
            InitializeComponent();
            Info = "Define general entity information";
        }

        public EntityInfoModel EntityInfo { get; set; }

        public string Info { get; }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SetupBindings();
            var repository = LoadXmlMetadataRepository(
                ConfigurationManager.AppSettings["XmlRepoPath"]);
            cmbEntity.DataSource = repository.Entities
                .OrderBy(entity => entity.Name)
                .ToList();
        }

        private void Entity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEntity.SelectedItem != null)
            {
                EntityInfo.Entity = cmbEntity.SelectedItem as Entity;
            }
        }

        private static Repository LoadXmlMetadataRepository(string path)
        {
            var serializer = new BasicXmlSerializer();
            var repository = serializer.Deserialize(path, typeof(Repository)) as Repository;
            Array.ForEach(repository.Entities.ToArray(), entity => entity.Repository = repository);
            return repository;
        }

        private void SetupBindings()
        {
            txtSingularName.DataBindings.Add("Text", EntityInfo, "SingularName");
            txtPluralName.DataBindings.Add("Text", EntityInfo, "PluralName");
            chkIsFiscal.DataBindings.Add("Checked", EntityInfo, "IsFiscalEntity");
            chkIsSystem.DataBindings.Add("Checked", EntityInfo, "IsSystemEntity");
        }

        private bool ValidateModel()
        {
            bool isComplete = !String.IsNullOrWhiteSpace(EntityInfo.SingularName)
                && !String.IsNullOrWhiteSpace(EntityInfo.PluralName)
                && EntityInfo.Entity != null;
            return isComplete;
        }
    }
}
