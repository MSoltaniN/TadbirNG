USE [TadbirDemo]
GO

SET IDENTITY_INSERT [Corporate].[Company] ON
INSERT INTO [Corporate].[Company] (CompanyID, Name) VALUES (1, N'پردازش موازی سامان')
SET IDENTITY_INSERT [Corporate].[Company] OFF

SET IDENTITY_INSERT [Corporate].[Branch] ON
INSERT INTO [Corporate].[Branch] (BranchID, CompanyID, Name) VALUES (1, 1, N'ساختمان مرکزی (تهران)')
INSERT INTO [Corporate].[Branch] (BranchID, CompanyID, Name) VALUES (2, 1, N'نمایندگی کرج')
SET IDENTITY_INSERT [Corporate].[Branch] OFF

SET IDENTITY_INSERT [Finance].[FiscalPeriod] ON
INSERT INTO [Finance].[FiscalPeriod] (FiscalPeriodID, CompanyID, Name, StartDate, EndDate) VALUES (1, 1, N'سال مالی 1395', N'2016-03-20', N'2017-03-20')
INSERT INTO [Finance].[FiscalPeriod] (FiscalPeriodID, CompanyID, Name, StartDate, EndDate) VALUES (2, 1, N'سال مالی 1396', N'2017-03-21', N'2018-03-20')
SET IDENTITY_INSERT [Finance].[FiscalPeriod] OFF

SET IDENTITY_INSERT [Finance].[Account] ON
INSERT INTO [Finance].[Account] (AccountID, FiscalPeriodID, BranchID, Code, Name) VALUES (1, 1, 1, N'100', N'دارایی های جاری')
INSERT INTO [Finance].[Account] (AccountID, FiscalPeriodID, BranchID, Code, Name) VALUES (2, 1, 1, N'107', N'دارایی های ثابت مشهود')
INSERT INTO [Finance].[Account] (AccountID, FiscalPeriodID, BranchID, Code, Name) VALUES (3, 2, 2, N'1000', N'دارایی های جاری')
INSERT INTO [Finance].[Account] (AccountID, FiscalPeriodID, BranchID, Code, Name) VALUES (4, 1, 1, N'110', N'حساب شماره 220-800-123456-4 بانک پاسارگاد شعبه میدان ولی عصر تهران')
INSERT INTO [Finance].[Account] (AccountID, FiscalPeriodID, BranchID, Code, Name) VALUES (5, 1, 1, N'120', N'موجودی نقدی - صندوق')
INSERT INTO [Finance].[Account] (AccountID, FiscalPeriodID, BranchID, Code, Name) VALUES (6, 1, 1, N'400', N'سرمایه')
INSERT INTO [Finance].[Account] (AccountID, FiscalPeriodID, BranchID, Code, Name) VALUES (7, 1, 1, N'200', N'اثاثه و منصوبات اداری')
SET IDENTITY_INSERT [Finance].[Account] OFF

SET IDENTITY_INSERT [Finance].[Currency] ON
INSERT INTO [Finance].[Currency] (CurrencyID, Name) VALUES (1, N'ریال')
SET IDENTITY_INSERT [Finance].[Currency] OFF

SET IDENTITY_INSERT [Finance].[Transaction] ON
INSERT INTO [Finance].[Transaction] (TransactionID, FiscalPeriodID, BranchID, DocumentID, CreatedByID, ModifiedByID, [No], [Date], [Description], [Status], OperationalStatus)
  VALUES (1, 1, 1, 6, 1, 1, N'1', N'2017-02-14', N'سند اولیه تاسیس', N'Draft', N'Created')
INSERT INTO [Finance].[Transaction] (TransactionID, FiscalPeriodID, BranchID, DocumentID, CreatedByID, ModifiedByID, [No], [Date], [Description], [Status], OperationalStatus)
  VALUES (2, 1, 1, 7, 1, 1, N'2', N'2017-02-17', N'خرید نقدی لوازم اداری', N'Draft', N'Created')
SET IDENTITY_INSERT [Finance].[Transaction] OFF

