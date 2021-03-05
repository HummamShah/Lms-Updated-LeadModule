CREATE TABLE [dbo].[CompanyBranches]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[CompanyId] int,
	[BranchName] nvarchar(max),
	[BranchCode] nvarchar(max),
	[Latitude] float,
	[Longitude] float,
	[Address] nvarchar(max),
	Constraint [FK_Agent_Company] foreign key ([CompanyId]) References [dbo].[Company] ([Id]),
)
