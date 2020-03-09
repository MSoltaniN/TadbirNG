using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    internal enum OperationId
    {
        None = 0,
        View = 1,
        Create = 2,
        Edit = 3,
        Delete = 4,
        Filter = 5,
        Print = 6,
        Save = 7,
        Archive = 8,
        SetDefault = 9,
        Design = 10,
        Check = 11,
        UndoCheck = 12,
        Confirm = 13,
        UndoConfirm = 14,
        Approve = 15,
        UndoApprove = 16,
        Finalize = 17,
        UndoFinalize = 18,
        Mark = 19,
        QuickReportDesign = 20,
        GroupDelete = 21,
        FailedLogin = 22,
        CompanyLogin = 23,
        SwitchFiscalPeriod = 24,
        SwitchBranch = 25,
        AssignRole = 26,
        AssignUser = 27,
        BranchAccess = 28,
        FiscalPeriodAccess = 29,
        ViewArchive = 30,
        CalendarChange = 31,
        CurrencyChange = 32,
        DecimalCountChange = 33,
        DefaultCodingChange = 34,
        RoleAccess = 35
    }
}
