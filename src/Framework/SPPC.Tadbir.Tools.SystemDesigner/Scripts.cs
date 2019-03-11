using System;
using System.Collections.Generic;

namespace SPPC.Tadbir.Tools.SystemDesigner
{
    public static class Scripts
    {
        public const string InsertDefaultBranch =
            @"INSERT INTO [Corporate].[Branch]
           ([BranchID]
		   ,[CompanyID]
           ,[Name]
           ,[Level])
     VALUES
           (1, 1, N'دفتر مرکزی', 0)";

        public const string InsertDefaultCurrency =
            @"INSERT INTO [Finance].[Currency]
           ([CurrencyID], [Name])
     VALUES
           (1, N'ریال')";

        public const string InsertFiscalPeriod =
            @"INSERT INTO [Finance].[FiscalPeriod]
           ([FiscalPeriodID]
		   ,[CompanyID]
           ,[Name]
           ,[StartDate]
           ,[EndDate])
     VALUES
           ('{0}', '1', N'{1}', '{2}', '{3}')";

        public const string InsertAccountGroup =
            @"INSERT INTO [Finance].[AccountGroup]
           ([GroupID]
		   ,[Name]
           ,[InventoryMode]
           ,[Category])
     VALUES
           ('{0}', N'{1}', '{2}', '{3}')";

        public const string InsertAccount =
            @"INSERT INTO [Finance].[Account]
           ([AccountID]
		   ,[ParentID]
           ,[GroupID]
           ,[FiscalPeriodID]
           ,[BranchID]
           ,[CurrencyID]
           ,[BranchScope]
           ,[Code]
           ,[FullCode]
           ,[Name]
           ,[Level])
     VALUES
           ('{0}',{1},{2},'{3}','1','1','0','{4}','{5}',N'{6}','{7}')";
    }
}
