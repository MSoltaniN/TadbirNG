﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    internal enum OperationSourceId
    {
        None = 0,
        Journal = 1,
        AccountBook = 2,
        CurrencyBook = 3,
        TestBalance = 4,
        ItemBalance = 5
    }
}