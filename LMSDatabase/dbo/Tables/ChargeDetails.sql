CREATE TABLE [dbo].[ChargeDetails]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(18,3),
	[ChargeId] int not null,
	[InvoiceId] int not null,
	[Name] nvarchar(max),
	[Value] decimal(18,3)default(0),
	[Amount] decimal(18,3)default(0),
	[ValueType] int,
	[Type] int,
	[CreatedBy] nvarchar(max),
	[CreatedAt] DateTime,
	[UpdatedBy] nvarchar(max),
	[UpdatedAt] DateTime,
	[Date] Datetime,
	Constraint [FK_ChargeDetails_Charge] foreign key ([ChargeId]) References [dbo].[Charge] ([Id]),
	Constraint [FK_ChargeDetails_Invoice] foreign key ([InvoiceId]) References [dbo].[Invoice] ([Id]),
)
