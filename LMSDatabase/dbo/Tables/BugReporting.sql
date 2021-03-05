CREATE TABLE [dbo].[BugReporting]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[Description] nvarchar(max),
	[Type] int,
	[AssignedDeveloperId] int,
	[CreatedBy] nvarchar(max),
	[CreatedAt] DateTime,
	[UpdatedBy] nvarchar(max),
	[UpdatedAt] DateTime,
	[Date] Datetime,
	Constraint [FK_Bug_Agent] foreign key ([AssignedDeveloperId]) References [dbo].[Agent] ([Id]),
)
