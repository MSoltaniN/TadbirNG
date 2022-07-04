using System;

namespace SPPC.Framework.Extensions
{
    /// <summary>
    /// عملیات تکمیلی مورد نیاز برای کار با آرایه ها را به کلاس موجود در دات نت اضافه می کند
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// با استفاده از نسخه مدرن الگوریتم فیشر-ییتز، اقلام موجود در آرایه ورودی را
        /// با یک ترتیب تصادفی جابجا می کند
        /// </summary>
        /// <param name="array">آرایه ورودی برای درهم سازی. اقلام این آرایه درجا درهم سازی می شوند، در نتیجه
        /// کپی غیرضروری از این اقلام انجام نمی شود</param>
        public static void Shuffle(this Array array)
        {
            var random = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < array.Length - 1; i++)
            {
                int j = random.Next(i, array.Length);
                var temp = array.GetValue(i);
                array.SetValue(array.GetValue(j), i);
                array.SetValue(temp, j);
            }
        }
    }
}
