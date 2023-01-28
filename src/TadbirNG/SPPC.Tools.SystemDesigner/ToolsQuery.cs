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

        public const string SysLogSettings = @"
SELECT sls.SysLogSettingID, sls.EntityTypeID, et.Name AS EntityName, et.Description AS EntityDescription,
    sls.SourceID, os.Name AS SourceName, os.Description AS SourceDescription,
    sls.OperationID, op.Name AS OperationName, op.Description AS OperationDescription, sls.IsEnabled
FROM Config.SysLogSetting [sls]
    LEFT OUTER JOIN Metadata.EntityType [et] ON et.EntityTypeID = sls.EntityTypeID
    LEFT OUTER JOIN Metadata.OperationSource [os] ON sls.SourceID = os.OperationSourceID
    INNER JOIN Metadata.Operation [op] ON sls.OperationID = op.OperationID";
    }
}