SET IDENTITY_INSERT [Finance].[TransactionLine] ON
INSERT INTO [Finance].[TransactionLine] (LineID, TransactionID, FiscalPeriodID, BranchID, AccountID, CurrencyID, [Description], Debit, Credit)
  VALUES (1, 1, 1, 1, 4, 1, N'ثبت موجودی نقدی اولیه در حساب بانکی', 100000000, 0)
INSERT INTO [Finance].[TransactionLine] (LineID, TransactionID, FiscalPeriodID, BranchID, AccountID, CurrencyID, [Description], Debit, Credit)
  VALUES (2, 1, 1, 1, 5, 1, N'ثبت موجودی نقدی اولیه صندوق', 5000000, 0)
INSERT INTO [Finance].[TransactionLine] (LineID, TransactionID, FiscalPeriodID, BranchID, AccountID, CurrencyID, [Description], Debit, Credit)
  VALUES (3, 1, 1, 1, 6, 1, N'ثبت سرمایه اولیه', 0, 105000000)
INSERT INTO [Finance].[TransactionLine] (LineID, TransactionID, FiscalPeriodID, BranchID, AccountID, CurrencyID, [Description], Debit, Credit)
  VALUES (4, 2, 1, 1, 5, 1, N'خرید نقدی لوازم اداری مصرفی', 850000, 0)
INSERT INTO [Finance].[TransactionLine] (LineID, TransactionID, FiscalPeriodID, BranchID, AccountID, CurrencyID, [Description], Debit, Credit)
  VALUES (5, 2, 1, 1, 7, 1, N'خرید نقدی لوازم اداری مصرفی', 0, 850000)
SET IDENTITY_INSERT [Finance].[TransactionLine] OFF

SET IDENTITY_INSERT [Core].[DocumentType] ON
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (1, N'سند مالی')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (2, N'درخواست کالا')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (3, N'سفارش خرید')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (4, N'حواله انبار مقداری')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (5, N'حواله انبار ریالی')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (6, N'رسید انبار مقداری')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (7, N'رسید انبار ریالی')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (8, N'فاکتور خرید')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (9, N'فاکتور فروش')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (10, N'فاکتور برگشت از خرید')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (11, N'فاکتور برگشت از فروش')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (12, N'پیش فاکتور')
SET IDENTITY_INSERT [Core].[DocumentType] OFF

SET IDENTITY_INSERT [Core].[DocumentStatus] ON
INSERT INTO [Core].[DocumentStatus] (StatusID, Name) VALUES (1, N'Draft')
INSERT INTO [Core].[DocumentStatus] (StatusID, Name) VALUES (2, N'Unchecked')
INSERT INTO [Core].[DocumentStatus] (StatusID, Name) VALUES (3, N'Checked')
INSERT INTO [Core].[DocumentStatus] (StatusID, Name) VALUES (4, N'NormalCheck')
INSERT INTO [Core].[DocumentStatus] (StatusID, Name) VALUES (5, N'FinalCheck')
INSERT INTO [Core].[DocumentStatus] (StatusID, Name) VALUES (6, N'NotPriced')
INSERT INTO [Core].[DocumentStatus] (StatusID, Name) VALUES (7, N'Priced')
SET IDENTITY_INSERT [Core].[DocumentStatus] OFF

SET IDENTITY_INSERT [Corporate].[BusinessUnit] ON
INSERT INTO [Corporate].[BusinessUnit] (UnitID, Name, [Description]) VALUES (1, N'واحد مالی', N'شرح واحد مالی')
INSERT INTO [Corporate].[BusinessUnit] (UnitID, Name, [Description]) VALUES (2, N'واحد اداری', N'شرح واحد اداری')
INSERT INTO [Corporate].[BusinessUnit] (UnitID, Name, [Description]) VALUES (3, N'واحد فروش', N'شرح واحد فروش')
INSERT INTO [Corporate].[BusinessUnit] (UnitID, Name, [Description]) VALUES (4, N'واحد مدیریت', N'شرح واحد مدیریت')
INSERT INTO [Corporate].[BusinessUnit] (UnitID, Name, [Description]) VALUES (5, N'واحد انبار', N'شرح واحد انبار')
SET IDENTITY_INSERT [Corporate].[BusinessUnit] OFF

