using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Security
{
    /// <summary>
    /// عملیات مورد نیاز برای تشخیص اطلاعات یک جلسه کاری در برنامه را تعریف می کند
    /// </summary>
    public interface ISessionProvider
    {
        /// <summary>
        /// اطلاعات کامل جلسه کاری فعلی کاربر در برنامه را تهیه کرده و برمی گرداند
        /// </summary>
        /// <param name="userAgent">اطلاعات فرستاده شده توسط مرورگر وب در هدر عامل کاربری</param>
        /// <param name="ipAddress">آدرس آی پی که از دستگاه کاربر به دست آمده است</param>
        /// <returns>اطلاعات کامل جلسه کاری فعلی کاربر در برنامه</returns>
        SessionViewModel GetSession(string userAgent, string ipAddress);
    }
}
