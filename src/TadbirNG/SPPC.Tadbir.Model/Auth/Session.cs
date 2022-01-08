using System;

namespace SPPC.Tadbir.Model.Auth
{
    public partial class Session
    {
        /// <summary>
        /// شناسه دیتابیسی کاربر متناظر با این جلسه کاری برنامه
        /// </summary>
        public virtual int UserId { get; set; }
    }
}
