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

UPDATE [Core].[Document]
SET [StatusID] = 1, [OperationalStatus] = N'Created'
GO

UPDATE [Core].[DocumentAction]
SET ConfirmedByID = NULL, ApprovedByID = NULL, ConfirmedDate = NULL, ApprovedDate = NULL
GO
