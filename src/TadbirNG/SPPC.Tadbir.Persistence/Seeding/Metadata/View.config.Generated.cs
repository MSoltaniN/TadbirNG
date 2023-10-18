
// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1596
//     Template Version: 1.0
//     Generation Date: 10/17/2023 4:16:21 PM 
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using SPPC.Tadbir.Model;
using SPPC.Tadbir.Model.Metadata;
using SPPC.Tadbir.Model.Auth;
using SPPC.Tadbir.Model.Config;
using SPPC.Tadbir.Model.Reporting;

namespace SPPC.Tadbir.Persistence.Seeding
{

    internal class ViewConfiguration : IEntityTypeConfiguration<View>
    {
        public void Configure(EntityTypeBuilder<View> builder)
        {
          
            builder.HasData(new View {    Id =  1 ,    Name =  "Account" ,    EntityName =  "Account" ,    EntityType =  "Base" ,    IsHierarchy =  true ,    IsCartableIntegrated =  true ,    FetchUrl =  "/lookup/accounts" ,    SearchUrl =  "/accounts/lookup"    });
          
            builder.HasData(new View {    Id =  2 ,    Name =  "Voucher" ,    EntityName =  "Voucher" ,    EntityType =  "Operational" ,    IsHierarchy =  false ,    IsCartableIntegrated =  true ,    FetchUrl =  "/lookup/vouchers" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  3 ,    Name =  "VoucherLine" ,    EntityName =  "VoucherLine" ,    EntityType =  "Operational" ,    IsHierarchy =  false ,    IsCartableIntegrated =  true ,    FetchUrl =  "/lookup/vouchers/lines" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  4 ,    Name =  "User" ,    EntityName =  "User" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  5 ,    Name =  "Role" ,    EntityName =  "Role" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  6 ,    Name =  "DetailAccount" ,    EntityName =  "DetailAccount" ,    EntityType =  "Base" ,    IsHierarchy =  true ,    IsCartableIntegrated =  true ,    FetchUrl =  "/lookup/faccounts" ,    SearchUrl =  "/faccounts"    });
          
            builder.HasData(new View {    Id =  7 ,    Name =  "CostCenter" ,    EntityName =  "CostCenter" ,    EntityType =  "Base" ,    IsHierarchy =  true ,    IsCartableIntegrated =  true ,    FetchUrl =  "/lookup/ccenters" ,    SearchUrl =  "/ccenters"    });
          
            builder.HasData(new View {    Id =  8 ,    Name =  "Project" ,    EntityName =  "Project" ,    EntityType =  "Base" ,    IsHierarchy =  true ,    IsCartableIntegrated =  true ,    FetchUrl =  "/lookup/projects" ,    SearchUrl =  "/projects"    });
          
            builder.HasData(new View {    Id =  9 ,    Name =  "FiscalPeriod" ,    EntityName =  "FiscalPeriod" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  true ,    FetchUrl =  "" ,    SearchUrl =  "/fperiods"    });
          
            builder.HasData(new View {    Id =  10 ,    Name =  "Branch" ,    EntityName =  "Branch" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "/branches"    });
          
            builder.HasData(new View {    Id =  11 ,    Name =  "CompanyDb" ,    EntityName =  "Company" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  12 ,    Name =  "AccountGroup" ,    EntityName =  "AccountGroup" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  13 ,    Name =  "OperationLog" ,    EntityName =  "OperationLog" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  14 ,    Name =  "AccountCollectionAccount" ,    EntityName =  "AccountCollection" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  15 ,    Name =  "JournalByDateByRow" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  16 ,    Name =  "JournalByDateByRowDetail" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  17 ,    Name =  "JournalByDateByLedger" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  18 ,    Name =  "JournalByDateBySubsidiary" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  19 ,    Name =  "JournalByDateSummary" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  20 ,    Name =  "JournalByDateSummaryByDate" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  21 ,    Name =  "JournalByDateSummaryByMonth" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  22 ,    Name =  "JournalByNoByRow" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  23 ,    Name =  "JournalByNoByRowDetail" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  24 ,    Name =  "JournalByNoByLedger" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  25 ,    Name =  "JournalByNoBySubsidiary" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  26 ,    Name =  "JournalByNoSummary" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  27 ,    Name =  "AccountBookSingle" ,    EntityName =  "AccountBook" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  28 ,    Name =  "AccountBookSingleSummary" ,    EntityName =  "AccountBook" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  29 ,    Name =  "AccountBookSummary" ,    EntityName =  "AccountBook" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  30 ,    Name =  "Currency" ,    EntityName =  "Currency" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  31 ,    Name =  "CurrencyRate" ,    EntityName =  "CurrencyRate" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  32 ,    Name =  "TestBalance2Column" ,    EntityName =  "TestBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  33 ,    Name =  "TestBalance4Column" ,    EntityName =  "TestBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  34 ,    Name =  "TestBalance6Column" ,    EntityName =  "TestBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  35 ,    Name =  "TestBalance8Column" ,    EntityName =  "TestBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  36 ,    Name =  "TestBalance10Column" ,    EntityName =  "TestBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  37 ,    Name =  "CurrencyBook" ,    EntityName =  "CurrencyBook" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  38 ,    Name =  "CurrencyBookSingle" ,    EntityName =  "CurrencyBook" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  39 ,    Name =  "CurrencyBookSingleSummary" ,    EntityName =  "CurrencyBook" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  40 ,    Name =  "CurrencyBookSummary" ,    EntityName =  "CurrencyBook" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  41 ,    Name =  "NumberList" ,    EntityName =  "NumberList" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  42 ,    Name =  "VoucherLineDetail" ,    EntityName =  "VoucherLineDetail" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  43 ,    Name =  "DetailAccountBalance2Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  44 ,    Name =  "DetailAccountBalance4Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  45 ,    Name =  "DetailAccountBalance6Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  46 ,    Name =  "DetailAccountBalance8Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  47 ,    Name =  "DetailAccountBalance10Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  48 ,    Name =  "CostCenterBalance2Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  49 ,    Name =  "CostCenterBalance4Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  50 ,    Name =  "CostCenterBalance6Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  51 ,    Name =  "CostCenterBalance8Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  52 ,    Name =  "CostCenterBalance10Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  53 ,    Name =  "ProjectBalance2Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  54 ,    Name =  "ProjectBalance4Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  55 ,    Name =  "ProjectBalance6Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  56 ,    Name =  "ProjectBalance8Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  57 ,    Name =  "ProjectBalance10Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  58 ,    Name =  "BalanceByAccount" ,    EntityName =  "BalanceByAccount" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  59 ,    Name =  "SysOperationLog" ,    EntityName =  "SysOperationLog" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  60 ,    Name =  "SysOperationLogArchive" ,    EntityName =  "SysOperationLogArchive" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  61 ,    Name =  "OperationLogArchive" ,    EntityName =  "OperationLogArchive" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  62 ,    Name =  "ProfitLoss" ,    EntityName =  "ProfitLoss" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  63 ,    Name =  "GroupActionResult" ,    EntityName =  "GroupActionResult" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  64 ,    Name =  "ProfitLossSimple" ,    EntityName =  "ProfitLoss" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  65 ,    Name =  "ComparativeProfitLoss" ,    EntityName =  "ProfitLoss" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  66 ,    Name =  "ComparativeProfitLossSimple" ,    EntityName =  "ProfitLoss" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  67 ,    Name =  "BalanceSheet" ,    EntityName =  "BalanceSheet" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  68 ,    Name =  "Widget" ,    EntityName =  "Dashboard" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  69 ,    Name =  "CheckBookPage" ,    EntityName =  "CheckBookPage" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  70 ,    Name =  "CashRegister" ,    EntityName =  "CashRegister" ,    EntityType =  "Base" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  71 ,    Name =  "CheckBook" ,    EntityName =  "CheckBook" ,    EntityType =  "Operational" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  72 ,    Name =  "CheckBookReport" ,    EntityName =  "CheckBookReport" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  73 ,    Name =  "SourceApp" ,    EntityName =  "SourceApp" ,    EntityType =  "Base" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  74 ,    Name =  "Payment" ,    EntityName =  "Payment" ,    EntityType =  "Operational" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  75 ,    Name =  "Receipt" ,    EntityName =  "Receipt" ,    EntityType =  "Operational" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  76 ,    Name =  "PayReceiveAccount" ,    EntityName =  "PayReceiveAccount" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  77 ,    Name =  "PayReceiveCashAccount" ,    EntityName =  "PayReceiveCashAccount" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  78 ,    Name =  "VouchersByDate" ,    EntityName =  "VouchersByDate" ,    EntityType =  "Operational" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  100001 ,    Name =  "Brand" ,    EntityName =  "Brand" ,    EntityType =  "Base" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  100002 ,    Name =  "Unit" ,    EntityName =  "Unit" ,    EntityType =  "Base" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  100003 ,    Name =  "Property" ,    EntityName =  "Property" ,    EntityType =  "Base" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  100004 ,    Name =  "Attribute" ,    EntityName =  "Attribute" ,    EntityType =  "Base" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
          
            builder.HasData(new View {    Id =  100005 ,    Name =  "File" ,    EntityName =  "File" ,    EntityType =  "Base" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  ""    });
        
        }
    }
}