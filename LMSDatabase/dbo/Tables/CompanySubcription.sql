CREATE TABLE [dbo].[CompanySubcription]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[CompanyId] int not null,
	[OTC] decimal(18,3),
	[MRC] decimal(18,3),
	[DateFrom] Datetime,
	[DateTo] Datetime,
	[Description] nvarchar(max),
	[Name] nvarchar(max),
	[CreatedBy] nvarchar(max),
	[CreatedAt] DateTime,
	[UpdatedBy] nvarchar(max),
	[UpdatedAt] DateTime,
	[Date] Datetime,
	Constraint [FK_CompanySubcription_Company] foreign key ([CompanyId]) References [dbo].[Company] ([Id]),
)
