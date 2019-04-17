using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Helpers
{
    /// <summary>
    /// امکان مقایسه عددی دو مقدار متنی را فراهم می کند
    /// </summary>
    public class NumericTextComparer : IComparer<string>
    {
        /// <summary>
        /// دو مقدار متنی داده شده را با یکدیگر مقایسه می کند
        /// </summary>
        /// <param name="x">مقدار متنی اول</param>
        /// <param name="y">مقدار متنی دوم</param>
        /// <returns>اگر مقدار اول بزرگتر از مقدار دوم باشد عدد 1،
        /// اگر مقدار اول کوچکتر از مقدار دوم باشد عدد منفی 1
        /// و اگر دو مقدار برابر باشند عدد 0 را برمی گرداند
        /// </returns>
        public int Compare(string x, string y)
        {
            int xAsNum, yAsNum;
            if (Int32.TryParse(x ?? String.Empty, out xAsNum)
                && Int32.TryParse(y ?? String.Empty, out yAsNum))
            {
                return (xAsNum > yAsNum)
                    ? 1
                    : ((xAsNum < yAsNum) ? -1 : 0);
            }
            else
            {
                return String.Compare(x, y);
            }
        }
    }
}
