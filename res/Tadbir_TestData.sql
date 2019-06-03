USE [NGTadbir]
GO

SET IDENTITY_INSERT [Corporate].[Branch] ON
INSERT INTO [Corporate].[Branch] (BranchID, CompanyID, Name, [Level]) VALUES (1, 1, N'ساختمان مرکزی (تهران)', 0)
INSERT INTO [Corporate].[Branch] (BranchID, CompanyID, Name, [Level]) VALUES (2, 1, N'نمایندگی کرج', 0)
SET IDENTITY_INSERT [Corporate].[Branch] OFF

SET IDENTITY_INSERT [Finance].[FiscalPeriod] ON
INSERT INTO [Finance].[FiscalPeriod] (FiscalPeriodID, CompanyID, Name, StartDate, EndDate) VALUES (1, 1, N'سال مالی 1397', N'2018-03-21', N'2019-03-20')
INSERT INTO [Finance].[FiscalPeriod] (FiscalPeriodID, CompanyID, Name, StartDate, EndDate) VALUES (2, 1, N'سال مالی 1398', N'2019-03-21', N'2020-03-20')
SET IDENTITY_INSERT [Finance].[FiscalPeriod] OFF

SET IDENTITY_INSERT [Auth].[RoleBranch] ON
INSERT INTO [Auth].[RoleBranch] (RoleBranchID, RoleID, BranchID) VALUES (1, 1, 1)
INSERT INTO [Auth].[RoleBranch] (RoleBranchID, RoleID, BranchID) VALUES (2, 2, 1)
INSERT INTO [Auth].[RoleBranch] (RoleBranchID, RoleID, BranchID) VALUES (3, 3, 1)
INSERT INTO [Auth].[RoleBranch] (RoleBranchID, RoleID, BranchID) VALUES (4, 4, 1)
INSERT INTO [Auth].[RoleBranch] (RoleBranchID, RoleID, BranchID) VALUES (5, 5, 1)
INSERT INTO [Auth].[RoleBranch] (RoleBranchID, RoleID, BranchID) VALUES (6, 1, 2)
INSERT INTO [Auth].[RoleBranch] (RoleBranchID, RoleID, BranchID) VALUES (7, 2, 2)
INSERT INTO [Auth].[RoleBranch] (RoleBranchID, RoleID, BranchID) VALUES (8, 3, 2)
INSERT INTO [Auth].[RoleBranch] (RoleBranchID, RoleID, BranchID) VALUES (9, 4, 2)
INSERT INTO [Auth].[RoleBranch] (RoleBranchID, RoleID, BranchID) VALUES (10, 5, 2)
SET IDENTITY_INSERT [Auth].[RoleBranch] OFF

SET IDENTITY_INSERT [Auth].[RoleFiscalPeriod] ON
INSERT INTO [Auth].[RoleFiscalPeriod] (RoleFiscalPeriodID, RoleID, FiscalPeriodID) VALUES (1, 1, 1)
INSERT INTO [Auth].[RoleFiscalPeriod] (RoleFiscalPeriodID, RoleID, FiscalPeriodID) VALUES (2, 2, 1)
INSERT INTO [Auth].[RoleFiscalPeriod] (RoleFiscalPeriodID, RoleID, FiscalPeriodID) VALUES (3, 3, 1)
INSERT INTO [Auth].[RoleFiscalPeriod] (RoleFiscalPeriodID, RoleID, FiscalPeriodID) VALUES (4, 4, 1)
INSERT INTO [Auth].[RoleFiscalPeriod] (RoleFiscalPeriodID, RoleID, FiscalPeriodID) VALUES (5, 5, 1)
INSERT INTO [Auth].[RoleFiscalPeriod] (RoleFiscalPeriodID, RoleID, FiscalPeriodID) VALUES (6, 1, 2)
INSERT INTO [Auth].[RoleFiscalPeriod] (RoleFiscalPeriodID, RoleID, FiscalPeriodID) VALUES (7, 2, 2)
INSERT INTO [Auth].[RoleFiscalPeriod] (RoleFiscalPeriodID, RoleID, FiscalPeriodID) VALUES (8, 3, 2)
INSERT INTO [Auth].[RoleFiscalPeriod] (RoleFiscalPeriodID, RoleID, FiscalPeriodID) VALUES (9, 4, 2)
INSERT INTO [Auth].[RoleFiscalPeriod] (RoleFiscalPeriodID, RoleID, FiscalPeriodID) VALUES (10,5, 2)
SET IDENTITY_INSERT [Auth].[RoleFiscalPeriod] OFF

SET IDENTITY_INSERT [Finance].[Currency] ON
INSERT INTO [Finance].[Currency] (CurrencyID, Name) VALUES (1, N'ریال')
SET IDENTITY_INSERT [Finance].[Currency] OFF

