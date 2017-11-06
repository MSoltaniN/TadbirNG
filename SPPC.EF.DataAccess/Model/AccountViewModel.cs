using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SPPC.Tadbir.DataAccess
{
    public partial class AccountViewModel
    {
        [Key]
        public int AccountId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the fiscal period in which this financial account is defined.
        /// </summary>
        public int FiscalPeriodId { get; set; }

        public int BranchId { get; set; }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Name;
        }

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
