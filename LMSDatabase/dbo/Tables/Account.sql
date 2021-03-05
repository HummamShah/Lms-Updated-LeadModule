CREATE TABLE [dbo].[Account]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[CompanyId] int not null,
	[Name] nvarchar(max),
	[OpeningBalance]  decimal(18,3) not null default(0),
	[Balance] decimal(18,3) default(0),
	[TaxWitholding] decimal(18,3) default(0),
	[IsActive] bit,
	[CreatedAt] Datetime ,
	[UpdatedAt] Datetime ,
	[CreatedBy] nvarchar(max),
	[UpdatedBy] nvarchar(max),
	[Date] Datetime,
	Constraint [FK_Account_Company] foreign key ([CompanyId]) References [dbo].[Company] ([Id])
)
