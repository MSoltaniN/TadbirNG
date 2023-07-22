using System;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// مشخصات مورد نیاز برای نمایش موجودیت های پایه ای برنامه را تعریف می کند
    /// </summary>
    public interface IBaseEntityView : IFiscalEntity
    {
        /// <summary>
        /// وضعیت فعال یا غیر فعال برای این سطر اطلاعاتی
        /// </summary>
        string State { get; set; }
    }
}
