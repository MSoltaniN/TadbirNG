-- 1.1.600
-- Demo : Creating a new table
CREATE TABLE [Core].[Test] (
    [TestID]         INT              NOT NULL,
    [Name]           VARCHAR(16)      NOT NULL,
    [rowguid]        UNIQUEIDENTIFIER CONSTRAINT [DF_Core_Test_rowguid] DEFAULT (newid()) ROWGUIDCOL NOT NULL,
    [ModifiedDate]   DATETIME         CONSTRAINT [DF_Core_Test_ModifiedDate] DEFAULT (getdate()) NOT NULL
    , CONSTRAINT [PK_Core_Test] PRIMARY KEY CLUSTERED ([TestID] ASC)
)

-- Demo : Inserting a record in an existing table
INSERT INTO [Core].[Test] ([TestID], [Name])
VALUES(1, 'Test Name')

-- 1.1.607
-- Demo : Adding a new NOT NULL column to an existing table, with default value
ALTER TABLE [Core].[Test]
ADD MyColumn NVARCHAR(64) NOT NULL
CONSTRAINT DF_Core_Test_MyColumn DEFAULT N'Surprise!'
WITH VALUES;
GO

-- Demo : Adding a new NULL column to an existing table, with default value
ALTER TABLE [Core].[Test]
ADD MyOptionalColumn NVARCHAR(32) NULL
CONSTRAINT DF_Core_Test_MyOptionalColumn DEFAULT N'Outstanding!'
WITH VALUES;
GO

-- 1.1.612
-- Demo : Inserting several rows with Identity On/Off directives
SET IDENTITY_INSERT [Auth].[User] ON
INSERT INTO [Auth].[User] ([UserID], [UserName], [PasswordHash], [IsEnabled]) VALUES(8, N'babak', '70db1869eb931d8978843e6c2d95a5ea3a038fce782b939479374126040cb6f5', 1)
INSERT INTO [Auth].[User] ([UserID], [UserName], [PasswordHash], [IsEnabled]) VALUES(9, N'albert', '70db1869eb931d8978843e6c2d95a5ea3a038fce782b939479374126040cb6f5', 1)
INSERT INTO [Auth].[User] ([UserID], [UserName], [PasswordHash], [IsEnabled]) VALUES(10, N'teymour', '70db1869eb931d8978843e6c2d95a5ea3a038fce782b939479374126040cb6f5', 1)
SET IDENTITY_INSERT [Auth].[User] OFF

SET IDENTITY_INSERT [Contact].[Person] ON
INSERT INTO [Contact].[Person] ([PersonID], [UserID], [FirstName], [LastName]) VALUES(8, 8, N'Babak', N'Eslamieh')
INSERT INTO [Contact].[Person] ([PersonID], [UserID], [FirstName], [LastName]) VALUES(9, 9, N'Edward', N'Scissorhands')
INSERT INTO [Contact].[Person] ([PersonID], [UserID], [FirstName], [LastName]) VALUES(10, 10, N'Teymour', N'Lang')
SET IDENTITY_INSERT [Contact].[Person] OFF

-- Demo : Updating existing data
UPDATE [Auth].[Permission]
SET Name = N'Check'
WHERE GroupID = 7 AND Flag = 256

UPDATE [Auth].[Permission]
SET Name = N'UndoCheck'
WHERE GroupID = 7 AND Flag = 512

-- 1.1.618
-- Demo : Deleting expired data
DELETE FROM [Contact].[Person]
WHERE PersonID >= 8

DELETE FROM [Auth].[User]
WHERE UserID >= 8

-- Demo : Deleting existing columns
ALTER TABLE [Core].[Test]
DROP DF_Core_Test_MyColumn
GO

ALTER TABLE [Core].[Test]
DROP COLUMN [MyColumn];
GO

ALTER TABLE [Core].[Test]
DROP DF_Core_Test_MyOptionalColumn
GO

ALTER TABLE [Core].[Test]
DROP COLUMN [MyOptionalColumn];
GO

-- 1.1.622
-- Demo : Deleting an expired table
DROP TABLE [Core].[Test]
GO

