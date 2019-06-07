using System;
using BabakSoft.Platform.Metadata;

namespace SPPC.Tadbir.Tools.SystemDesigner.Models
{
    public class EntityInfoModel
    {
        public EntityInfoModel()
        {
            IsFiscalEntity = true;
        }

        public Entity Entity { get; set; }

        public string SingularName { get; set; }

        public string PluralName { get; set; }

        public bool IsFiscalEntity { get; set; }
    }
}
