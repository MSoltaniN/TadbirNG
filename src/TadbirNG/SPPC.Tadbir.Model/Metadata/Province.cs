using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.Model.Metadata
{
    public partial class Province
    {
        /// <summary>
        /// مجموعه شهرهای استان
        /// </summary>
        public IList<City> Cities { get; set; }
    }
}
