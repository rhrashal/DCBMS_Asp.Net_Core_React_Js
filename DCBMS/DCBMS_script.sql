CREATE DATABASE [DCBMS]
GO

USE [DCBMS]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 7/16/2021 8:29:36 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 7/16/2021 8:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 7/16/2021 8:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 7/16/2021 8:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 7/16/2021 8:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 7/16/2021 8:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 7/16/2021 8:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 7/16/2021 8:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Patients]    Script Date: 7/16/2021 8:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patients](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientName] [nvarchar](max) NOT NULL,
	[DateOfBirth] [datetime2](7) NOT NULL,
	[Mobile] [nvarchar](max) NULL,
	[BillNo] [nvarchar](max) NULL,
	[TestDate] [datetime2](7) NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[Status] [nvarchar](max) NULL,
	[IsPaid] [bit] NOT NULL,
	[IsComplete] [bit] NOT NULL,
	[IsDelivered] [bit] NOT NULL,
	[DueDate] [datetime2](7) NOT NULL,
	[PaymentDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Patients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestRequests]    Script Date: 7/16/2021 8:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestRequests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PatientId] [int] NOT NULL,
	[TestId] [int] NOT NULL,
	[PayableAmount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_TestRequests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tests]    Script Date: 7/16/2021 8:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tests](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TestName] [nvarchar](max) NOT NULL,
	[TestTypeId] [int] NOT NULL,
	[Fee] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Tests] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestTypes]    Script Date: 7/16/2021 8:29:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TestTypeName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_TestTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210712161319_Inital', N'5.0.7')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210712201059_testFieldAdd', N'5.0.7')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210713061214_patient_RequestTableAdd', N'5.0.7')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210713062217_RequestTableAdd', N'5.0.7')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210713185324_filedAdd', N'5.0.7')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'8ace30a6-fedc-4af2-83af-3300131a3848', N'Admin', N'ADMIN', N'4df4515f-1bc8-4584-aa20-c775c08af710')
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'c5e3c534-78fa-404a-a090-f69fb1cfee86', N'8ace30a6-fedc-4af2-83af-3300131a3848')
GO
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'c5e3c534-78fa-404a-a090-f69fb1cfee86', NULL, NULL, N'admin', N'ADMIN', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEFBmx1hVBqnq1khloEBkNpoOZWFUmfou57Q+kThGiz4NWHaMZKtKqBJsSbXuxqC9Ig==', N'6D4YMEHCRKUQ5UJS6E3PEFD54DRS6VFO', N'1f068be2-98b6-43e7-b316-7a82baf36a0c', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[Patients] ON 

