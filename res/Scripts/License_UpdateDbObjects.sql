﻿-- 1.2.1313
ALTER TABLE [dbo].[License]
ADD [OfflineLimit] INT NOT NULL
CONSTRAINT DF_License_OfflineLimit DEFAULT 0
WITH VALUES;
GO
