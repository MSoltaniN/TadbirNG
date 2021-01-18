using System;
using System.Collections.Generic;

namespace SPPC.Tools.Model
{
    public class CurrencyDetail
    {
        public string NameKey { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string MinorUnitKey { get; set; }

        public string MinorUnit { get; set; }

        public int DecimalCount { get; set; }
    }
}