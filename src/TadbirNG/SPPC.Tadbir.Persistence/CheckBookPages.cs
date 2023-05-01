using System;
using System.Collections.Generic;
using SPPC.Framework.Common;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// کلاس کمکی برای تولید شماره سریال های یک دسته چک
    /// </summary>
    public class CheckBookPages
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="firstPageSerial">شماره سریال اولین برگه از دسته چک</param>
        /// <param name="sayyadStartNo">شماره شروع صیاد</param>
        /// <param name="pageCount">تعداد برگه های مورد نیاز برای دسته چک</param>
        public CheckBookPages(string firstPageSerial, string sayyadStartNo, int pageCount)
        {
            Verify.ArgumentNotNullOrWhitespace(firstPageSerial, nameof(firstPageSerial));
            Count = pageCount;
            InitSerials(firstPageSerial, sayyadStartNo, pageCount);
        }

        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="firstPageSerial">شماره سریال اولین برگه از دسته چک</param>
        /// <param name="lastPageSerial">شماره سریال آخرین برگه از دسته چک</param>
        /// <param name="sayyadStartNo">شماره شروع صیاد</param>
        public CheckBookPages(string firstPageSerial, string lastPageSerial, string sayyadStartNo)
        {
            Verify.ArgumentNotNullOrWhitespace(firstPageSerial, nameof(firstPageSerial));
            Verify.ArgumentNotNullOrWhitespace(lastPageSerial, nameof(lastPageSerial));
            Count = GetPageCount(firstPageSerial, lastPageSerial);
            InitSerials(firstPageSerial, sayyadStartNo, Count);
        }

        /// <summary>
        /// تعداد برگه های تعیین شده یا به دست آمده برای دسته چک
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// مجموعه شماره سریال های تولیدشده برای برگه های دسته چک 
        /// </summary>
        public IEnumerable<string> Serials
        {
            get { return _serials; }
        }

        /// <summary>
        /// مجموعه شماره سریال های تولیدشده برای برگه های دسته چک 
        /// </summary>
        public IEnumerable<string> SayyadNumbers
        {
            get { return _sayyadNumbers; }
        }
        private void InitSerials(string firstPageSerial, string sayyadStartNo, int pageCount)
        {
            _serials = new List<string>(pageCount);
            _sayyadNumbers = new List<string>(pageCount);
            int paddingCount = GetZeroPaddingCount(firstPageSerial);
            int serialNo = Convert.ToInt32(firstPageSerial);
            long sayyadNo = Convert.ToInt64(sayyadStartNo);
            for (int index = 0; index < pageCount; index++)
            {
                _serials.Add($"{new string('0', paddingCount)}{serialNo + index}");
                _sayyadNumbers.Add($"{sayyadNo + index}");
            }
        }

        private static int GetPageCount(string firstPageSerial, string lastPageSerial)
        {
            int startNo = 0;
            int endNo = 0;
            if (!String.IsNullOrEmpty(firstPageSerial))
            {
                startNo = Convert.ToInt32(firstPageSerial);
            }

            if (!String.IsNullOrEmpty(lastPageSerial))
            {
                endNo = Convert.ToInt32(lastPageSerial);
            }

            return endNo - startNo + 1;
        }
        
        private static int GetZeroPaddingCount(string number)
        {
            if (!Int32.TryParse(number, out int numberValue))
            {
                throw ExceptionBuilder.NewArgumentException(
                    "Input string must represent a number.", nameof(number));
            }

            return number.ToString().Length - GetDigitCount(numberValue);
        }

        // Warning: This method DOES NOT support negative numbers.
        private static int GetDigitCount(int number)
        {
            int fixedNumber = Math.Max(number, 1);
            return (int)Math.Floor(Math.Log10(fixedNumber)) + 1;
        }

        private List<string> _serials;
        private List<string> _sayyadNumbers;
    }
}
