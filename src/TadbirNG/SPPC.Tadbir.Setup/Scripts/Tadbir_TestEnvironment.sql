-- The following commands can be removed, but are now necessary because Angular client has a bug that looks for non-existing FiscalPeriodID = 1
-- NOTE: Having a default environment would be a good start for new user, even if the above bug is fixed in Angular client.
USE [@FirstDbName]
GO

SET IDENTITY_INSERT [Corporate].[Branch] ON
INSERT INTO [Corporate].[Branch] ([BranchID], [CompanyID], [Name], [Level])
VALUES (1, 1, N'دفتر مرکزی', 0)
SET IDENTITY_INSERT [Corporate].[Branch] OFF

SET IDENTITY_INSERT [Finance].[FiscalPeriod] ON
INSERT INTO [Finance].[FiscalPeriod] ([FiscalPeriodID], [CompanyID], [Name], [StartDate], [EndDate], [InventoryMode])
VALUES (1, 1, N'سال 1400', '2021-03-21', '2022-03-20', 1)
SET IDENTITY_INSERT [Finance].[FiscalPeriod] OFF
