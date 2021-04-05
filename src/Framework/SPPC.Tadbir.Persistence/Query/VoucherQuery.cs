using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence
{
    internal static class VoucherQuery
    {
        internal const string EnvironmentVouchers = @"
SELECT v.VoucherID, v.No, v.Date, v.Description, st.Name AS StatusName, v.Reference, v.Association, v.DailyNo, v.IsBalanced,
    v.ConfirmerName, v.ApproverName, v.ConfirmedByID, v.ApprovedByID, br.Name AS BranchName, v.IssuerName, vo.Name AS OriginName,
    v.SubjectType
FROM [Finance].[Voucher] v
    INNER JOIN [Corporate].[Branch] br ON v.BranchID = br.BranchID
    INNER JOIN [Core].[DocumentStatus] st ON v.StatusID = st.StatusID
    INNER JOIN [Finance].[VoucherOrigin] vo ON v.OriginID = vo.OriginID
WHERE {0}
ORDER BY v.Date, v.No";
    }
}
