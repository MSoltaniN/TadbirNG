﻿using SPPC.Framework.Utility;
using SPPC.Tools.Model;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class StarterCrudController : ITextTemplate
    {
        public StarterCrudController(ControllerModel model)
        {
            _model = model;
        }

        private readonly ControllerModel _model;
    }
}
