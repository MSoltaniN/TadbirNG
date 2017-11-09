using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class Customer
    {
        public Customer()
        {
            Invoice = new HashSet<Invoice>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CommerceCode { get; set; }
        public string Address { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<Invoice> Invoice { get; set; }
    }
}
