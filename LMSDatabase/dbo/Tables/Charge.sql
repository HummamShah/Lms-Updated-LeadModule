CREATE TABLE [dbo].[Charge]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1),
	[Value] decimal(18,3) not null default(0),
	[Amount] decimal(18,3) not null default(0),
	[Type] int not null default(0),
	[ValueType] int not null default(0),
	[Name] nvarchar(max),
	[CreatedBy] nvarchar(max),
	[CreatedAt] DateTime,
	[UpdatedBy] nvarchar(max),
	[UpdatedAt] DateTime,
	[Date] Datetime,
)
