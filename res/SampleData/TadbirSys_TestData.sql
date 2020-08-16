USE [NGTadbirSys]
GO

SET IDENTITY_INSERT [Auth].[User] ON 
GO
-- NOTE: All users are created with password 'Demo7890' (case-sesitive)
INSERT [Auth].[User] ([UserID], [UserName], [PasswordHash], [LastLoginDate], [IsEnabled]) VALUES (2, N'f_farhadi', N'70db1869eb931d8978843e6c2d95a5ea3a038fce782b939479374126040cb6f5', NULL, 1)
GO
INSERT [Auth].[User] ([UserID], [UserName], [PasswordHash], [LastLoginDate], [IsEnabled]) VALUES (3, N'n_ghorbani', N'70db1869eb931d8978843e6c2d95a5ea3a038fce782b939479374126040cb6f5', NULL, 1)
GO
INSERT [Auth].[User] ([UserID], [UserName], [PasswordHash], [LastLoginDate], [IsEnabled]) VALUES (4, N'm_naderi', N'70db1869eb931d8978843e6c2d95a5ea3a038fce782b939479374126040cb6f5', NULL, 1)
GO
INSERT [Auth].[User] ([UserID], [UserName], [PasswordHash], [LastLoginDate], [IsEnabled]) VALUES (5, N'k_jafari', N'70db1869eb931d8978843e6c2d95a5ea3a038fce782b939479374126040cb6f5', NULL, 1)
GO
INSERT [Auth].[User] ([UserID], [UserName], [PasswordHash], [LastLoginDate], [IsEnabled]) VALUES (6, N'a_teymourian', N'70db1869eb931d8978843e6c2d95a5ea3a038fce782b939479374126040cb6f5', NULL, 1)
GO
INSERT [Auth].[User] ([UserID], [UserName], [PasswordHash], [LastLoginDate], [IsEnabled]) VALUES (7, N'n_bakhtiary', N'70db1869eb931d8978843e6c2d95a5ea3a038fce782b939479374126040cb6f5', NULL, 1)
GO

SET IDENTITY_INSERT [Auth].[User] OFF
GO


SET IDENTITY_INSERT [Contact].[Person] ON 
GO
INSERT [Contact].[Person] ([PersonID], [UserID], [FirstName], [LastName]) VALUES (2, 2, N'فرزاد', N'فرهادی')
GO
INSERT [Contact].[Person] ([PersonID], [UserID], [FirstName], [LastName]) VALUES (3, 3, N'نادر', N'قربانی')
GO
INSERT [Contact].[Person] ([PersonID], [UserID], [FirstName], [LastName]) VALUES (4, 4, N'مریم', N'نادری')
GO
INSERT [Contact].[Person] ([PersonID], [UserID], [FirstName], [LastName]) VALUES (5, 5, N'کیوان', N'جعفری')
GO
INSERT [Contact].[Person] ([PersonID], [UserID], [FirstName], [LastName]) VALUES (6, 6, N'آندره', N'تیموریان')
GO
INSERT [Contact].[Person] ([PersonID], [UserID], [FirstName], [LastName]) VALUES (7, 7, N'نوشین', N'بختیاری')
GO
SET IDENTITY_INSERT [Contact].[Person] OFF
GO


SET IDENTITY_INSERT [Auth].[Role] ON 
GO

