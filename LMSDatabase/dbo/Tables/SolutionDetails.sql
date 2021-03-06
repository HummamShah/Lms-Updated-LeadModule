CREATE TABLE [dbo].[SolutionDetails]
(
	[Id] INT NOT NULL PRIMARY KEY identity(1,1),
	[LeadId] int not null,
	[Date] Datetime,
	[SolutionType] int not null,
	[SolutionSubType] int ,
	[SolutionServiceProvider] nvarchar(max),
	[SolutionServiceProduct] nvarchar(max),
	[IsNew] bit,
	[CurrentServiceInfo] nvarchar(max),
	[Duration] int,
	[OtherMeasurements] nvarchar(max),
	[Quantity] int,
	[Remarks] nvarchar(max),
	[CreatedBy] nvarchar(max),
	[CreatedAt] DateTime,
	[UpdatedBy] nvarchar(max),
	[UpdatedAt] DateTime,
	Constraint [FK_SolutionDetails_Leads] foreign key ([LeadId]) References [dbo].[Lead] ([Id]),
)
