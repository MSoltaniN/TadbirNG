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
INSERT INTO [Finance].[Transaction] (TransactionID, FiscalPeriodID, BranchID, CreatorID, ModifierID, [No], [Date], [Description], [Status], OperationalStatus)
  VALUES (1, 1, 1, 1, 1, N'1', N'2017-02-14', N'سند اولیه تاسیس', N'Draft', N'Created')
INSERT INTO [Finance].[Transaction] (TransactionID, FiscalPeriodID, BranchID, CreatorID, ModifierID, [No], [Date], [Description], [Status], OperationalStatus)
  VALUES (2, 1, 1, 1, 1, N'2', N'2017-02-17', N'خرید نقدی لوازم اداری', N'Draft', N'Created')
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