INSERT [Auth].[Role] ([RoleID], [Name], [Description]) VALUES (2, N'Role_Accountant', NULL)
GO
INSERT [Auth].[Role] ([RoleID], [Name], [Description]) VALUES (3, N'Role_AccountingHead', NULL)
GO
INSERT [Auth].[Role] ([RoleID], [Name], [Description]) VALUES (4, N'Role_CFOAssistant', NULL)
GO
INSERT [Auth].[Role] ([RoleID], [Name], [Description]) VALUES (5, N'Role_CFO', NULL)
GO
INSERT [Auth].[Role] ([RoleID], [Name], [Description]) VALUES (6, N'Role_DepartmentHead', NULL)
GO
INSERT [Auth].[Role] ([RoleID], [Name], [Description]) VALUES (7, N'Role_DepartmentManager', NULL)
GO
INSERT [Auth].[Role] ([RoleID], [Name], [Description]) VALUES (8, N'Role_WarehouseRep', NULL)
GO
INSERT [Auth].[Role] ([RoleID], [Name], [Description]) VALUES (9, N'Role_WarehouseHead', NULL)
GO
INSERT [Auth].[Role] ([RoleID], [Name], [Description]) VALUES (10, N'Role_WarehouseManager', NULL)
GO
INSERT [Auth].[Role] ([RoleID], [Name], [Description]) VALUES (11, N'Role_WarehouseAccountant', NULL)
GO
INSERT [Auth].[Role] ([RoleID], [Name], [Description]) VALUES (12, N'Role_SalesRep', NULL)
GO
INSERT [Auth].[Role] ([RoleID], [Name], [Description]) VALUES (13, N'Role_SalesAssistant', NULL)
GO
INSERT [Auth].[Role] ([RoleID], [Name], [Description]) VALUES (14, N'Role_SalesManager', NULL)
GO

SET IDENTITY_INSERT [Auth].[Role] OFF
GO


SET IDENTITY_INSERT [Config].[CompanyDb] ON 
GO

INSERT [Config].[CompanyDb] ([CompanyID], [Name], [DbName], [DbPath], [Server], [UserName], [Password], [Description]) VALUES (1, N'پردازش موازی سامان', N'NGTadbir', N'C:\Path\NGTadbir.mdf', N'.', NULL, NULL, N'شرکت پردازش موازی سامان (با مسئولیت محدود)')
GO
INSERT [Config].[CompanyDb] ([CompanyID], [Name], [DbName], [DbPath], [Server], [UserName], [Password], [Description]) VALUES (2, N'کلران', N'NGColoran', N'C:\Path\NGColoran.mdf', N'.', NULL, NULL, N'(تبدیل شده از دیتابیس تدبیر دسکتاپ)')
GO
INSERT [Config].[CompanyDb] ([CompanyID], [Name], [DbName], [DbPath], [Server], [UserName], [Password], [Description]) VALUES (3, N'ویلاپخش', N'NGVilapakhsh', N'C:\Path\NGVilapakhsh.mdf', N'.', NULL, NULL, N'(تبدیل شده از دیتابیس تدبیر دسکتاپ)')
GO

SET IDENTITY_INSERT [Config].[CompanyDb] OFF
GO


SET IDENTITY_INSERT [Auth].[RoleCompany] ON 
GO

INSERT [Auth].[RoleCompany] ([RoleCompanyID], [RoleID], [CompanyID]) VALUES (1, 1, 1)
GO
INSERT [Auth].[RoleCompany] ([RoleCompanyID], [RoleID], [CompanyID]) VALUES (2, 1, 2)
GO
INSERT [Auth].[RoleCompany] ([RoleCompanyID], [RoleID], [CompanyID]) VALUES (3, 1, 3)
GO
INSERT [Auth].[RoleCompany] ([RoleCompanyID], [RoleID], [CompanyID]) VALUES (4, 2, 1)
GO
INSERT [Auth].[RoleCompany] ([RoleCompanyID], [RoleID], [CompanyID]) VALUES (5, 2, 2)
GO
INSERT [Auth].[RoleCompany] ([RoleCompanyID], [RoleID], [CompanyID]) VALUES (6, 2, 3)
GO
INSERT [Auth].[RoleCompany] ([RoleCompanyID], [RoleID], [CompanyID]) VALUES (7, 3, 1)
GO
INSERT [Auth].[RoleCompany] ([RoleCompanyID], [RoleID], [CompanyID]) VALUES (8, 3, 2)
GO
INSERT [Auth].[RoleCompany] ([RoleCompanyID], [RoleID], [CompanyID]) VALUES (9, 3, 3)
GO
INSERT [Auth].[RoleCompany] ([RoleCompanyID], [RoleID], [CompanyID]) VALUES (10, 4, 1)
GO
INSERT [Auth].[RoleCompany] ([RoleCompanyID], [RoleID], [CompanyID]) VALUES (11, 4, 2)
GO
INSERT [Auth].[RoleCompany] ([RoleCompanyID], [RoleID], [CompanyID]) VALUES (12, 4, 3)
GO
INSERT [Auth].[RoleCompany] ([RoleCompanyID], [RoleID], [CompanyID]) VALUES (13, 5, 1)
GO
INSERT [Auth].[RoleCompany] ([RoleCompanyID], [RoleID], [CompanyID]) VALUES (14, 5, 2)
GO
INSERT [Auth].[RoleCompany] ([RoleCompanyID], [RoleID], [CompanyID]) VALUES (15, 5, 3)
GO

