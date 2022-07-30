using System;

namespace SPPC.Licensing.Model
{
    [Flags]
    public enum Subsystems
    {
        None = 0x0,
        Accounting = 0x1,
        Cheque = 0x2,
        CashFlow = 0x4,
        WagePayment = 0x8,
        Personnel = 0x10,
        Inventory = 0x20,
        Purchase = 0x40,
        Sales = 0x80,
        Warehousing = 0x100,
        Budgeting = 0x200,
    }
}
