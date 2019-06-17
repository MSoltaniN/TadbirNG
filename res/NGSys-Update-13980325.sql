USE [NGTadbirSys]
GO

INSERT INTO [Config].[Setting]
           ([SettingID]
		   ,[ParentID]
           ,[Subsystem]
           ,[TitleKey]
           ,[Type]
           ,[ScopeType]
           ,[ModelType]
           ,[Values]
           ,[DefaultValues]
           ,[DescriptionKey])
     VALUES
           (6, NULL, NULL, 'QuickSearchSetting', 3, 2, 'QuickSearchConfig', N'{}', N'{}', 'QuickSearchConfigDescription')
GO


