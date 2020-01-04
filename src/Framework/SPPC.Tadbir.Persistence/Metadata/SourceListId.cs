﻿using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    internal enum SourceListId
    {
        None = 0,
        JournalByDateByRow = 1,
        JournalByDateByRowDetail = 2,
        JournalByDateByLedger = 3,
        JournalByDateBySubsidiary = 4,
        JournalByDateSummary = 5,
        JournalByDateSummaryByDate = 6,
        JournalByDateSummaryByMonth = 7,
        JournalByNoByRow = 8,
        JournalByNoByRowDetail = 9,
        JournalByNoByLedger = 10,
        JournalByNoBySubsidiary = 11,
        JournalByNoSummary = 12,
        AccountBookByRow = 13,
        AccountBookVoucherSum = 14,
        AccountBookDailySum = 15,
        AccountBookMonthlySum = 16,
        CurrencyBookByRow = 17,
        CurrencyBookVoucherSum = 18,
        CurrencyBookDailySum = 19,
        CurrencyBookMonthlySum = 20,
        CurrencyBookAllCurrencies = 21,
        TestBalance2Column = 22,
        TestBalance4Column = 23,
        TestBalance6Column = 24,
        TestBalance8Column = 25,
        TestBalance10Column = 26,
        DetailAccountBalance2Column = 27,
        DetailAccountBalance4Column = 28,
        DetailAccountBalance6Column = 29,
        DetailAccountBalance8Column = 30,
        DetailAccountBalance10Column = 31,
        CostCenterBalance2Column = 32,
        CostCenterBalance4Column = 33,
        CostCenterBalance6Column = 34,
        CostCenterBalance8Column = 35,
        CostCenterBalance10Column = 36,
        ProjectBalance2Column = 37,
        ProjectBalance4Column = 38,
        ProjectBalance6Column = 39,
        ProjectBalance8Column = 40,
        ProjectBalance10Column = 41
    }
}