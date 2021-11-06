using SPPC.Tools.Model;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class EmptyCrudController : ITextTemplate
    {
        public EmptyCrudController(ControllerModel model)
        {
            _model = model;
        }

        private readonly ControllerModel _model;
    }
}
