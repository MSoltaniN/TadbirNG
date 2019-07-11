SET IDENTITY_INSERT [Config].[Setting] ON
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey)
    VALUES (7, 'QuickReportSettings', 3, 2, 'QuickReportConfig', N'{}', N'{}', 'QuickReportSettingsDescription')
SET IDENTITY_INSERT [Config].[Setting] OFF
