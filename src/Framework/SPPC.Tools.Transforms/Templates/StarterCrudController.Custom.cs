using System;
using SPPC.Framework.Common;
using SPPC.Tools.Model;

namespace SPPC.Tools.Transforms.Templates
{
    public partial class StarterCrudController : ITextTemplate
    {
        public StarterCrudController(ControllerModel model)
        {
            _model = model;
        }

        private static string GetPluralName(string name)
        {
            Verify.ArgumentNotNullOrEmptyString(name, "name");
            char lastChar = name[name.Length - 1];
            string plural = name;
            switch (lastChar)
            {
                case 'h':
                case 's':
                case 'x':
                case 'z':
                    plural = String.Format("{0}es", name);
                    break;
                case 'y':
                    plural = String.Format("{0}ies", name.Substring(0, name.Length - 1));
                    break;
                default:
                    plural = String.Format("{0}s", name);
                    break;
            }

            return plural;
        }

        private readonly ControllerModel _model;
    }
}
