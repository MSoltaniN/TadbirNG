USE [TadbirDemo]
GO

SET IDENTITY_INSERT [Corporate].[Branch] ON
INSERT INTO [Corporate].[Branch] (BranchID, CompanyID, Name) VALUES (1, 1, N'ساختمان مرکزی (تهران)')
INSERT INTO [Corporate].[Branch] (BranchID, CompanyID, Name) VALUES (2, 1, N'نمایندگی کرج')
SET IDENTITY_INSERT [Corporate].[Branch] OFF

SET IDENTITY_INSERT [Finance].[FiscalPeriod] ON
INSERT INTO [Finance].[FiscalPeriod] (FiscalPeriodID, CompanyID, Name, StartDate, EndDate) VALUES (1, 1, N'سال مالی 1395', N'2016-03-20', N'2017-03-20')
INSERT INTO [Finance].[FiscalPeriod] (FiscalPeriodID, CompanyID, Name, StartDate, EndDate) VALUES (2, 1, N'سال مالی 1396', N'2017-03-21', N'2018-03-20')
SET IDENTITY_INSERT [Finance].[FiscalPeriod] OFF

SET IDENTITY_INSERT [Auth].[RoleBranch] ON
INSERT INTO [Auth].[RoleBranch] (RoleBranchID, RoleID, BranchID) VALUES (1, 1, 1)
INSERT INTO [Auth].[RoleBranch] (RoleBranchID, RoleID, BranchID) VALUES (2, 1, 2)
SET IDENTITY_INSERT [Auth].[RoleBranch] OFF

SET IDENTITY_INSERT [Auth].[RoleFiscalPeriod] ON
INSERT INTO [Auth].[RoleFiscalPeriod] (RoleFiscalPeriodID, RoleID, FiscalPeriodID) VALUES (1, 1, 1)
INSERT INTO [Auth].[RoleFiscalPeriod] (RoleFiscalPeriodID, RoleID, FiscalPeriodID) VALUES (2, 1, 2)
SET IDENTITY_INSERT [Auth].[RoleFiscalPeriod] OFF

SET IDENTITY_INSERT [Finance].[Account] ON
INSERT INTO [Finance].[Account] (AccountID, FiscalPeriodID, BranchID, Code, FullCode, Name, [Level]) VALUES (1, 1, 1, N'100', N'100', N'دارایی های جاری', 0)
INSERT INTO [Finance].[Account] (AccountID, FiscalPeriodID, BranchID, Code, FullCode, Name, [Level]) VALUES (2, 1, 1, N'107', N'107', N'دارایی های ثابت مشهود', 0)
INSERT INTO [Finance].[Account] (AccountID, FiscalPeriodID, BranchID, Code, FullCode, Name, [Level]) VALUES (3, 2, 2, N'1000', N'1000', N'دارایی های جاری', 0)
INSERT INTO [Finance].[Account] (AccountID, FiscalPeriodID, BranchID, Code, FullCode, Name, [Level]) VALUES (4, 1, 1, N'110', N'110', N'حساب شماره 220-800-123456-4 بانک پاسارگاد شعبه میدان ولی عصر تهران', 0)
INSERT INTO [Finance].[Account] (AccountID, FiscalPeriodID, BranchID, Code, FullCode, Name, [Level]) VALUES (5, 1, 1, N'120', N'120', N'موجودی نقدی - صندوق', 0)
INSERT INTO [Finance].[Account] (AccountID, FiscalPeriodID, BranchID, Code, FullCode, Name, [Level]) VALUES (6, 1, 1, N'400', N'400', N'سرمایه', 0)
INSERT INTO [Finance].[Account] (AccountID, FiscalPeriodID, BranchID, Code, FullCode, Name, [Level]) VALUES (7, 1, 1, N'200', N'200', N'اثاثه و منصوبات اداری', 0)
SET IDENTITY_INSERT [Finance].[Account] OFF

