USE [ToDoApp]
/****** Object:  Table [dbo].[Homework]    Script Date: 9/11/2022 10:03:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Homework](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[homework] [nvarchar](500) NOT NULL,
	[date_created] [datetime] NOT NULL,
	[date_modified] [datetime] NOT NULL,
 CONSTRAINT [PK_Homework] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Homework] ON 

INSERT [dbo].[Homework] ([id], [homework], [date_created], [date_modified]) VALUES (1, N'testing home work', CAST(N'2022-09-08T23:11:16.910' AS DateTime), CAST(N'2022-09-08T23:11:16.910' AS DateTime))
INSERT [dbo].[Homework] ([id], [homework], [date_created], [date_modified]) VALUES (2, N'elias home work', CAST(N'2022-09-08T23:11:16.910' AS DateTime), CAST(N'2022-09-08T23:11:16.910' AS DateTime))
INSERT [dbo].[Homework] ([id], [homework], [date_created], [date_modified]) VALUES (3, N'ceridian', CAST(N'2022-09-08T23:11:16.910' AS DateTime), CAST(N'2022-09-08T23:11:16.910' AS DateTime))
INSERT [dbo].[Homework] ([id], [homework], [date_created], [date_modified]) VALUES (5, N'remote homework (modified by elias)', CAST(N'2022-09-08T23:11:16.910' AS DateTime), CAST(N'2022-09-11T20:44:48.853' AS DateTime))
INSERT [dbo].[Homework] ([id], [homework], [date_created], [date_modified]) VALUES (6, N'global', CAST(N'2022-09-08T23:11:16.910' AS DateTime), CAST(N'2022-09-08T23:11:16.910' AS DateTime))
INSERT [dbo].[Homework] ([id], [homework], [date_created], [date_modified]) VALUES (7, N'sun', CAST(N'2022-09-08T23:11:16.910' AS DateTime), CAST(N'2022-09-08T23:11:16.910' AS DateTime))
INSERT [dbo].[Homework] ([id], [homework], [date_created], [date_modified]) VALUES (8, N'insight', CAST(N'2022-09-08T23:11:16.910' AS DateTime), CAST(N'2022-09-08T23:11:16.910' AS DateTime))
INSERT [dbo].[Homework] ([id], [homework], [date_created], [date_modified]) VALUES (9, N'test', CAST(N'2022-09-11T19:56:56.853' AS DateTime), CAST(N'2022-09-11T19:56:56.853' AS DateTime))
INSERT [dbo].[Homework] ([id], [homework], [date_created], [date_modified]) VALUES (10, N'test2', CAST(N'2022-09-11T20:00:13.503' AS DateTime), CAST(N'2022-09-11T20:00:14.047' AS DateTime))
INSERT [dbo].[Homework] ([id], [homework], [date_created], [date_modified]) VALUES (11, N'test56', CAST(N'2022-09-11T20:02:52.790' AS DateTime), CAST(N'2022-09-11T20:02:52.790' AS DateTime))
INSERT [dbo].[Homework] ([id], [homework], [date_created], [date_modified]) VALUES (12, N'test56', CAST(N'2022-09-11T20:03:11.187' AS DateTime), CAST(N'2022-09-11T20:03:11.597' AS DateTime))
INSERT [dbo].[Homework] ([id], [homework], [date_created], [date_modified]) VALUES (13, N'tes555', CAST(N'2022-09-11T20:06:24.337' AS DateTime), CAST(N'2022-09-11T20:06:24.337' AS DateTime))
INSERT [dbo].[Homework] ([id], [homework], [date_created], [date_modified]) VALUES (14, N'tesgffgdg', CAST(N'2022-09-11T20:13:46.930' AS DateTime), CAST(N'2022-09-11T20:13:46.930' AS DateTime))
INSERT [dbo].[Homework] ([id], [homework], [date_created], [date_modified]) VALUES (15, N'sz', CAST(N'2022-09-11T20:23:56.590' AS DateTime), CAST(N'2022-09-11T20:23:56.590' AS DateTime))
INSERT [dbo].[Homework] ([id], [homework], [date_created], [date_modified]) VALUES (16, N'sz1', CAST(N'2022-09-11T20:26:47.447' AS DateTime), CAST(N'2022-09-11T20:26:47.447' AS DateTime))
INSERT [dbo].[Homework] ([id], [homework], [date_created], [date_modified]) VALUES (17, N'sz2', CAST(N'2022-09-11T20:34:45.937' AS DateTime), CAST(N'2022-09-11T20:34:45.937' AS DateTime))
SET IDENTITY_INSERT [dbo].[Homework] OFF
GO
ALTER TABLE [dbo].[Homework] ADD  CONSTRAINT [DF_Homework_date_created]  DEFAULT (getdate()) FOR [date_created]
GO
ALTER TABLE [dbo].[Homework] ADD  CONSTRAINT [DF_Homework_date_modified]  DEFAULT (getdate()) FOR [date_modified]
GO
