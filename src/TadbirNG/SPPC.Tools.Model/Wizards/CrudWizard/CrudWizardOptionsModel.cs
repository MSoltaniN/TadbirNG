﻿using System;
using System.Collections.Generic;

namespace SPPC.Tools.Model
{
    public class CrudWizardOptionsModel
    {
        public CrudWizardOptionsModel()
        {
            Controller = new ControllerModel();
            Api = new ApiModel();
            HasController = HasModel = HasViewModel = HasDbMapping = HasDbScript =
                HasRepoInterface = HasRepoImplementation = HasApiRouting =
                HasPermissionEnum = HasTsViewModel = HasTsApiRouting = true;
        }

        public ControllerModel Controller { get; set; }

        public ApiModel Api { get; set; }

        public bool HasController { get; set; }

        public bool HasModel { get; set; }

        public bool HasViewModel { get; set; }

        public bool HasDbMapping { get; set; }

        public bool HasDbScript { get; set; }

        public bool HasRepoInterface { get; set; }

        public bool HasRepoImplementation { get; set; }

        public bool HasApiRouting { get; set; }

        public bool HasPermissionEnum { get; set; }

        public bool HasTsViewModel { get; set; }

        public bool HasTsApiRouting { get; set; }
    }
}
