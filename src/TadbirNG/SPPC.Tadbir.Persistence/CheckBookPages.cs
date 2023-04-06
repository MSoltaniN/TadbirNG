using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="firstPage">شماره سریال اولین برگه از دسته چک</param>
        /// <param name="pageCount">تعداد برگه های مورد نیاز برای دسته چک</param>
        public CheckBookPages(string firstPage, int pageCount)
        {
            Verify.ArgumentNotNullOrWhitespace(firstPage, nameof(firstPage));
            Count = pageCount;
            InitSerials(firstPage, pageCount);
        }

        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="firstPage">شماره سریال اولین برگه از دسته چک</param>
        /// <param name="lastPage">شماره سریال آخرین برگه از دسته چک</param>
        public CheckBookPages(string firstPage, string lastPage)
        {
            Verify.ArgumentNotNullOrWhitespace(firstPage, nameof(firstPage));
            Verify.ArgumentNotNullOrWhitespace(lastPage, nameof(lastPage));
            Count = GetPageCount(firstPage, lastPage);
            InitSerials(firstPage, Count);
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

        private void InitSerials(string firstPage, int pageCount)
        {
            _serials = new List<string>(Count);
            GetSeriesAndSerialNo(firstPage, out string series, out string serial);
            int paddingCount = GetZeroPaddingCount(serial);
            int serialNo = Convert.ToInt32(serial);
            for (int index = 0; index < Count; index++)
            {
                _serials.Add($"{series}{new string('0', paddingCount)}{serialNo + index}");
            }
        }

        private static int GetPageCount(string firstPage, string lastPage)
        {
            int startNo = 0;
            int endNo = 0;
            GetSeriesAndSerialNo(firstPage, out _, out string startSerial);
            GetSeriesAndSerialNo(lastPage, out _, out string endSerial);
            if (!String.IsNullOrEmpty(startSerial))
            {
                startNo = Convert.ToInt32(startSerial);
            }

            if (!String.IsNullOrEmpty(endSerial))
            {
                endNo = Convert.ToInt32(endSerial);
            }

            return endNo - startNo + 1;
        }

        private static void GetSeriesAndSerialNo(string pageNo, out string series, out string serialNo)
        {
            series = new string(pageNo
                .TakeWhile(ch => !Char.IsDigit(ch))
                .ToArray());
            serialNo = new string(pageNo
                .SkipWhile(ch => !Char.IsDigit(ch))
                .TakeWhile(ch => Char.IsDigit(ch))
                .ToArray());
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

        private static int GetDigitCount(int number)
        {
            int fixedNumber = Math.Max(number, 1);
            return (int)Math.Floor(Math.Log10(fixedNumber)) + 1;
        }

        private List<string> _serials;
    }
}