SET IDENTITY_INSERT [Auth].[RoleCompany] OFF
GO


SET IDENTITY_INSERT [Auth].[UserRole] ON 
GO

INSERT [Auth].[UserRole] ([UserRoleID], [UserID], [RoleID]) VALUES (2, 5, 2)
GO
INSERT [Auth].[UserRole] ([UserRoleID], [UserID], [RoleID]) VALUES (3, 6, 2)
GO
INSERT [Auth].[UserRole] ([UserRoleID], [UserID], [RoleID]) VALUES (4, 3, 5)
GO
INSERT [Auth].[UserRole] ([UserRoleID], [UserID], [RoleID]) VALUES (5, 4, 4)
GO
INSERT [Auth].[UserRole] ([UserRoleID], [UserID], [RoleID]) VALUES (6, 2, 3)
GO
INSERT [Auth].[UserRole] ([UserRoleID], [UserID], [RoleID]) VALUES (7, 7, 2)
GO

SET IDENTITY_INSERT [Auth].[UserRole] OFF
GO


SET IDENTITY_INSERT [Auth].[RolePermission] ON 

INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (1, 2, 1)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (2, 2, 2)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (3, 2, 3)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (4, 2, 4)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (5, 2, 5)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (6, 2, 6)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (7, 2, 7)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (8, 2, 8)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (9, 2, 9)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (10, 2, 10)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (11, 2, 11)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (12, 2, 12)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (13, 2, 13)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (14, 2, 14)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (15, 2, 15)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (16, 2, 16)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (17, 2, 22)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (18, 2, 23)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (19, 2, 24)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (20, 2, 25)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (21, 2, 26)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (22, 2, 27)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (23, 2, 28)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (24, 2, 29)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (25, 2, 30)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (26, 2, 17)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (27, 2, 36)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (28, 2, 41)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (29, 2, 45)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (30, 2, 49)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (31, 2, 56)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (32, 2, 57)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (33, 2, 60)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (34, 2, 62)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (35, 2, 63)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (36, 2, 64)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (37, 2, 65)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (38, 2, 66)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (39, 2, 67)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (40, 3, 1)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (41, 3, 5)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (42, 3, 9)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (43, 3, 13)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (44, 3, 17)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (45, 3, 18)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (46, 3, 19)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (47, 3, 20)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (48, 3, 22)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (49, 3, 26)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (50, 3, 31)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (51, 3, 34)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (52, 3, 35)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (53, 3, 36)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (54, 3, 41)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (55, 3, 45)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (56, 3, 49)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (57, 3, 56)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (58, 3, 57)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (59, 3, 60)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (60, 3, 62)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (61, 3, 66)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (62, 3, 69)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (63, 3, 71)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (64, 4, 1)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (65, 4, 5)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (66, 4, 9)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (67, 4, 13)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (68, 4, 17)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (69, 4, 22)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (70, 4, 26)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (71, 4, 32)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (72, 4, 35)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (73, 4, 41)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (74, 4, 45)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (75, 4, 49)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (76, 4, 60)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (77, 4, 69)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (78, 4, 70)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (79, 4, 71)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (80, 5, 1)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (81, 5, 5)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (82, 5, 9)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (83, 5, 13)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (84, 5, 17)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (85, 5, 22)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (86, 5, 26)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (87, 5, 32)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (88, 5, 33)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (89, 5, 35)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (90, 5, 41)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (91, 5, 45)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (92, 5, 49)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (93, 5, 60)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (94, 5, 69)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (95, 5, 70)
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID]) VALUES (96, 5, 71)

SET IDENTITY_INSERT [Auth].[RolePermission] OFF

