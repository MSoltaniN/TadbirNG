using System;
using System.Collections.Generic;

namespace SPPC.Tools.Model
{
    public class CurrencyInfo
    {
        public CurrencyInfo()
        {
            Currency = new CurrencyDetail();
        }

        public string Country { get; set; }

        public CurrencyDetail Currency { get; set; }
    }
}
