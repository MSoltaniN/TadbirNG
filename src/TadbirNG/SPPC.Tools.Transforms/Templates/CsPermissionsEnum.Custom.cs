using System;
using System.Collections.Generic;
using SPPC.Tools.Model;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class CsPermissionsEnum
    {
        public CsPermissionsEnum(EntityInfoModel entity)
        {
            _entity = entity;
        }

        private readonly EntityInfoModel _entity;
    }
}
