﻿using System;
using System.Collections.Generic;

namespace SPPC.Tools.Model
{
    public class CrudWizardModel
    {
        public CrudWizardModel()
        {
            EntityInfo = new EntityInfoModel();
            EntityActions = new EntityActionsModel();
            Options = new CrudWizardOptionsModel();
        }

        public EntityInfoModel EntityInfo { get; set; }

        public CrudWizardOptionsModel Options { get; set; }

        public EntityActionsModel EntityActions { get; set; }
    }
}
