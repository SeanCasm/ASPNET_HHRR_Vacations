USE [Enterprise]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 21-02-2024 11:43:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequestStatus]    Script Date: 21-02-2024 11:43:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestStatus](
	[RequestID] [int] IDENTITY(1,1) NOT NULL,
	[RequestType] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[RequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserCredentials]    Script Date: 21-02-2024 11:43:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCredentials](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Email] [varchar](40) NOT NULL,
	[PasswordHash] [varchar](max) NOT NULL,
	[IsAdmin] [bit] NOT NULL,
	[CreationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vacation]    Script Date: 21-02-2024 11:43:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vacation](
	[VacationID] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[VacationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VacationTicket]    Script Date: 21-02-2024 11:43:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VacationTicket](
	[TicketID] [int] IDENTITY(1,1) NOT NULL,
	[VacationID] [int] NOT NULL,
	[RequestID] [int] NOT NULL,
	[EmployeeID] [int] NOT NULL,
	[Issued] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TicketID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([EmployeeID], [FirstName], [LastName]) VALUES (1031, N'Sebastian', N'Cataldo')
INSERT [dbo].[Employee] ([EmployeeID], [FirstName], [LastName]) VALUES (1032, N'Gustavo', N'Rivera')
INSERT [dbo].[Employee] ([EmployeeID], [FirstName], [LastName]) VALUES (2005, N'Emilia', N'Latorre')
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
SET IDENTITY_INSERT [dbo].[RequestStatus] ON 

INSERT [dbo].[RequestStatus] ([RequestID], [RequestType]) VALUES (1, N'Sent')
INSERT [dbo].[RequestStatus] ([RequestID], [RequestType]) VALUES (2, N'Approved')
INSERT [dbo].[RequestStatus] ([RequestID], [RequestType]) VALUES (3, N'Declined')
SET IDENTITY_INSERT [dbo].[RequestStatus] OFF
GO
SET IDENTITY_INSERT [dbo].[UserCredentials] ON 

INSERT [dbo].[UserCredentials] ([UserID], [EmployeeID], [Email], [PasswordHash], [IsAdmin], [CreationDate]) VALUES (2032, 1031, N'sebastian.cataldo@enterprise.com', N'AQAAAAIAAYagAAAAEAdAA4Ek+4BJ1he4e94G7O8ZOgQJWZA3PELiPaYWifJOFs6C62ZjqbUcEt13oT2/pw==', 1, CAST(N'2024-02-16T19:42:02.717' AS DateTime))
INSERT [dbo].[UserCredentials] ([UserID], [EmployeeID], [Email], [PasswordHash], [IsAdmin], [CreationDate]) VALUES (2033, 1032, N'gustavo.rivera@enterprise.com', N'AQAAAAIAAYagAAAAEP4tU6wmyrj6vGmTCsjM/9gUztQsqkaXGE79vYA663Ou5LrMNOb3684aJEkO2lCc0A==', 0, CAST(N'2024-02-16T20:34:16.187' AS DateTime))
INSERT [dbo].[UserCredentials] ([UserID], [EmployeeID], [Email], [PasswordHash], [IsAdmin], [CreationDate]) VALUES (3004, 2005, N'emilia.latorre@enterprise.com', N'AQAAAAIAAYagAAAAEEhZBayuT0BAcdTIm2boJNMh/Z972KR1jy2pK2fqKfY3p8tIlKElUVpfKp+PmMiGJg==', 0, CAST(N'2024-02-20T00:07:54.640' AS DateTime))
SET IDENTITY_INSERT [dbo].[UserCredentials] OFF
GO
/****** Object:  Index [UQ__UserCred__7AD04FF07711573F]    Script Date: 21-02-2024 11:43:12 ******/
ALTER TABLE [dbo].[UserCredentials] ADD UNIQUE NONCLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[UserCredentials] ADD  DEFAULT (getdate()) FOR [CreationDate]
GO
ALTER TABLE [dbo].[VacationTicket] ADD  DEFAULT (getdate()) FOR [Issued]
GO
ALTER TABLE [dbo].[UserCredentials]  WITH CHECK ADD FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([EmployeeID])
GO
ALTER TABLE [dbo].[UserCredentials]  WITH CHECK ADD  CONSTRAINT [FK_UserCredentials_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([EmployeeID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserCredentials] CHECK CONSTRAINT [FK_UserCredentials_Employee]
GO
ALTER TABLE [dbo].[Vacation]  WITH CHECK ADD FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([EmployeeID])
GO
ALTER TABLE [dbo].[Vacation]  WITH CHECK ADD  CONSTRAINT [FK_Vacation_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([EmployeeID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Vacation] CHECK CONSTRAINT [FK_Vacation_Employee]
GO
ALTER TABLE [dbo].[VacationTicket]  WITH CHECK ADD FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([EmployeeID])
GO
ALTER TABLE [dbo].[VacationTicket]  WITH CHECK ADD FOREIGN KEY([RequestID])
REFERENCES [dbo].[RequestStatus] ([RequestID])
GO
ALTER TABLE [dbo].[VacationTicket]  WITH CHECK ADD FOREIGN KEY([VacationID])
REFERENCES [dbo].[Vacation] ([VacationID])
GO
ALTER TABLE [dbo].[VacationTicket]  WITH CHECK ADD  CONSTRAINT [FK_VacationTicket_Employee] FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employee] ([EmployeeID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[VacationTicket] CHECK CONSTRAINT [FK_VacationTicket_Employee]
GO
ALTER TABLE [dbo].[Vacation]  WITH CHECK ADD CHECK  (([StartDate]<=[EndDate]))
GO
