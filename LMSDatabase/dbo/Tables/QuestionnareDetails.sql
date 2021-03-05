CREATE TABLE [dbo].[QuestionnareDetails]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[LeadId] int not null,
	[Requirements] nvarchar(max) not null,
	[HeadOffice] nvarchar(max),
	[BranchOffice] nvarchar(max),
	[Details] nvarchar(max),
	[Type] int not null default(0),
	Constraint [FK_Questionnare_Lead] foreign key ([LeadId]) References [dbo].[Lead] ([Id])
)
