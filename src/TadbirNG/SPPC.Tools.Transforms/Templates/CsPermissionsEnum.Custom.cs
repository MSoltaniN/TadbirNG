using SPPC.Framework.Utility;
using SPPC.Tools.Model;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class CsPermissionsEnum : ITextTemplate
    {
        public CsPermissionsEnum(EntityInfoModel entity, EntityActionsModel model)
        {
            _entity = entity;
            _model = model;
        }

        private readonly EntityInfoModel _entity;
        private readonly EntityActionsModel _model;
    }
}
