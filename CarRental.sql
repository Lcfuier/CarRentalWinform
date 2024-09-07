USE [master]
GO
/****** Object:  Database [CarRental]    Script Date: 9/7/2024 11:43:46 PM ******/
CREATE DATABASE [CarRental]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CarRental', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\CarRental.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CarRental_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\CarRental_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [CarRental] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CarRental].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CarRental] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CarRental] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CarRental] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CarRental] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CarRental] SET ARITHABORT OFF 
GO
ALTER DATABASE [CarRental] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [CarRental] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CarRental] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CarRental] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CarRental] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CarRental] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CarRental] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CarRental] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CarRental] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CarRental] SET  ENABLE_BROKER 
GO
ALTER DATABASE [CarRental] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CarRental] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CarRental] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CarRental] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CarRental] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CarRental] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CarRental] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CarRental] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CarRental] SET  MULTI_USER 
GO
ALTER DATABASE [CarRental] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CarRental] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CarRental] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CarRental] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CarRental] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CarRental] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [CarRental] SET QUERY_STORE = OFF
GO
USE [CarRental]
GO
/****** Object:  Table [dbo].[Car]    Script Date: 9/7/2024 11:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Car](
	[Id] [varchar](9) NOT NULL,
	[Brand] [varchar](12) NULL,
	[Model] [varchar](12) NULL,
	[StatusCar] [varchar](12) NULL,
	[Price] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 9/7/2024 11:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [varchar](12) NOT NULL,
	[FullName] [nvarchar](30) NULL,
	[Addr] [nvarchar](50) NULL,
	[PhoneNumber] [varchar](10) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rental]    Script Date: 9/7/2024 11:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rental](
	[Id] [int] NOT NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[RentalFee] [int] NULL,
	[CarId] [varchar](9) NULL,
	[CustomerId] [varchar](12) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReturnCar]    Script Date: 9/7/2024 11:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReturnCar](
	[Id] [int] NOT NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[RentalFee] [int] NULL,
	[ReturnDate] [date] NULL,
	[FineDelay] [int] NULL,
	[TotalAmount] [int] NULL,
	[CarId] [varchar](9) NULL,
	[CustomerId] [varchar](12) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 9/7/2024 11:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[id] [varchar](12) NOT NULL,
	[Fullname] [nvarchar](30) NULL,
	[Username] [varchar](36) NULL,
	[PassWordHash] [varchar](256) NULL,
	[Roles] [varchar](5) NULL,
	[UserStatus] [varchar](10) NULL,
	[Salt] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Car] ([Id], [Brand], [Model], [StatusCar], [Price]) VALUES (N'51K-77777', N'Toyota', N'camry 2020', N'Available', 850000)
INSERT [dbo].[Car] ([Id], [Brand], [Model], [StatusCar], [Price]) VALUES (N'54H-12345', N'Huyndai', N'a10', N'Available', 300000)
INSERT [dbo].[Car] ([Id], [Brand], [Model], [StatusCar], [Price]) VALUES (N'59A-88888', N'Suzuki', N'S9', N'Available', 500000)
INSERT [dbo].[Car] ([Id], [Brand], [Model], [StatusCar], [Price]) VALUES (N'72G-34521', N'Honda', N'Civic', N'Available', 700000)
INSERT [dbo].[Car] ([Id], [Brand], [Model], [StatusCar], [Price]) VALUES (N'77A-34567', N'MEC ', N'S300', N'Rented', 1300000)
GO
INSERT [dbo].[Customer] ([Id], [FullName], [Addr], [PhoneNumber]) VALUES (N'123456789', N'Huỳnh Chí Giáp', N'Cần Thơ', N'34567891')
INSERT [dbo].[Customer] ([Id], [FullName], [Addr], [PhoneNumber]) VALUES (N'215595523', N'Phạm Khánh Duy', N'Bình Định', N'123456789')
INSERT [dbo].[Customer] ([Id], [FullName], [Addr], [PhoneNumber]) VALUES (N'343576765', N'Hồ Trúc Điệp', N'Quãng Ngãi', N'984793456')
GO
INSERT [dbo].[ReturnCar] ([Id], [StartDate], [EndDate], [RentalFee], [ReturnDate], [FineDelay], [TotalAmount], [CarId], [CustomerId]) VALUES (1, CAST(N'2024-07-09' AS Date), CAST(N'2024-07-12' AS Date), 1500000, CAST(N'2024-07-12' AS Date), 500000, 2000000, N'59A-88888', N'215595523')
INSERT [dbo].[ReturnCar] ([Id], [StartDate], [EndDate], [RentalFee], [ReturnDate], [FineDelay], [TotalAmount], [CarId], [CustomerId]) VALUES (2, CAST(N'2024-09-07' AS Date), CAST(N'2024-09-08' AS Date), 500000, CAST(N'2024-09-11' AS Date), 2000000, 2500000, N'59A-88888', N'215595523')
INSERT [dbo].[ReturnCar] ([Id], [StartDate], [EndDate], [RentalFee], [ReturnDate], [FineDelay], [TotalAmount], [CarId], [CustomerId]) VALUES (3, CAST(N'2024-09-07' AS Date), CAST(N'2024-09-12' AS Date), 1500000, CAST(N'2024-09-19' AS Date), 2600000, 4100000, N'54H-12345', N'343576765')
INSERT [dbo].[ReturnCar] ([Id], [StartDate], [EndDate], [RentalFee], [ReturnDate], [FineDelay], [TotalAmount], [CarId], [CustomerId]) VALUES (5, CAST(N'2024-09-07' AS Date), CAST(N'2024-11-15' AS Date), 34500000, CAST(N'2024-11-29' AS Date), 7500000, 42000000, N'59A-88888', N'343576765')
INSERT [dbo].[ReturnCar] ([Id], [StartDate], [EndDate], [RentalFee], [ReturnDate], [FineDelay], [TotalAmount], [CarId], [CustomerId]) VALUES (6, CAST(N'2024-09-06' AS Date), CAST(N'2024-09-13' AS Date), 2100000, CAST(N'2024-09-19' AS Date), 2300000, 4400000, N'54H-12345', N'215595523')
GO
INSERT [dbo].[Users] ([id], [Fullname], [Username], [PassWordHash], [Roles], [UserStatus], [Salt]) VALUES (N'1', N'Admin', N'Admin', N'ljmloG2/DZsR12KydEuodYidDkY4eswpgJJxqGaUsbUlNUnxMBOJ30qu0MixIFGL', N'Admin', N'Active', N'ljmloG2/DZsR12KydEuodQ==')
INSERT [dbo].[Users] ([id], [Fullname], [Username], [PassWordHash], [Roles], [UserStatus], [Salt]) VALUES (N'2', N'User1', N'User1', N'+1tgnh7IEya/UpedJhxq7U4Oyem+8/sWtD28Tar+mO/LRmvD5Hmu4Y3fFqXiN2qS', N'User', N'Active', N'+1tgnh7IEya/UpedJhxq7Q==')
GO
ALTER TABLE [dbo].[Rental]  WITH CHECK ADD  CONSTRAINT [FK_Car_Rental] FOREIGN KEY([CarId])
REFERENCES [dbo].[Car] ([Id])
GO
ALTER TABLE [dbo].[Rental] CHECK CONSTRAINT [FK_Car_Rental]
GO
ALTER TABLE [dbo].[Rental]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Rentals] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[Rental] CHECK CONSTRAINT [FK_Customer_Rentals]
GO
ALTER TABLE [dbo].[ReturnCar]  WITH CHECK ADD  CONSTRAINT [FK_Car_Return] FOREIGN KEY([CarId])
REFERENCES [dbo].[Car] ([Id])
GO
ALTER TABLE [dbo].[ReturnCar] CHECK CONSTRAINT [FK_Car_Return]
GO
ALTER TABLE [dbo].[ReturnCar]  WITH CHECK ADD  CONSTRAINT [FK_Customer_Return] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
GO
ALTER TABLE [dbo].[ReturnCar] CHECK CONSTRAINT [FK_Customer_Return]
GO
USE [master]
GO
ALTER DATABASE [CarRental] SET  READ_WRITE 
GO
