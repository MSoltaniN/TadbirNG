using System;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Model
{
    /// <summary>
    /// مشخصات مورد نیاز برای موجودیت های پایه ای برنامه را نگهداری می کند
    /// </summary>
    public class BaseEntity : FiscalEntity, IBaseEntity
    {
        /// <inheritdoc/>
        public virtual short BranchScope { get; set; }

        /// <inheritdoc/>
        public virtual int CreatedById { get; set; }

        /// <inheritdoc/>
        public virtual string CreatedByName { get; set; }

        /// <inheritdoc/>
        public virtual DateTime CreatedDate { get; set; }

        /// <inheritdoc/>
        public virtual int ModifiedById { get; set; }

        /// <inheritdoc/>
        public virtual string ModifiedByName { get; set; }
    }
}
