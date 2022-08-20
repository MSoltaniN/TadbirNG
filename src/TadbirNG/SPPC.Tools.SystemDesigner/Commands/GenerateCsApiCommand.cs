﻿using System;
using System.IO;
using SPPC.Framework.Common;
using SPPC.Framework.Utility;
using SPPC.Tools.Model;
using SPPC.Tools.Transforms.Templates;

namespace SPPC.Tools.SystemDesigner.Commands
{
    class GenerateCsApiCommand
    {
        public GenerateCsApiCommand(ApiModel model)
        {
            Verify.ArgumentNotNull(model);
            _model = model;
        }

        public void Execute()
        {
            var template = GetTemplate();
            string transformed = template.TransformText();
            string path = Path.Combine(
                _model.OutputPath, String.Format("{0}Api.cs", _model.EntityName));
            File.WriteAllText(path, transformed);
        }

        private ITextTemplate GetTemplate()
        {
            ITextTemplate template = null;
            template = new CsApiClass(_model);
            return template;
        }
        private readonly ApiModel _model;
    }
}
