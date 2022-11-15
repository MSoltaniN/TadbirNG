using System;

namespace SPPC.Tadbir.Utility
{
    /// <summary>
    /// اطلاعات ابتدا و انتهای یکی از فواصل زمانی - ماه، هفته، فصل و غیره - در یک تقویم را نگهداری می کند
    /// </summary>
    public class DateSpanInfo
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        public DateSpanInfo()
        {
        }

        /// <summary>
        /// نام محلی شده فاصله زمانی در تقویم مربوطه
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// تاریخ ابتدای فاصله زمانی در تقویم مرجع - میلادی
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// تاریخ انتهای فاصله زمانی در تقویم مرجع - میلادی
        /// </summary>
        public DateTime End { get; set; }

        /// <summary>
        /// آبجکت مورد نظر را به صورت متنی برمی گرداند
        /// </summary>
        /// <returns>آبجکت مورد نظر به صورت متنی</returns>
        public override string ToString()
        {
            return String.Format(
                "Name : {1}{0}From {2} to {3}",
                Environment.NewLine, Name, Start, End);
        }
    }
}
