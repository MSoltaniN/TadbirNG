using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class Company
    {
        public Company()
        {
            Branch = new HashSet<Branch>();
            FiscalPeriod = new HashSet<FiscalPeriod>();
        }

        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<Branch> Branch { get; set; }
        public ICollection<FiscalPeriod> FiscalPeriod { get; set; }
    }
}
