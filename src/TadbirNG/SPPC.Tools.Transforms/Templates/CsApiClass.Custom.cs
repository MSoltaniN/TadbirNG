using SPPC.Framework.Utility;
using SPPC.Tools.Model;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class CsApiClass : ITextTemplate
    {
        public CsApiClass(ApiModel model)
        {
            _model = model;
        }

        private readonly ApiModel _model;
    }
}
