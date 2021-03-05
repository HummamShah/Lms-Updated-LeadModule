CREATE TABLE [dbo].[TicketComments]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1),
	[TicketId] int not null,
	[Comment] nvarchar(max),
	[CreatedAt] Datetime ,
	[UpdatedAt] Datetime ,
	[CreatedBy] nvarchar(max),
	[UpdatedBy] nvarchar(max),
	[Date] Datetime,
	Constraint [FK_TicketDetails_Ticket] foreign key ([TicketId]) References [dbo].[Ticket] ([Id])
)
