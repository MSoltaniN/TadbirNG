
-- 1.1.1045
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (54, N'Export')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (55, N'ExportRates')
INSERT INTO [Metadata].[Operation] ([OperationID],[Name]) VALUES (56, N'FilterRates')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

DELETE FROM [Config].[LogSetting]
GO

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (1, 1, 1, NULL, 1, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (2, 1, 1, NULL, 1, 2, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (3, 1, 1, NULL, 1, 3, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (4, 1, 1, NULL, 1, 4, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (5, 1, 1, NULL, 1, 5, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (6, 1, 1, NULL, 1, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (7, 1, 1, NULL, 1, 21, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (8, 1, 1, NULL, 1, 54, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (9, 1, 3, 2, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (10, 1, 3, 2, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (11, 1, 3, 2, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (12, 1, 3, 2, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (13, 1, 1, NULL, 2, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (14, 1, 1, NULL, 2, 7, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (15, 1, 1, NULL, 4, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (16, 1, 1, NULL, 4, 2, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (17, 1, 1, NULL, 4, 3, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (18, 1, 1, NULL, 4, 4, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (19, 1, 1, NULL, 4, 5, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (20, 1, 1, NULL, 4, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (21, 1, 1, NULL, 4, 21, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (22, 1, 1, NULL, 4, 54, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (23, 1, 1, NULL, 3, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (24, 1, 1, NULL, 3, 7, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (25, 1, 3, 6, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (26, 1, 3, 6, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (27, 1, 3, 6, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (28, 1, 3, 6, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (29, 1, 1, NULL, 5, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (30, 1, 1, NULL, 5, 2, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (31, 1, 1, NULL, 5, 3, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (32, 1, 1, NULL, 5, 4, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (33, 1, 1, NULL, 5, 5, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (34, 1, 1, NULL, 5, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (35, 1, 1, NULL, 5, 21, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (36, 1, 1, NULL, 5, 35, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (37, 1, 1, NULL, 5, 54, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (38, 1, 1, NULL, 6, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (39, 1, 1, NULL, 6, 2, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (40, 1, 1, NULL, 6, 3, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (41, 1, 1, NULL, 6, 4, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (42, 1, 1, NULL, 6, 5, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (43, 1, 1, NULL, 6, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (44, 1, 1, NULL, 6, 21, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (45, 1, 1, NULL, 6, 54, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (46, 1, 1, NULL, 7, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (47, 1, 1, NULL, 7, 2, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (48, 1, 1, NULL, 7, 3, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (49, 1, 1, NULL, 7, 4, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (50, 1, 1, NULL, 7, 5, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (51, 1, 1, NULL, 7, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (52, 1, 1, NULL, 7, 21, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (53, 1, 1, NULL, 7, 40, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (54, 1, 1, NULL, 7, 41, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (55, 1, 1, NULL, 7, 42, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (56, 1, 1, NULL, 7, 43, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (57, 1, 1, NULL, 7, 44, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (58, 1, 1, NULL, 7, 45, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (59, 1, 1, NULL, 7, 54, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (60, 1, 1, NULL, 7, 55, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (61, 1, 1, NULL, 7, 56, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (62, 1, 3, 3, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (63, 1, 3, 3, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (64, 1, 3, 3, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (65, 1, 3, 3, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (66, 1, 1, NULL, 9, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (67, 1, 1, NULL, 9, 2, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (68, 1, 1, NULL, 9, 3, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (69, 1, 1, NULL, 9, 4, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (70, 1, 1, NULL, 9, 5, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (71, 1, 1, NULL, 9, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (72, 1, 1, NULL, 9, 21, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (73, 1, 1, NULL, 9, 54, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (74, 1, 2, NULL, 18, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (75, 1, 2, NULL, 18, 2, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (76, 1, 2, NULL, 18, 3, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (77, 1, 2, NULL, 18, 4, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (78, 1, 2, NULL, 18, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (79, 1, 2, NULL, 18, 11, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (80, 1, 2, NULL, 18, 12, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (81, 1, 2, NULL, 18, 21, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (82, 1, 2, NULL, 18, 36, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (83, 1, 2, NULL, 18, 37, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (84, 1, 2, NULL, 18, 38, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (85, 1, 2, NULL, 18, 39, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (86, 1, 2, NULL, 18, 46, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (87, 1, 2, NULL, 18, 47, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (88, 1, 2, NULL, 18, 52, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (89, 1, 2, NULL, 18, 53, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (90, 1, 1, 9, NULL, 31, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (91, 1, 1, 9, NULL, 32, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (92, 1, 1, 9, NULL, 33, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (93, 1, 1, 9, NULL, 34, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (94, 1, 1, NULL, 10, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (95, 1, 1, NULL, 10, 2, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (96, 1, 1, NULL, 10, 3, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (97, 1, 1, NULL, 10, 4, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (98, 1, 1, NULL, 10, 5, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (99, 1, 1, NULL, 10, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (100, 1, 1, NULL, 10, 21, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (101, 1, 1, NULL, 10, 35, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (102, 1, 1, NULL, 10, 54, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (103, 1, 3, 5, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (104, 1, 3, 5, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (105, 1, 3, 5, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (106, 1, 3, 5, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (107, 1, 3, 1, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (108, 1, 3, 1, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (109, 1, 3, 1, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (110, 1, 3, 1, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (111, 1, 2, NULL, 11, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (112, 1, 2, NULL, 11, 4, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (113, 1, 2, NULL, 11, 5, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (114, 1, 2, NULL, 11, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (115, 1, 2, NULL, 11, 8, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (116, 1, 2, NULL, 11, 30, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (117, 1, 2, NULL, 11, 54, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (118, 1, 3, 10, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (119, 1, 3, 10, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (120, 1, 3, 10, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (121, 1, 3, 10, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (122, 1, 1, NULL, 12, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (123, 1, 1, NULL, 12, 2, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (124, 1, 1, NULL, 12, 3, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (125, 1, 1, NULL, 12, 4, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (126, 1, 1, NULL, 12, 5, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (127, 1, 1, NULL, 12, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (128, 1, 1, NULL, 12, 21, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (129, 1, 1, NULL, 12, 54, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (130, 1, 1, NULL, 15, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (131, 1, 1, NULL, 15, 7, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (132, 1, 1, NULL, 16, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (133, 1, 1, NULL, 16, 3, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (134, 1, 1, NULL, 16, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (135, 1, 3, 4, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (136, 1, 3, 4, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (137, 1, 3, 4, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (138, 1, 3, 4, NULL, 54, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (139, 1, 2, NULL, 17, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (140, 1, 2, NULL, 17, 2, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (141, 1, 2, NULL, 17, 3, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (142, 1, 2, NULL, 17, 4, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (143, 1, 2, NULL, 17, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (144, 1, 2, NULL, 17, 11, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (145, 1, 2, NULL, 17, 12, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (146, 1, 2, NULL, 17, 13, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (147, 1, 2, NULL, 17, 14, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (148, 1, 2, NULL, 17, 15, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (149, 1, 2, NULL, 17, 16, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (150, 1, 2, NULL, 17, 17, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (151, 1, 2, NULL, 17, 18, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (152, 1, 2, NULL, 17, 21, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (153, 1, 2, NULL, 17, 36, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (154, 1, 2, NULL, 17, 37, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (155, 1, 2, NULL, 17, 38, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (156, 1, 2, NULL, 17, 39, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (157, 1, 2, NULL, 17, 46, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (158, 1, 2, NULL, 17, 47, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (159, 1, 2, NULL, 17, 48, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (160, 1, 2, NULL, 17, 49, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (161, 1, 2, NULL, 17, 50, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (162, 1, 2, NULL, 17, 51, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.1.1048
SET IDENTITY_INSERT [Config].[Setting] ON
INSERT INTO [Config].[Setting] (SettingID, TitleKey, [Type], ScopeType, ModelType, [Values], DefaultValues, DescriptionKey, IsStandalone)
    VALUES (10, 'FormLabelSettings', 2, 3, 'FormLabelConfig', N'{}', N'{}', NULL, 0)
SET IDENTITY_INSERT [Config].[Setting] OFF

CREATE TABLE [Metadata].[CustomForm] (
    [CustomFormID]   INT              IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR(128)    NOT NULL,
    [Description]    NVARCHAR(512)    NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Metadata_CustomForm_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Metadata_CustomForm_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Metadata_CustomForm] PRIMARY KEY CLUSTERED ([CustomFormID] ASC)
)
GO

SET IDENTITY_INSERT [Metadata].[CustomForm] ON
INSERT INTO [Metadata].[CustomForm] ([CustomFormID], [Name]) VALUES (1, 'ProfitLoss')
SET IDENTITY_INSERT [Metadata].[CustomForm] OFF

CREATE TABLE [Config].[LabelSetting] (
    [LabelSettingID] INT              IDENTITY (1, 1) NOT NULL,
    [SettingID]      INT              NOT NULL,
    [CustomFormID]   INT              NOT NULL,
	[LocaleID]       INT              NOT NULL,
	[ModelType]      VARCHAR(128)     NOT NULL,
	[Values]         NTEXT            NOT NULL,
	[DefaultValues]  NTEXT            NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Config_LabelSetting_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Config_LabelSetting_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Config_LabelSetting] PRIMARY KEY CLUSTERED ([LabelSettingID] ASC)
    , CONSTRAINT [FK_Config_LabelSetting_Config_Setting] FOREIGN KEY ([SettingID]) REFERENCES [Config].[Setting]([SettingID])
)
GO

SET IDENTITY_INSERT [Config].[LabelSetting] ON
INSERT INTO [Config].[LabelSetting] ([LabelSettingID], [SettingID], [CustomFormID], [LocaleID], [ModelType], [Values], [DefaultValues])
    VALUES (1, 10, 1, 1, 'FormLabelConfig', N'{"formId": 1, "localeId": 1, "labelMap": {"GrossProfitCalculation": "Gross profit calculation", "NetRevenue": "Net revenue", "GrossProfit": "Gross profit", "SoldProductCost": "Sold product cost", "OperationalCost": "Operational cost", "OperationalCostTotal": "Operational cost total", "OperationalProfit": "Operational profit", "OtherCostAndRevenue": "Other cost and revenue", "OtherCostAndRevenueNet": "Other cost and revenue net", "ProfitBeforeTax": "Profit before tax", "Tax": "Tax", "NetProfit": "Net profit" }}', N'{"formId": 1, "localeId": 1, "labelMap": {"GrossProfitCalculation": "Gross profit calculation", "NetRevenue": "Net revenue", "GrossProfit": "Gross profit", "SoldProductCost": "Sold product cost", "OperationalCost": "Operational cost", "OperationalCostTotal": "Operational cost total", "OperationalProfit": "Operational profit", "OtherCostAndRevenue": "Other cost and revenue", "OtherCostAndRevenueNet": "Other cost and revenue net", "ProfitBeforeTax": "Profit before tax", "Tax": "Tax", "NetProfit": "Net profit" }}')
INSERT INTO [Config].[LabelSetting] ([LabelSettingID], [SettingID], [CustomFormID], [LocaleID], [ModelType], [Values], [DefaultValues])
    VALUES (2, 10, 1, 2, 'FormLabelConfig', N'{"formId": 1, "localeId": 2, "labelMap": {"GrossProfitCalculation": "محاسبه سود ناخالص", "NetRevenue": "درآمد خالص", "GrossProfit": "سود ناخالص", "SoldProductCost": "بهای تمام شده کالای فروش رفته", "OperationalCost": "هزینه های عملیاتی", "OperationalCostTotal": "جمع هزینه های عملیاتی", "OperationalProfit": "سود عملیاتی", "OtherCostAndRevenue": "سایر هزینه ها و درآمدها", "OtherCostAndRevenueNet": "خالص سایر هزینه ها و درآمدها", "ProfitBeforeTax": "سود قبل از کسر مالیات", "Tax": "مالیات", "NetProfit": "سود خالص" }}', N'{"formId": 1, "localeId": 2, "labelMap": {"GrossProfitCalculation": "محاسبه سود ناخالص", "NetRevenue": "درآمد خالص", "GrossProfit": "سود ناخالص", "SoldProductCost": "بهای تمام شده کالای فروش رفته", "OperationalCost": "هزینه های عملیاتی", "OperationalCostTotal": "جمع هزینه های عملیاتی", "OperationalProfit": "سود عملیاتی", "OtherCostAndRevenue": "سایر هزینه ها و درآمدها", "OtherCostAndRevenueNet": "خالص سایر هزینه ها و درآمدها", "ProfitBeforeTax": "سود قبل از کسر مالیات", "Tax": "مالیات", "NetProfit": "سود خالص" }}')
SET IDENTITY_INSERT [Config].[LabelSetting] OFF

-- 1.1.1055
DELETE FROM [Config].[LogSetting]
WHERE EntityTypeID = 3

DELETE FROM [Metadata].[EntityType]
WHERE Name = N'AccountRelations'

SET IDENTITY_INSERT [Metadata].[OperationSource] ON
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID], [Name]) VALUES (11, N'AccountRelations')
SET IDENTITY_INSERT [Metadata].[OperationSource] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (163, 1, 1, 11, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (164, 1, 1, 11, NULL, 7, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.1.1056
SET IDENTITY_INSERT [Metadata].[OperationSource] ON
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID], [Name]) VALUES (12, N'BalanceSheet')
SET IDENTITY_INSERT [Metadata].[OperationSource] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (165, 1, 3, 12, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (166, 1, 3, 12, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (167, 1, 3, 12, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (168, 1, 3, 12, NULL, 54, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

SET IDENTITY_INSERT [Metadata].[OperationSourceList] ON
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID], [Name]) VALUES (55, N'BalanceSheet')
SET IDENTITY_INSERT [Metadata].[OperationSourceList] OFF

-- 1.1.1091
UPDATE [Config].[Setting]
SET IsStandalone = 0
WHERE TitleKey = 'QuickSearchSettings'

-- 1.1.1128
UPDATE [Config].[Setting]
SET TitleKey = 'FinanceReportSettings', ModelType = 'FinanceReportConfig', DescriptionKey = 'FinanceReportSettingsDescription'
WHERE ModelType = 'TestBalanceConfig'

-- 1.1.1133
UPDATE [Config].[Setting]
SET [Values] = '{"openingAsFirstVoucher":false}', DefaultValues = '{"openingAsFirstVoucher":false}'
WHERE ModelType = 'FinanceReportConfig'

-- 1.1.1137
SET IDENTITY_INSERT [Metadata].[OperationSource] ON
INSERT INTO [Metadata].[OperationSource] ([OperationSourceID], [Name]) VALUES (13, N'SystemIssue')
SET IDENTITY_INSERT [Metadata].[OperationSource] OFF

SET IDENTITY_INSERT [Metadata].[OperationSourceList] ON
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID], [Name]) VALUES (56, N'UnbalancedVouchers')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID], [Name]) VALUES (57, N'VouchersWithNoArticle')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID], [Name]) VALUES (58, N'ArticlesHavingZeroAmount')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID], [Name]) VALUES (59, N'ArticlesWithMissingAccount')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID], [Name]) VALUES (60, N'ArticlesWithInvalidAccountItems')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID], [Name]) VALUES (61, N'MissingVoucherNumbers')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID], [Name]) VALUES (62, N'AccountsWithInvalidBalance')
INSERT INTO [Metadata].[OperationSourceList] ([OperationSourceListID], [Name]) VALUES (63, N'AccountsWithInvalidPeriodTurnover')
SET IDENTITY_INSERT [Metadata].[OperationSourceList] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (169, 1, 3, 13, NULL, 1, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (170, 1, 3, 13, NULL, 5, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (171, 1, 3, 13, NULL, 6, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (172, 1, 3, 13, NULL, 54, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.1.1140
UPDATE [Config].[Setting]
SET [Values] = '{"openingAsFirstVoucher":false,"startTurnoverAsInitBalance":false}',
  DefaultValues = '{"openingAsFirstVoucher":false,"startTurnoverAsInitBalance":false}'
WHERE ModelType = 'FinanceReportConfig'

-- 1.1.1163
UPDATE [Config].[LogSetting]
SET SourceID = NULL, EntityTypeID = 15
WHERE OperationID IN(31,32,33,34)

-- 1.1.1166
CREATE TABLE [Finance].[InactiveAccount] (
    [InactiveAccountID]  INT              IDENTITY (1, 1) NOT NULL,
    [AccountID]          INT              NOT NULL,
    [FiscalPeriodID]     INT              NOT NULL,
    [rowguid]            UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_InactiveAccount_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]       DATETIME         CONSTRAINT [DF_Finance_InactiveAccount_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_InactiveAccount] PRIMARY KEY CLUSTERED ([InactiveAccountID] ASC)
    , CONSTRAINT [FK_Finance_InactiveAccount_Finance_Account] FOREIGN KEY ([AccountID]) REFERENCES [Finance].[Account] ([AccountID])
    , CONSTRAINT [FK_Finance_InactiveAccount_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod] ([FiscalPeriodID])
)
GO

-- 1.1.1171
CREATE TABLE [Finance].[InactiveCurrency] (
    [InactiveCurrencyID] INT              IDENTITY (1, 1) NOT NULL,
    [CurrencyID]         INT              NOT NULL,
    [FiscalPeriodID]     INT              NOT NULL,
    [rowguid]            UNIQUEIDENTIFIER CONSTRAINT [DF_Finance_InactiveCurrency_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]       DATETIME         CONSTRAINT [DF_Finance_InactiveCurrency_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Finance_InactiveCurrency] PRIMARY KEY CLUSTERED ([InactiveCurrencyID] ASC)
    , CONSTRAINT [FK_Finance_InactiveCurrency_Finance_Currency] FOREIGN KEY ([CurrencyID]) REFERENCES [Finance].[Currency] ([CurrencyID])
    , CONSTRAINT [FK_Finance_InactiveCurrency_Finance_FiscalPeriod] FOREIGN KEY ([FiscalPeriodID]) REFERENCES [Finance].[FiscalPeriod] ([FiscalPeriodID])
)
GO

-- 1.1.1174
UPDATE [Finance].[VoucherLine]
SET Mark = NULL
WHERE Mark = ''

-- 1.1.1176
SET IDENTITY_INSERT [Metadata].[Operation] ON
INSERT INTO [Metadata].[Operation] ([OperationID], [Name]) VALUES (58, N'PrintPreview')
SET IDENTITY_INSERT [Metadata].[Operation] OFF

SET IDENTITY_INSERT [Config].[LogSetting] ON
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (173, 1, 1, NULL, 1, 58, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (174, 1, 3, 2, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (175, 1, 1, NULL, 4, 58, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (176, 1, 3, 6, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (177, 1, 1, NULL, 5, 58, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (178, 1, 1, NULL, 6, 58, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (179, 1, 1, NULL, 7, 58, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (180, 1, 3, 3, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (181, 1, 1, NULL, 9, 58, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (182, 1, 2, NULL, 18, 58, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (183, 1, 1, NULL, 10, 58, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (184, 1, 3, 5, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (185, 1, 3, 1, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (186, 1, 2, NULL, 11, 58, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (187, 1, 3, 10, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (188, 1, 1, NULL, 12, 58, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (189, 1, 1, NULL, 16, 58, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (190, 1, 3, 4, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (191, 1, 2, NULL, 17, 58, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (192, 1, 3, 12, NULL, 58, 1)
INSERT INTO [Config].[LogSetting] (LogSettingID, SubsystemID, SourceTypeID, SourceID, EntityTypeID, OperationID, IsEnabled)
    VALUES (193, 1, 3, 13, NULL, 58, 1)
SET IDENTITY_INSERT [Config].[LogSetting] OFF

-- 1.1.1201
UPDATE [Config].[Setting]
SET [Values] = N'{"defaultCurrencyNameKey":"CUnit_IranianRial","defaultDecimalCount":0,"defaultCalendars": [{"language":"fa", "calendar":0}, {"language":"en", "calendar":1}],"usesDefaultCoding":true}',
DefaultValues = N'{"defaultCurrencyNameKey":"CUnit_IranianRial","defaultDecimalCount":0,"defaultCalendars": [{"language":"fa", "calendar":0}, {"language":"en", "calendar":1}],"usesDefaultCoding":true}'
WHERE ModelType = 'SystemConfig'

-- 1.1.1210
UPDATE [Config].[Setting]
SET [Values] = N'{"defaultCurrencyNameKey":"CUnit_IranianRial","defaultDecimalCount":0,"defaultCalendar":0,"defaultCalendars": [{"language":"fa", "calendar":0}, {"language":"en", "calendar":1}],"usesDefaultCoding":true}',
DefaultValues = N'{"defaultCurrencyNameKey":"CUnit_IranianRial","defaultDecimalCount":0,"defaultCalendar":0,"defaultCalendars": [{"language":"fa", "calendar":0}, {"language":"en", "calendar":1}],"usesDefaultCoding":true}'
WHERE ModelType = 'SystemConfig'

-- 1.2.1275
UPDATE [Finance].[Voucher]
SET DocumentID = NULL
GO

ALTER TABLE [Finance].[Voucher]
DROP CONSTRAINT [FK_Finance_Voucher_Finance_Document]

ALTER TABLE [Finance].[Voucher]
DROP COLUMN [DocumentID]

DROP TABLE [WorkItemHistory]

DROP TABLE [WorkItemDocument]

DROP TABLE [WorkItem]

DROP TABLE [DocumentAction]

DROP TABLE [Document]

-- 1.2.1376
UPDATE [Config].[ViewSetting]
SET [DefaultValues] = N'{"viewId":1,"maxDepth":3,"levels":[{"no":1,"name":"LevelGeneral","codeLength":3,"isEnabled":true,"isUsed":true},{"no":2,"name":"LevelAuxiliary","codeLength":3,"isEnabled":true,"isUsed":true},{"no":3,"name":"LevelDetail","codeLength":4,"isEnabled":true,"isUsed":true},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false}]}'
WHERE ViewID = 1

UPDATE [Config].[ViewSetting]
SET [DefaultValues] = N'{"viewId":6,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false}]}'
WHERE ViewID = 6

UPDATE [Config].[ViewSetting]
SET [DefaultValues] = N'{"viewId":7,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false}]}'
WHERE ViewID = 7

UPDATE [Config].[ViewSetting]
SET [DefaultValues] = N'{"viewId":8,"maxDepth":4,"levels":[{"no":1,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":2,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":3,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":4,"name":"LevelX","codeLength":4,"isEnabled":true,"isUsed":false},{"no":5,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":6,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":7,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":8,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":9,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":10,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":11,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":12,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":13,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":14,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":15,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false},{"no":16,"name":"LevelX","codeLength":4,"isEnabled":false,"isUsed":false}]}'
WHERE ViewID = 8

-- 1.2.1377
UPDATE [Config].[Setting] 
SET  [Values] =  N'{"defaultCurrencyNameKey":"CUnit_IranianRial","defaultDecimalCount":0,"defaultCalendar":0,"defaultCalendars": [{"language":"fa", "calendar":0}, {"language":"en", "calendar":1}],"usesDefaultCoding":true,"inventoryMode": 1}' ,
    [DefaultValues] =  N'{"defaultCurrencyNameKey":"CUnit_IranianRial","defaultDecimalCount":0,"defaultCalendar":0,"defaultCalendars": [{"language":"fa", "calendar":0}, {"language":"en", "calendar":1}],"usesDefaultCoding":true,"inventoryMode": 1}'
WHERE ModelType = 'SystemConfig'
