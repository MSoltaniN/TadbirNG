using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Helpers
{
    /// <summary>
    /// اطلاعات ابتدا و انتهای یکی از ماه های یک تقویم را نگهداری می کند
    /// </summary>
    public class MonthInfo
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public MonthInfo()
        {
        }

        /// <summary>
        /// نام محلی شده ماه در تقویم مربوطه
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// تاریخ ابتدای ماه در تقویم مرجع (میلادی)
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// تاریخ انتهای ماه در تقویم مرجع (میلادی)
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// آبجکت مورد نظر را به صورت متنی برمی گرداند
        /// </summary>
        /// <returns>آبجکت مورد نظر به صورت متنی</returns>
        public override string ToString()
        {
            return String.Format(
                "Month : {1}{0}From {2} to {3}",
                Environment.NewLine, Name, Start, End);
        }
    }
}
