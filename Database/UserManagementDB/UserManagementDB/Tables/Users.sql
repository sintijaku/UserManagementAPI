CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1),
	[Name] nvarchar(50) not null,
	[Surname] nvarchar(50) not null
)
