using System;

namespace SPPC.Tadbir.Utility.Model
{
    /// <summary>
    /// شناسه ویرایش های موجود برنامه را نگهداری می کند
    /// </summary>
    public sealed class Edition
    {
        private Edition()
        {
        }

        /// <summary>
        /// نام کامل ویرایش استاندارد برنامه
        /// </summary>
        public const string Standard = "Standard";

        /// <summary>
        /// نام کامل ویرایش حرفه ای برنامه
        /// </summary>
        public const string Professional = "Professional";

        /// <summary>
        /// نام کامل ویرایش سازمانی برنامه
        /// </summary>
        public const string Enterprise = "Enterprise";

        /// <summary>
        /// برچسب کوتاه شده برای ویرایش استاندارد برنامه
        /// </summary>
        public const string StandardTag = "std";

        /// <summary>
        /// برچسب کوتاه شده برای ویرایش حرفه ای برنامه
        /// </summary>
        public const string ProfessionalTag = "pro";

        /// <summary>
        /// برچسب کوتاه شده برای ویرایش سازمانی برنامه
        /// </summary>
        public const string EnterpriseTag = "ent";
    }
}
