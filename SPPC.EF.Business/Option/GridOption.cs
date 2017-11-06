using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Business
{
    public class GridOption
    {
        public int? StartIndex { get; set; }

        public int? Count { get; set; }

        public IList<Filter> Filters { get; set; }

        public string OrderBy { get; set; }
    }    
}
