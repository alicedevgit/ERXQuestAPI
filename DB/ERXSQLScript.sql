USE [master]
GO
/****** Object:  Database [ERXQN]    Script Date: 5/9/2021 2:51:48 AM ******/
CREATE DATABASE [ERXQN]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ERXQN', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ERXQN.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ERXQN_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ERXQN_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ERXQN] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ERXQN].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ERXQN] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ERXQN] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ERXQN] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ERXQN] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ERXQN] SET ARITHABORT OFF 
GO
ALTER DATABASE [ERXQN] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ERXQN] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ERXQN] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ERXQN] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ERXQN] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ERXQN] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ERXQN] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ERXQN] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ERXQN] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ERXQN] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ERXQN] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ERXQN] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ERXQN] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ERXQN] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ERXQN] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ERXQN] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ERXQN] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ERXQN] SET RECOVERY FULL 
GO
ALTER DATABASE [ERXQN] SET  MULTI_USER 
GO
ALTER DATABASE [ERXQN] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ERXQN] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ERXQN] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ERXQN] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ERXQN] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ERXQN] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ERXQN', N'ON'
GO
ALTER DATABASE [ERXQN] SET QUERY_STORE = OFF
GO
USE [ERXQN]
GO
/****** Object:  Table [dbo].[Answer]    Script Date: 5/9/2021 2:51:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answer](
	[token] [varchar](400) NOT NULL,
	[questionId] [int] NOT NULL,
	[value] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Answer] PRIMARY KEY CLUSTERED 
(
	[token] ASC,
	[questionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AnswerRestriction]    Script Date: 5/9/2021 2:51:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnswerRestriction](
	[questionId] [int] NOT NULL,
	[restrictionId] [int] NOT NULL,
 CONSTRAINT [PK_AnswerRestriction] PRIMARY KEY CLUSTERED 
(
	[questionId] ASC,
	[restrictionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AnswerType]    Script Date: 5/9/2021 2:51:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AnswerType](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](400) NOT NULL,
 CONSTRAINT [PK_AnswerType] PRIMARY KEY CLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Question]    Script Date: 5/9/2021 2:51:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Question](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](400) NOT NULL,
	[categoryId] [int] NOT NULL,
	[answerTypeId] [int] NOT NULL,
	[sequence] [int] NULL,
 CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionCategory]    Script Date: 5/9/2021 2:51:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionCategory](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](400) NOT NULL,
	[sequence] [int] NULL,
	[remark] [nvarchar](max) NULL,
 CONSTRAINT [PK_QuestionCategory] PRIMARY KEY CLUSTERED 
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[QuestionChoice]    Script Date: 5/9/2021 2:51:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QuestionChoice](
	[questionId] [int] NOT NULL,
	[sequence] [int] NOT NULL,
	[value] [nvarchar](max) NULL,
	[text] [nvarchar](max) NULL,
	[sourceURI] [nvarchar](max) NULL,
 CONSTRAINT [PK_QuestionChoice] PRIMARY KEY CLUSTERED 
(
	[questionId] ASC,
	[sequence] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Restriction]    Script Date: 5/9/2021 2:51:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Restriction](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[notAllowedValue] [nvarchar](max) NULL,
	[warningMessage] [nvarchar](max) NULL,
	[operation] [nvarchar](max) NULL,
 CONSTRAINT [PK_Restriction] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

INSERT [dbo].[AnswerRestriction] ([questionId], [restrictionId]) VALUES (5, 1)
GO
SET IDENTITY_INSERT [dbo].[AnswerType] ON 
GO
INSERT [dbo].[AnswerType] ([id], [name]) VALUES (4, N'Date Picker')
GO
INSERT [dbo].[AnswerType] ([id], [name]) VALUES (2, N'Dropdown List')
GO
INSERT [dbo].[AnswerType] ([id], [name]) VALUES (3, N'Dropdown Searchable List')
GO
INSERT [dbo].[AnswerType] ([id], [name]) VALUES (5, N'Text Area')
GO
INSERT [dbo].[AnswerType] ([id], [name]) VALUES (1, N'Text Box')
GO
SET IDENTITY_INSERT [dbo].[AnswerType] OFF
GO
SET IDENTITY_INSERT [dbo].[Question] ON 
GO
INSERT [dbo].[Question] ([id], [name], [categoryId], [answerTypeId], [sequence]) VALUES (1, N'Title', 1, 2, 1)
GO
INSERT [dbo].[Question] ([id], [name], [categoryId], [answerTypeId], [sequence]) VALUES (2, N'First name', 1, 1, 2)
GO
INSERT [dbo].[Question] ([id], [name], [categoryId], [answerTypeId], [sequence]) VALUES (3, N'Last name', 1, 1, 3)
GO
INSERT [dbo].[Question] ([id], [name], [categoryId], [answerTypeId], [sequence]) VALUES (4, N'Date of birth', 1, 4, 4)
GO
INSERT [dbo].[Question] ([id], [name], [categoryId], [answerTypeId], [sequence]) VALUES (5, N'Country of residence', 1, 3, 5)
GO
INSERT [dbo].[Question] ([id], [name], [categoryId], [answerTypeId], [sequence]) VALUES (6, N'House', 2, 5, 1)
GO
INSERT [dbo].[Question] ([id], [name], [categoryId], [answerTypeId], [sequence]) VALUES (7, N'Work', 2, 5, 2)
GO
INSERT [dbo].[Question] ([id], [name], [categoryId], [answerTypeId], [sequence]) VALUES (8, N'Occupation ', 3, 3, 1)
GO
INSERT [dbo].[Question] ([id], [name], [categoryId], [answerTypeId], [sequence]) VALUES (9, N'Job Title', 3, 1, 2)
GO
INSERT [dbo].[Question] ([id], [name], [categoryId], [answerTypeId], [sequence]) VALUES (10, N'Business Type', 3, 1, 3)
GO
SET IDENTITY_INSERT [dbo].[Question] OFF
GO
SET IDENTITY_INSERT [dbo].[QuestionCategory] ON 
GO
INSERT [dbo].[QuestionCategory] ([id], [name], [sequence], [remark]) VALUES (2, N'Address', 2, N'You don''t need to fill both of them.')
GO
INSERT [dbo].[QuestionCategory] ([id], [name], [sequence], [remark]) VALUES (4, N'Finish', 4, N'So now you reach the last step, just make sure your answers and then click Finish to done your job.')
GO
INSERT [dbo].[QuestionCategory] ([id], [name], [sequence], [remark]) VALUES (3, N'Occupation', 3, N'Let skip this if you wouldn''t let us know.')
GO
INSERT [dbo].[QuestionCategory] ([id], [name], [sequence], [remark]) VALUES (1, N'Personal Information', 1, N'Please tell us about you...')
GO
SET IDENTITY_INSERT [dbo].[QuestionCategory] OFF
GO
INSERT [dbo].[QuestionChoice] ([questionId], [sequence], [value], [text], [sourceURI]) VALUES (1, 1, N'1', N'Mr.', NULL)
GO
INSERT [dbo].[QuestionChoice] ([questionId], [sequence], [value], [text], [sourceURI]) VALUES (1, 2, N'2', N'Ms.', NULL)
GO
INSERT [dbo].[QuestionChoice] ([questionId], [sequence], [value], [text], [sourceURI]) VALUES (1, 3, N'3', N'Mrs.', NULL)
GO
INSERT [dbo].[QuestionChoice] ([questionId], [sequence], [value], [text], [sourceURI]) VALUES (5, 1, NULL, NULL, N'Countries.json')
GO
INSERT [dbo].[QuestionChoice] ([questionId], [sequence], [value], [text], [sourceURI]) VALUES (8, 1, NULL, NULL, N'Occupations.json')
GO
SET IDENTITY_INSERT [dbo].[Restriction] ON 
GO
INSERT [dbo].[Restriction] ([id], [notAllowedValue], [warningMessage], [operation]) VALUES (1, N'Cambodia, Myanmar, Pakistan', N'As you live in this country, so the questionnaire is terminated.', N'Done')
GO
SET IDENTITY_INSERT [dbo].[Restriction] OFF
GO
USE [master]
GO
ALTER DATABASE [ERXQN] SET  READ_WRITE 
GO
