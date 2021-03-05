CREATE TABLE [dbo].[AccountEntries]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[AccountId] int not null,
	[Debit] decimal(18,3)default(0),
	[Credit] decimal(18,3)default(0),
	[Description] nvarchar(max),
	[CreatedAt] Datetime ,
	[UpdatedAt] Datetime ,
	[CreatedBy] nvarchar(max),
	[UpdatedBy] nvarchar(max),
	[Date] Datetime,
	Constraint [FK_AccountEntries_Account] foreign key ([AccountId]) References [dbo].[Account] ([Id])
)
