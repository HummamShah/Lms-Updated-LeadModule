CREATE TABLE [dbo].[PmdDetails]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[LeadId] int,
	[VendorId] int,
	[OTC] decimal(18,3),
	[MRC] decimal(18,3),
	[ConnectivityType] int not null default(0),
	[Bandwidth] nvarchar(max),
	[Remarks] nvarchar(max),
    [CreatedBy] nvarchar(max),
	[CreatedAt] DateTime,
	[UpdatedBy] nvarchar(max),
	[UpdatedAt] DateTime,
		[Date] Datetime,
	Constraint [FK_PMD_Leads] foreign key ([LeadId]) References [dbo].[Lead] ([Id]),
	Constraint [FK_PMD_Vendor] foreign key ([VendorId]) References [dbo].[Vendor] ([Id])
)
