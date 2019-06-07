using System;
using SPPC.Framework.Common;
using SPPC.Tadbir.Tools.SystemDesigner.Models;

namespace SPPC.Tadbir.Tools.SystemDesigner.Templates
{
    public partial class RepoImplementationFromMetadata
    {
        public RepoImplementationFromMetadata(EntityInfoModel model)
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

        private readonly EntityInfoModel _model;
    }
}