-- Insert suggested account coding...
SET IDENTITY_INSERT [Finance].[Account] ON
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (101, NULL, 1, 1, 1, 1, 0, N'111', N'111', N'وجوه نقد', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (102, 101, 1, 1, 1, 1, 0, N'001', N'111001', N'صندوقها', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (103, 102, 1, 1, 1, 1, 0, N'0001', N'1110010001', N'صندوق', 2)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (104, 101, 1, 1, 1, 1, 0, N'002', N'111002', N'تنخواه گردانها', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (105, 101, 1, 1, 1, 1, 0, N'003', N'111003', N'وجوه در راه', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (106, NULL, 1, 1, 1, 1, 0, N'112', N'112', N'موجودى نزد بانكها', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (107, 106, 1, 1, 1, 1, 0, N'001', N'112001', N'بانك ملى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (108, 106, 1, 1, 1, 1, 0, N'002', N'112002', N'بانك صادرات', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (109, 106, 1, 1, 1, 1, 0, N'003', N'112003', N'بانك تجارت', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (110, 106, 1, 1, 1, 1, 0, N'004', N'112004', N'بانك ملت', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (111, 106, 1, 1, 1, 1, 0, N'005', N'112005', N'بانك سپه', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (112, 106, 1, 1, 1, 1, 0, N'006', N'112006', N'بانك مسكن', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (113, 106, 1, 1, 1, 1, 0, N'007', N'112007', N'بانك رفاه كارگران', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (114, 106, 1, 1, 1, 1, 0, N'008', N'112008', N'بانك كشاورزى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (115, 106, 1, 1, 1, 1, 0, N'009', N'112009', N'بانک پاسارگاد', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (116, 106, 1, 1, 1, 1, 0, N'010', N'112010', N'بانک پارسیان', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (117, 106, 1, 1, 1, 1, 0, N'011', N'112011', N'بانک شهر', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (118, 106, 1, 1, 1, 1, 0, N'012', N'112012', N'بانک اقتصاد نوین', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (119, NULL, 1, 1, 1, 1, 0, N'113', N'113', N'سرمايه گذاريهاى كوتاه مدت', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (120, NULL, 1, 1, 1, 1, 0, N'114', N'114', N'اسناد دريافتنى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (121, 120, 1, 1, 1, 1, 0, N'001', N'114001', N'اسناد دريافتنى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (122, 120, 1, 1, 1, 1, 0, N'002', N'114002', N'اسناد در جريان وصول', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (123, 120, 1, 1, 1, 1, 0, N'003', N'114003', N'اسناد برگشتى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (124, NULL, 1, 1, 1, 1, 0, N'115', N'115', N'حسابهاى دريافتنى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (125, 124, 1, 1, 1, 1, 0, N'001', N'115001', N'بدهكاران تجارى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (126, NULL, 1, 1, 1, 1, 0, N'116', N'116', N'ساير حسابها و اسناد دريافتنى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (127, 126, 1, 1, 1, 1, 0, N'001', N'116001', N'وام كاركنان', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (128, 126, 1, 1, 1, 1, 0, N'002', N'116002', N'مساعده كاركنان', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (129, 126, 1, 1, 1, 1, 0, N'003', N'116003', N'جارى كاركنان', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (130, NULL, 1, 1, 1, 1, 0, N'117', N'117', N'موجودى مواد و كالا', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (131, NULL, 1, 1, 1, 1, 0, N'118', N'118', N'سپرده ها و پيش پرداختها', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (132, 131, 1, 1, 1, 1, 0, N'001', N'118001', N'پيش پرداخت ماليات ارزش افزوده', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (133, 132, 1, 1, 1, 1, 0, N'0001', N'1180010001', N'ماليات ارزش افزوده دريافتنى', 2)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (134, 132, 1, 1, 1, 1, 0, N'0002', N'1180010002', N'عوارض ارزش افزوده دريافتنى', 2)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (135, NULL, 1, 1, 1, 1, 0, N'119', N'119', N'سفارشات', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (136, NULL, 1, 1, 1, 1, 0, N'151', N'151', N'داراييهاى ثابت مشهود', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (137, 136, 1, 1, 1, 1, 0, N'001', N'151001', N'ساختمانها', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (138, 136, 1, 1, 1, 1, 0, N'002', N'151002', N'ماشين آلات و تاسيسات', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (139, 136, 1, 1, 1, 1, 0, N'003', N'151003', N'وسايط نقليه', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (140, 136, 1, 1, 1, 1, 0, N'004', N'151004', N'اثاثه و منصوبات', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (141, 136, 1, 1, 1, 1, 0, N'005', N'151005', N'تجهيزات', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (142, 136, 1, 1, 1, 1, 0, N'006', N'151006', N'اموال انتقالى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (143, 136, 1, 1, 1, 1, 0, N'007', N'151007', N'استهلاك انباشته', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (144, NULL, 1, 1, 1, 1, 0, N'152', N'152', N'سرمايه گذاريهاى بلند مدت', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (145, NULL, 1, 1, 1, 1, 0, N'153', N'153', N'ساير داراييها', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (146, NULL, 1, 1, 1, 1, 0, N'211', N'211', N'اسناد پرداختنى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (147, 146, 1, 1, 1, 1, 0, N'001', N'211001', N'اسناد پرداختنى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (148, NULL, 1, 1, 1, 1, 0, N'212', N'212', N'حسابهاى پرداختنى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (149, 148, 1, 1, 1, 1, 0, N'001', N'212001', N'بستانكاران تجارى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (150, NULL, 1, 1, 1, 1, 0, N'213', N'213', N'ساير حسابها و اسناد پرداختنى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (151, 150, 1, 1, 1, 1, 0, N'001', N'213001', N'بيمه پرداختنى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (152, 151, 1, 1, 1, 1, 0, N'0001', N'2130010001', N'بيمه تامين اجتماعى شعبه ....', 2)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (153, 150, 1, 1, 1, 1, 0, N'002', N'213002', N'ماليات پرداختنى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (154, 153, 1, 1, 1, 1, 0, N'0001', N'2130020001', N'ماليات حقوق', 2)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (155, 153, 1, 1, 1, 1, 0, N'0002', N'2130020002', N'ماليات عملكرد', 2)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (156, 153, 1, 1, 1, 1, 0, N'0003', N'2130020003', N'ماليات تكليفى', 2)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (157, 153, 1, 1, 1, 1, 0, N'0004', N'2130020004', N'ماليات ارزش افزوده پرداختنى', 2)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (158, 153, 1, 1, 1, 1, 0, N'0005', N'2130020005', N'عوارض ارزش افزوده پرداختنى', 2)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (159, 150, 1, 1, 1, 1, 0, N'003', N'213003', N'حقوق پرداختنى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (160, NULL, 1, 1, 1, 1, 0, N'214', N'214', N'پيش دريافتها', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (161, NULL, 1, 1, 1, 1, 0, N'215', N'215', N'ذخيره ماليات', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (162, NULL, 1, 1, 1, 1, 0, N'216', N'216', N'سود سهام پيشنهادى و پرداختنى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (163, NULL, 1, 1, 1, 1, 0, N'217', N'217', N'تسهيلات و اعتبارات مالى دريافتى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (164, NULL, 1, 1, 1, 1, 0, N'251', N'251', N'اسناد پرداختنى بلند مدت', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (165, NULL, 1, 1, 1, 1, 0, N'252', N'252', N'حسابهاى پرداختنى بلند مدت', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (166, NULL, 1, 1, 1, 1, 0, N'253', N'253', N'تسهيلات مالى دريافتى بلند مدت', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (167, NULL, 1, 1, 1, 1, 0, N'254', N'254', N'ذخيره مزاياى پايان خدمت كاركنان', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (168, NULL, 1, 1, 1, 1, 0, N'311', N'311', N'سرمايه', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (169, NULL, 1, 1, 1, 1, 0, N'312', N'312', N'اندوخته قانونى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (170, NULL, 1, 1, 1, 1, 0, N'313', N'313', N'ساير اندوخته ها', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (171, NULL, 1, 1, 1, 1, 0, N'314', N'314', N'مازاد تجديد ارزيابى داراييهاى ثابت', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (172, NULL, 1, 1, 1, 1, 0, N'315', N'315', N'سود( زيان )انباشته', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (173, NULL, 1, 1, 1, 1, 0, N'411', N'411', N'فروش', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (174, 173, 1, 1, 1, 1, 0, N'001', N'411001', N'فروش', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (175, NULL, 1, 1, 1, 1, 0, N'412', N'412', N'برگشت از فروش و تخفيفات', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (176, 175, 1, 1, 1, 1, 0, N'001', N'412001', N'برگشت از فروش', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (177, 175, 1, 1, 1, 1, 0, N'002', N'412002', N'تخفيفات فروش', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (178, NULL, 1, 1, 1, 1, 0, N'451', N'451', N'ساير درآمدها', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (179, 178, 1, 1, 1, 1, 0, N'001', N'451001', N'سود سپرده بانكى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (180, 178, 1, 1, 1, 1, 0, N'002', N'451002', N'درآمد متفرقه', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (181, NULL, 1, 1, 1, 1, 0, N'452', N'452', N'سود و زيان عمليات اموال', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (182, NULL, 1, 1, 1, 1, 0, N'551', N'551', N'قيمت تمام شده كالاى فروش رفته', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (183, NULL, 1, 1, 1, 1, 0, N'552', N'552', N'كنترل دستمزد و سربار', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (184, 183, 1, 1, 1, 1, 0, N'001', N'552001', N'كنترل دستمزد', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (185, 183, 1, 1, 1, 1, 0, N'002', N'552002', N'كنترل سربار', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (186, NULL, 1, 1, 1, 1, 0, N'610', N'610', N'هزينه هاى عمومى و اداری', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (187, 186, 1, 1, 1, 1, 0, N'001', N'610001', N'هزینه آبدارخانه و پذيرايى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (188, 186, 1, 1, 1, 1, 0, N'002', N'610002', N'هزینه اياب و ذهاب', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (189, 186, 1, 1, 1, 1, 0, N'003', N'610003', N'هزینه ملزومات مصرفى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (190, 186, 1, 1, 1, 1, 0, N'004', N'610004', N'هزینه پيك و آژانس', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (191, 186, 1, 1, 1, 1, 0, N'005', N'610005', N'هزینه شارژ ماهيانه', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (192, 186, 1, 1, 1, 1, 0, N'006', N'610006', N'هزینه تعمير و نگهدارى اثاثه و تجهيزات ادارى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (193, 186, 1, 1, 1, 1, 0, N'007', N'610007', N'هزینه استهلاك اثاثه و تجهيزات ادارى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (194, NULL, 1, 1, 1, 1, 0, N'611', N'611', N'هزينه هاى حقوق و دستمزد', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (195, 194, 1, 1, 1, 1, 0, N'001', N'611001', N'هزینه حقوق پايه', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (196, 194, 1, 1, 1, 1, 0, N'002', N'611002', N'هزینه اضافه كارى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (197, 194, 1, 1, 1, 1, 0, N'003', N'611003', N'هزینه حق مسكن و خواربار', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (198, 194, 1, 1, 1, 1, 0, N'004', N'611004', N'هزینه حق بن', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (199, 194, 1, 1, 1, 1, 0, N'005', N'611005', N'هزینه حق اولاد', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (200, 194, 1, 1, 1, 1, 0, N'006', N'611006', N'هزینه بيمه سهم كارفرما', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (201, 194, 1, 1, 1, 1, 0, N'007', N'611007', N'هزینه عيدى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (202, 194, 1, 1, 1, 1, 0, N'008', N'611008', N'هزینه سنوات', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (203, 194, 1, 1, 1, 1, 0, N'009', N'611009', N'هزینه بازخريد مرخصى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (204, 194, 1, 1, 1, 1, 0, N'010', N'611010', N'هزینه پاداش', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (205, 194, 1, 1, 1, 1, 0, N'011', N'611011', N'هزینه كسركار', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (206, NULL, 1, 1, 1, 1, 0, N'612', N'612', N'هزينه هاى عملياتى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (207, 206, 1, 1, 1, 1, 0, N'001', N'612001', N'هزینه تعمير و نگهدارى ماشين آلات', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (208, 206, 1, 1, 1, 1, 0, N'002', N'612002', N'هزینه استهلاك ماشين آلات', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (209, NULL, 1, 1, 1, 1, 0, N'613', N'613', N'هزينه هاى توزيع و فروش', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (210, 209, 1, 1, 1, 1, 0, N'001', N'613001', N'هزینه هدايا و كمكها', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (211, 209, 1, 1, 1, 1, 0, N'002', N'613002', N'هزینه چاپ و تبليغات', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (212, 209, 1, 1, 1, 1, 0, N'003', N'613003', N'هزینه پورسانت فروش', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (213, NULL, 1, 1, 1, 1, 0, N'614', N'614', N'هزينه هاى مالى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (214, 213, 1, 1, 1, 1, 0, N'001', N'614001', N'هزينه بهره تسهيلات بانكى', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (215, 213, 1, 1, 1, 1, 0, N'002', N'614002', N'هزینه کارمزد بانکی', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (216, 213, 1, 1, 1, 1, 0, N'003', N'614003', N'هزینه صدور دسته چک', 1)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (217, NULL, 1, 1, 1, 1, 0, N'711', N'711', N'افتتاحيه', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (218, NULL, 1, 1, 1, 1, 0, N'712', N'712', N'اختتاميه', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (219, NULL, 1, 1, 1, 1, 0, N'713', N'713', N'عملكرد', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (220, NULL, 1, 1, 1, 1, 0, N'714', N'714', N'سود و زيان', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (221, NULL, 1, 1, 1, 1, 0, N'811', N'811', N'حسابهاى انتظامى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (222, NULL, 1, 1, 1, 1, 0, N'812', N'812', N'طرف حسابهاى انتظامى', 0)
INSERT INTO [Finance].[Account] (AccountID, ParentID, GroupID, FiscalPeriodID, BranchID, CurrencyID, BranchScope, Code, FullCode, Name, [Level])
    VALUES (223, 130, 1, 1, 1, 1, 0, N'001', N'117001', N'انبار مرکزی', 1)
SET IDENTITY_INSERT [Finance].[Account] OFF

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

SET IDENTITY_INSERT [Finance].[Voucher] ON
INSERT [Finance].[Voucher] ([VoucherID], [FiscalPeriodID], [BranchID], [StatusID], [IssuedByID], [ModifiedByID], [ConfirmedByID], [ApprovedByID], [No], [DailyNo], [Date], [Reference], [Association], [IsBalanced], [Type], [SubjectType], [SaveCount], [Description], [IssuerName], [ModifierName], [ConfirmerName], [ApproverName], [rowguid], [ModifiedDate]) VALUES (1, 1, 1, 1, 1, 1, NULL, NULL, 1, 1, CAST(N'2018-04-04 12:00:00.000' AS DateTime), N'', NULL, 1, 0, 0, 1, N'', N'سیستم، راهبر', N'سیستم، راهبر', NULL, NULL, N'8e7c5f46-352b-44a7-9296-d43737b5b6e8', CAST(N'2018-11-04 07:13:21.663' AS DateTime))
GO
INSERT [Finance].[Voucher] ([VoucherID], [FiscalPeriodID], [BranchID], [StatusID], [IssuedByID], [ModifiedByID], [ConfirmedByID], [ApprovedByID], [No], [DailyNo], [Date], [Reference], [Association], [IsBalanced], [Type], [SubjectType], [SaveCount], [Description], [IssuerName], [ModifierName], [ConfirmerName], [ApproverName], [rowguid], [ModifiedDate]) VALUES (2, 1, 1, 1, 1, 1, NULL, NULL, 2, 1, CAST(N'2018-04-24 12:00:00.000' AS DateTime), N'', NULL, 1, 0, 0, 1, NULL, N'سیستم، راهبر', N'سیستم، راهبر', NULL, NULL, N'38c69296-7ac5-4be7-a24a-0fcaeb1c7d77', CAST(N'2018-11-04 07:13:49.057' AS DateTime))
GO
INSERT [Finance].[Voucher] ([VoucherID], [FiscalPeriodID], [BranchID], [StatusID], [IssuedByID], [ModifiedByID], [ConfirmedByID], [ApprovedByID], [No], [DailyNo], [Date], [Reference], [Association], [IsBalanced], [Type], [SubjectType], [SaveCount], [Description], [IssuerName], [ModifierName], [ConfirmerName], [ApproverName], [rowguid], [ModifiedDate]) VALUES (3, 1, 1, 2, 1, 1, NULL, NULL, 3, 1, CAST(N'2018-06-01 12:00:00.000' AS DateTime), N'', NULL, 1, 0, 0, 1, N'فروش آنلاین کالا در سایت', N'سیستم، راهبر', N'سیستم، راهبر', NULL, NULL, N'4543eaf3-c507-4cbb-8b50-3dcf0b12e8ea', CAST(N'2018-10-31 11:05:22.427' AS DateTime))
GO
INSERT [Finance].[Voucher] ([VoucherID], [FiscalPeriodID], [BranchID], [StatusID], [IssuedByID], [ModifiedByID], [ConfirmedByID], [ApprovedByID], [No], [DailyNo], [Date], [Reference], [Association], [IsBalanced], [Type], [SubjectType], [SaveCount], [Description], [IssuerName], [ModifierName], [ConfirmerName], [ApproverName], [rowguid], [ModifiedDate]) VALUES (4, 1, 1, 1, 1, 1, NULL, NULL, 4, 1, CAST(N'2018-06-04 12:00:00.000' AS DateTime), N'', NULL, 1, 0, 0, 1, N'فروش آنلاین در سایت', N'سیستم، راهبر', N'سیستم، راهبر', NULL, NULL, N'e778cf80-48ee-45d2-9f4d-2b6608aa1c0e', CAST(N'2018-10-31 11:10:08.337' AS DateTime))
GO
INSERT [Finance].[Voucher] ([VoucherID], [FiscalPeriodID], [BranchID], [StatusID], [IssuedByID], [ModifiedByID], [ConfirmedByID], [ApprovedByID], [No], [DailyNo], [Date], [Reference], [Association], [IsBalanced], [Type], [SubjectType], [SaveCount], [Description], [IssuerName], [ModifierName], [ConfirmerName], [ApproverName], [rowguid], [ModifiedDate]) VALUES (5, 1, 1, 1, 1, 1, NULL, NULL, 5, 1, CAST(N'2018-07-01 12:00:00.000' AS DateTime), N'', NULL, 1, 0, 0, 1, N'فروش آنلاین کالا با تخفیف', N'سیستم، راهبر', N'سیستم، راهبر', NULL, NULL, N'b66c6616-0579-43c5-a858-de90f957aef7', CAST(N'2018-10-31 11:16:38.837' AS DateTime))
GO
INSERT [Finance].[Voucher] ([VoucherID], [FiscalPeriodID], [BranchID], [StatusID], [IssuedByID], [ModifiedByID], [ConfirmedByID], [ApprovedByID], [No], [DailyNo], [Date], [Reference], [Association], [IsBalanced], [Type], [SubjectType], [SaveCount], [Description], [IssuerName], [ModifierName], [ConfirmerName], [ApproverName], [rowguid], [ModifiedDate]) VALUES (6, 1, 1, 1, 1, 1, NULL, NULL, 6, 1, CAST(N'2018-07-10 10:44:00.000' AS DateTime), N'', NULL, 1, 0, 0, 1, N'فاکتور خرید', N'سیستم، راهبر', N'سیستم، راهبر', NULL, NULL, N'2794a377-0388-4310-a7a0-b2b917755baa', CAST(N'2018-11-04 07:14:30.793' AS DateTime))
GO
INSERT [Finance].[Voucher] ([VoucherID], [FiscalPeriodID], [BranchID], [StatusID], [IssuedByID], [ModifiedByID], [ConfirmedByID], [ApprovedByID], [No], [DailyNo], [Date], [Reference], [Association], [IsBalanced], [Type], [SubjectType], [SaveCount], [Description], [IssuerName], [ModifierName], [ConfirmerName], [ApproverName], [rowguid], [ModifiedDate]) VALUES (7, 1, 1, 1, 1, 1, NULL, NULL, 7, 1, CAST(N'2018-08-01 10:44:00.000' AS DateTime), N'', NULL, 1, 0, 0, 1, N'فاکتور فروش', N'سیستم، راهبر', N'سیستم، راهبر', NULL, NULL, N'634569fb-4c15-44a1-9cbe-e37b98523dd7', CAST(N'2018-11-04 07:15:05.543' AS DateTime))
GO
INSERT [Finance].[Voucher] ([VoucherID], [FiscalPeriodID], [BranchID], [StatusID], [IssuedByID], [ModifiedByID], [ConfirmedByID], [ApprovedByID], [No], [DailyNo], [Date], [Reference], [Association], [IsBalanced], [Type], [SubjectType], [SaveCount], [Description], [IssuerName], [ModifierName], [ConfirmerName], [ApproverName], [rowguid], [ModifiedDate]) VALUES (8, 1, 1, 1, 1, 1, NULL, NULL, 8, 1, CAST(N'2018-08-03 00:00:00.000' AS DateTime), N'', NULL, 1, 0, 0, 1, N'بدون شرح', N'سیستم، راهبر', N'سیستم، راهبر', NULL, NULL, N'3a6e5034-ed09-4a86-964b-32a920f2fa2d', CAST(N'2018-10-21 13:00:47.617' AS DateTime))
GO
INSERT [Finance].[Voucher] ([VoucherID], [FiscalPeriodID], [BranchID], [StatusID], [IssuedByID], [ModifiedByID], [ConfirmedByID], [ApprovedByID], [No], [DailyNo], [Date], [Reference], [Association], [IsBalanced], [Type], [SubjectType], [SaveCount], [Description], [IssuerName], [ModifierName], [ConfirmerName], [ApproverName], [rowguid], [ModifiedDate]) VALUES (9, 1, 1, 1, 1, 1, NULL, NULL, 9, 1, CAST(N'2018-09-04 00:00:00.000' AS DateTime), N'', NULL, 1, 0, 0, 1, N'بدون شرح', N'سیستم، راهبر', N'سیستم، راهبر', NULL, NULL, N'954638fa-a5d6-467c-8104-29cc380fb514', CAST(N'2018-10-21 13:16:14.413' AS DateTime))
GO
INSERT [Finance].[Voucher] ([VoucherID], [FiscalPeriodID], [BranchID], [StatusID], [IssuedByID], [ModifiedByID], [ConfirmedByID], [ApprovedByID], [No], [DailyNo], [Date], [Reference], [Association], [IsBalanced], [Type], [SubjectType], [SaveCount], [Description], [IssuerName], [ModifierName], [ConfirmerName], [ApproverName], [rowguid], [ModifiedDate]) VALUES (10, 1, 1, 1, 1, 1, NULL, NULL, 10, 1, CAST(N'2018-09-14 00:00:00.000' AS DateTime), N'', NULL, 1, 0, 0, 1, N'بدون شرح', N'سیستم، راهبر', N'سیستم، راهبر', NULL, NULL, N'38541825-e553-4b8b-aad9-92724fc29b8c', CAST(N'2018-10-21 14:03:57.910' AS DateTime))
GO
INSERT [Finance].[Voucher] ([VoucherID], [FiscalPeriodID], [BranchID], [StatusID], [IssuedByID], [ModifiedByID], [ConfirmedByID], [ApprovedByID], [No], [DailyNo], [Date], [Reference], [Association], [IsBalanced], [Type], [SubjectType], [SaveCount], [Description], [IssuerName], [ModifierName], [ConfirmerName], [ApproverName], [rowguid], [ModifiedDate]) VALUES (11, 1, 1, 1, 1, 1, NULL, NULL, 11, 1, CAST(N'2018-09-10 10:45:00.000' AS DateTime), N'', NULL, 1, 0, 0, 1, N'برگشت از خرید', N'سیستم، راهبر', N'سیستم، راهبر', NULL, NULL, N'e6b6c5f8-0cb3-4638-84b1-a1bf73c76e8c', CAST(N'2018-11-04 07:15:39.443' AS DateTime))
GO
INSERT [Finance].[Voucher] ([VoucherID], [FiscalPeriodID], [BranchID], [StatusID], [IssuedByID], [ModifiedByID], [ConfirmedByID], [ApprovedByID], [No], [DailyNo], [Date], [Reference], [Association], [IsBalanced], [Type], [SubjectType], [SaveCount], [Description], [IssuerName], [ModifierName], [ConfirmerName], [ApproverName], [rowguid], [ModifiedDate]) VALUES (12, 1, 1, 1, 1, 1, NULL, NULL, 12, 1, CAST(N'2018-09-26 12:00:00.000' AS DateTime), N'', NULL, 1, 0, 0, 1, N'برگشت از فروش', N'سیستم، راهبر', N'سیستم، راهبر', NULL, NULL, N'1c2288ad-e9d7-4917-8b15-73b0adbd0c40', CAST(N'2018-11-04 07:16:16.567' AS DateTime))
GO
INSERT [Finance].[Voucher] ([VoucherID], [FiscalPeriodID], [BranchID], [StatusID], [IssuedByID], [ModifiedByID], [ConfirmedByID], [ApprovedByID], [No], [DailyNo], [Date], [Reference], [Association], [IsBalanced], [Type], [SubjectType], [SaveCount], [Description], [IssuerName], [ModifierName], [ConfirmerName], [ApproverName], [rowguid], [ModifiedDate]) VALUES (13, 1, 1, 2, 1, 1, NULL, NULL, 13, 1, CAST(N'2018-10-28 10:46:00.000' AS DateTime), N'', NULL, 1, 0, 0, 1, N'فاکتور فروش با تخفیف', N'سیستم، راهبر', N'سیستم، راهبر', NULL, NULL, N'ddb6a66f-afe9-408f-8eae-70dcdc1ac9fd', CAST(N'2018-11-04 07:16:40.353' AS DateTime))
GO
INSERT [Finance].[Voucher] ([VoucherID], [FiscalPeriodID], [BranchID], [StatusID], [IssuedByID], [ModifiedByID], [ConfirmedByID], [ApprovedByID], [No], [DailyNo], [Date], [Reference], [Association], [IsBalanced], [Type], [SubjectType], [SaveCount], [Description], [IssuerName], [ModifierName], [ConfirmerName], [ApproverName], [rowguid], [ModifiedDate]) VALUES (14, 1, 1, 2, 1, 1, NULL, NULL, 14, 1, CAST(N'2018-10-30 10:47:00.000' AS DateTime), N'', NULL, 1, 0, 0, 1, N'برگشت از فروش با تخفیف', N'سیستم، راهبر', N'سیستم، راهبر', NULL, NULL, N'2794029d-e1fc-4ed1-9852-8198466b3604', CAST(N'2018-11-04 07:17:23.013' AS DateTime))
GO
INSERT [Finance].[Voucher] ([VoucherID], [FiscalPeriodID], [BranchID], [StatusID], [IssuedByID], [ModifiedByID], [ConfirmedByID], [ApprovedByID], [No], [DailyNo], [Date], [Reference], [Association], [IsBalanced], [Type], [SubjectType], [SaveCount], [Description], [IssuerName], [ModifierName], [ConfirmerName], [ApproverName], [rowguid], [ModifiedDate]) VALUES (15, 1, 1, 2, 1, 1, NULL, NULL, 15, 1, CAST(N'2018-11-01 10:47:00.000' AS DateTime), N'', NULL, 1, 0, 0, 1, N'سند دریافت به صندوق ', N'سیستم، راهبر', N'سیستم، راهبر', NULL, NULL, N'1ceaee59-3a8d-43c5-bf10-0fb5cfe657cf', CAST(N'2018-11-04 07:17:41.533' AS DateTime))
GO
INSERT [Finance].[Voucher] ([VoucherID], [FiscalPeriodID], [BranchID], [StatusID], [IssuedByID], [ModifiedByID], [ConfirmedByID], [ApprovedByID], [No], [DailyNo], [Date], [Reference], [Association], [IsBalanced], [Type], [SubjectType], [SaveCount], [Description], [IssuerName], [ModifierName], [ConfirmerName], [ApproverName], [rowguid], [ModifiedDate]) VALUES (16, 1, 1, 1, 1, 1, NULL, NULL, 16, 1, CAST(N'2018-11-02 03:15:00.000' AS DateTime), N'', NULL, 1, 0, 0, 1, N'سند دریافت به بانک', N'سیستم، راهبر', N'سیستم، راهبر', NULL, NULL, N'20e833d5-6df4-4eaf-9b72-9e1ebdf00971', CAST(N'2018-11-04 11:45:36.310' AS DateTime))
GO
INSERT [Finance].[Voucher] ([VoucherID], [FiscalPeriodID], [BranchID], [StatusID], [IssuedByID], [ModifiedByID], [ConfirmedByID], [ApprovedByID], [No], [DailyNo], [Date], [Reference], [Association], [IsBalanced], [Type], [SubjectType], [SaveCount], [Description], [IssuerName], [ModifierName], [ConfirmerName], [ApproverName], [rowguid], [ModifiedDate]) VALUES (17, 1, 1, 2, 1, 1, NULL, NULL, 17, 1, CAST(N'2018-11-03 03:22:00.000' AS DateTime), N'', NULL, 1, 0, 0, 1, N'سند پرداخت از صندوق ', N'سیستم، راهبر', N'سیستم، راهبر', NULL, NULL, N'b764ae32-4afa-47f7-bc06-dc336f3874d1', CAST(N'2018-11-04 11:52:31.520' AS DateTime))
GO
INSERT [Finance].[Voucher] ([VoucherID], [FiscalPeriodID], [BranchID], [StatusID], [IssuedByID], [ModifiedByID], [ConfirmedByID], [ApprovedByID], [No], [DailyNo], [Date], [Reference], [Association], [IsBalanced], [Type], [SubjectType], [SaveCount], [Description], [IssuerName], [ModifierName], [ConfirmerName], [ApproverName], [rowguid], [ModifiedDate]) VALUES (18, 1, 1, 2, 1, 1, NULL, NULL, 18, 1, CAST(N'2018-11-03 03:25:00.000' AS DateTime), N'', NULL, 1, 0, 0, 1, N'سند پرداخت از بانک', N'سیستم، راهبر', N'سیستم، راهبر', NULL, NULL, N'91877e89-90af-4da0-bd42-52b6136752af', CAST(N'2018-11-04 11:56:23.747' AS DateTime))
GO
SET IDENTITY_INSERT [Finance].[Voucher] OFF

SET IDENTITY_INSERT [Finance].[VoucherLine] ON 
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (1, 1, 1, 1, 107, NULL, 1, 1, 400000000.0000, 0.0000, N'واریز پول از بانک پاسارگاد به حساب صندوق', NULL, NULL, NULL, NULL, 0, NULL, N'5544a059-fa73-4d81-ab18-df63454890c7', CAST(N'2018-11-04 07:28:28.437' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (2, 1, 1, 1, 119, NULL, 1, 2, 0.0000, 800000000.0000, NULL, NULL, NULL, NULL, NULL, 0, NULL, N'0a2809bb-1667-44b4-945e-0ee597038d1f', CAST(N'2018-11-04 07:28:48.947' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (3, 2, 1, 1, 103, NULL, 1, 1, 14000000.0000, 0.0000, N'بابت دریافت طی سند دریافت شماره ۱', NULL, NULL, NULL, NULL, 0, NULL, N'ea3f7377-c6fb-4f5b-8252-6c74dc2142c8', CAST(N'2018-11-04 07:31:21.980' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (4, 2, 1, 1, 149, NULL, 1, 2, 0.0000, 14000000.0000, NULL, NULL, NULL, NULL, NULL, 0, NULL, N'215219f3-06a9-4897-81a1-e9f389b4a849', CAST(N'2018-11-04 07:35:17.343' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (5, 2, 1, 1, 121, NULL, 1, 3, 1000000.0000, 0.0000, N'بابت دریافت چک شماره (۱)  - به تاریخ: ۱۳۹۷/۰۷/۰۸', NULL, NULL, NULL, NULL, 0, NULL, N'f5484ac8-d87f-4123-8a14-c7b726234328', CAST(N'2018-11-04 07:36:00.663' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (6, 2, 1, 1, 149, NULL, 1, 4, 0.0000, 1000000.0000, NULL, NULL, NULL, NULL, NULL, 0, NULL, N'ae3e2fe8-d837-404c-b2e3-35b218d03189', CAST(N'2018-11-04 07:39:56.443' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (7, 3, 1, 1, 125, NULL, 1, 1, 100000.0000, 0.0000, N'خرید آنلاین توسط آقای مالکی', NULL, NULL, NULL, NULL, 0, NULL, N'78bdcdca-379a-48b2-abcc-5d4c07f83947', CAST(N'2018-10-31 11:07:55.980' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (8, 3, 1, 1, 174, NULL, 1, 2, 0.0000, 100000.0000, N'فروش مواد غذایی', NULL, NULL, NULL, NULL, 0, NULL, N'd3c97a81-5e8a-452e-9482-9744eacee05f', CAST(N'2018-10-31 11:08:51.417' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (9, 4, 1, 1, 125, NULL, 1, 1, 50000.0000, 0.0000, N'خرید آقای زارعی', NULL, NULL, NULL, NULL, 0, NULL, N'fe2c3ed3-fb61-4172-8d78-f417459c32b3', CAST(N'2018-10-31 11:11:17.877' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (10, 4, 1, 1, 125, NULL, 1, 2, 256000.0000, 0.0000, N'خرید خانم بخشی', NULL, NULL, NULL, NULL, 0, NULL, N'd48a7133-933b-4625-af64-5899fbda6b6f', CAST(N'2018-10-31 11:11:39.937' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (11, 4, 1, 1, 125, NULL, 1, 3, 168500.0000, 0.0000, N'خرید خانم یاوری', NULL, NULL, NULL, NULL, 0, NULL, N'4afcb9da-4f60-453b-8f1f-80dab3dc2471', CAST(N'2018-10-31 11:12:33.043' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (12, 4, 1, 1, 174, NULL, 1, 4, 0.0000, 50000.0000, N'فروش آنلاین لپ تاپ', NULL, NULL, NULL, NULL, 0, NULL, N'c35dc0ed-5917-48db-9726-b52bb6c1719a', CAST(N'2018-10-31 11:13:55.000' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (13, 4, 1, 1, 174, NULL, 1, 5, 0.0000, 256000.0000, N'فروش آنلاین دستمال کاغذی', NULL, NULL, NULL, NULL, 0, NULL, N'a31e1235-38fd-402e-a8b7-442ca2bd6d65', CAST(N'2018-10-31 11:14:49.273' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (14, 4, 1, 1, 174, NULL, 1, 6, 0.0000, 168500.0000, N'فروش آنلاین لوازم آرایشی', NULL, NULL, NULL, NULL, 0, NULL, N'5294082b-1352-49e8-a15a-76b60c20e080', CAST(N'2018-10-31 11:15:27.183' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (15, 5, 1, 1, 110, NULL, 1, 1, 125000.0000, 0.0000, N'انتقال وجه فروش کالا - آقای نادری', NULL, NULL, NULL, NULL, 0, NULL, N'442038e7-23c8-4fb8-bac5-b210d338f477', CAST(N'2018-10-31 11:18:02.140' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (16, 5, 1, 1, 109, NULL, 1, 2, 96800.0000, 0.0000, N'انتقال وجه فروش کالا - خانم نصیری', NULL, NULL, NULL, NULL, 0, NULL, N'756ba245-5ed4-4b48-8f49-0662c93dcef0', CAST(N'2018-10-31 11:18:30.523' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (17, 5, 1, 1, 111, NULL, 1, 3, 79200.0000, 0.0000, N'انتقال وجه فروش کالا - آقای بابایی', NULL, NULL, NULL, NULL, 0, NULL, N'c8269034-6edb-4371-a711-5fb99adf17c7', CAST(N'2018-10-31 11:19:08.047' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (18, 5, 1, 1, 177, NULL, 1, 4, 12000.0000, 0.0000, N'تخفیف خرید آنلاین', NULL, NULL, NULL, NULL, 0, NULL, N'4f55c6fe-05d4-44fc-86e2-0eebe06d011c', CAST(N'2018-10-31 11:20:12.007' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (19, 5, 1, 1, 174, NULL, 1, 5, 0.0000, 313000.0000, N'فروش آنلاین', NULL, NULL, NULL, NULL, 0, NULL, N'45ca584f-07d0-452f-b9c8-7fa5e3233e31', CAST(N'2018-10-31 11:20:45.340' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (20, 6, 1, 1, 165, NULL, 1, 1, 130000000.0000, 0.0000, N'بابت خرید طی فاکتور شماره 4', NULL, NULL, NULL, NULL, 0, NULL, N'cc901fa3-5363-47ab-865e-89c8d656a63c', CAST(N'2018-11-04 09:11:23.213' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (21, 6, 1, 1, 149, NULL, 1, 2, 0.0000, 130000000.0000, N'بابت خرید طی فاکتور شماره 4', NULL, NULL, NULL, NULL, 0, NULL, N'4aefeecb-8e6b-4445-90d9-3368b6fbc4f7', CAST(N'2018-11-04 09:12:02.310' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (22, 7, 1, 1, 125, NULL, 1, 1, 55450000.0000, 0.0000, N'بابت فروش طی فاکتور شماره 5', NULL, NULL, NULL, NULL, 0, NULL, N'e8a6a03e-f851-41b9-a42b-b36a3695a49f', CAST(N'2018-11-04 09:32:51.560' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (23, 7, 1, 1, 174, NULL, 1, 2, 0.0000, 55450000.0000, N'بابت فروش طی فاکتور شماره 5', NULL, NULL, NULL, NULL, 0, NULL, N'ed25453b-48d8-42be-9f77-2dba4b26b310', CAST(N'2018-11-04 09:34:44.080' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (24, 7, 1, 1, 182, NULL, 1, 3, 130000000.0000, 0.0000, N'بابت فروش طی فاکتور شماره 5', NULL, NULL, NULL, NULL, 0, NULL, N'c41f370c-6724-45cc-b755-2da7e389b141', CAST(N'2018-11-04 09:36:18.487' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (25, 7, 1, 1, 223, NULL, 1, 4, 0.0000, 116000000.0000, N'بابت فروش طی فاکتور شماره 5', NULL, NULL, NULL, NULL, 0, NULL, N'1e7d4fd0-3aec-4844-8ec5-468c52b0012b', CAST(N'2018-11-04 09:37:18.050' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (26, 7, 1, 1, 223, NULL, 1, 5, 0.0000, 14000000.0000, N'بابت فروش طی فاکتور شماره 5', NULL, NULL, NULL, NULL, 0, NULL, N'78234918-70a9-49b1-aa75-34425f67a324', CAST(N'2018-11-04 09:37:41.770' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (27, 8, 1, 1, 108, NULL, 1, 1, 500000.0000, 0.0000, N'واریز به حساب بانک صادرات', NULL, NULL, NULL, NULL, 0, NULL, N'eb5ba38c-d8e2-4ff9-b979-a72f3a2c1a92', CAST(N'2018-10-31 09:39:46.933' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (28, 8, 1, 1, 115, NULL, 1, 2, 48000.0000, 0.0000, N'واریز به حساب بانک پاسارگاد', NULL, NULL, NULL, NULL, 0, NULL, N'3595e773-c317-4c0e-a496-13155be4c78e', CAST(N'2018-10-31 09:40:43.653' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (29, 8, 1, 1, 114, NULL, 1, 3, 650000.0000, 0.0000, N'واریز به حساب بانک کشاورزی', NULL, NULL, NULL, NULL, 0, NULL, N'777dc4de-0431-4e7d-8ec7-092a511201ae', CAST(N'2018-10-31 09:41:36.530' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (30, 8, 1, 1, 168, NULL, 1, 4, 0.0000, 1198000.0000, N'افزایش سرمایه', NULL, NULL, NULL, NULL, 0, NULL, N'cb9721ba-9a58-454e-8429-a906ce1cae1a', CAST(N'2018-10-31 09:42:20.857' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (31, 9, 1, 1, 103, NULL, 1, 1, 60000.0000, 0.0000, N'دریافت وجه نقد از آقای فرهادی', NULL, NULL, NULL, NULL, 0, NULL, N'468f11ab-d639-49cc-8b43-464491ad47fc', CAST(N'2018-10-31 09:44:22.417' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (32, 9, 1, 1, 103, NULL, 1, 2, 95000.0000, 0.0000, N'دریافت وجه نقد از خانم صادقی', NULL, NULL, NULL, NULL, 0, NULL, N'ea2aa1b2-b0e3-42e3-8cd7-f7a121773cdc', CAST(N'2018-10-31 09:45:31.363' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (33, 9, 1, 1, 103, NULL, 1, 3, 120000.0000, 0.0000, N'دریافت وجه نقد از آقای محمودی', NULL, NULL, NULL, NULL, 0, NULL, N'c5449e5c-6132-487a-9385-4d7939856d03', CAST(N'2018-10-31 09:46:12.780' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (34, 9, 1, 1, 152, NULL, 1, 4, 0.0000, 275000.0000, N'پرداخت حق بیمه مهر ماه', NULL, NULL, NULL, NULL, 0, NULL, N'0bc8ac68-3174-4a65-898a-9f79d52a250a', CAST(N'2018-10-31 09:47:25.610' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (35, 10, 1, 1, 125, NULL, 1, 1, 180000.0000, 0.0000, N'بدهی آقای افروز', NULL, NULL, NULL, NULL, 0, NULL, N'0f9521c0-476e-4864-a9c9-aac186809248', CAST(N'2018-10-31 10:02:01.697' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (36, 10, 1, 1, 125, NULL, 1, 2, 45000.0000, 0.0000, N'بدهی آقای جوانمرد', NULL, NULL, NULL, NULL, 0, NULL, N'f4ee3900-c452-4415-a777-614ec0b5e09f', CAST(N'2018-10-31 10:03:01.803' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (37, 10, 1, 1, 121, NULL, 1, 3, 65000.0000, 0.0000, N'وصول چک خانم ساعدی', NULL, NULL, NULL, NULL, 0, NULL, N'2059a500-7cd3-4a58-87cc-ae70997e77b0', CAST(N'2018-10-31 10:04:14.920' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (38, 10, 1, 1, 149, NULL, 1, 4, 0.0000, 200000.0000, N'پرداخت بدهی آقای نارستانی', NULL, NULL, NULL, NULL, 0, NULL, N'9f7279ba-0530-44f4-90dd-db5bd07482bf', CAST(N'2018-10-31 10:05:18.833' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (39, 11, 1, 1, 125, NULL, 1, 1, 130000000.0000, 0.0000, N'بابت برگشت از خرید طی فاکتور شماره 4', NULL, NULL, NULL, NULL, 0, NULL, N'42bd4b7b-6193-4517-87db-5b6424439cc7', CAST(N'2018-11-04 09:48:06.010' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (40, 11, 1, 1, 223, NULL, 1, 2, 0.0000, 12000000.0000, N'بابت برگشت از خرید طی فاکتور شماره 4', NULL, NULL, NULL, NULL, 0, NULL, N'9533e85f-b74e-45ad-abe9-560521ff7ddf', CAST(N'2018-11-04 09:49:39.317' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (41, 11, 1, 1, 223, NULL, 1, 3, 0.0000, 118000000.0000, N'بابت هزینه برگشت از خرید', NULL, NULL, NULL, NULL, 0, NULL, N'e007e2e4-57cb-4e60-9f93-887136e1c6fd', CAST(N'2018-11-04 09:51:35.233' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (42, 12, 1, 1, 176, NULL, 1, 1, 55450000.0000, 0.0000, N'بابت برگشت از فروش طی فاکتور شماره 6', NULL, NULL, NULL, NULL, 0, NULL, N'5c715db3-40b5-43c4-a6c2-1076dc399665', CAST(N'2018-11-04 10:19:08.603' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (43, 12, 1, 1, 125, NULL, 1, 2, 0.0000, 55450000.0000, N'بابت برگشت از فروش طی فاکتور  شماره 6', NULL, NULL, NULL, NULL, 0, NULL, N'cd523b60-7f10-406e-bbd4-0c57a6086100', CAST(N'2018-11-04 10:21:43.723' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (44, 12, 1, 1, 223, NULL, 1, 3, 130000000.0000, 0.0000, N'بابت برگشت از فروش طی فاکتور  شماره 6', NULL, NULL, NULL, NULL, 0, NULL, N'2d1e553e-82f8-4cff-85e6-4e75b399adfe', CAST(N'2018-11-04 10:23:03.367' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (45, 12, 1, 1, 182, NULL, 1, 4, 0.0000, 130000000.0000, N'بابت برگشت از فروش طی فاکتور  شماره 6', NULL, NULL, NULL, NULL, 0, NULL, N'5bf1bcb0-583a-4cd2-9659-26a77f7e1bb1', CAST(N'2018-11-04 10:23:29.803' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (46, 13, 1, 1, 125, NULL, 1, 1, 209880000.0000, 0.0000, N'بابت فروش طی فاکتور شماره  7 ( با تخفیف)', NULL, NULL, NULL, NULL, 0, NULL, N'1aea2e46-f928-4445-a9ee-75de9c9f2a43', CAST(N'2018-11-04 10:46:40.677' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (47, 13, 1, 1, 177, NULL, 1, 2, 120000.0000, 0.0000, N'بابت فروش طی فاکتور شماره  7 ( با تخفیف)', NULL, NULL, NULL, NULL, 0, NULL, N'd9868bee-113a-4938-ae01-d342a74a8192', CAST(N'2018-11-04 10:50:49.530' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (48, 13, 1, 1, 174, NULL, 1, 3, 0.0000, 210000000.0000, N'بابت فروش طی فاکتور شماره  7 ( با تخفیف)', NULL, NULL, NULL, NULL, 0, NULL, N'ef29250d-c11e-4e8b-96d7-13c7226c6351', CAST(N'2018-11-04 10:52:56.617' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (49, 13, 1, 1, 182, NULL, 1, 4, 140000000.0000, 0.0000, N'بابت فروش طی فاکتور شماره  7 ( با تخفیف)', NULL, NULL, NULL, NULL, 0, NULL, N'659198f1-7dcf-4786-9f0a-561dd817f85a', CAST(N'2018-11-04 10:53:41.040' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (50, 13, 1, 1, 223, NULL, 1, 5, 0.0000, 120000000.0000, N'بابت فروش طی فاکتور شماره  7 ( با تخفیف)', NULL, NULL, NULL, NULL, 0, NULL, N'd1cbeb09-aef9-4f6c-92ac-0aaca3a23752', CAST(N'2018-11-04 10:54:35.477' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (51, 13, 1, 1, 223, NULL, 1, 6, 0.0000, 20000000.0000, N'بابت فروش طی فاکتور شماره  7 ( با تخفیف)', NULL, NULL, NULL, NULL, 0, NULL, N'c29271f2-d941-4b97-95cb-7f6c5cea0af5', CAST(N'2018-11-04 10:55:22.630' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (52, 14, 1, 1, 176, NULL, 1, 1, 21000000.0000, 0.0000, N'بابت برگشت از فروش طی فاکتور شماره 7  با تخفیف', NULL, NULL, NULL, NULL, 0, NULL, N'a6265f78-95d3-436d-a137-d6777624bc0b', CAST(N'2018-11-04 11:01:57.293' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (53, 14, 1, 1, 177, NULL, 1, 2, 0.0000, 12000.0000, N'بابت برگشت از فروش طی فاکتور شماره 7  با تخفیف120000', NULL, NULL, NULL, NULL, 0, NULL, N'4a9ea9af-4337-4ea7-853f-cd3a62c060f1', CAST(N'2018-11-04 11:06:05.057' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (54, 14, 1, 1, 125, NULL, 1, 3, 0.0000, 20988000.0000, N'بابت برگشت از فروش طی فاکتور شماره 7  با تخفیف', NULL, NULL, NULL, NULL, 0, NULL, N'3e49cf39-5ab8-4e43-b3d8-efeb8fa83121', CAST(N'2018-11-04 11:08:17.397' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (55, 14, 1, 1, 223, NULL, 1, 4, 140000000.0000, 0.0000, N'بابت برگشت از فروش طی فاکتور شماره 7  با تخفیف', NULL, NULL, NULL, NULL, 0, NULL, N'81be325e-d5ee-41ae-9f2e-bdc31abe537c', CAST(N'2018-11-04 11:10:27.387' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (56, 14, 1, 1, 182, NULL, 1, 5, 0.0000, 140000000.0000, N'بابت برگشت از فروش طی فاکتور شماره 7  با تخفیف', NULL, NULL, NULL, NULL, 0, NULL, N'a30e6bf7-5493-4f48-a3bb-5f5208a58df9', CAST(N'2018-11-04 11:11:08.163' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (57, 15, 1, 1, 103, NULL, 1, 1, 45600000.0000, 0.0000, N'بابت دریافت طی سند دریافت شماره 10', NULL, NULL, NULL, NULL, 0, NULL, N'f2078868-1f09-4680-aafb-7d6014dd634c', CAST(N'2018-11-04 11:43:56.510' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (58, 15, 1, 1, 149, NULL, 1, 2, 0.0000, 45600000.0000, N'بابت دریافت طی سند دریافت شماره 10', NULL, NULL, NULL, NULL, 0, NULL, N'69a30a92-4fd8-4ae4-8463-e40bd6763f94', CAST(N'2018-11-04 11:44:44.703' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (59, 16, 1, 1, 107, NULL, 1, 1, 9635200.0000, 0.0000, N'بابت دریافت طی سند دریافت شماره 7', NULL, NULL, NULL, NULL, 0, NULL, N'03b70293-50f0-4f18-b04b-e6afee499689', CAST(N'2018-11-04 11:48:00.303' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (60, 16, 1, 1, 125, NULL, 1, 2, 0.0000, 9635200.0000, N'بابت دریافت طی سند دریافت شماره 7', NULL, NULL, NULL, NULL, 0, NULL, N'455a1439-5515-44d4-bfda-fc332f60a057', CAST(N'2018-11-04 11:48:38.653' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (61, 17, 1, 1, 149, NULL, 1, 1, 8000000.0000, 0.0000, N'بابت پرداخت طی سند دریافت شماره 11', NULL, NULL, NULL, NULL, 0, NULL, N'b313e0b1-16e5-4697-aa6e-b9ab7b32e408', CAST(N'2018-11-04 11:54:32.443' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (62, 17, 1, 1, 103, NULL, 1, 2, 0.0000, 8000000.0000, N'بابت پرداخت طی سند دریافت شماره 11', NULL, NULL, NULL, NULL, 0, NULL, N'3d9c2116-dccc-43d8-b57a-47fe0bb1645a', CAST(N'2018-11-04 11:55:34.223' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (63, 18, 1, 1, 149, NULL, 1, 1, 9000000.0000, 0.0000, N'بابت پرداخت طی سند دریافت شماره 9', NULL, NULL, NULL, NULL, 0, NULL, N'201f3b00-b650-4650-8f02-f87687b4cb5d', CAST(N'2018-11-04 12:01:24.023' AS DateTime))
GO
INSERT [Finance].[VoucherLine] ([LineID], [VoucherID], [FiscalPeriodID], [BranchID], [AccountID], [CurrencyID], [CreatedByID], [RowNo], [Debit], [Credit], [Description], [Amount], [FollowupNo], [CurrencyValue], [Mark], [TypeID], [SourceID], [rowguid], [ModifiedDate]) VALUES (64, 18, 1, 1, 109, NULL, 1, 2, 0.0000, 9000000.0000, N'بابت پرداخت طی سند دریافت شماره 9', NULL, NULL, NULL, NULL, 0, NULL, N'45efbd6b-afd4-461b-9101-54fcc50ac324', CAST(N'2018-11-04 12:01:39.053' AS DateTime))
GO
SET IDENTITY_INSERT [Finance].[VoucherLine] OFF