SET IDENTITY_INSERT [Contact].[BusinessPartner] ON
INSERT INTO [Contact].[BusinessPartner] (PartnerID, Name) VALUES (1, N'آقای الف')
INSERT INTO [Contact].[BusinessPartner] (PartnerID, Name) VALUES (2, N'خانم ب')
INSERT INTO [Contact].[BusinessPartner] (PartnerID, Name) VALUES (3, N'خانم ش')
INSERT INTO [Contact].[BusinessPartner] (PartnerID, Name) VALUES (4, N'آقای س')
INSERT INTO [Contact].[BusinessPartner] (PartnerID, Name) VALUES (5, N'آقای ف')
INSERT INTO [Contact].[BusinessPartner] (PartnerID, Name) VALUES (6, N'خانم ل')
INSERT INTO [Contact].[BusinessPartner] (PartnerID, Name) VALUES (7, N'خانم ن')
INSERT INTO [Contact].[BusinessPartner] (PartnerID, Name) VALUES (8, N'آقای ک')
SET IDENTITY_INSERT [Contact].[BusinessPartner] OFF

SET IDENTITY_INSERT [Finance].[DetailAccount] ON
INSERT INTO [Finance].[DetailAccount] (DetailID, Code, FullCode, Name, [Level]) VALUES (1, N'DET100', N'DET100', N'شناور 1', 0)
INSERT INTO [Finance].[DetailAccount] (DetailID, Code, FullCode, Name, [Level]) VALUES (2, N'DET200', N'DET200', N'شناور 2', 0)
INSERT INTO [Finance].[DetailAccount] (DetailID, Code, FullCode, Name, [Level]) VALUES (3, N'DET300', N'DET300', N'شناور 3', 0)
INSERT INTO [Finance].[DetailAccount] (DetailID, Code, FullCode, Name, [Level]) VALUES (4, N'DET400', N'DET100', N'شناور 4', 0)
SET IDENTITY_INSERT [Finance].[DetailAccount] OFF

SET IDENTITY_INSERT [Finance].[CostCenter] ON
INSERT INTO [Finance].[CostCenter] (CostCenterID, Code, FullCode, Name, [Level]) VALUES (1, N'CC100', N'CC100', N'مرکز هزینه 1', 0)
INSERT INTO [Finance].[CostCenter] (CostCenterID, Code, FullCode, Name, [Level]) VALUES (2, N'CC200', N'CC200', N'مرکز هزینه 2', 0)
INSERT INTO [Finance].[CostCenter] (CostCenterID, Code, FullCode, Name, [Level]) VALUES (3, N'CC300', N'CC300', N'مرکز هزینه 3', 0)
INSERT INTO [Finance].[CostCenter] (CostCenterID, Code, FullCode, Name, [Level]) VALUES (4, N'CC400', N'CC400', N'مرکز هزینه 4', 0)
SET IDENTITY_INSERT [Finance].[CostCenter] OFF

SET IDENTITY_INSERT [Finance].[Project] ON
INSERT INTO [Finance].[Project] (ProjectID, Code, FullCode, Name, [Level]) VALUES (1, N'PRJ100', N'PRJ100', N'پروژه 1', 0)
INSERT INTO [Finance].[Project] (ProjectID, Code, FullCode, Name, [Level]) VALUES (2, N'PRJ200', N'PRJ200', N'پروژه 2', 0)
INSERT INTO [Finance].[Project] (ProjectID, Code, FullCode, Name, [Level]) VALUES (3, N'PRJ300', N'PRJ300', N'پروژه 3', 0)
INSERT INTO [Finance].[Project] (ProjectID, Code, FullCode, Name, [Level]) VALUES (4, N'PRJ400', N'PRJ400', N'پروژه 4', 0)
SET IDENTITY_INSERT [Finance].[Project] OFF

SET IDENTITY_INSERT [Finance].[FullAccount] ON
INSERT INTO [Finance].[FullAccount] (FullAccountID, AccountID) VALUES (1, 4)
SET IDENTITY_INSERT [Finance].[FullAccount] OFF

