﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Tools.SystemDesigner.Models
{
    public class CurrencyDetail
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string MinorUnit { get; set; }

        public int DecimalCount { get; set; }
    }
}