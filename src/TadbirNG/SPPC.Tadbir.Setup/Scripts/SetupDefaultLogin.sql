USE master;
GO

IF(NOT EXISTS(
  SELECT [principal_id]
  FROM [sys].[server_principals]
  WHERE [name] = '@LoginName'))
BEGIN
  CREATE LOGIN [@LoginName]
  WITH PASSWORD = '@Password',
  DEFAULT_DATABASE = master,
  CHECK_POLICY = OFF,
  CHECK_EXPIRATION = OFF;
END
GO

ALTER SERVER ROLE securityadmin ADD MEMBER [@LoginName];
GO

ALTER SERVER ROLE dbcreator ADD MEMBER [@LoginName];
GO

ALTER SERVER ROLE sysadmin ADD MEMBER [@LoginName];
GO
