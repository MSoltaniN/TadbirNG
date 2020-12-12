using System;
using System.Collections.Generic;

namespace SPPC.Framework.Cryptography
{
    /// <summary>
    /// عملیات مورد نیاز برای تبدیل آبجکت های سی شارپ به رشته کدشده و بالعکس را تعریف می کند
    /// </summary>
    public interface IEncodedSerializer
    {
        /// <summary>
        /// آبجکت داده شده را به متن کدشده تبدیل کرده و برمی گرداند
        /// </summary>
        /// <typeparam name="T">نوع کلاس آبجکت داده شده برای تبدیل</typeparam>
        /// <param name="data">آبجکت داده شده برای تبدیل</param>
        /// <returns>شکل متنی و کدشده اطلاعات آبجکت داده شده</returns>
        string Serialize<T>(T data);

        /// <summary>
        /// متن کدشده داده شده را تبدیل به آبجکت اولیه می کند
        /// </summary>
        /// <typeparam name="T">نوع کلاس آبجکت مورد نظر برای تبدیل</typeparam>
        /// <param name="encoded">شکل متنی کدشده داده شده برای تبدیل</param>
        /// <returns>آبجکت تبدیل شده</returns>
        T Deserialize<T>(string encoded);
    }
}
