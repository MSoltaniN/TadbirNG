USE [NGTadbirSys]
GO

UPDATE [Auth].[Permission]
SET Name = N'NavigateEntities,Vouchers'
WHERE GroupID = 7 AND Flag = 16

UPDATE [Auth].[Permission]
SET Name = N'Lookup'
WHERE GroupID = 7 AND Flag = 32

UPDATE [Auth].[Permission]
SET Name = N'Filter'
WHERE GroupID = 7 AND Flag = 64

UPDATE [Auth].[Permission]
SET Name = N'Print'
WHERE GroupID = 7 AND Flag = 128

UPDATE [Auth].[Permission]
SET Name = N'Check'
WHERE GroupID = 7 AND Flag = 256

UPDATE [Auth].[Permission]
SET Name = N'UndoCheck'
WHERE GroupID = 7 AND Flag = 512
