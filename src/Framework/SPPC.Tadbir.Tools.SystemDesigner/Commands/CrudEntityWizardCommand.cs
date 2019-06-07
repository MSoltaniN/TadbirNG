using System;
using SPPC.Framework.Common;
using SPPC.Tadbir.Tools.SystemDesigner.Models;

namespace SPPC.Tadbir.Tools.SystemDesigner.Commands
{
    public class CrudEntityWizardCommand : ICommand
    {
        public CrudEntityWizardCommand(CrudWizardModel model)
        {
            Verify.ArgumentNotNull(model);
            _model = model;
        }

        public void Execute()
        {
            if (_model.Options.HasController)
            {
                GenerateController();
            }
            if (_model.Options.HasModel || _model.Options.HasViewModel
                || _model.Options.HasDbMapping || _model.Options.HasDbScript)
            {
                GenerateModelLayer();
            }
        }

        private void GenerateController()
        {
            _model.Options.Controller.EntityName = _model.EntityInfo.Entity.Name;
            _model.Options.Controller.EntityArea = _model.EntityInfo.Entity.Area;
            _model.Options.Controller.IsFiscalEntity = _model.EntityInfo.IsFiscalEntity;
            var command = new GenerateControllerCommand(_model.Options.Controller);
            command.Execute();
        }

        private void GenerateModelLayer()
        {
            var command = new GenerateCsModelCommand(_model);
            command.Execute();
        }

        private readonly CrudWizardModel _model;
    }
}
