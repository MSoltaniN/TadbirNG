USE [NGTadbirSys]
GO

-- Add Triggers for MetaData.Columns...

CREATE TRIGGER [Metadata].[TR_MetaDataView_Delete] ON [Metadata].[Column]
AFTER Delete
AS
   update viw set ModifiedDate = GETDATE() from deleted del join [Metadata].[View] viw on del.ViewID = viw.ViewID
   delete setting from deleted del join Config.UserSetting setting on del.ViewID = setting.ViewID and setting.ModelType = 'ListFormViewConfig'   


GO

ALTER TABLE [Metadata].[Column] ENABLE TRIGGER [TR_MetaDataView_Delete]
GO

-----------------------------

CREATE TRIGGER [Metadata].[TR_MetaDataView_Insert] ON [Metadata].[Column]
AFTER Insert 
AS
   update viw set ModifiedDate = GETDATE() from inserted ins join [Metadata].[View] viw on ins.ViewID = viw.ViewID
   delete setting from inserted ins join Config.UserSetting setting on ins.ViewID = setting.ViewID and ModelType = 'ListFormViewConfig'   


GO

ALTER TABLE [Metadata].[Column] ENABLE TRIGGER [TR_MetaDataView_Insert]
GO

-----------------------------

CREATE TRIGGER [Metadata].[TR_MetaDataView_Update] ON [Metadata].[Column]
AFTER UPDATE 
AS
 
IF UPDATE([Name]) or UPDATE([Type]) or UPDATE([DotNetType]) or UPDATE(StorageType) or UPDATE([Length]) or UPDATE(MinLength) or  UPDATE(IsFixedLength)
 or UPDATE(IsNullable) or UPDATE(AllowSorting) or UPDATE(AllowFiltering) or UPDATE(Visibility) or UPDATE(DisplayIndex) or UPDATE(Expression)
BEGIN
   update viw set ModifiedDate = GETDATE() from inserted ins join [Metadata].[View] viw on ins.ViewID = viw.ViewID
   delete setting from inserted ins join Config.UserSetting setting on ins.ViewID = setting.ViewID and ModelType = 'ListFormViewConfig'   
END

GO

ALTER TABLE [Metadata].[Column] ENABLE TRIGGER [TR_MetaDataView_Update]
GO
