﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    internal enum EntityTypeId
    {
        None = 0,
        Account = 1,
        AccountCollectionAccount = 2,
        AccountRelations = 3,
        AccountGroup = 4,
        Branch = 5,
        CostCenter = 6,
        Currency = 7,
        CurrencyRate = 8,
        DetailAccount = 9,
        FiscalPeriod = 10,
        OperationLog = 11,
        Project = 12,
        RoleBranch = 13,
        RoleFiscalPeriod = 14,
        Setting = 15,
        TaxCurrency = 16,
        Voucher = 17,
        VoucherLine = 18
    }
}