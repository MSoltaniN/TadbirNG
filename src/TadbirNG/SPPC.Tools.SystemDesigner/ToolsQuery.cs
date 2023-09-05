using System;

namespace SPPC.Tools.SystemDesigner
{
    public static class ToolsQuery
    {
        public const string LogSettings = @"
SELECT ls.LogSettingID, ls.EntityTypeID, et.Name AS EntityName, et.Description AS EntityDescription,
    ls.SourceID, os.Name AS SourceName, os.Description AS SourceDescription,
    ls.OperationID, op.Name AS OperationName, op.Description AS OperationDescription,
    ls.SourceTypeID, ost.Name AS SourceTypeName, ls.SubsystemID, ss.Name AS SubsystemName, ls.IsEnabled
FROM Config.LogSetting [ls]
    LEFT OUTER JOIN Metadata.EntityType [et] ON ls.EntityTypeID = et.EntityTypeID
    LEFT OUTER JOIN Metadata.OperationSource [os] ON ls.SourceID = os.OperationSourceID
    INNER JOIN Metadata.Operation [op] ON ls.OperationID = op.OperationID
    INNER JOIN Metadata.OperationSourceType [ost] ON ls.SourceTypeID = ost.OperationSourceTypeID
    INNER JOIN Metadata.Subsystem [ss] ON ls.SubsystemID = ss.SubsystemID";

        public const string NewSubsystems = @"
SELECT [SubsystemID], [Name]
FROM [Metadata].[Subsystem]
WHERE [SubsystemID] NOT IN(
    SELECT DISTINCT [SubsystemID]
    FROM [Config].[LogSetting])";

        public const string SysLogSettings = @"
SELECT sls.SysLogSettingID, sls.EntityTypeID, et.Name AS EntityName, et.Description AS EntityDescription,
    sls.SourceID, os.Name AS SourceName, os.Description AS SourceDescription,
    sls.OperationID, op.Name AS OperationName, op.Description AS OperationDescription, sls.IsEnabled
FROM Config.SysLogSetting [sls]
    LEFT OUTER JOIN Metadata.EntityType [et] ON et.EntityTypeID = sls.EntityTypeID
    LEFT OUTER JOIN Metadata.OperationSource [os] ON sls.SourceID = os.OperationSourceID
    INNER JOIN Metadata.Operation [op] ON sls.OperationID = op.OperationID";

        public const string AddDbVersion = @"
INSERT INTO [Core].[Version] ([VersionID], [Number])
VALUES (1, '{0}')";

        public const string UsersLookup = @"
SELECT [u].[UserID], [u].[UserName], [p].[FullName]
FROM [Auth].[User] u INNER JOIN [Contact].[Person] p
    ON [u].[UserID] = [p].[UserID]
WHERE [u].[IsEnabled] = 1";

        public const string LicenseQueryByInstanceKey = @"
SELECT [LicenseID]
FROM [dbo].[License]
WHERE [LicenseKey] = '{0}' AND [CustomerKey] = '{1}'";

        public const string DeactivateLicense = @"
UPDATE [dbo].[License]
SET [HardwareKey] = NULL, [ClientKey] = NULL, [Secret] = NULL, [IsActivated] = 0,
    [UserCount] = {0}, [Edition] = '{1}', [StartDate] = '{2}', [EndDate] = '{3}', [OfflineLimit] = {4}
WHERE [LicenseID] = {5}";
    }
}
