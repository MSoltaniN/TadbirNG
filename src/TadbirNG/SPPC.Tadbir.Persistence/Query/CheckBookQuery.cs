using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPC.Tadbir.Persistence
{
    internal static class CheckBookQuery
    {
        internal const string AllCheckBook = @"
SELECT c.CheckBookID, c.No, c.Name, c.IssueDate,c.SartNo, c.EndNo, c.BankName, c.IsArchived,
    c.AccountID, c.DetailAccountID, c.CostCenterID, c.ProjectID,c.IsArchived, c.BranchID, br.Name AS BranchName
FROM [Check].[CheckBook] c
    INNER JOIN [Corporate].[Branch] br ON c.BranchID = br.BranchID
WHERE {0}
ORDER BY {1}";
    }
}
