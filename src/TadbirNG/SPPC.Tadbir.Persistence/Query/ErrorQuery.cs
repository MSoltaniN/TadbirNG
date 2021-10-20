using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Persistence.Query
{
    /// <summary>
    ///
    /// </summary>
    public static class ErrorQuery
    {
        /// <summary>
        ///
        /// </summary>
        public const string Insert = @"
INSERT INTO [Core].[SystemError]
    ([CompanyID], [FiscalPeriodID], [BranchID], [TimestampUtc], [Code], [Message], [FaultingMethod], [FaultType], [StackTrace])
VALUES
    ({0}, {1}, {2}, '{3}', {4}, '{5}', '{6}', '{7}', '{8}')";
    }
}