SET IDENTITY_INSERT [Inventory].[UOM] ON
INSERT INTO [Inventory].[UOM] (UomID, Name) VALUES (1, N'کیلوگرم')
INSERT INTO [Inventory].[UOM] (UomID, Name) VALUES (2, N'متر')
INSERT INTO [Inventory].[UOM] (UomID, Name) VALUES (3, N'بسته')
INSERT INTO [Inventory].[UOM] (UomID, Name) VALUES (4, N'کارتن')
SET IDENTITY_INSERT [Inventory].[UOM] OFF

SET IDENTITY_INSERT [Inventory].[ProductCategory] ON
INSERT INTO [Inventory].[ProductCategory] (CategoryID, Code, FullCode, Name, [Level]) VALUES (1, N'100', N'100', N'گروه 1', 0)
INSERT INTO [Inventory].[ProductCategory] (CategoryID, Code, FullCode, Name, [Level]) VALUES (2, N'200', N'200', N'گروه 2', 0)
INSERT INTO [Inventory].[ProductCategory] (CategoryID, Code, FullCode, Name, [Level]) VALUES (3, N'300', N'300', N'گروه 3', 0)
INSERT INTO [Inventory].[ProductCategory] (CategoryID, Code, FullCode, Name, [Level]) VALUES (4, N'400', N'400', N'گروه 4', 0)
SET IDENTITY_INSERT [Inventory].[ProductCategory] OFF

SET IDENTITY_INSERT [Inventory].[Product] ON
INSERT INTO [Inventory].[Product] (ProductID, CategoryID, Code, Name) VALUES (1, 1, N'ABC2002', N'کالای 1 در گروه 1')
INSERT INTO [Inventory].[Product] (ProductID, CategoryID, Code, Name) VALUES (2, 4, N'XYZ5005', N'کالای 1 در گروه 4')
INSERT INTO [Inventory].[Product] (ProductID, CategoryID, Code, Name) VALUES (3, 2, N'CTG4004', N'کالای 1 در گروه 2')
INSERT INTO [Inventory].[Product] (ProductID, CategoryID, Code, Name) VALUES (4, 4, N'XYZ7007', N'کالای 2 در گروه 4')
INSERT INTO [Inventory].[Product] (ProductID, CategoryID, Code, Name) VALUES (5, 1, N'ABC8008', N'کالای 2 در گروه 1')
INSERT INTO [Inventory].[Product] (ProductID, CategoryID, Code, Name) VALUES (6, 2, N'CTG3003', N'کالای 2 در گروه 2')
INSERT INTO [Inventory].[Product] (ProductID, CategoryID, Code, Name) VALUES (7, 2, N'CTG1001', N'کالای 3 در گروه 2')
SET IDENTITY_INSERT [Inventory].[Product] OFF

SET IDENTITY_INSERT [Inventory].[Warehouse] ON
INSERT INTO [Inventory].[Warehouse] (WarehouseID, Code, Name) VALUES (1, N'WRHS1234', N'انبار 1')
INSERT INTO [Inventory].[Warehouse] (WarehouseID, Code, Name) VALUES (2, N'WRHS4567', N'انبار 2')
INSERT INTO [Inventory].[Warehouse] (WarehouseID, Code, Name) VALUES (3, N'WRHS7890', N'انبار 3')
SET IDENTITY_INSERT [Inventory].[Warehouse] OFF

SET IDENTITY_INSERT [Procurement].[RequisitionVoucherType] ON
INSERT INTO [Procurement].[RequisitionVoucherType] (VoucherTypeID, Name) VALUES (1, N'درخواست کالای نوع اول')
INSERT INTO [Procurement].[RequisitionVoucherType] (VoucherTypeID, Name) VALUES (2, N'درخواست کالای نوع دوم')
INSERT INTO [Procurement].[RequisitionVoucherType] (VoucherTypeID, Name) VALUES (3, N'درخواست کالای نوع سوم')
INSERT INTO [Procurement].[RequisitionVoucherType] (VoucherTypeID, Name) VALUES (4, N'درخواست کالای نوع چهارم')
SET IDENTITY_INSERT [Procurement].[RequisitionVoucherType] OFF

