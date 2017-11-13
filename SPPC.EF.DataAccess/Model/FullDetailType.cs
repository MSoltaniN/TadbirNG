using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.DataAccess
{
    public partial class FullDetailType
    {
        public FullDetailType()
        {
            FullDetail = new HashSet<FullDetail>();
        }

        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public ICollection<FullDetail> FullDetail { get; set; }
    }
}
