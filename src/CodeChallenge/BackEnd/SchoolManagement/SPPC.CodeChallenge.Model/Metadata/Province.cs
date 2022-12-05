using System.Collections.Generic;

namespace SPPC.CodeChallenge.Model.Metadata
{
    public partial class Province
    {
        /// <summary>
        /// مجموعه شهرهای استان
        /// </summary>
        public virtual IList<City> Cities { get; set; }
    }
}
