USE [master]
GO
/****** Object:  Database [EducationSystemDB]    Script Date: 26.10.2021 10:49:40 ******/
CREATE DATABASE [EducationSystemDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EducationSystemDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MAK_SERVER\MSSQL\DATA\EducationSystemDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EducationSystemDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MAK_SERVER\MSSQL\DATA\EducationSystemDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [EducationSystemDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EducationSystemDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EducationSystemDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EducationSystemDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EducationSystemDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EducationSystemDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EducationSystemDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [EducationSystemDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EducationSystemDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EducationSystemDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EducationSystemDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EducationSystemDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EducationSystemDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EducationSystemDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EducationSystemDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EducationSystemDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EducationSystemDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EducationSystemDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EducationSystemDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EducationSystemDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EducationSystemDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EducationSystemDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EducationSystemDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EducationSystemDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EducationSystemDB] SET RECOVERY FULL 
GO
ALTER DATABASE [EducationSystemDB] SET  MULTI_USER 
GO
ALTER DATABASE [EducationSystemDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EducationSystemDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EducationSystemDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EducationSystemDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EducationSystemDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'EducationSystemDB', N'ON'
GO
ALTER DATABASE [EducationSystemDB] SET QUERY_STORE = OFF
GO
USE [EducationSystemDB]
GO
/****** Object:  Table [dbo].[Criterion]    Script Date: 26.10.2021 10:49:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Criterion](
	[CriterionCode] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[MaxScore] [int] NOT NULL,
	[SubjectTaskCode] [int] NOT NULL,
 CONSTRAINT [Unique_Identifier20] PRIMARY KEY CLUSTERED 
(
	[CriterionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 26.10.2021 10:49:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[EventCode] [int] IDENTITY(1,1) NOT NULL,
	[EventName] [varchar](50) NOT NULL,
 CONSTRAINT [Unique_Identifier11] PRIMARY KEY CLUSTERED 
(
	[EventCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Examination]    Script Date: 26.10.2021 10:49:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Examination](
	[ExamCode] [bigint] IDENTITY(1,1) NOT NULL,
	[ExamDate] [datetime] NOT NULL,
	[SubjectCode] [int] NOT NULL,
 CONSTRAINT [Unique_Identifier14] PRIMARY KEY CLUSTERED 
(
	[ExamCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Expert]    Script Date: 26.10.2021 10:49:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expert](
	[ExpertCode] [int] IDENTITY(1,1) NOT NULL,
	[SecondName] [varchar](50) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[Patronymic] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Phone] [varchar](50) NOT NULL,
 CONSTRAINT [Unique_Identifier13] PRIMARY KEY CLUSTERED 
(
	[ExpertCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExpertSubject]    Script Date: 26.10.2021 10:49:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExpertSubject](
	[CodeSubjectExpert] [bigint] IDENTITY(1,1) NOT NULL,
	[SubjectCode] [int] NOT NULL,
	[ExpertCode] [int] NOT NULL,
 CONSTRAINT [Unique_Identifier16] PRIMARY KEY CLUSTERED 
(
	[CodeSubjectExpert] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Graduate]    Script Date: 26.10.2021 10:49:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Graduate](
	[GraduateCode] [bigint] IDENTITY(1,1) NOT NULL,
	[Password] [varchar](30) NOT NULL,
	[CodeSubjectExpert] [bigint] NOT NULL,
	[VariantCode] [bigint] NOT NULL,
	[ExamCode] [bigint] NULL,
 CONSTRAINT [Unique_Identifier17] PRIMARY KEY CLUSTERED 
(
	[GraduateCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GraduateAnswer]    Script Date: 26.10.2021 10:49:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GraduateAnswer](
	[Score] [int] NOT NULL,
	[GraduateCode] [bigint] NOT NULL,
	[VariantTaskCode] [bigint] NOT NULL,
	[CriterionCode] [bigint] NOT NULL,
 CONSTRAINT [Unique_Identifier19] PRIMARY KEY CLUSTERED 
(
	[GraduateCode] ASC,
	[VariantTaskCode] ASC,
	[CriterionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Solution]    Script Date: 26.10.2021 10:49:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Solution](
	[SolutionCode] [bigint] IDENTITY(1,1) NOT NULL,
	[SolutionImage] [image] NOT NULL,
	[TaskCode] [bigint] NOT NULL,
 CONSTRAINT [Unique_Identifier8] PRIMARY KEY CLUSTERED 
(
	[SolutionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SolutionCriterionScore]    Script Date: 26.10.2021 10:49:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolutionCriterionScore](
	[SolutionCriterionCode] [bigint] IDENTITY(1,1) NOT NULL,
	[Comment] [varchar](1000) NOT NULL,
	[isValid] [bit] NOT NULL,
	[PossibleScore] [int] NOT NULL,
	[SolutionCode] [bigint] NOT NULL,
	[CriterionCode] [bigint] NOT NULL,
 CONSTRAINT [Unique_Identifier9] PRIMARY KEY CLUSTERED 
(
	[SolutionCriterionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 26.10.2021 10:49:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[SubjectCode] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Class] [int] NOT NULL,
 CONSTRAINT [Unique_Identifier3] PRIMARY KEY CLUSTERED 
(
	[SubjectCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubjectTask]    Script Date: 26.10.2021 10:49:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubjectTask](
	[SubjectTaskCode] [int] IDENTITY(1,1) NOT NULL,
	[Number] [int] NOT NULL,
	[SubjectCode] [int] NOT NULL,
 CONSTRAINT [Unique_Identifier4] PRIMARY KEY CLUSTERED 
(
	[SubjectTaskCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 26.10.2021 10:49:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[TaskCode] [bigint] IDENTITY(1,1) NOT NULL,
	[TaskImage] [image] NOT NULL,
	[Year] [date] NOT NULL,
	[CriterionFile] [image] NOT NULL,
	[CriterionFileName] [varchar](50) NOT NULL,
	[EventCode] [int] NOT NULL,
	[SubjectTaskCode] [int] NOT NULL,
 CONSTRAINT [Unique_Identifier10] PRIMARY KEY CLUSTERED 
(
	[TaskCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Variant]    Script Date: 26.10.2021 10:49:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Variant](
	[VariantCode] [bigint] NOT NULL,
	[ExamCode] [bigint] NOT NULL,
 CONSTRAINT [Unique_Identifier15] PRIMARY KEY CLUSTERED 
(
	[VariantCode] ASC,
	[ExamCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VariantSolution]    Script Date: 26.10.2021 10:49:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VariantSolution](
	[VariantTaskCode] [bigint] IDENTITY(1,1) NOT NULL,
	[Number] [bigint] NOT NULL,
	[SolutionCode] [bigint] NOT NULL,
	[VariantCode] [bigint] NOT NULL,
	[ExamCode] [bigint] NOT NULL,
 CONSTRAINT [Unique_Identifier18] PRIMARY KEY CLUSTERED 
(
	[VariantTaskCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Expert] ON 

INSERT [dbo].[Expert] ([ExpertCode], [SecondName], [FirstName], [Patronymic], [Email], [Phone]) VALUES (1, N'Павлов', N'Виктор', N'Фёдорович', N'aaa@qwerty.com', N'+7 777 77 77')
SET IDENTITY_INSERT [dbo].[Expert] OFF
/****** Object:  Index [IX_Номер задания - Критерий]    Script Date: 26.10.2021 10:49:41 ******/
CREATE NONCLUSTERED INDEX [IX_Номер задания - Критерий] ON [dbo].[Criterion]
(
	[SubjectTaskCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Предмет на экзамене]    Script Date: 26.10.2021 10:49:41 ******/
CREATE NONCLUSTERED INDEX [IX_Предмет на экзамене] ON [dbo].[Examination]
(
	[SubjectCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Предмет - Специализация эксперта]    Script Date: 26.10.2021 10:49:41 ******/
CREATE NONCLUSTERED INDEX [IX_Предмет - Специализация эксперта] ON [dbo].[ExpertSubject]
(
	[SubjectCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Специализация - Эксперт]    Script Date: 26.10.2021 10:49:41 ******/
CREATE NONCLUSTERED INDEX [IX_Специализация - Эксперт] ON [dbo].[ExpertSubject]
(
	[ExpertCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Номер варианта - Назначение]    Script Date: 26.10.2021 10:49:41 ******/
CREATE NONCLUSTERED INDEX [IX_Номер варианта - Назначение] ON [dbo].[Graduate]
(
	[VariantCode] ASC,
	[ExamCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Специализация - Назначение]    Script Date: 26.10.2021 10:49:41 ******/
CREATE NONCLUSTERED INDEX [IX_Специализация - Назначение] ON [dbo].[Graduate]
(
	[CodeSubjectExpert] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Задание - Решение]    Script Date: 26.10.2021 10:49:41 ******/
CREATE NONCLUSTERED INDEX [IX_Задание - Решение] ON [dbo].[Solution]
(
	[TaskCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Комментарии к решению - Решение]    Script Date: 26.10.2021 10:49:41 ******/
CREATE NONCLUSTERED INDEX [IX_Комментарии к решению - Решение] ON [dbo].[SolutionCriterionScore]
(
	[SolutionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Критерий - Комментарии к решению]    Script Date: 26.10.2021 10:49:41 ******/
CREATE NONCLUSTERED INDEX [IX_Критерий - Комментарии к решению] ON [dbo].[SolutionCriterionScore]
(
	[CriterionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Предмет - Номер задания]    Script Date: 26.10.2021 10:49:41 ******/
CREATE NONCLUSTERED INDEX [IX_Предмет - Номер задания] ON [dbo].[SubjectTask]
(
	[SubjectCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Задание - Мероприятие]    Script Date: 26.10.2021 10:49:41 ******/
CREATE NONCLUSTERED INDEX [IX_Задание - Мероприятие] ON [dbo].[Task]
(
	[EventCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Номер задания - Задание]    Script Date: 26.10.2021 10:49:41 ******/
CREATE NONCLUSTERED INDEX [IX_Номер задания - Задание] ON [dbo].[Task]
(
	[SubjectTaskCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Вариант - Решение]    Script Date: 26.10.2021 10:49:41 ******/
CREATE NONCLUSTERED INDEX [IX_Вариант - Решение] ON [dbo].[VariantSolution]
(
	[SolutionCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Номер варианта - Вариант]    Script Date: 26.10.2021 10:49:41 ******/
CREATE NONCLUSTERED INDEX [IX_Номер варианта - Вариант] ON [dbo].[VariantSolution]
(
	[VariantCode] ASC,
	[ExamCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Criterion]  WITH CHECK ADD  CONSTRAINT [Номер задания - Критерий] FOREIGN KEY([SubjectTaskCode])
REFERENCES [dbo].[SubjectTask] ([SubjectTaskCode])
GO
ALTER TABLE [dbo].[Criterion] CHECK CONSTRAINT [Номер задания - Критерий]
GO
ALTER TABLE [dbo].[Examination]  WITH CHECK ADD  CONSTRAINT [Предмет на экзамене] FOREIGN KEY([SubjectCode])
REFERENCES [dbo].[Subject] ([SubjectCode])
GO
ALTER TABLE [dbo].[Examination] CHECK CONSTRAINT [Предмет на экзамене]
GO
ALTER TABLE [dbo].[ExpertSubject]  WITH CHECK ADD  CONSTRAINT [Предмет - Специализация эксперта] FOREIGN KEY([SubjectCode])
REFERENCES [dbo].[Subject] ([SubjectCode])
GO
ALTER TABLE [dbo].[ExpertSubject] CHECK CONSTRAINT [Предмет - Специализация эксперта]
GO
ALTER TABLE [dbo].[ExpertSubject]  WITH CHECK ADD  CONSTRAINT [Специализация - Эксперт] FOREIGN KEY([ExpertCode])
REFERENCES [dbo].[Expert] ([ExpertCode])
GO
ALTER TABLE [dbo].[ExpertSubject] CHECK CONSTRAINT [Специализация - Эксперт]
GO
ALTER TABLE [dbo].[Graduate]  WITH CHECK ADD  CONSTRAINT [Номер варианта - Назначение] FOREIGN KEY([VariantCode], [ExamCode])
REFERENCES [dbo].[Variant] ([VariantCode], [ExamCode])
GO
ALTER TABLE [dbo].[Graduate] CHECK CONSTRAINT [Номер варианта - Назначение]
GO
ALTER TABLE [dbo].[Graduate]  WITH CHECK ADD  CONSTRAINT [Специализация - Назначение] FOREIGN KEY([CodeSubjectExpert])
REFERENCES [dbo].[ExpertSubject] ([CodeSubjectExpert])
GO
ALTER TABLE [dbo].[Graduate] CHECK CONSTRAINT [Специализация - Назначение]
GO
ALTER TABLE [dbo].[GraduateAnswer]  WITH CHECK ADD  CONSTRAINT [Вариант - Балл] FOREIGN KEY([VariantTaskCode])
REFERENCES [dbo].[VariantSolution] ([VariantTaskCode])
GO
ALTER TABLE [dbo].[GraduateAnswer] CHECK CONSTRAINT [Вариант - Балл]
GO
ALTER TABLE [dbo].[GraduateAnswer]  WITH CHECK ADD  CONSTRAINT [Критерий - Балл] FOREIGN KEY([CriterionCode])
REFERENCES [dbo].[Criterion] ([CriterionCode])
GO
ALTER TABLE [dbo].[GraduateAnswer] CHECK CONSTRAINT [Критерий - Балл]
GO
ALTER TABLE [dbo].[GraduateAnswer]  WITH CHECK ADD  CONSTRAINT [Назначение - Балл] FOREIGN KEY([GraduateCode])
REFERENCES [dbo].[Graduate] ([GraduateCode])
GO
ALTER TABLE [dbo].[GraduateAnswer] CHECK CONSTRAINT [Назначение - Балл]
GO
ALTER TABLE [dbo].[Solution]  WITH CHECK ADD  CONSTRAINT [Задание - Решение] FOREIGN KEY([TaskCode])
REFERENCES [dbo].[Task] ([TaskCode])
GO
ALTER TABLE [dbo].[Solution] CHECK CONSTRAINT [Задание - Решение]
GO
ALTER TABLE [dbo].[SolutionCriterionScore]  WITH CHECK ADD  CONSTRAINT [Комментарии к решению - Решение] FOREIGN KEY([SolutionCode])
REFERENCES [dbo].[Solution] ([SolutionCode])
GO
ALTER TABLE [dbo].[SolutionCriterionScore] CHECK CONSTRAINT [Комментарии к решению - Решение]
GO
ALTER TABLE [dbo].[SolutionCriterionScore]  WITH CHECK ADD  CONSTRAINT [Критерий - Комментарии к решению] FOREIGN KEY([CriterionCode])
REFERENCES [dbo].[Criterion] ([CriterionCode])
GO
ALTER TABLE [dbo].[SolutionCriterionScore] CHECK CONSTRAINT [Критерий - Комментарии к решению]
GO
ALTER TABLE [dbo].[SubjectTask]  WITH CHECK ADD  CONSTRAINT [Предмет - Номер задания] FOREIGN KEY([SubjectCode])
REFERENCES [dbo].[Subject] ([SubjectCode])
GO
ALTER TABLE [dbo].[SubjectTask] CHECK CONSTRAINT [Предмет - Номер задания]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [Задание - Мероприятие] FOREIGN KEY([EventCode])
REFERENCES [dbo].[Event] ([EventCode])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [Задание - Мероприятие]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [Номер задания - Задание] FOREIGN KEY([SubjectTaskCode])
REFERENCES [dbo].[SubjectTask] ([SubjectTaskCode])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [Номер задания - Задание]
GO
ALTER TABLE [dbo].[Variant]  WITH CHECK ADD  CONSTRAINT [Экзамен - Вариант] FOREIGN KEY([ExamCode])
REFERENCES [dbo].[Examination] ([ExamCode])
GO
ALTER TABLE [dbo].[Variant] CHECK CONSTRAINT [Экзамен - Вариант]
GO
ALTER TABLE [dbo].[VariantSolution]  WITH CHECK ADD  CONSTRAINT [Вариант - Решение] FOREIGN KEY([SolutionCode])
REFERENCES [dbo].[Solution] ([SolutionCode])
GO
ALTER TABLE [dbo].[VariantSolution] CHECK CONSTRAINT [Вариант - Решение]
GO
ALTER TABLE [dbo].[VariantSolution]  WITH CHECK ADD  CONSTRAINT [Номер варианта - Вариант] FOREIGN KEY([VariantCode], [ExamCode])
REFERENCES [dbo].[Variant] ([VariantCode], [ExamCode])
GO
ALTER TABLE [dbo].[VariantSolution] CHECK CONSTRAINT [Номер варианта - Вариант]
GO
USE [master]
GO
ALTER DATABASE [EducationSystemDB] SET  READ_WRITE 
GO
