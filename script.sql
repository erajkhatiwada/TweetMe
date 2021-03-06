USE [master]
GO
/****** Object:  Database [WinterBreak]    Script Date: 3/17/2019 9:08:55 PM ******/
CREATE DATABASE [WinterBreak]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'WinterBreak', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\WinterBreak.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'WinterBreak_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\WinterBreak_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [WinterBreak] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WinterBreak].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WinterBreak] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [WinterBreak] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [WinterBreak] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [WinterBreak] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [WinterBreak] SET ARITHABORT OFF 
GO
ALTER DATABASE [WinterBreak] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [WinterBreak] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [WinterBreak] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [WinterBreak] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [WinterBreak] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [WinterBreak] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [WinterBreak] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [WinterBreak] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [WinterBreak] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [WinterBreak] SET  DISABLE_BROKER 
GO
ALTER DATABASE [WinterBreak] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [WinterBreak] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [WinterBreak] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [WinterBreak] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [WinterBreak] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [WinterBreak] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [WinterBreak] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [WinterBreak] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [WinterBreak] SET  MULTI_USER 
GO
ALTER DATABASE [WinterBreak] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [WinterBreak] SET DB_CHAINING OFF 
GO
ALTER DATABASE [WinterBreak] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [WinterBreak] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [WinterBreak] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [WinterBreak] SET QUERY_STORE = OFF
GO
USE [WinterBreak]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 3/17/2019 9:08:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[UserComment] [nvarchar](max) NOT NULL,
	[DateCreated] [nvarchar](max) NOT NULL,
	[CommentType] [nvarchar](max) NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Follow]    Script Date: 3/17/2019 9:08:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Follow](
	[FollowId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FollowedUserId] [int] NOT NULL,
 CONSTRAINT [PK_Follow] PRIMARY KEY CLUSTERED 
(
	[FollowId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Like]    Script Date: 3/17/2019 9:08:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Like](
	[LikeId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CommentId] [int] NOT NULL,
 CONSTRAINT [PK_Like] PRIMARY KEY CLUSTERED 
(
	[LikeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Picture]    Script Date: 3/17/2019 9:08:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Picture](
	[PictureId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PictureUrl] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Picture] PRIMARY KEY CLUSTERED 
(
	[PictureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/17/2019 9:08:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](max) NOT NULL,
	[LastName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Comment]  WITH CHECK ADD  CONSTRAINT [FK_Comment_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Comment] CHECK CONSTRAINT [FK_Comment_User]
GO
ALTER TABLE [dbo].[Follow]  WITH CHECK ADD  CONSTRAINT [FK_Follow_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Follow] CHECK CONSTRAINT [FK_Follow_User]
GO
ALTER TABLE [dbo].[Like]  WITH CHECK ADD  CONSTRAINT [FK_Like_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Like] CHECK CONSTRAINT [FK_Like_User]
GO
ALTER TABLE [dbo].[Like]  WITH CHECK ADD  CONSTRAINT [FK_Like_User_Comment] FOREIGN KEY([CommentId])
REFERENCES [dbo].[Comment] ([CommentId])
GO
ALTER TABLE [dbo].[Like] CHECK CONSTRAINT [FK_Like_User_Comment]
GO
ALTER TABLE [dbo].[Picture]  WITH CHECK ADD  CONSTRAINT [FK_Picture_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Picture] CHECK CONSTRAINT [FK_Picture_User]
GO
/****** Object:  StoredProcedure [dbo].[eraj]    Script Date: 3/17/2019 9:08:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[eraj]
@search nvarchar(30)
	-- Add the parameters for the stored procedure here
	/*<@Param1, sysname, @p1> <Datatype_For_Param1, , int> = <Default_Value_For_Param1, , 0>, 
	<@Param2, sysname, @p2> <Datatype_For_Param2, , int> = <Default_Value_For_Param2, , 0> */
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from [dbo].[User] WHERE ([User].[Username] like @search + '%' 
	OR [User].[FirstName] like @search +'%'
	OR [User].[LastName] like @search +'%')
END
GO
/****** Object:  StoredProcedure [dbo].[eraj1]    Script Date: 3/17/2019 9:08:56 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[eraj1] 
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM [dbo].[Comment]
END
GO
USE [master]
GO
ALTER DATABASE [WinterBreak] SET  READ_WRITE 
GO
