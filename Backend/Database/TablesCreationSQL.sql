CREATE SCHEMA [dbo]
GO

CREATE TABLE [dbo].[users] (
  [id] UNIQUEIDENTIFIER NOT NULL,
  [name] NCHAR(10) NOT NULL,
  [surname] NCHAR(10) NOT NULL,
  [username] NVARCHAR(50),
  [password] NVARCHAR(50) NOT NULL,
  [admin?] BIT NOT NULL DEFAULT (0),
  PRIMARY KEY ([id])
)
GO

CREATE TABLE [dbo].[logs] (
  [id] BIGINT NOT NULL IDENTITY(1, 1),
  [authcode] NVARCHAR(10) NOT NULL,
  [unlockcode] NVARCHAR(10) ,
  [openend?] BIT DEFAULT (0),
  [user_id] UNIQUEIDENTIFIER,
  [pic_id] UNIQUEIDENTIFIER,
  PRIMARY KEY ([id])
)
GO

CREATE TABLE [dbo].[pics] (
  [id] UNIQUEIDENTIFIER NOT NULL,
  [portnumber] INT NOT NULL,
  [buildingnumber] INT NOT NULL,
  PRIMARY KEY ([id])
)
GO

CREATE TABLE [dbo].[raspberries] (
  [id] UNIQUEIDENTIFIER NOT NULL,
  [buildingnumber] INT NOT NULL,
  PRIMARY KEY ([buildingnumber])
)
GO

CREATE TABLE [dbo].[groups_users] (
  [id] UNIQUEIDENTIFIER NOT NULL,
  [user_id] UNIQUEIDENTIFIER NOT NULL,
  [group_id] INT NOT NULL,
  PRIMARY KEY ([id])
)
GO

CREATE TABLE [dbo].[groups] (
  [id] INT NOT NULL IDENTITY(1, 1),
  [name] NCHAR(10) NOT NULL,
  PRIMARY KEY ([id])
)
GO

CREATE TABLE [dbo].[groups_pics] (
  [id] UNIQUEIDENTIFIER NOT NULL,
  [pic_id] UNIQUEIDENTIFIER NOT NULL,
  [group_id] INT NOT NULL,
  PRIMARY KEY ([id])
)
GO

ALTER TABLE [dbo].[groups_users] ADD FOREIGN KEY ([user_id]) REFERENCES [dbo].[users] ([id])
GO

ALTER TABLE [dbo].[groups_users] ADD FOREIGN KEY ([group_id]) REFERENCES [dbo].[groups] ([id])
GO

ALTER TABLE [dbo].[groups_pics] ADD FOREIGN KEY ([group_id]) REFERENCES [dbo].[groups] ([id])
GO

ALTER TABLE [dbo].[groups_pics] ADD FOREIGN KEY ([pic_id]) REFERENCES [dbo].[pics] ([id])
GO

ALTER TABLE [dbo].[logs] ADD FOREIGN KEY ([user_id]) REFERENCES [dbo].[users] ([id])
GO

ALTER TABLE [dbo].[logs] ADD FOREIGN KEY ([pic_id]) REFERENCES [dbo].[pics] ([id])
GO

ALTER TABLE [dbo].[pics] ADD FOREIGN KEY ([buildingnumber]) REFERENCES [dbo].[raspberries] ([buildingnumber])
GO
