CREATE TABLE [dbo].[Employee]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[Name] nvarchar (max) ,
	[CompanyId] int,
	[Contact1] nvarchar(max),
	[Contact2] nvarchar(max),
	[Email] nvarchar(max),
	[Description] nvarchar(max),
	[CreatedAt] Datetime ,
	[UpdatedAt] Datetime ,
	[CreatedBy] nvarchar(max),
	[UpdatedBy] nvarchar(max),
    Constraint [FK_Emp_Comp] foreign key ([CompanyId]) References [dbo].[Company] ([Id])
)
