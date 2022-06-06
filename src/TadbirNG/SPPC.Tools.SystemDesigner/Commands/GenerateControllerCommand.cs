using System;
using System.IO;
using SPPC.Framework.Common;
using SPPC.Framework.Extensions;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms;
using SPPC.Tools.Transforms.Templates;
using SPPC.Tools.Utility;

namespace SPPC.Tools.SystemDesigner.Commands
{
    public class GenerateControllerCommand
    {
        public GenerateControllerCommand(ControllerModel model)
        {
            Verify.ArgumentNotNull(model);
            _model = model;
        }

        public void Execute()
        {
            var template = GetTemplate();
            string transformed = template.TransformText();
            string path = Path.Combine(
                _model.OutputPath, String.Format("{0}Controller.cs", _model.EntityName.ToPlural()));
            File.WriteAllText(path, transformed);
        }

        private ITextTemplate GetTemplate()
        {
            ITextTemplate template = null;
            if (!_model.HasCrudImpl && !_model.HasCrudMethods)
            {
                template = new BlankController(_model);
            }
            else if (_model.HasCrudMethods && !_model.HasCrudImpl)
            {
                template = new EmptyCrudController(_model);
            }
            else if (_model.HasCrudImpl)
            {
                template = new StarterCrudController(_model);
            }

            return template;
        }

        private readonly ControllerModel _model;
    }
}