SET IDENTITY_INSERT [Finance].[Currency] ON
INSERT INTO [Finance].[Currency] (CurrencyID, Name) VALUES (1, N'ریال')
SET IDENTITY_INSERT [Finance].[Currency] OFF

SET IDENTITY_INSERT [Core].[DocumentType] ON
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (1, N'Voucher')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (2, N'RequisitionVoucher')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (3, N'PurchaseOrder')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (4, N'IssueVoucher')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (5, N'PricedIssueVoucher')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (6, N'ReceiptVoucher')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (7, N'PricedReceiptVoucher')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (8, N'PurchaseInvoice')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (9, N'SalesInvoice')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (10, N'PurchaseRefundInvoice')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (11, N'SalesRefundInvoice')
INSERT INTO [Core].[DocumentType] (TypeID, Name) VALUES (12, N'SalesQuote')
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

SET IDENTITY_INSERT [Finance].[DetailAccount] ON
INSERT INTO [Finance].[DetailAccount] (DetailID, FiscalPeriodID, BranchID, Code, FullCode, Name, [Level]) VALUES (1, 1, 1, N'D100', N'D100', N'شناور 1', 0)
INSERT INTO [Finance].[DetailAccount] (DetailID, FiscalPeriodID, BranchID, Code, FullCode, Name, [Level]) VALUES (2, 1, 1, N'D200', N'D200', N'شناور 2', 0)
INSERT INTO [Finance].[DetailAccount] (DetailID, FiscalPeriodID, BranchID, Code, FullCode, Name, [Level]) VALUES (3, 1, 1, N'D300', N'D300', N'شناور 3', 0)
INSERT INTO [Finance].[DetailAccount] (DetailID, FiscalPeriodID, BranchID, Code, FullCode, Name, [Level]) VALUES (4, 1, 1, N'D400', N'D100', N'شناور 4', 0)
SET IDENTITY_INSERT [Finance].[DetailAccount] OFF

SET IDENTITY_INSERT [Finance].[CostCenter] ON
INSERT INTO [Finance].[CostCenter] (CostCenterID, FiscalPeriodID, BranchID, Code, FullCode, Name, [Level]) VALUES (1, 1, 1, N'C100', N'C100', N'مرکز هزینه 1', 0)
INSERT INTO [Finance].[CostCenter] (CostCenterID, FiscalPeriodID, BranchID, Code, FullCode, Name, [Level]) VALUES (2, 1, 1, N'C200', N'C200', N'مرکز هزینه 2', 0)
INSERT INTO [Finance].[CostCenter] (CostCenterID, FiscalPeriodID, BranchID, Code, FullCode, Name, [Level]) VALUES (3, 1, 1, N'C300', N'C300', N'مرکز هزینه 3', 0)
INSERT INTO [Finance].[CostCenter] (CostCenterID, FiscalPeriodID, BranchID, Code, FullCode, Name, [Level]) VALUES (4, 1, 1, N'C400', N'C400', N'مرکز هزینه 4', 0)
SET IDENTITY_INSERT [Finance].[CostCenter] OFF

SET IDENTITY_INSERT [Finance].[Project] ON
INSERT INTO [Finance].[Project] (ProjectID, FiscalPeriodID, BranchID, Code, FullCode, Name, [Level]) VALUES (1, 1, 1, N'P100', N'P100', N'پروژه 1', 0)
INSERT INTO [Finance].[Project] (ProjectID, FiscalPeriodID, BranchID, Code, FullCode, Name, [Level]) VALUES (2, 1, 1, N'P200', N'P200', N'پروژه 2', 0)
INSERT INTO [Finance].[Project] (ProjectID, FiscalPeriodID, BranchID, Code, FullCode, Name, [Level]) VALUES (3, 1, 1, N'P300', N'P300', N'پروژه 3', 0)
INSERT INTO [Finance].[Project] (ProjectID, FiscalPeriodID, BranchID, Code, FullCode, Name, [Level]) VALUES (4, 1, 1, N'P400', N'P400', N'پروژه 4', 0)
SET IDENTITY_INSERT [Finance].[Project] OFF

