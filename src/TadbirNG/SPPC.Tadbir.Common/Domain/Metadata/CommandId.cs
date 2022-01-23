using System;

namespace SPPC.Tadbir.Domain
{
    /// <summary>
    /// داده شمارشی برای تعریف شناسه های دیتابیسی دستورات خاص در منوی برنامه
    /// </summary>
    public enum CommandId
    {
        /// <summary>
        /// دستور منوی نامشخص
        /// </summary>
        None = 0,

        /// <summary>
        /// شناسه منوی دسترسی به سطرها
        /// </summary>
        RowAccessSettings = 30
    }
}
