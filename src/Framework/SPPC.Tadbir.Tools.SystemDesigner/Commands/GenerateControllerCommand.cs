using System;
using System.IO;
using SPPC.Framework.Common;
using SPPC.Tadbir.Tools.SystemDesigner.Models;
using SPPC.Tadbir.Tools.SystemDesigner.Templates;

namespace SPPC.Tadbir.Tools.SystemDesigner.Commands
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
                _model.OutputPath, String.Format("{0}Controller.cs", GetPluralName(_model.EntityName)));
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

        private static string GetPluralName(string name)
        {
            Verify.ArgumentNotNullOrEmptyString(name, "name");
            char lastChar = name[name.Length - 1];
            string plural = name;
            switch (lastChar)
            {
                case 'h':
                case 's':
                case 'x':
                case 'z':
                    plural = String.Format("{0}es", name);
                    break;
                case 'y':
                    plural = String.Format("{0}ies", name.Substring(0, name.Length - 1));
                    break;
                default:
                    plural = String.Format("{0}s", name);
                    break;
            }

            return plural;
        }

        private readonly ControllerModel _model;
    }
}
