using SPPC.Tadbir.Configuration.Models;

namespace SPPC.Tadbir.Service
{
    /// <summary>
    /// اطلاعات یک سطر اطلاعاتی را با توجه به تنظیمات ویرایش جاری برنامه اعتبارسنجی می کند
    /// </summary>
    public interface IModelValidator
    {
        /// <summary>
        /// اطلاعات یک سطر اطلاعاتی را با توجه به تنظیمات ویرایش جاری برنامه اعتبارسنجی می کند
        /// </summary>
        /// <param name="model">سطر اطلاعاتی مورد نظر برای اعتبارسنجی اطلاعات</param>
        /// <param name="config">تنظیمات ویرایش جاری برنامه</param>
        /// <returns>نتیجه اعتبارسنجی به صورت پیغام خطا یا رشته خالی در صورت معتبر بودن اطلاعات</returns>
        string Validate(object model, EditionConfig config);
    }
}