INSERT [dbo].[Patients] ([Id], [PatientName], [DateOfBirth], [Mobile], [BillNo], [TestDate], [TotalAmount], [Status], [IsPaid], [IsComplete], [IsDelivered], [DueDate], [PaymentDate]) VALUES (15, N'Md Yeasin', CAST(N'2020-05-15T18:00:00.0000000' AS DateTime2), N'01836645380', N'84017-16721', CAST(N'2021-07-16T17:40:08.2171338' AS DateTime2), CAST(650.00 AS Decimal(18, 2)), N'Paid', 1, 1, 0, CAST(N'2021-07-16T17:40:08.2172029' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Patients] ([Id], [PatientName], [DateOfBirth], [Mobile], [BillNo], [TestDate], [TotalAmount], [Status], [IsPaid], [IsComplete], [IsDelivered], [DueDate], [PaymentDate]) VALUES (16, N'Md Yeasin1', CAST(N'2020-05-15T18:00:00.0000000' AS DateTime2), N'01836645380', N'214217-16721', CAST(N'2021-07-16T17:42:21.4136648' AS DateTime2), CAST(900.00 AS Decimal(18, 2)), N'Paid', 1, 1, 0, CAST(N'2021-07-16T17:42:21.4136686' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Patients] ([Id], [PatientName], [DateOfBirth], [Mobile], [BillNo], [TestDate], [TotalAmount], [Status], [IsPaid], [IsComplete], [IsDelivered], [DueDate], [PaymentDate]) VALUES (17, N'Iqbal', CAST(N'2020-01-08T18:00:00.0000000' AS DateTime2), N'01622013167', N'474617-16721', CAST(N'2021-07-16T17:46:47.2818666' AS DateTime2), CAST(700.00 AS Decimal(18, 2)), N'Paid', 1, 1, 0, CAST(N'2021-07-16T17:46:47.2818683' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Patients] ([Id], [PatientName], [DateOfBirth], [Mobile], [BillNo], [TestDate], [TotalAmount], [Status], [IsPaid], [IsComplete], [IsDelivered], [DueDate], [PaymentDate]) VALUES (18, N'Atik', CAST(N'2021-07-20T18:00:00.0000000' AS DateTime2), N'01686535458', N'30618-16721', CAST(N'2021-07-16T18:06:30.4715081' AS DateTime2), CAST(850.00 AS Decimal(18, 2)), N'Unpaid', 0, 0, 0, CAST(N'2021-07-16T18:06:30.4715100' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[Patients] ([Id], [PatientName], [DateOfBirth], [Mobile], [BillNo], [TestDate], [TotalAmount], [Status], [IsPaid], [IsComplete], [IsDelivered], [DueDate], [PaymentDate]) VALUES (19, N'MD Pabel', CAST(N'2020-12-15T18:00:00.0000000' AS DateTime2), N'01836645380', N'302220-16721', CAST(N'2021-07-16T20:22:30.4327805' AS DateTime2), CAST(1050.00 AS Decimal(18, 2)), N'Paid', 1, 1, 0, CAST(N'2021-07-16T20:22:30.4328481' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[Patients] OFF
GO
SET IDENTITY_INSERT [dbo].[TestRequests] ON 

INSERT [dbo].[TestRequests] ([Id], [PatientId], [TestId], [PayableAmount]) VALUES (23, 15, 18, CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[TestRequests] ([Id], [PatientId], [TestId], [PayableAmount]) VALUES (25, 16, 18, CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[TestRequests] ([Id], [PatientId], [TestId], [PayableAmount]) VALUES (26, 16, 16, CAST(250.00 AS Decimal(18, 2)))
INSERT [dbo].[TestRequests] ([Id], [PatientId], [TestId], [PayableAmount]) VALUES (27, 17, 17, CAST(200.00 AS Decimal(18, 2)))
INSERT [dbo].[TestRequests] ([Id], [PatientId], [TestId], [PayableAmount]) VALUES (28, 17, 18, CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[TestRequests] ([Id], [PatientId], [TestId], [PayableAmount]) VALUES (29, 18, 17, CAST(200.00 AS Decimal(18, 2)))
INSERT [dbo].[TestRequests] ([Id], [PatientId], [TestId], [PayableAmount]) VALUES (30, 18, 18, CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[TestRequests] ([Id], [PatientId], [TestId], [PayableAmount]) VALUES (31, 18, 19, CAST(150.00 AS Decimal(18, 2)))
INSERT [dbo].[TestRequests] ([Id], [PatientId], [TestId], [PayableAmount]) VALUES (32, 19, 20, CAST(300.00 AS Decimal(18, 2)))
INSERT [dbo].[TestRequests] ([Id], [PatientId], [TestId], [PayableAmount]) VALUES (33, 19, 18, CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[TestRequests] ([Id], [PatientId], [TestId], [PayableAmount]) VALUES (34, 19, 16, CAST(250.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[TestRequests] OFF
GO
SET IDENTITY_INSERT [dbo].[Tests] ON 

INSERT [dbo].[Tests] ([Id], [TestName], [TestTypeId], [Fee]) VALUES (16, N'pregnancy profile', 21, CAST(250.00 AS Decimal(18, 2)))
INSERT [dbo].[Tests] ([Id], [TestName], [TestTypeId], [Fee]) VALUES (17, N'Urine C/S', 22, CAST(200.00 AS Decimal(18, 2)))
INSERT [dbo].[Tests] ([Id], [TestName], [TestTypeId], [Fee]) VALUES (18, N' X-Ray LS Spine', 24, CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Tests] ([Id], [TestName], [TestTypeId], [Fee]) VALUES (19, N'abdomen', 21, CAST(150.00 AS Decimal(18, 2)))
INSERT [dbo].[Tests] ([Id], [TestName], [TestTypeId], [Fee]) VALUES (20, N'Blood suger', 21, CAST(300.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Tests] OFF
GO
SET IDENTITY_INSERT [dbo].[TestTypes] ON 

INSERT [dbo].[TestTypes] ([Id], [TestTypeName]) VALUES (21, N'Blood')
INSERT [dbo].[TestTypes] ([Id], [TestTypeName]) VALUES (22, N'Urine')
INSERT [dbo].[TestTypes] ([Id], [TestTypeName]) VALUES (23, N'ECG')
INSERT [dbo].[TestTypes] ([Id], [TestTypeName]) VALUES (24, N'X-Ray')
INSERT [dbo].[TestTypes] ([Id], [TestTypeName]) VALUES (25, N'New Test ')
SET IDENTITY_INSERT [dbo].[TestTypes] OFF
GO
ALTER TABLE [dbo].[Patients] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [DueDate]
GO
ALTER TABLE [dbo].[Patients] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [PaymentDate]
GO
ALTER TABLE [dbo].[Tests] ADD  DEFAULT (N'') FOR [TestName]
GO
ALTER TABLE [dbo].[Tests] ADD  DEFAULT ((0.0)) FOR [Fee]
GO
ALTER TABLE [dbo].[TestTypes] ADD  DEFAULT (N'') FOR [TestTypeName]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[TestRequests]  WITH CHECK ADD  CONSTRAINT [FK_TestRequests_Patients_PatientId] FOREIGN KEY([PatientId])
REFERENCES [dbo].[Patients] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TestRequests] CHECK CONSTRAINT [FK_TestRequests_Patients_PatientId]
GO
ALTER TABLE [dbo].[TestRequests]  WITH CHECK ADD  CONSTRAINT [FK_TestRequests_Tests_TestId] FOREIGN KEY([TestId])
REFERENCES [dbo].[Tests] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[TestRequests] CHECK CONSTRAINT [FK_TestRequests_Tests_TestId]
GO
ALTER TABLE [dbo].[Tests]  WITH CHECK ADD  CONSTRAINT [FK_Tests_TestTypes_TestTypeId] FOREIGN KEY([TestTypeId])
REFERENCES [dbo].[TestTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tests] CHECK CONSTRAINT [FK_Tests_TestTypes_TestTypeId]
GO
