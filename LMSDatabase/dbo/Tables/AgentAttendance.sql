CREATE TABLE [dbo].[AgentAttendance]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[AgentId] int not null,
	[IsPresent] bit not null default(0),
	[IsLate] bit not null default(0),
	[StartDateTime] datetime,
	[EndDate] datetime,
	[StartDate] datetime,
	[EndDateTime] datetime,
	[CreatedBy] nvarchar(max),
	[CreatedAt] DateTime,
	[UpdatedBy] nvarchar(max),
	[UpdatedAt] DateTime,
	[Date] Datetime,
	Constraint [FK_AgentAttendace_Agent] foreign key (AgentId) References [dbo].Agent ([Id]),
)
