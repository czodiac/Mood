USE [master]
GO
/****** Object:  Database [MoodDB]    Script Date: 11/8/2022 12:13:02 AM ******/
CREATE DATABASE [MoodDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MoodDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MoodDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MoodDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\MoodDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [MoodDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MoodDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MoodDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MoodDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MoodDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MoodDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MoodDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [MoodDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MoodDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MoodDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MoodDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MoodDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MoodDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MoodDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MoodDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MoodDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MoodDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MoodDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MoodDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MoodDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MoodDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MoodDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MoodDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MoodDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MoodDB] SET RECOVERY FULL 
GO
ALTER DATABASE [MoodDB] SET  MULTI_USER 
GO
ALTER DATABASE [MoodDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MoodDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MoodDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MoodDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MoodDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MoodDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MoodDB', N'ON'
GO
ALTER DATABASE [MoodDB] SET QUERY_STORE = OFF
GO
USE [MoodDB]
GO
/****** Object:  Table [dbo].[tblLocations]    Script Date: 11/8/2022 12:13:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblLocations](
	[LocationID] [int] IDENTITY(1,1) NOT NULL,
	[LocationName] [nvarchar](100) NOT NULL,
	[DistanceXaxis] [int] NOT NULL,
	[DistanceYaxis] [int] NOT NULL,
 CONSTRAINT [PK_tblLocations] PRIMARY KEY CLUSTERED 
(
	[LocationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblMoodNames]    Script Date: 11/8/2022 12:13:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMoodNames](
	[MoodID] [int] IDENTITY(1,1) NOT NULL,
	[MoodName] [nvarchar](50) NOT NULL,
	[Weight] [int] NOT NULL,
 CONSTRAINT [PK_tblMoodNames] PRIMARY KEY CLUSTERED 
(
	[MoodID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblMoods]    Script Date: 11/8/2022 12:13:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblMoods](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[MoodID] [int] NOT NULL,
	[LocationID] [int] NOT NULL,
 CONSTRAINT [PK_tblMoods] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 11/8/2022 12:13:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblUsers](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[Password] [nchar](100) NULL,
	[EmailAddress] [nvarchar](150) NULL,
	[Role] [nchar](50) NULL,
	[Surname] [nvarchar](100) NULL,
	[GivenName] [nvarchar](100) NULL,
 CONSTRAINT [PK_tblUsers] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblLocations] ON 

INSERT [dbo].[tblLocations] ([LocationID], [LocationName], [DistanceXaxis], [DistanceYaxis]) VALUES (1, N'A', 2, 0)
INSERT [dbo].[tblLocations] ([LocationID], [LocationName], [DistanceXaxis], [DistanceYaxis]) VALUES (2, N'B', -2, 1)
INSERT [dbo].[tblLocations] ([LocationID], [LocationName], [DistanceXaxis], [DistanceYaxis]) VALUES (3, N'C', 3, 3)
INSERT [dbo].[tblLocations] ([LocationID], [LocationName], [DistanceXaxis], [DistanceYaxis]) VALUES (4, N'D', -2, -3)
INSERT [dbo].[tblLocations] ([LocationID], [LocationName], [DistanceXaxis], [DistanceYaxis]) VALUES (5, N'E', 4, -2)
SET IDENTITY_INSERT [dbo].[tblLocations] OFF
GO
SET IDENTITY_INSERT [dbo].[tblMoodNames] ON 

INSERT [dbo].[tblMoodNames] ([MoodID], [MoodName], [Weight]) VALUES (1, N'Happy', 1)
INSERT [dbo].[tblMoodNames] ([MoodID], [MoodName], [Weight]) VALUES (2, N'Sad', -1)
INSERT [dbo].[tblMoodNames] ([MoodID], [MoodName], [Weight]) VALUES (3, N'Neutral', 0)
SET IDENTITY_INSERT [dbo].[tblMoodNames] OFF
GO
SET IDENTITY_INSERT [dbo].[tblMoods] ON 

INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (2, 1, 1, 1)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (3, 1, 1, 1)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (4, 1, 1, 1)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (5, 1, 1, 1)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (6, 1, 1, 1)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (7, 1, 2, 1)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (8, 1, 2, 1)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (9, 1, 2, 1)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (10, 1, 3, 1)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (11, 1, 1, 2)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (12, 1, 1, 2)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (13, 1, 1, 2)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (14, 1, 2, 2)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (15, 1, 2, 2)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (16, 1, 2, 2)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (17, 1, 2, 2)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (18, 1, 3, 2)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (19, 1, 3, 2)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (20, 1, 1, 3)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (21, 1, 1, 3)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (22, 1, 1, 3)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (23, 1, 1, 3)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (24, 1, 1, 3)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (25, 1, 1, 3)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (26, 1, 1, 3)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (27, 1, 2, 3)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (28, 1, 2, 3)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (29, 1, 2, 3)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (30, 1, 1, 4)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (31, 1, 1, 4)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (32, 1, 2, 4)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (33, 1, 3, 4)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (34, 1, 1, 5)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (35, 1, 1, 5)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (36, 1, 1, 5)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (37, 1, 1, 5)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (38, 1, 2, 5)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (39, 2, 1, 1)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (40, 3, 1, 3)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (41, 3, 1, 3)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (42, 3, 2, 1)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (43, 3, 2, 1)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (45, 3, 2, 1)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (51, 3, 2, 1)
INSERT [dbo].[tblMoods] ([ID], [UserID], [MoodID], [LocationID]) VALUES (55, 3, 2, 1)
SET IDENTITY_INSERT [dbo].[tblMoods] OFF
GO
SET IDENTITY_INSERT [dbo].[tblUsers] ON 

INSERT [dbo].[tblUsers] ([UserID], [UserName], [Password], [EmailAddress], [Role], [Surname], [GivenName]) VALUES (1, N'UserA', N'passA', N'user_a@gmail.com', N'Administrator', N'User', N'A')
INSERT [dbo].[tblUsers] ([UserID], [UserName], [Password], [EmailAddress], [Role], [Surname], [GivenName]) VALUES (2, N'UserB', N'passB', N'user_b@gmail.com', N'User', N'User', N'B')
INSERT [dbo].[tblUsers] ([UserID], [UserName], [Password], [EmailAddress], [Role], [Surname], [GivenName]) VALUES (3, N'UserC', N'passC', N'user_c@gmail.com', N'User', N'User', N'C')
SET IDENTITY_INSERT [dbo].[tblUsers] OFF
GO
ALTER TABLE [dbo].[tblMoods]  WITH CHECK ADD  CONSTRAINT [FK_tblMoods_tblLocations] FOREIGN KEY([LocationID])
REFERENCES [dbo].[tblLocations] ([LocationID])
GO
ALTER TABLE [dbo].[tblMoods] CHECK CONSTRAINT [FK_tblMoods_tblLocations]
GO
ALTER TABLE [dbo].[tblMoods]  WITH CHECK ADD  CONSTRAINT [FK_tblMoods_tblMoodNames] FOREIGN KEY([MoodID])
REFERENCES [dbo].[tblMoodNames] ([MoodID])
GO
ALTER TABLE [dbo].[tblMoods] CHECK CONSTRAINT [FK_tblMoods_tblMoodNames]
GO
ALTER TABLE [dbo].[tblMoods]  WITH CHECK ADD  CONSTRAINT [FK_tblMoods_tblUsers] FOREIGN KEY([UserID])
REFERENCES [dbo].[tblUsers] ([UserID])
GO
ALTER TABLE [dbo].[tblMoods] CHECK CONSTRAINT [FK_tblMoods_tblUsers]
GO
USE [master]
GO
ALTER DATABASE [MoodDB] SET  READ_WRITE 
GO
