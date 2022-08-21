using SPPC.Framework.Utility;
using SPPC.Tools.Model;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class BlankController : ITextTemplate
    {
        public BlankController(ControllerModel model)
        {
            _model = model;
        }

        private readonly ControllerModel _model;
    }
}
