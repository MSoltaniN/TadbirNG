-- Important Note:
-- If you want to run this script from SSMS, replace {0} in line 79 with your SQL Server instance name.

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
INSERT [Contact].[Person] ([PersonID], [UserID], [FullName]) VALUES (2, 2, N'فرزاد فرهادی')
GO												  
INSERT [Contact].[Person] ([PersonID], [UserID], [FullName]) VALUES (3, 3, N'نادر قربانی')
GO												  
INSERT [Contact].[Person] ([PersonID], [UserID], [FullName]) VALUES (4, 4, N'مریم نادری')
GO												  
INSERT [Contact].[Person] ([PersonID], [UserID], [FullName]) VALUES (5, 5, N'کیوان جعفری')
GO												  
INSERT [Contact].[Person] ([PersonID], [UserID], [FullName]) VALUES (6, 6, N'آندره تیموریان')
GO												  
INSERT [Contact].[Person] ([PersonID], [UserID], [FullName]) VALUES (7, 7, N'نوشین بختیاری')
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

INSERT [Config].[CompanyDb] ([CompanyID], [Name], [DbName], [Server], [IsActive]) VALUES (1, N'شرکت تدبیر', N'NGTadbir', N'{0}', 1)
GO

SET IDENTITY_INSERT [Config].[CompanyDb] OFF
GO


SET IDENTITY_INSERT [Auth].[RoleCompany] ON 
GO

INSERT [Auth].[RoleCompany] ([RoleCompanyID], [RoleID], [CompanyID]) VALUES (1, 2, 1)
GO
INSERT [Auth].[RoleCompany] ([RoleCompanyID], [RoleID], [CompanyID]) VALUES (2, 3, 1)
GO
INSERT [Auth].[RoleCompany] ([RoleCompanyID], [RoleID], [CompanyID]) VALUES (3, 4, 1)
GO
INSERT [Auth].[RoleCompany] ([RoleCompanyID], [RoleID], [CompanyID]) VALUES (4, 5, 1)
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
