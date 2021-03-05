CREATE TABLE [dbo].[BillingInformation]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[Address1] nvarchar(max),
	[Address2] nvarchar(max),
	[City] nvarchar(max),
	[State] nvarchar(max),
	[Country] nvarchar(max),
	[Zip]nvarchar(max),
	[BillingFormatType] nvarchar(max),
	[POCName] nvarchar(max),
	[POCNumber] nvarchar(max),
	[CompanyContact] nvarchar(max),
	[CompanyEmail] nvarchar(max),
	[DeliveryMode] nvarchar(max),
	[BillingStatus] int default(0),
	[IsActive] bit,
	[CreatedBy] nvarchar(max),
	[CreatedAt] DateTime,
	[UpdatedBy] nvarchar(max),
	[UpdatedAt] DateTime,
	[Date] Datetime,

)
