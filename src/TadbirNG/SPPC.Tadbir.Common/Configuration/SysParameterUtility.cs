using System.IO;
using System.Reflection;
using SPPC.Framework.Helpers;

namespace SPPC.Tadbir.Configuration
{
    /// <summary>
    /// عملیات کمکی برای استفاده راحت تر از پارامترهای سیستمی برنامه را پیاده سازی می کند
    /// </summary>
    public class SysParameterUtility
    {
        static SysParameterUtility()
        {
            _sysParams = LoadParameters();
        }

        /// <summary>
        /// اطلاعات کامل پارامترهای سیستمی برنامه
        /// </summary>
        public static SysParameters AllParameters
        {
            get { return _sysParams; }
        }

        /// <summary>
        /// پیشوند مورد استفاده برای ایمیج های داکر که معمولا نام حساب کاربری سرویس داکرهاب است
        /// </summary>
        public static string DockerHubHandle
        {
            get { return _sysParams?.Docker?.HubHandle; }
        }

        /// <summary>
        /// پارامترهای مورد استفاده در سرویس داکری مجوزدهی
        /// </summary>
        public static DockerServiceParameters LicenseServer
        {
            get { return _sysParams?.Docker?.License; }
        }

        /// <summary>
        /// پارامترهای مورد استفاده در سرویس وب اصلی برنامه
        /// </summary>
        public static DockerServiceParameters ApiServer
        {
            get { return _sysParams?.Docker?.Api; }
        }

        /// <summary>
        /// پارامترهای مورد استفاده در سرویس داکری سرور دیتابیس
        /// </summary>
        public static DockerServiceParameters DbServer
        {
            get { return _sysParams?.Docker?.Db; }
        }

        /// <summary>
        /// پارامترهای مورد استفاده در سرویس داکری برنامه وب تدبیر
        /// </summary>
        public static DockerServiceParameters WebApp
        {
            get { return _sysParams?.Docker?.App; }
        }

        /// <summary>
        /// پارامترهای مورد استفاده در سرویس شناسه سخت افزاری
        /// </summary>
        public static ServiceParameters Service
        {
            get { return _sysParams?.Service; }
        }

        /// <summary>
        /// نام کامل ایمیج ساخنه شده برای یکی از سرویس های داکری برنامه را برمی گرداند
        /// </summary>
        /// <param name="serviceParams">اطلاعات پارامترهای سیستمی ایمیج مورد نظر</param>
        /// <param name="tag">برچسب مورد نیاز برای سرویس داکری مورد نظر</param>
        /// <returns>نام کامل ایمیج ساخته شده برای سرویس</returns>
        public static string GetImageFullName(DockerServiceParameters serviceParams, string tag = null)
        {
            return $"{DockerHubHandle}/{serviceParams.ImageName}:{tag ?? serviceParams.Tag}";
        }

        private static SysParameters LoadParameters()
        {
            var sysParams = default(SysParameters);
            var toolsAssembly = Assembly.GetAssembly(typeof(SysParameterUtility));
            if (toolsAssembly != null)
            {
                using var reader = new StreamReader(
                    toolsAssembly.GetManifestResourceStream(_sysParamsUri));
                string jsonConfig = reader.ReadToEnd();
                sysParams = JsonHelper.To<SysParameters>(jsonConfig);
            }

            return sysParams;
        }

        private const string _sysParamsUri = "SPPC.Tadbir.Configuration.sys-parameters.json";
        private static readonly SysParameters _sysParams;
    }
}
