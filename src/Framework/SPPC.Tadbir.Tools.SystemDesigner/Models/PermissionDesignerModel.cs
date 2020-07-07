using System;
using System.Collections.Generic;
using System.Data;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Tools.SystemDesigner.Models
{
    public class PermissionDesignerModel
    {
        public PermissionDesignerModel()
        {
            PermissionGroup = new PermissionGroupViewModel();
            Permissions = new List<PermissionViewModel>();
        }
        public PermissionGroupViewModel PermissionGroup { get; set; }
        public List<PermissionViewModel> Permissions { get; private set; }
    }
}