SET IDENTITY_INSERT [Core].[Document] ON
INSERT INTO [Core].[Document] (DocumentID, TypeID, StatusID, EntityNo, [No], OperationalStatus) VALUES (1, 2, 1, N'1', N'1', N'Created')
INSERT INTO [Core].[Document] (DocumentID, TypeID, StatusID, EntityNo, [No], OperationalStatus) VALUES (2, 2, 1, N'2', N'2', N'Created')
INSERT INTO [Core].[Document] (DocumentID, TypeID, StatusID, EntityNo, [No], OperationalStatus) VALUES (3, 2, 1, N'3', N'3', N'Created')
INSERT INTO [Core].[Document] (DocumentID, TypeID, StatusID, EntityNo, [No], OperationalStatus) VALUES (4, 2, 1, N'4', N'4', N'Created')
INSERT INTO [Core].[Document] (DocumentID, TypeID, StatusID, EntityNo, [No], OperationalStatus) VALUES (5, 2, 1, N'5', N'5', N'Created')
INSERT INTO [Core].[Document] (DocumentID, TypeID, StatusID, EntityNo, [No], OperationalStatus) VALUES (6, 1, 1, N'1', N'6', N'Created')
INSERT INTO [Core].[Document] (DocumentID, TypeID, StatusID, EntityNo, [No], OperationalStatus) VALUES (7, 1, 1, N'2', N'7', N'Created')
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

SET IDENTITY_INSERT [Finance].[Voucher] ON
INSERT INTO [Finance].[Voucher] (VoucherID, FiscalPeriodID, BranchID, [No], [Date], [Description])
  VALUES (1, 1, 1, N'1', N'2017-02-14', N'سند اولیه تاسیس')
INSERT INTO [Finance].[Voucher] (VoucherID, FiscalPeriodID, BranchID, [No], [Date], [Description])
  VALUES (2, 1, 1, N'2', N'2017-02-17', N'خرید نقدی لوازم اداری')
SET IDENTITY_INSERT [Finance].[Voucher] OFF

SET IDENTITY_INSERT [Finance].[VoucherLine] ON
INSERT INTO [Finance].[VoucherLine] (LineID, VoucherID, FiscalPeriodID, BranchID, AccountID, CurrencyID, [Description], Debit, Credit)
  VALUES (1, 1, 1, 1, 2, 1, N'ثبت موجودی نقدی اولیه در حساب بانکی', 100000000, 0)
INSERT INTO [Finance].[VoucherLine] (LineID, VoucherID, FiscalPeriodID, BranchID, AccountID, CurrencyID, [Description], Debit, Credit)
  VALUES (2, 1, 1, 1, 1, 1, N'ثبت موجودی نقدی اولیه صندوق', 5000000, 0)
INSERT INTO [Finance].[VoucherLine] (LineID, VoucherID, FiscalPeriodID, BranchID, AccountID, CurrencyID, [Description], Debit, Credit)
  VALUES (3, 1, 1, 1, 3, 1, N'ثبت سرمایه اولیه', 0, 105000000)
INSERT INTO [Finance].[VoucherLine] (LineID, VoucherID, FiscalPeriodID, BranchID, AccountID, CurrencyID, [Description], Debit, Credit)
  VALUES (4, 2, 1, 1, 2, 1, N'خرید نقدی لوازم اداری مصرفی', 850000, 0)
INSERT INTO [Finance].[VoucherLine] (LineID, VoucherID, FiscalPeriodID, BranchID, AccountID, CurrencyID, [Description], Debit, Credit)
  VALUES (5, 2, 1, 1, 4, 1, N'خرید نقدی لوازم اداری مصرفی', 0, 850000)
SET IDENTITY_INSERT [Finance].[VoucherLine] OFF
