using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Tadbir.ViewModel.Metadata
{
    /// <summary>
    /// اطلاعات استان و شهرها موجود در فایل اکسس
    /// </summary>
    public class ZoneViewModel
    {
        /// <summary>
        /// نام استان
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// کد استان
        /// </summary>
        public string ProvinceCode { get; set; }

        /// <summary>
        /// نام شهر
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// کد شهر
        /// </summary>
        public string CityCode { get; set; }
    }
}
