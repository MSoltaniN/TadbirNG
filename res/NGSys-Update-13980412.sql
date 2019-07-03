SET IDENTITY_INSERT [Auth].[PermissionGroup] ON
INSERT INTO [Auth].[PermissionGroup] ([PermissionGroupID], [Name], [EntityName]) VALUES (21, N'AccountBookReport', N'AccountBook')
SET IDENTITY_INSERT [Auth].[PermissionGroup] OFF

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (81, 21, N'View', 1)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (82, 21, N'Lookup', 2)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (83, 21, N'Filter', 4)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (84, 21, N'Print', 8)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (85, 21, N'Mark', 16)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (86, 21, N'ViewByBranch', 32)
SET IDENTITY_INSERT [Auth].[Permission] OFF

INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 81)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 82)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 83)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 84)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 85)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 86)

UPDATE [Metadata].[Command]
SET PermissionID = 28
WHERE CommandID IN(28, 29)

