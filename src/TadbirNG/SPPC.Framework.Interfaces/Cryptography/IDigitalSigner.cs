using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace SPPC.Framework.Cryptography
{
    /// <summary>
    /// عملیات مورد نیاز برای امضای دیجیتالی و تأیید امضا را تعریف می کند
    /// </summary>
    public interface IDigitalSigner
    {
        /// <summary>
        /// گواهینامه امنیتی مورد نظر برای انجام عملیات
        /// </summary>
        X509Certificate2 Certificate { get; set; }

        /// <summary>
        /// اطلاعات باینری داده شده را امضای دیجیتالی می کند
        /// </summary>
        /// <param name="data">اطلاعات مورد نظر برای امضا</param>
        /// <returns>امضای دیجیتالی اطلاعات داده شده به شکل متنی</returns>
        string SignData(byte[] data);

        /// <summary>
        /// اطلاعات باینری داده شده را با توجه به امضای دیجیتالی داده شده تأیید یا رد می کند
        /// </summary>
        /// <param name="data">اطلاعات مورد نظر برای تأیید امضا</param>
        /// <param name="signature">امضای دیجیتالی مورد استفاده برای تأیید اطلاعات</param>
        /// <returns>در صورت درستی اطلاعات داده شده مقدار بولی "درست" و در غیر این صورت
        /// مقدار بولی "نادرست" را برمی گرداند</returns>
        bool VerifyData(byte[] data, string signature);
    }
}
