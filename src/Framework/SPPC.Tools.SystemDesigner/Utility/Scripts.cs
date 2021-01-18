using System;
using System.Collections.Generic;

namespace SPPC.Tools.SystemDesigner
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

        public const string InsertCurrency =
            @"INSERT INTO [Finance].[Currency]
           ([CurrencyID], [Name])
     VALUES
           ('{0}', N'{1}')";

        public const string InsertFiscalPeriod =
            @"INSERT INTO [Finance].[FiscalPeriod]
           ([FiscalPeriodID]
		   ,[CompanyID]
           ,[Name]
           ,[StartDate]
           ,[EndDate]
           ,[Description])
     VALUES
           ('{0}', '1', N'{1}', '{2}', '{3}', N'{4}')";

        public const string InsertAccountGroup =
            @"INSERT INTO [Finance].[AccountGroup]
           ([GroupID]
		   ,[Name]
           ,[InventoryMode]
           ,[Category]
           ,[Description])
     VALUES
           ('{0}', N'{1}', '{2}', '{3}', N'{4}')";

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
           ,[Level]
           ,[IsActive]
           ,[IsCurrencyAdjustable]
           ,[TurnoverMode]
           ,[Description])
     VALUES
           ('{0}',{1},{2},'{3}','1',NULL,'0', N'{4}', N'{5}',N'{6}','{7}', '1', '1', '-1', N'{7}')";

        public const string InsertDetailAccount =
            @"INSERT INTO [Finance].[DetailAccount]
           ([DetailID]
		   ,[ParentID]
           ,[FiscalPeriodID]
           ,[BranchID]
           ,[BranchScope]
           ,[Code]
           ,[FullCode]
           ,[Name]
           ,[Level]
           ,[Description])
     VALUES
           ('{0}', {1}, '{2}', 1, 0, N'{3}', N'{4}', N'{5}', {6}, N'{7}')";

        public const string InsertCostCenter =
            @"INSERT INTO [Finance].[CostCenter]
           ([CostCenterID]
		   ,[ParentID]
           ,[FiscalPeriodID]
           ,[BranchID]
           ,[BranchScope]
           ,[Code]
           ,[FullCode]
           ,[Name]
           ,[Level]
           ,[Description])
     VALUES
           ('{0}', NULL, '{1}', 1, 0, N'{2}', N'{3}', N'{4}', 0, N'{5}')";

        public const string InsertProject =
            @"INSERT INTO [Finance].[Project]
           ([ProjectID]
		   ,[ParentID]
           ,[FiscalPeriodID]
           ,[BranchID]
           ,[BranchScope]
           ,[Code]
           ,[FullCode]
           ,[Name]
           ,[Level]
           ,[Description])
     VALUES
           ('{0}', NULL, '{1}', 1, 0, N'{2}', N'{3}', N'{4}', 0, N'{5}')";

        public const string InsertAccountDetailAccount =
            @"INSERT INTO [Finance].[AccountDetailAccount]
           ([AccountDetailAccountID], [AccountID], [DetailID])
     VALUES
           ('{0}', '{1}', '{2}')";

        public const string InsertAccountCostCenter =
            @"INSERT INTO [Finance].[AccountCostCenter]
           ([AccountCostCenterID], [AccountID], [CostCenterID])
     VALUES
           ('{0}', '{1}', '{2}')";

        public const string InsertAccountProject =
            @"INSERT INTO [Finance].[AccountProject]
           ([AccountProjectID], [AccountID], [ProjectID])
     VALUES
           ('{0}', '{1}', '{2}')";

        public const string InsertVoucher =
            @"INSERT INTO [Finance].[Voucher]
           ([VoucherID]
		   ,[FiscalPeriodID]
           ,[BranchID]
           ,[StatusID]
           ,[IssuedByID]
           ,[ModifiedByID]
           ,[No]
           ,[Date]
           ,[Reference]
           ,[Description]
           ,[DailyNo]
           ,[IsBalanced]
           ,[Type]
           ,[SubjectType]
           ,[SaveCount]
           ,[IssuerName]
           ,[ModifierName])
     VALUES
           ('{0}', '{1}', 1, 1, 1, 1, '{2}', '{3}', N'{4}', N'{5}', 1, {6}, 0, 0, {7}, N'{8}', N'{8}')";

        public const string InsertVoucherLine =
            @"INSERT INTO [Finance].[VoucherLine]
           ([LineID]
		   ,[VoucherID]
           ,[FiscalPeriodID]
           ,[BranchID]
           ,[AccountID]
           ,[DetailID]
           ,[CostCenterID]
           ,[ProjectID]
           ,[CurrencyID]
           ,[CreatedByID]
           ,[RowNo]
           ,[Debit]
           ,[Credit]
           ,[Description]
           ,[CurrencyValue]
           ,[Mark]
           ,[TypeID]
           ,[SourceID])
     VALUES
           ('{0}', '{1}', '{2}', 1, '{3}', {4}, {5}, {6}, {7}, 1, '{8}', '{9}', '{10}', N'{11}', '{12}', N'{13}', 0, {14})";
    }
}
