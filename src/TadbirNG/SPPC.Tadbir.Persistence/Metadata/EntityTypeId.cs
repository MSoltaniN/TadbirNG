using System;

namespace SPPC.Tadbir.Persistence
{
    internal enum EntityTypeId
    {
        None = 0,
        Account = 1,
        AccountCollectionAccount = 2,
        AccountGroup = 4,
        Branch = 5,
        CostCenter = 6,
        Currency = 7,
        DetailAccount = 9,
        FiscalPeriod = 10,
        OperationLog = 11,
        Project = 12,
        Setting = 15,
        Voucher = 17,
        DraftVoucher = 18,
        DashboardTab = 19,
        Widget = 20,
        CheckBook = 21,
        CashRegister = 22,
        SourceApp = 23,
        Receipt = 24,
        Payment = 25,
        Brand = 100001
    }
}
