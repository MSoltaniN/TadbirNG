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
GO

INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (72, 2, 1, N'edcf7914-4064-471a-aa37-e7684b8e4690', CAST(N'2019-06-02 11:59:57.950' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (73, 2, 2, N'49a8045a-d2d5-4539-85af-f4a9ae571cd5', CAST(N'2019-06-02 12:00:01.060' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (74, 2, 3, N'4ad7bde3-fa98-4de9-9438-fcb24e2b8759', CAST(N'2019-06-02 12:00:05.027' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (75, 2, 4, N'bcccaf9e-4511-4e21-9d7f-71be2d908e24', CAST(N'2019-06-02 12:00:08.930' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (76, 2, 5, N'2241dcbc-a303-451a-9935-1c8f4d12a82c', CAST(N'2019-06-02 12:00:12.320' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (77, 2, 6, N'a2133e0c-6fd2-4b7f-abc2-7fd638ce2387', CAST(N'2019-06-02 12:00:15.170' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (78, 2, 7, N'c19e0403-7c74-4fe3-a7fe-f2f1b1f18f97', CAST(N'2019-06-02 12:00:17.537' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (79, 2, 8, N'b44728cb-2648-446d-a42d-5ba819afc568', CAST(N'2019-06-02 12:00:21.080' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (80, 2, 9, N'fb8919b5-8b48-4c4f-8943-bcb5486669e8', CAST(N'2019-06-02 12:00:24.467' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (81, 2, 10, N'4ae35b80-a1c4-41fb-9972-8ae0537d25e7', CAST(N'2019-06-02 12:00:28.353' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (82, 2, 11, N'547c6413-9a40-4b7b-a86a-a182715cefa0', CAST(N'2019-06-02 12:00:33.227' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (83, 2, 12, N'78da6815-b81b-4fe5-8aec-8e6e188cf178', CAST(N'2019-06-02 12:00:36.233' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (84, 2, 13, N'e5cc477d-f7c5-418d-a7b3-a740063cc1f6', CAST(N'2019-06-02 12:00:39.210' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (85, 2, 14, N'001460ed-e742-4c21-bd94-811282d74b67', CAST(N'2019-06-02 12:00:42.220' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (86, 2, 15, N'9afd5119-3d92-4253-93ec-25a2e14e0aff', CAST(N'2019-06-02 12:00:45.003' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (87, 2, 16, N'016727dc-267b-4b11-99fc-87dda7b85b24', CAST(N'2019-06-02 12:00:48.870' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (88, 2, 22, N'd9046c44-22f7-446d-8267-8c49ada251ed', CAST(N'2019-06-02 12:01:36.410' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (89, 2, 23, N'1d16ced1-e6a7-44e3-a21b-4f4842a64993', CAST(N'2019-06-02 12:01:40.327' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (90, 2, 24, N'f7b69c3c-71de-4b56-97de-65c9560b785a', CAST(N'2019-06-02 12:01:42.930' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (91, 2, 25, N'321c5123-5ab2-4098-9c61-854d32350093', CAST(N'2019-06-02 12:01:46.087' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (92, 2, 26, N'363a9bba-e5ac-4062-96f7-e9179200fe46', CAST(N'2019-06-02 12:03:03.293' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (93, 2, 27, N'752ba72d-499c-4d63-96b1-f8c095a224b9', CAST(N'2019-06-02 12:03:09.610' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (94, 2, 28, N'f5beea67-792e-49a6-be6f-88c2bf1c3d06', CAST(N'2019-06-02 12:03:13.180' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (95, 2, 29, N'c3b074c9-915a-4148-8235-41c16a56c186', CAST(N'2019-06-02 12:03:17.660' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (96, 2, 30, N'124fdf06-288a-4c81-8053-ffc9e938b73c', CAST(N'2019-06-02 12:03:21.237' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (97, 2, 17, N'aab090d6-2e90-4f25-bd3c-6c5539d1d7f3', CAST(N'2019-06-02 12:04:15.397' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (98, 2, 36, N'bc096409-6e9a-4ce6-b2a5-e179358596ba', CAST(N'2019-06-02 12:05:05.317' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (99, 2, 41, N'b792b435-7b68-434d-bd0a-8b3048612e57', CAST(N'2019-06-02 12:05:12.283' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (100, 2, 45, N'26416381-f1db-42e9-b17b-3f943dc32ec4', CAST(N'2019-06-02 12:07:52.237' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (101, 2, 49, N'4e10ff86-b795-414a-8726-dc03328b1f52', CAST(N'2019-06-02 12:07:55.193' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (102, 2, 56, N'49fe332a-6154-4363-976d-3131d802d605', CAST(N'2019-06-02 12:08:22.100' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (103, 2, 57, N'b647f082-3678-4c5b-bf7b-a6c3ef9dfc8d', CAST(N'2019-06-02 12:08:24.700' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (104, 2, 60, N'97a0cc6a-3fc6-4396-a41e-5f878a1d9e64', CAST(N'2019-06-02 12:09:41.800' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (105, 2, 62, N'e8fe8a14-ff44-476f-925e-0e7da51128e7', CAST(N'2019-06-02 12:09:52.717' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (106, 2, 63, N'061dddeb-5971-4a94-8f61-6dbe4aba9bfa', CAST(N'2019-06-02 12:09:58.980' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (107, 2, 64, N'440a5ba4-157a-4c54-a28f-85a9407a1138', CAST(N'2019-06-02 12:10:04.240' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (108, 2, 65, N'06da03d6-3f54-4844-bf10-8f8e70c41077', CAST(N'2019-06-02 12:10:07.883' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (109, 2, 66, N'3df172dd-e96b-40a4-9704-0ad18bfe164a', CAST(N'2019-06-02 12:10:17.007' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (110, 2, 67, N'd87c3ed1-ee6c-41ff-a394-90278a8b0186', CAST(N'2019-06-02 12:10:28.243' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (111, 3, 1, N'4fe7f286-cde1-4b88-8b08-9686b5c90926', CAST(N'2019-06-02 12:12:12.167' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (112, 3, 5, N'2af00dcb-5426-4e1b-a5bc-9b40dc70d5ec', CAST(N'2019-06-02 12:12:14.550' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (113, 3, 9, N'a9ae51c4-cae9-42d6-bca0-387460e46ab7', CAST(N'2019-06-02 12:12:16.837' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (114, 3, 13, N'bf917674-1d52-441f-9165-96c53754237a', CAST(N'2019-06-02 12:12:20.657' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (115, 3, 17, N'b456dc5b-850b-4059-8be5-9dce954f3f00', CAST(N'2019-06-02 12:12:25.320' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (116, 3, 18, N'522900c2-a62d-45c5-a24f-3c9142580da6', CAST(N'2019-06-02 12:12:48.880' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (117, 3, 19, N'0e18eb9b-4d39-4130-87dc-8abd4747601e', CAST(N'2019-06-02 12:12:51.093' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (118, 3, 20, N'fb9eefa6-7ef7-419a-aee4-2fb45fc49845', CAST(N'2019-06-02 12:12:54.817' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (119, 3, 22, N'da3b2a74-b99d-4f55-b396-4587fb12a4d8', CAST(N'2019-06-02 12:13:59.073' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (120, 3, 26, N'09eeaa3c-7982-4c41-8547-eba2b06ed6ab', CAST(N'2019-06-02 12:14:01.247' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (121, 3, 31, N'0f2d9888-9ee0-4d4e-996c-516f8376bb8c', CAST(N'2019-06-02 12:14:03.987' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (122, 3, 34, N'b1197e6b-2ef3-4ff2-8ee2-8c51baf007be', CAST(N'2019-06-02 12:14:37.810' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (123, 3, 35, N'0b4af0c0-3c56-4353-8c48-decdc6a81c81', CAST(N'2019-06-02 12:14:40.393' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (124, 3, 36, N'410ec626-795e-40e1-8c2b-c90d36033a38', CAST(N'2019-06-02 12:14:43.653' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (125, 3, 41, N'cbcbf8c6-2fcd-467c-8cec-aa7660738171', CAST(N'2019-06-02 12:15:04.737' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (126, 3, 45, N'83a15d05-3b13-465f-9a26-551a05aa50c0', CAST(N'2019-06-02 12:15:07.420' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (127, 3, 49, N'644e16d5-1dea-443a-9830-eadcb1c594c1', CAST(N'2019-06-02 12:15:10.850' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (128, 3, 56, N'8864eee1-a8b2-445d-9098-6c190136033b', CAST(N'2019-06-02 12:15:32.543' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (129, 3, 57, N'7666e92f-0e20-4d39-a15d-3a088d96fa89', CAST(N'2019-06-02 12:15:35.083' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (130, 3, 60, N'e1d54479-c009-4573-9519-1789bd85a74f', CAST(N'2019-06-02 12:16:00.990' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (131, 3, 62, N'21acc7ab-75ab-4b08-880a-0e3db07b5ce6', CAST(N'2019-06-02 12:16:07.493' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (132, 3, 66, N'5bc9cb2e-f7df-4721-8629-cc8f32003e84', CAST(N'2019-06-02 12:16:10.097' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (133, 3, 69, N'aa4dd792-7089-48dc-ace7-5303be69c9c5', CAST(N'2019-06-02 12:16:51.753' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (134, 3, 71, N'b08cae52-0967-471b-8541-a2397eec83d8', CAST(N'2019-06-02 12:16:54.287' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (135, 4, 1, N'4fe4d256-07fd-431d-8943-9881a4b2c7f4', CAST(N'2019-06-02 12:18:00.273' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (136, 4, 5, N'd3f9cad9-9005-4e25-8fc2-8b280734587e', CAST(N'2019-06-02 12:18:02.693' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (137, 4, 9, N'9d7b100d-15b1-4c23-86da-465da121c8ef', CAST(N'2019-06-02 12:18:04.353' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (138, 4, 13, N'754329e6-2ef2-45a3-96da-493280edecde', CAST(N'2019-06-02 12:18:07.060' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (139, 4, 17, N'90009f43-9249-4eb0-aab2-4fed09044b61', CAST(N'2019-06-02 12:18:10.173' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (140, 4, 22, N'b22d9446-74c8-415a-bdfb-01c2c1b1fbc9', CAST(N'2019-06-02 12:19:08.123' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (141, 4, 26, N'2ce5a622-301b-4dd2-ad99-63b40e84548a', CAST(N'2019-06-02 12:19:09.987' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (142, 4, 32, N'f6727211-3e99-4f00-9ad4-e9551ba7c3dc', CAST(N'2019-06-02 12:19:13.673' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (143, 4, 35, N'0dd23975-7c61-40a4-b25e-ba295080a459', CAST(N'2019-06-02 12:19:42.940' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (144, 4, 41, N'65d43f1b-8b43-4343-9189-8275fb014d8d', CAST(N'2019-06-02 12:19:45.403' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (145, 4, 45, N'97ec74ef-7d5c-4fbd-baa9-230bbad1fad3', CAST(N'2019-06-02 12:19:47.900' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (146, 4, 49, N'1a129700-5f4a-4ecd-9c9e-c1f86adab38e', CAST(N'2019-06-02 12:19:51.373' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (147, 4, 60, N'c5bdc7da-ff65-4423-8054-1235da2c263f', CAST(N'2019-06-02 12:20:59.730' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (148, 4, 69, N'a39e6055-07a8-4941-bb7a-dc785fc1f4bd', CAST(N'2019-06-02 12:21:02.250' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (149, 4, 70, N'a2e017a3-63a7-45d9-a6b4-37a5bd22b265', CAST(N'2019-06-02 12:21:05.530' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (150, 4, 71, N'f384ddcd-d0e1-4630-a284-ec338b7b9a28', CAST(N'2019-06-02 12:21:08.300' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (151, 5, 1, N'18818138-db90-4263-b2a6-cedc8ce53955', CAST(N'2019-06-02 12:22:33.070' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (152, 5, 5, N'1a10cc04-7222-4a92-872b-dc9aee3f3deb', CAST(N'2019-06-02 12:22:34.480' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (153, 5, 9, N'3bbc16ec-8f57-4ab7-a27c-ef8538f86ae1', CAST(N'2019-06-02 12:22:36.883' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (154, 5, 13, N'54e8af47-fce1-4b15-8c44-d98c453ab5bf', CAST(N'2019-06-02 12:22:41.923' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (155, 5, 17, N'233d5469-de74-4c8f-9b1c-e127740a834a', CAST(N'2019-06-02 12:22:44.957' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (156, 5, 22, N'b13ec960-f00e-439c-bb44-5f37cccbb0f9', CAST(N'2019-06-02 12:22:56.710' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (157, 5, 26, N'18821da7-b6b6-4fc0-a82d-80ba67b2a3a6', CAST(N'2019-06-02 12:22:59.147' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (158, 5, 32, N'63bdea87-a408-499c-9ef6-9596186175c3', CAST(N'2019-06-02 12:23:02.590' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (159, 5, 33, N'0bb4f2e5-950d-4f92-9edc-2fc9117b0172', CAST(N'2019-06-02 12:23:05.073' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (160, 5, 35, N'40cc055b-9b5f-4b8b-be58-f77045b31eb8', CAST(N'2019-06-02 12:23:16.690' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (161, 5, 41, N'3eda9cdd-f6e4-4f33-9a3d-b75df840de46', CAST(N'2019-06-02 12:23:18.553' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (162, 5, 45, N'03babcca-b902-4e94-8c21-150bbab8cf78', CAST(N'2019-06-02 12:23:20.440' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (163, 5, 49, N'e33e40be-5842-4281-908c-3a43cea1cfc3', CAST(N'2019-06-02 12:23:22.820' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (164, 5, 60, N'00a2f881-2cf0-418c-b5d3-8b01858cca18', CAST(N'2019-06-02 12:23:37.880' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (165, 5, 69, N'e0bbee30-a030-46cc-be00-1481bbb1f8ac', CAST(N'2019-06-02 12:23:40.460' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (166, 5, 70, N'da202893-f558-4a27-a43d-c581b808e9b2', CAST(N'2019-06-02 12:23:47.327' AS DateTime))
GO
INSERT [Auth].[RolePermission] ([RolePermissionID], [RoleID], [PermissionID], [rowguid], [ModifiedDate]) VALUES (167, 5, 71, N'e73a2dce-9b01-4338-aad2-042deb0a971e', CAST(N'2019-06-02 12:23:50.567' AS DateTime))
GO

SET IDENTITY_INSERT [Auth].[RolePermission] OFF
GO