SET IDENTITY_INSERT [Core].[Document] ON
INSERT INTO [Core].[Document] (DocumentID, TypeID, StatusID, [No], OperationalStatus) VALUES (1, 2, 1, N'1', N'Created')
INSERT INTO [Core].[Document] (DocumentID, TypeID, StatusID, [No], OperationalStatus) VALUES (2, 2, 1, N'2', N'Created')
INSERT INTO [Core].[Document] (DocumentID, TypeID, StatusID, [No], OperationalStatus) VALUES (3, 2, 1, N'3', N'Created')
INSERT INTO [Core].[Document] (DocumentID, TypeID, StatusID, [No], OperationalStatus) VALUES (4, 2, 1, N'4', N'Created')
INSERT INTO [Core].[Document] (DocumentID, TypeID, StatusID, [No], OperationalStatus) VALUES (5, 2, 1, N'5', N'Created')
INSERT INTO [Core].[Document] (DocumentID, TypeID, StatusID, [No], OperationalStatus) VALUES (6, 1, 1, N'6', N'Created')
INSERT INTO [Core].[Document] (DocumentID, TypeID, StatusID, [No], OperationalStatus) VALUES (7, 1, 1, N'7', N'Created')
SET IDENTITY_INSERT [Core].[Document] OFF

SET IDENTITY_INSERT [Core].[DocumentAction] ON
INSERT INTO [Core].[DocumentAction] (ActionID, DocumentID, CreatedByID, ModifiedByID, CreatedDate, ModifiedDate)
    VALUES (1, 1, 1, 1, getdate(), getdate())
INSERT INTO [Core].[DocumentAction] (ActionID, DocumentID, CreatedByID, ModifiedByID, CreatedDate, ModifiedDate)
    VALUES (2, 2, 1, 1, getdate(), getdate())
INSERT INTO [Core].[DocumentAction] (ActionID, DocumentID, CreatedByID, ModifiedByID, CreatedDate, ModifiedDate)
    VALUES (3, 3, 1, 1, getdate(), getdate())
INSERT INTO [Core].[DocumentAction] (ActionID, DocumentID, CreatedByID, ModifiedByID, CreatedDate, ModifiedDate)
    VALUES (4, 4, 1, 1, getdate(), getdate())
INSERT INTO [Core].[DocumentAction] (ActionID, DocumentID, CreatedByID, ModifiedByID, CreatedDate, ModifiedDate)
    VALUES (5, 4, 1, 1, getdate(), getdate())
INSERT INTO [Core].[DocumentAction] (ActionID, DocumentID, CreatedByID, ModifiedByID, CreatedDate, ModifiedDate)
    VALUES (6, 4, 1, 1, getdate(), getdate())
INSERT INTO [Core].[DocumentAction] (ActionID, DocumentID, CreatedByID, ModifiedByID, CreatedDate, ModifiedDate)
    VALUES (7, 4, 1, 1, getdate(), getdate())
INSERT INTO [Core].[DocumentAction] (ActionID, DocumentID, CreatedByID, ModifiedByID, CreatedDate, ModifiedDate)
    VALUES (8, 4, 1, 1, getdate(), getdate())
INSERT INTO [Core].[DocumentAction] (ActionID, DocumentID, CreatedByID, ModifiedByID, CreatedDate, ModifiedDate)
    VALUES (9, 4, 1, 1, getdate(), getdate())
INSERT INTO [Core].[DocumentAction] (ActionID, DocumentID, CreatedByID, ModifiedByID, CreatedDate, ModifiedDate)
    VALUES (10, 5, 1, 1, getdate(), getdate())
INSERT INTO [Core].[DocumentAction] (ActionID, DocumentID, CreatedByID, ModifiedByID, CreatedDate, ModifiedDate)
    VALUES (11, 6, 1, 1, getdate(), getdate())
INSERT INTO [Core].[DocumentAction] (ActionID, DocumentID, CreatedByID, ModifiedByID, CreatedDate, ModifiedDate)
    VALUES (12, 7, 1, 1, getdate(), getdate())
SET IDENTITY_INSERT [Core].[DocumentAction] OFF

