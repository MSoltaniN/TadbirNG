using System;

namespace SPPC.CodeChallenge.Model.Core
{
    public partial class School
    {
        /// <summary>
        /// شناسه دیتابیسی استانی که مدرسه در آن واقع شده
        /// </summary>
        public virtual int ProvinceId { get; set; }

        /// <summary>
        /// شناسه دیتابیسی شهری که مدرسه در آن واقع شده
        /// </summary>
        public virtual int CityId { get; set; }
    }
}
