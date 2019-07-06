
USE [NGTadbirSys]
GO

UPDATE [Metadata].[Command]
SET TitleKey = N'ChangeCompany'
WHERE CommandID = 20

SET IDENTITY_INSERT [Auth].[Permission] ON
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (87, 7, N'Confirm', 1024)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (88, 7, N'UndoConfirm', 2048)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (89, 7, N'Approve', 4096)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (90, 7, N'UndoApprove', 8192)
INSERT INTO [Auth].[Permission] ([PermissionID], [GroupID], [Name], [Flag]) VALUES (91, 7, N'Finalize', 16384)
SET IDENTITY_INSERT [Auth].[Permission] OFF

UPDATE [Auth].[Permission]
SET Name = N'Check'
WHERE PermissionID = 34
GO

UPDATE [Auth].[Permission]
SET Name = N'UndoCheck'
WHERE PermissionID = 35
GO

INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 87)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 88)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 89)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 90)
INSERT INTO [Auth].[RolePermission] (RoleID, PermissionID) VALUES (1, 91)
