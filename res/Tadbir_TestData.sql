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
INSERT INTO [Finance].[FiscalPeriod] (FiscalPeriodID, BranchID, Name, StartDate, EndDate) VALUES (1, 1, N'سال مالی 1395', N'2016-03-20', N'2017-03-20')
INSERT INTO [Finance].[FiscalPeriod] (FiscalPeriodID, BranchID, Name, StartDate, EndDate) VALUES (2, 1, N'سال مالی 1396', N'2017-03-21', N'2018-03-20')
INSERT INTO [Finance].[FiscalPeriod] (FiscalPeriodID, BranchID, Name, StartDate, EndDate) VALUES (3, 2, N'سال مالی 1395', N'2016-03-20', N'2017-03-20')
SET IDENTITY_INSERT [Finance].[FiscalPeriod] OFF

SET IDENTITY_INSERT [Finance].[Account] ON
INSERT INTO [Finance].[Account] (AccountID, FiscalPeriodID, Code, Name) VALUES (1, 1, N'100', N'دارایی های جاری')
INSERT INTO [Finance].[Account] (AccountID, FiscalPeriodID, Code, Name) VALUES (2, 1, N'107', N'دارایی های ثابت مشهود')
INSERT INTO [Finance].[Account] (AccountID, FiscalPeriodID, Code, Name) VALUES (3, 3, N'1000', N'دارایی های جاری')
SET IDENTITY_INSERT [Finance].[Account] OFF
