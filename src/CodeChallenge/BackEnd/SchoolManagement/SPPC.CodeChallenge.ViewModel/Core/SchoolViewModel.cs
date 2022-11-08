using System;

namespace SPPC.CodeChallenge.ViewModel.Core
{
    public partial class SchoolViewModel
    {
        /// <summary>
        /// شناسه دیتابیسی استانی که مدرسه در آن واقع شده
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شهری که مدرسه در آن واقع شده
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// نام استانی که مدرسه در آن واقع شده
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// نام شهری که مدرسه در آن واقع شده
        /// </summary>
        public string CityName { get; set; }
    }
}
