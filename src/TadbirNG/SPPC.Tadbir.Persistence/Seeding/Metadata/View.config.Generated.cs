
// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.2.1596
//     Template Version: 1.0
//     Generation Date: 10/16/2023 3:05:54 PM 
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
          
            builder.HasData(new View {    Name =  "Account" ,    EntityName =  "Account" ,    EntityType =  "Base" ,    IsHierarchy =  true ,    IsCartableIntegrated =  true ,    FetchUrl =  "/lookup/accounts" ,    SearchUrl =  "/accounts/lookup" ,    Id =  1 ,    RowGuid =  new Guid("7a078968-4470-49a7-aaa5-396ae400e02c") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "Voucher" ,    EntityName =  "Voucher" ,    EntityType =  "Operational" ,    IsHierarchy =  false ,    IsCartableIntegrated =  true ,    FetchUrl =  "/lookup/vouchers" ,    SearchUrl =  "" ,    Id =  2 ,    RowGuid =  new Guid("eb27fb68-0d6c-4e8b-ba88-5f25f518c579") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "VoucherLine" ,    EntityName =  "VoucherLine" ,    EntityType =  "Operational" ,    IsHierarchy =  false ,    IsCartableIntegrated =  true ,    FetchUrl =  "/lookup/vouchers/lines" ,    SearchUrl =  "" ,    Id =  3 ,    RowGuid =  new Guid("09d85b66-d12d-4015-bf31-4a8143104598") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "User" ,    EntityName =  "User" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  4 ,    RowGuid =  new Guid("e4dd6995-561b-4eba-a401-bdd4cb24cdb4") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "Role" ,    EntityName =  "Role" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  5 ,    RowGuid =  new Guid("751241cc-38dc-40dd-9ae3-c45963e3febb") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "DetailAccount" ,    EntityName =  "DetailAccount" ,    EntityType =  "Base" ,    IsHierarchy =  true ,    IsCartableIntegrated =  true ,    FetchUrl =  "/lookup/faccounts" ,    SearchUrl =  "/faccounts" ,    Id =  6 ,    RowGuid =  new Guid("9ebbb3c1-1aee-4d9b-b6c4-9e3ee8ca1543") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "CostCenter" ,    EntityName =  "CostCenter" ,    EntityType =  "Base" ,    IsHierarchy =  true ,    IsCartableIntegrated =  true ,    FetchUrl =  "/lookup/ccenters" ,    SearchUrl =  "/ccenters" ,    Id =  7 ,    RowGuid =  new Guid("a2033c86-4050-4b8c-983d-40871002fe81") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "Project" ,    EntityName =  "Project" ,    EntityType =  "Base" ,    IsHierarchy =  true ,    IsCartableIntegrated =  true ,    FetchUrl =  "/lookup/projects" ,    SearchUrl =  "/projects" ,    Id =  8 ,    RowGuid =  new Guid("4ff7cf88-8424-4304-bc05-affbe1a2fbb5") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "FiscalPeriod" ,    EntityName =  "FiscalPeriod" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  true ,    FetchUrl =  "" ,    SearchUrl =  "/fperiods" ,    Id =  9 ,    RowGuid =  new Guid("c73f0f80-dbcd-4be4-a07a-931f8ad9729c") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "Branch" ,    EntityName =  "Branch" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "/branches" ,    Id =  10 ,    RowGuid =  new Guid("17320031-06b8-45ad-9794-6b8f81cda55a") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "CompanyDb" ,    EntityName =  "Company" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  11 ,    RowGuid =  new Guid("5bbe8693-f738-4567-9cb6-a98fb84f427c") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "AccountGroup" ,    EntityName =  "AccountGroup" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  12 ,    RowGuid =  new Guid("fb9c8df4-d670-4b03-a302-89cbd1316388") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "OperationLog" ,    EntityName =  "OperationLog" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  13 ,    RowGuid =  new Guid("c6f22e55-e167-42d5-90dc-62ef925c04b6") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "AccountCollectionAccount" ,    EntityName =  "AccountCollection" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  14 ,    RowGuid =  new Guid("cd31bbf7-e434-4c3b-b1ad-999cd97a20ef") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "JournalByDateByRow" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  15 ,    RowGuid =  new Guid("37405791-6669-418f-82ce-822f69ad9fcf") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "JournalByDateByRowDetail" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  16 ,    RowGuid =  new Guid("4a18b6db-e9ed-4ad8-b509-b9fcddb26be3") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "JournalByDateByLedger" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  17 ,    RowGuid =  new Guid("5a577867-eb1f-41cd-be8e-f136f4efad88") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "JournalByDateBySubsidiary" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  18 ,    RowGuid =  new Guid("ba5b80c1-d88f-46c0-9f90-b568de95c9d1") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "JournalByDateSummary" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  19 ,    RowGuid =  new Guid("c1df61e9-3b7d-4118-b6f9-f72f0421c781") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "JournalByDateSummaryByDate" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  20 ,    RowGuid =  new Guid("037ab456-bb78-4697-99ba-cf358a54a987") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "JournalByDateSummaryByMonth" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  21 ,    RowGuid =  new Guid("db0f1633-df62-4aed-a201-e37d40da0892") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "JournalByNoByRow" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  22 ,    RowGuid =  new Guid("71bd2ef1-a4aa-404d-bb70-aa67f12bb95c") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "JournalByNoByRowDetail" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  23 ,    RowGuid =  new Guid("4f92cac5-1194-47fd-89c7-b26d6e0c2289") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "JournalByNoByLedger" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  24 ,    RowGuid =  new Guid("389bacbd-9dad-4320-9b0e-01814e2c528d") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "JournalByNoBySubsidiary" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  25 ,    RowGuid =  new Guid("5d9b8d81-48a1-478a-80de-e6eb8a12825e") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "JournalByNoSummary" ,    EntityName =  "Journal" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  26 ,    RowGuid =  new Guid("483c5922-7739-413c-b148-8a8f339e1ddd") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "AccountBookSingle" ,    EntityName =  "AccountBook" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  27 ,    RowGuid =  new Guid("f7ce7722-f431-4aac-82b1-7fe29395571c") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "AccountBookSingleSummary" ,    EntityName =  "AccountBook" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  28 ,    RowGuid =  new Guid("576bcbde-d08d-4a54-a79c-9bb461f226a4") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "AccountBookSummary" ,    EntityName =  "AccountBook" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  29 ,    RowGuid =  new Guid("88a50f22-e094-4c62-822f-36a2eb0e9795") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "Currency" ,    EntityName =  "Currency" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  30 ,    RowGuid =  new Guid("bc0dd97e-3b25-453c-b942-4eddcf89ebfc") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "CurrencyRate" ,    EntityName =  "CurrencyRate" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  31 ,    RowGuid =  new Guid("8148071a-0111-4d57-8854-827f813b7988") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "TestBalance2Column" ,    EntityName =  "TestBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  32 ,    RowGuid =  new Guid("0ccb697c-3d0b-4d40-8a53-8ebe7082faf5") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "TestBalance4Column" ,    EntityName =  "TestBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  33 ,    RowGuid =  new Guid("1b26b836-6d3c-4256-8e08-abe81865ba4d") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "TestBalance6Column" ,    EntityName =  "TestBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  34 ,    RowGuid =  new Guid("474173af-8745-485a-84fa-5d1e28b62535") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "TestBalance8Column" ,    EntityName =  "TestBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  35 ,    RowGuid =  new Guid("f96feaea-094f-4729-b0d7-90694275b63b") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "TestBalance10Column" ,    EntityName =  "TestBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  36 ,    RowGuid =  new Guid("55babf36-4cd3-46d9-9944-1f7fe7322a56") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "CurrencyBook" ,    EntityName =  "CurrencyBook" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  37 ,    RowGuid =  new Guid("eae9d124-3843-431e-83b3-3d11e5d49fbb") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "CurrencyBookSingle" ,    EntityName =  "CurrencyBook" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  38 ,    RowGuid =  new Guid("1b28520f-db0f-4813-b8c8-3939fd52a9be") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "CurrencyBookSingleSummary" ,    EntityName =  "CurrencyBook" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  39 ,    RowGuid =  new Guid("09c53547-6cde-463e-8d9d-4073101dd9ef") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "CurrencyBookSummary" ,    EntityName =  "CurrencyBook" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  40 ,    RowGuid =  new Guid("3b376137-055c-4431-9b0d-e696b1eeeb4b") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "NumberList" ,    EntityName =  "NumberList" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  41 ,    RowGuid =  new Guid("f35eea7b-fc0c-4405-b689-0f67b061ab19") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "VoucherLineDetail" ,    EntityName =  "VoucherLineDetail" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  42 ,    RowGuid =  new Guid("9bd13254-84b2-4d51-b611-8444ce00b209") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "DetailAccountBalance2Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  43 ,    RowGuid =  new Guid("d9c56f3a-3515-49c2-b270-500893553ddb") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "DetailAccountBalance4Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  44 ,    RowGuid =  new Guid("b529ebc9-2431-444d-940e-8b50968948cb") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "DetailAccountBalance6Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  45 ,    RowGuid =  new Guid("f56fee44-23f7-4dd9-8dfb-73447e6bbd27") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "DetailAccountBalance8Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  46 ,    RowGuid =  new Guid("d4d5fa7d-ec1f-46b1-a70e-714cb042b38a") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "DetailAccountBalance10Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  47 ,    RowGuid =  new Guid("6634dd78-c5cf-4fb8-9e23-0045536f06a0") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "CostCenterBalance2Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  48 ,    RowGuid =  new Guid("64180b64-902c-451f-8f79-e585c3eface5") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "CostCenterBalance4Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  49 ,    RowGuid =  new Guid("0924a77b-0ef1-4086-ba60-1dcfa46a4bc3") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "CostCenterBalance6Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  50 ,    RowGuid =  new Guid("b7510be2-689d-4a7d-ac99-c8917af9d4de") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "CostCenterBalance8Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  51 ,    RowGuid =  new Guid("d80d6a4e-c997-4f02-bacf-18dc6253db64") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "CostCenterBalance10Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  52 ,    RowGuid =  new Guid("72dcc270-7e31-41b7-af11-252aa7f385df") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "ProjectBalance2Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  53 ,    RowGuid =  new Guid("0fa89131-ef40-4805-a94a-9dd0cec874bc") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "ProjectBalance4Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  54 ,    RowGuid =  new Guid("8bb84062-5533-4e3f-9b04-1a5ff81941c5") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "ProjectBalance6Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  55 ,    RowGuid =  new Guid("6e65a1d6-c707-40dc-ada8-7c87ef6239d7") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "ProjectBalance8Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  56 ,    RowGuid =  new Guid("8bf7c64d-f3fd-44d5-aa0f-0f9bad86c890") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "ProjectBalance10Column" ,    EntityName =  "ItemBalance" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  57 ,    RowGuid =  new Guid("3f4e4a35-7e84-4ddb-85ac-530d74495c44") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "BalanceByAccount" ,    EntityName =  "BalanceByAccount" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  58 ,    RowGuid =  new Guid("c43dd5ed-8b2a-4f3b-b620-a25fb2b340b6") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "SysOperationLog" ,    EntityName =  "SysOperationLog" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  59 ,    RowGuid =  new Guid("e1c9cfb2-3b2a-414e-a349-a59f67ced8b8") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "SysOperationLogArchive" ,    EntityName =  "SysOperationLogArchive" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  60 ,    RowGuid =  new Guid("9b605225-f68c-4ffd-bd72-36818fa2feef") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "OperationLogArchive" ,    EntityName =  "OperationLogArchive" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  61 ,    RowGuid =  new Guid("76365d2e-d462-4bf4-9a70-7433e6c53379") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "ProfitLoss" ,    EntityName =  "ProfitLoss" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  62 ,    RowGuid =  new Guid("1aec9060-f4bc-42ab-9280-cd49c621d956") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "GroupActionResult" ,    EntityName =  "GroupActionResult" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  63 ,    RowGuid =  new Guid("94a0b5fe-15f0-4558-b891-790cf54e8b55") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "ProfitLossSimple" ,    EntityName =  "ProfitLoss" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  64 ,    RowGuid =  new Guid("944f6b25-5d9c-4645-8025-b0caf5cbb9c9") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "ComparativeProfitLoss" ,    EntityName =  "ProfitLoss" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  65 ,    RowGuid =  new Guid("72443f21-835c-476e-aca6-d1e92712dc9c") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "ComparativeProfitLossSimple" ,    EntityName =  "ProfitLoss" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  66 ,    RowGuid =  new Guid("0ebec321-dfc7-41a1-99b0-d42f24d9eea7") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "BalanceSheet" ,    EntityName =  "BalanceSheet" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  67 ,    RowGuid =  new Guid("310b8d86-d6a3-4ab3-b962-069a4d8ca0db") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "Widget" ,    EntityName =  "Dashboard" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  68 ,    RowGuid =  new Guid("a2e7d0ec-a73e-405e-b3cb-0b1ce91efbf8") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "CheckBookPage" ,    EntityName =  "CheckBookPage" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  69 ,    RowGuid =  new Guid("551ba51a-1cb4-4f1b-bcc9-2e68eb0e77ca") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "CashRegister" ,    EntityName =  "CashRegister" ,    EntityType =  "Base" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  70 ,    RowGuid =  new Guid("3dd8d60a-6282-43ba-a592-c7f084c87901") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "CheckBook" ,    EntityName =  "CheckBook" ,    EntityType =  "Operational" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  71 ,    RowGuid =  new Guid("559d405d-385b-4a70-a278-999aeeb9c0b1") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "CheckBookReport" ,    EntityName =  "CheckBookReport" ,    EntityType =  "" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  72 ,    RowGuid =  new Guid("24d95289-f000-4d15-84b0-0ff065a96384") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "SourceApp" ,    EntityName =  "SourceApp" ,    EntityType =  "Base" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  73 ,    RowGuid =  new Guid("9a4a2e73-5858-47fe-bea1-d4cd9638b3f1") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "Payment" ,    EntityName =  "Payment" ,    EntityType =  "Operational" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  74 ,    RowGuid =  new Guid("a28319e9-2420-48d5-a668-6972c5f653e2") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "Receipt" ,    EntityName =  "Receipt" ,    EntityType =  "Operational" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  75 ,    RowGuid =  new Guid("68ea9b43-f889-43d0-a6c4-ad071c3613bf") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "PayReceiveAccount" ,    EntityName =  "PayReceiveAccount" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  76 ,    RowGuid =  new Guid("f13e2ea4-2aa6-4e27-b06e-117afccc4f1f") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "PayReceiveCashAccount" ,    EntityName =  "PayReceiveCashAccount" ,    EntityType =  "Core" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  77 ,    RowGuid =  new Guid("0b574e80-9a0b-4351-95d2-7660e673ed64") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "VouchersByDate" ,    EntityName =  "VouchersByDate" ,    EntityType =  "Operational" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  78 ,    RowGuid =  new Guid("ba9163d7-419f-4cfe-84c8-d0fa6e47a3d9") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "Brand" ,    EntityName =  "Brand" ,    EntityType =  "Base" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  100001 ,    RowGuid =  new Guid("d2332768-cbff-4410-8cde-fa197c130f63") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "Unit" ,    EntityName =  "Unit" ,    EntityType =  "Base" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  100002 ,    RowGuid =  new Guid("12985525-64fb-4923-8581-e46fce61580c") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "Property" ,    EntityName =  "Property" ,    EntityType =  "Base" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  100003 ,    RowGuid =  new Guid("b35008e5-e0dd-4538-8036-65ef4d946070") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "Attribute" ,    EntityName =  "Attribute" ,    EntityType =  "Base" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  100004 ,    RowGuid =  new Guid("92456aa5-7eb6-4fab-aba5-7d2d56aa8427") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
          
            builder.HasData(new View {    Name =  "File" ,    EntityName =  "File" ,    EntityType =  "Base" ,    IsHierarchy =  false ,    IsCartableIntegrated =  false ,    FetchUrl =  "" ,    SearchUrl =  "" ,    Id =  100005 ,    RowGuid =  new Guid("d125ce08-9733-467f-b46a-0c8530420131") ,    ModifiedDate =  DateTime.Parse("01/01/0001 12:00:00 AM")    });
        
        }
    }
}