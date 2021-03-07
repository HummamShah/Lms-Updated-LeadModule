CREATE TABLE [dbo].[BillingDetails]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[BillingInformationId] int not null,
	[BillingName] nvarchar(max),
	[SalesAgentId] int,
	[IsPaperBillRequired] bit,
	[BirthDate] Datetime,
	[InstallationDate] Datetime,
	[OTC] decimal(18,3) not null default(0),
	[MRC] decimal(18,3) not null default(0),
	[Package] nvarchar(max),
	[Medium] nvarchar(max),
	[Description] nvarchar(max),
	
	[BillingStatus] int default(0),
	[IsDirectBilling] bit not null default(0),
	[CreatedBy] nvarchar(max),
	[CreatedAt] DateTime,
	[UpdatedBy] nvarchar(max),
	[UpdatedAt] DateTime,
	[Date] Datetime,
	Constraint [FK_BillingDetails_BillingInfromation] foreign key ([BillingInformationId]) References [dbo].[BillingInformation] ([Id]),
)
