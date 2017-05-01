using System;
using System.Security.Principal;
using System.Web.Security;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// هویت کاربری برای کار با سیستم تدبیر در چارچوب استاندارد امنیتی دات نت
    /// </summary>
    public class TadbirIdentity : FormsIdentity, IIdentity
    {
        /// <summary>
        /// یک نمونه جدید از این کلاس را ایجاد می کند.
        /// </summary>
        /// <param name="userId">شناسه دیتابیسی کاربر موجود در دیتابیس سیستمی تدبیر</param>
        /// <param name="ticket">بلیت یا مجوز بدست آمده از احراز هویت فرمی</param>
        public TadbirIdentity(int userId, FormsAuthenticationTicket ticket)
            : base(ticket)
        {
            UserId = userId;
        }

        /// <summary>
        /// شناسه دیتابیسی مربوط به این هویت کاربری
        /// </summary>
        public int UserId { get; private set; }
    }
}
