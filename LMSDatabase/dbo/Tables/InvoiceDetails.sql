CREATE TABLE [dbo].[InvoiceDetails]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[InvoiceId] int not null,
	[Description] nvarchar(max),
	[Amount] decimal(18,3)default(0),
	[Discount] decimal(18,3)default(0),
	[CreatedBy] nvarchar(max),
	[CreatedAt] DateTime,
	[UpdatedBy] nvarchar(max),
	[UpdatedAt] DateTime,
	[Date] Datetime,
	Constraint [FK_InvoiceDetails_Invoice] foreign key ([InvoiceId]) References [dbo].[Invoice] ([Id]),
)
