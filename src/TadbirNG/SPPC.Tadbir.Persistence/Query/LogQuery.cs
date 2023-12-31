﻿using System;

namespace SPPC.Tadbir.Persistence
{
    internal static class LogQuery
    {
        internal const string OperationLogQuery = @"
SELECT [{0}ID] AS [Id], [Date], [Time], br.[Name] AS [BranchName], fp.[Name] AS [FiscalPeriodName], ent.[Name] AS [EntityTypeName],
    src.[Name] AS [SourceName], list.[Name] AS [SourceListName], op.[Name] AS [OperationName],
    [EntityCode], [EntityName], [EntityDescription], [EntityNo], [EntityDate], [EntityReference],
    [EntityAssociation], oplog.[Description], oplog.[UserID], oplog.[CompanyID], oplog.[BranchID], oplog.[FiscalPeriodID], oplog.[SourceID],
    oplog.[EntityTypeID]
FROM [Core].[{0}] oplog
    INNER JOIN [Corporate].[Branch] br ON oplog.BranchID = br.BranchID
    INNER JOIN [Finance].[FiscalPeriod] fp ON oplog.FiscalPeriodID = fp.FiscalPeriodID
	INNER JOIN [Metadata].[Operation] op ON oplog.OperationID = op.OperationID
	LEFT OUTER JOIN [Metadata].[EntityType] ent ON oplog.EntityTypeID = ent.EntityTypeID
	LEFT OUTER JOIN [Metadata].[OperationSource] src ON oplog.SourceID = src.OperationSourceID
	LEFT OUTER JOIN [Metadata].[OperationSourceList] list ON oplog.SourceListId = list.OperationSourceListID
ORDER BY {1}";

        internal const string SysOperationLogQuery = @"
SELECT [{0}ID] AS [Id], [Date], [Time], usr.[UserName] AS [UserName], comp.[Name] AS [CompanyName],
     ent.[Name] AS [EntityTypeName], src.[Name] AS [SourceName], op.[Name] AS [OperationName],
    [EntityCode], [EntityName], [EntityDescription], [EntityNo], [EntityDate],
    oplog.[Description], oplog.[UserID], oplog.[CompanyID], oplog.[EntityTypeID]
FROM [Core].[{0}] oplog
    INNER JOIN [Config].[CompanyDb] comp ON oplog.CompanyID = comp.CompanyID
    INNER JOIN [Auth].[User] usr ON oplog.UserID = usr.UserID
	INNER JOIN [Metadata].[Operation] op ON oplog.OperationID = op.OperationID
	LEFT OUTER JOIN [Metadata].[EntityType] ent ON oplog.EntityTypeID = ent.EntityTypeID
	LEFT OUTER JOIN [Metadata].[OperationSource] src ON oplog.SourceID = src.OperationSourceID
ORDER BY {1}";

        internal const string UserLookupQuery = @"
SELECT UserID, UserName FROM [Auth].[User] WHERE UserID IN({0})";

        internal const string CompanyLookupQuery = @"
SELECT CompanyID, Name FROM [Config].[CompanyDb] WHERE CompanyID IN({0})";
    }
}
