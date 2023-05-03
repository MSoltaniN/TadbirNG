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
        /// <param name="checkSeriesNo">شماره سری دسته چک</param>
        /// <param name="sayyadStartNo">شماره صیاد شروع دسته چک که معادل شماره صیاد اولین برگه است</param>
        /// <param name="pageCount">تعداد برگه های مورد نیاز برای دسته چک</param>
        public CheckBookPages(string firstPage, string checkSeriesNo, string sayyadStartNo, int pageCount)
        {
            Verify.ArgumentNotNullOrWhitespace(firstPage, nameof(firstPage));
            Verify.ArgumentNotNullOrWhitespace(checkSeriesNo, nameof(checkSeriesNo));
            Verify.ArgumentNotNullOrWhitespace(sayyadStartNo, nameof(sayyadStartNo));
            Count = pageCount;
            InitSerials(firstPage, checkSeriesNo, sayyadStartNo, pageCount);
        }

        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="firstPage">شماره سریال اولین برگه از دسته چک</param>
        /// <param name="checkSeriesNo">شماره سری دسته چک</param>
        /// <param name="sayyadStartNo">شماره صیاد شروع دسته چک که معادل شماره صیاد اولین برگه است</param>
        /// <param name="lastPage">شماره سریال آخرین برگه از دسته چک</param>
        public CheckBookPages(string firstPage, string lastPage, string checkSeriesNo, string sayyadStartNo)
        {
            Verify.ArgumentNotNullOrWhitespace(firstPage, nameof(firstPage));
            Verify.ArgumentNotNullOrWhitespace(lastPage, nameof(lastPage));
            Verify.ArgumentNotNullOrWhitespace(checkSeriesNo, nameof(checkSeriesNo));
            Verify.ArgumentNotNullOrWhitespace(sayyadStartNo, nameof(sayyadStartNo));
            Count = GetPageCount(firstPage, lastPage);
            InitSerials(firstPage, checkSeriesNo, sayyadStartNo, Count);
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
        /// مجموعه شماره صیادهای تولیدشده برای برگه های دسته چک 
        /// </summary>
        public IEnumerable<string> SayyadSerials
        {
            get { return _sayyadSerials; }
        }

        private void InitSerials(string firstPage, string checkSeriesNo, string sayyadStartNo, int pageCount)
        {
            _serials = new List<string>(pageCount);
            _sayyadSerials = new List<string>(pageCount);
            GetSeriesAndSerialNo(firstPage, out string series, out string serial);
            int paddingCount = GetZeroPaddingCount(serial);
            int serialNo = Convert.ToInt32(serial);
            long sayyadNo = Convert.ToInt64(sayyadStartNo);
            for (int index = 0; index < pageCount; index++)
            {
                _serials.Add($"{checkSeriesNo}/{series}{new string('0', paddingCount)}{serialNo + index}");
                _sayyadSerials.Add($"{sayyadNo + index}");
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
            var reversePageNo = new string(pageNo.Reverse().ToArray());
            serialNo = new string(reversePageNo
                .TakeWhile(ch => Char.IsDigit(ch))
                .Reverse()
                .ToArray());
            series = new string(reversePageNo
                .Skip(serialNo.Length)
                .Take(reversePageNo.Length - serialNo.Length)
                .Reverse()
                .ToArray());
        }

        private static int GetZeroPaddingCount(string number)
        {
            if (!Int32.TryParse(number, out int numberValue))
            {
                throw ExceptionBuilder.NewArgumentException(
                    "Input string must represent a number.", nameof(number));
            }

            return number.ToString().Length - numberValue.ToString().Length;
        }

        private List<string> _serials;
        private List<string> _sayyadSerials;
    }
}
