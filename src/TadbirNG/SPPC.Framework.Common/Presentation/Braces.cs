using System;
using System.Collections.Generic;
using System.Text;

namespace SPPC.Framework.Presentation
{
    /// <summary>
    /// کلاس مربوط به پرانتز
    /// </summary>
    public class Braces
    {
        /// <summary>
        /// شناسه فیلتر را نگهداری میکتد
        /// </summary>
        public string OuterId { get; set; }

        /// <summary>
        /// علامت پرانتز که می تواند ( یا ) باشد
        /// </summary>
        public string Brace { get; set; }
    }
}
