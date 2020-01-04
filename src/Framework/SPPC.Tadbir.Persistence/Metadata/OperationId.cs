﻿using System;
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
        QuickReportDesign = 20
    }
}