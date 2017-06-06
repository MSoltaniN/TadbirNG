USE [TadbirDemo]

DELETE FROM [Workflow].[WorkItemHistory]
GO

DELETE FROM [Workflow].[WorkItemDocument]
GO

DELETE FROM [Workflow].[WorkItem]
GO

DBCC CHECKIDENT ('[Workflow].[WorkItem]', RESEED, 0)
GO
DBCC CHECKIDENT ('[Workflow].[WorkItemDocument]', RESEED, 0)
GO
DBCC CHECKIDENT ('[Workflow].[WorkItemHistory]', RESEED, 0)
GO

UPDATE [Finance].[Transaction]
SET [Status] = N'Draft', [OperationalStatus] = N'Created', ConfirmedByID = NULL, ApprovedByID = NULL
GO
