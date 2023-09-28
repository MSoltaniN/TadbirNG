using System;
using System.ComponentModel.DataAnnotations;
using SPPC.Tadbir.Domain;

namespace SPPC.Tadbir.Model
{
    /// <summary>
    /// مشخصات مورد نیاز برای موجودیت های پایه ای برنامه را نگهداری می کند
    /// </summary>
    public class PBaseEntity : PFiscalEntity, IBaseEntity
    {
        /// <inheritdoc/>
        [Required]
        public virtual short BranchScope { get; set; }

        /// <inheritdoc/>
        [Required]
        public virtual int CreatedById { get; set; }

        /// <inheritdoc/>
        [Required]
        public virtual string CreatedByName { get; set; }

        /// <inheritdoc/>
        [Required]
        public virtual DateTime CreatedDate { get; set; }

        /// <inheritdoc/>
        [Required]
        public virtual int ModifiedById { get; set; }

        /// <inheritdoc/>
        [Required]
        public virtual string ModifiedByName { get; set; }
    }
}
