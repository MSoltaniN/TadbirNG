USE [NGTadbirSys]
GO

SET IDENTITY_INSERT [Metadata].[View] ON
INSERT [Metadata].[View] ([ViewID], [Name], [EntityName], [IsHierarchy], [IsCartableIntegrated], [FetchUrl], [SearchUrl], [EntityType])
  VALUES (68, N'School', N'School', 0, 0, N'/lookup/schools', NULL, N'Core')
SET IDENTITY_INSERT [Metadata].[View] OFF

SET IDENTITY_INSERT [Metadata].[Column] ON
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
  VALUES (687, 68, N'RowNo', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysVisible', 0, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
  VALUES (688, 68, N'Name', NULL, NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 0, 1, 1, NULL, 1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
  VALUES (689, 68, N'AdminSystem', NULL, NULL, N'System.String', N'nvarchar', N'string', 32, 0, 0, 0, 0, 1, 1, NULL, 2, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
  VALUES (690, 68, N'Manager', NULL, NULL, N'System.String', N'nvarchar', N'string', 32, 0, 0, 0, 0, 1, 1, NULL, 3, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
  VALUES (691, 68, N'Capacity', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 0, 1, 1, NULL, 4, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
  VALUES (692, 68, N'Tuition', NULL, N'Money', N'System.Decimal', N'money', N'number', 0, 0, 0, 0, 0, 1, 1, NULL, 5, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
  VALUES (693, 68, N'Address', NULL, NULL, N'System.String', N'nvarchar', N'string', 256, 0, 0, 0, 0, 1, 1, NULL, 6, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
  VALUES (694, 68, N'FoundedDate', NULL, N'Default', N'System.DateTime', N'datetime', N'Date', 0, 0, 0, 0, 0, 1, 1, NULL, 7, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
  VALUES (695, 68, N'IsListed', NULL, NULL, N'System.Boolean', N'bit', N'boolean', 0, 0, 0, 0, 0, 0, 0, NULL, 8, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
  VALUES (696, 68, N'Id', NULL, NULL, N'System.Int32', N'int', N'number', 0, 0, 0, 0, 0, 0, 0, N'AlwaysHidden', -1, NULL)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
  VALUES (703, 68, N'ProvinceName', NULL, NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 0, 1, 1, NULL, 9)
INSERT [Metadata].[Column] ([ColumnID], [ViewID], [Name], [GroupName], [Type], [DotNetType], [StorageType], [ScriptType], [Length], [MinLength], [IsDynamic], [IsFixedLength], [IsNullable], [AllowSorting], [AllowFiltering], [Visibility], [DisplayIndex], [Expression])
  VALUES (704, 68, N'CityName', NULL, NULL, N'System.String', N'nvarchar', N'string', 64, 0, 0, 0, 0, 1, 1, NULL, 10)
SET IDENTITY_INSERT [Metadata].[Column] OFF