SET IDENTITY_INSERT [Procurement].[RequisitionVoucher] ON
INSERT INTO [Procurement].[RequisitionVoucher]
    (VoucherID, VoucherTypeID, FiscalPeriodID, BranchID, RequesterID, ReceiverID, RequesterUnitID, ReceiverUnitID, WarehouseID, FullAccountID, DocumentID, [No], [Description])
    VALUES (1, 2, 1, 1, 1, 7, 1, 5, 2, 1, 1, N'1', N'شرح یک')
INSERT INTO [Procurement].[RequisitionVoucher]
    (VoucherID, VoucherTypeID, FiscalPeriodID, BranchID, RequesterID, ReceiverID, RequesterUnitID, ReceiverUnitID, WarehouseID, FullAccountID, DocumentID, [No], [Description])
    VALUES (2, 3, 1, 1, 5, 7, 2, 4, 1, 1, 2, N'2', N'شرح دو')
INSERT INTO [Procurement].[RequisitionVoucher]
    (VoucherID, VoucherTypeID, FiscalPeriodID, BranchID, RequesterID, ReceiverID, RequesterUnitID, ReceiverUnitID, WarehouseID, FullAccountID, DocumentID, [No], [Description])
    VALUES (3, 2, 1, 1, 4, 7, 3, 3, 3, 1, 3, N'3', N'شرح سه')
INSERT INTO [Procurement].[RequisitionVoucher]
    (VoucherID, VoucherTypeID, FiscalPeriodID, BranchID, RequesterID, ReceiverID, RequesterUnitID, ReceiverUnitID, WarehouseID, FullAccountID, DocumentID, [No], [Description])
    VALUES (4, 4, 1, 1, 2, 7, 4, 2, 1, 1, 4, N'4', N'شرح چهار')
INSERT INTO [Procurement].[RequisitionVoucher]
    (VoucherID, VoucherTypeID, FiscalPeriodID, BranchID, RequesterID, ReceiverID, RequesterUnitID, ReceiverUnitID, WarehouseID, FullAccountID, DocumentID, [No], [Description])
    VALUES (5, 1, 1, 1, 8, 7, 5, 1, 2, 1, 5, N'5', N'شرح پنج')
SET IDENTITY_INSERT [Procurement].[RequisitionVoucher] OFF

SET IDENTITY_INSERT [Procurement].[RequisitionVoucherLine] ON
INSERT INTO [Procurement].[RequisitionVoucherLine]
    (LineID, VoucherID, WarehouseID, ProductID, UomID, BranchID, FiscalPeriodID, FullAccountID, ActionID, [No], OrderedQuantity, RequiredDate)
    VALUES (1, 4, 2, 5, 2, 1, 1, 1, 5, 1, 6.5, N'2017-08-12')
INSERT INTO [Procurement].[RequisitionVoucherLine]
    (LineID, VoucherID, WarehouseID, ProductID, UomID, BranchID, FiscalPeriodID, FullAccountID, ActionID, [No], OrderedQuantity, RequiredDate)
    VALUES (2, 4, 1, 2, 1, 1, 1, 1, 6, 2, 1, N'2017-08-11')
INSERT INTO [Procurement].[RequisitionVoucherLine]
    (LineID, VoucherID, WarehouseID, ProductID, UomID, BranchID, FiscalPeriodID, FullAccountID, ActionID, [No], OrderedQuantity, RequiredDate)
    VALUES (3, 4, 3, 3, 1, 1, 1, 1, 7, 3, 4, N'2017-08-12')
INSERT INTO [Procurement].[RequisitionVoucherLine]
    (LineID, VoucherID, WarehouseID, ProductID, UomID, BranchID, FiscalPeriodID, FullAccountID, ActionID, [No], OrderedQuantity, RequiredDate)
    VALUES (4, 4, 3, 6, 3, 1, 1, 1, 8, 4, 2, N'2017-08-12')
INSERT INTO [Procurement].[RequisitionVoucherLine]
    (LineID, VoucherID, WarehouseID, ProductID, UomID, BranchID, FiscalPeriodID, FullAccountID, ActionID, [No], OrderedQuantity, RequiredDate)
    VALUES (5, 4, 1, 1, 1, 1, 1, 1, 9, 5, 1, N'2017-08-13')
SET IDENTITY_INSERT [Procurement].[RequisitionVoucherLine] OFF
