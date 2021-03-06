USE [master]
GO
/****** Object:  Database [Clients]    Script Date: 01.03.2020 17:07:18 ******/
CREATE DATABASE [Clients]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Clients', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Clients.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Clients_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Clients_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Clients] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Clients].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Clients] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Clients] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Clients] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Clients] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Clients] SET ARITHABORT OFF 
GO
ALTER DATABASE [Clients] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Clients] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Clients] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Clients] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Clients] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Clients] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Clients] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Clients] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Clients] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Clients] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Clients] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Clients] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Clients] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Clients] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Clients] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Clients] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Clients] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Clients] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Clients] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Clients] SET  MULTI_USER 
GO
ALTER DATABASE [Clients] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Clients] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Clients] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Clients] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Clients]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 01.03.2020 17:07:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[ClientID] [int] NOT NULL,
	[DepositTypeID] [int] NOT NULL,
	[AccountNumber] [nvarchar](13) NOT NULL,
	[MoneyAmount] [float] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[DaysCount] [int] NOT NULL,
	[CurrencyID] [int] NOT NULL,
	[PercentAccountID] [int] NULL,
	[IsClosed] [bit] NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BankResourse]    Script Date: 01.03.2020 17:07:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankResourse](
	[ResourseID] [int] IDENTITY(1,1) NOT NULL,
	[RealMoney] [float] NOT NULL,
	[PhysicalMoney] [float] NOT NULL,
	[CurrencyID] [int] NOT NULL,
 CONSTRAINT [PK_BankResourse] PRIMARY KEY CLUSTERED 
(
	[ResourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[City]    Script Date: 01.03.2020 17:07:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[CityID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[CityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Client]    Script Date: 01.03.2020 17:07:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[ClientID] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Patronimic] [nvarchar](50) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[PassportSeries] [nvarchar](10) NOT NULL,
	[PassportNumber] [nvarchar](10) NOT NULL,
	[Authority] [nvarchar](100) NOT NULL,
	[DateOfIssue] [date] NOT NULL,
	[IdentificationNumber] [nvarchar](50) NOT NULL,
	[PlaceOfBirth] [nvarchar](50) NOT NULL,
	[Location] [int] NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[MobileNumber] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[MaritalStatus] [int] NOT NULL,
	[Nationality] [int] NOT NULL,
	[Disability] [int] NOT NULL,
	[Pensioner] [bit] NOT NULL,
	[MonthlyIncome] [money] NULL,
	[RegistrationCity] [int] NOT NULL,
	[Gender] [bit] NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ClientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Currency]    Script Date: 01.03.2020 17:07:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[CurrencyID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ShortName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[CurrencyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DepositType]    Script Date: 01.03.2020 17:07:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DepositType](
	[DepositTypeID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Percents] [float] NOT NULL,
 CONSTRAINT [PK_DepositType] PRIMARY KEY CLUSTERED 
(
	[DepositTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Disability]    Script Date: 01.03.2020 17:07:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Disability](
	[DisabilityID] [int] IDENTITY(1,1) NOT NULL,
	[Disability] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Disability] PRIMARY KEY CLUSTERED 
(
	[DisabilityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MaritualStatus]    Script Date: 01.03.2020 17:07:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MaritualStatus](
	[StatusID] [int] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_MaritualStatus] PRIMARY KEY CLUSTERED 
(
	[StatusID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Nationality]    Script Date: 01.03.2020 17:07:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Nationality](
	[NationalityID] [int] IDENTITY(1,1) NOT NULL,
	[Country] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Nationality] PRIMARY KEY CLUSTERED 
(
	[NationalityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Client] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Client] ([ClientID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Client]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Currency] FOREIGN KEY([CurrencyID])
REFERENCES [dbo].[Currency] ([CurrencyID])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Currency]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_DepositType] FOREIGN KEY([DepositTypeID])
REFERENCES [dbo].[DepositType] ([DepositTypeID])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_DepositType]
GO
ALTER TABLE [dbo].[BankResourse]  WITH CHECK ADD  CONSTRAINT [FK_BankResourse_Currency] FOREIGN KEY([CurrencyID])
REFERENCES [dbo].[Currency] ([CurrencyID])
GO
ALTER TABLE [dbo].[BankResourse] CHECK CONSTRAINT [FK_BankResourse_Currency]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_City] FOREIGN KEY([Location])
REFERENCES [dbo].[City] ([CityID])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_City]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_City1] FOREIGN KEY([RegistrationCity])
REFERENCES [dbo].[City] ([CityID])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_City1]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_Disability] FOREIGN KEY([Disability])
REFERENCES [dbo].[Disability] ([DisabilityID])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Disability]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_MaritualStatus] FOREIGN KEY([MaritalStatus])
REFERENCES [dbo].[MaritualStatus] ([StatusID])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_MaritualStatus]
GO
ALTER TABLE [dbo].[Client]  WITH CHECK ADD  CONSTRAINT [FK_Client_Nationality] FOREIGN KEY([Nationality])
REFERENCES [dbo].[Nationality] ([NationalityID])
GO
ALTER TABLE [dbo].[Client] CHECK CONSTRAINT [FK_Client_Nationality]
GO
USE [master]
GO
ALTER DATABASE [Clients] SET  READ_WRITE 
GO
