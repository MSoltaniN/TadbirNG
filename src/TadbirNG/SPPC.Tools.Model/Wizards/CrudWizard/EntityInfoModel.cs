using System;
using System.Collections.Generic;

namespace SPPC.Tools.Model
{
    public class EntityInfoModel
    {
        public EntityInfoModel()
        {
            IsFiscalEntity = true;
        }

        public Repository Repository { get; set; }

        public Entity Entity { get; set; }

        public string SingularName { get; set; }

        public string PluralName { get; set; }

        public bool IsFiscalEntity { get; set; }

        public bool IsSystemEntity { get; set; }
    }
}
