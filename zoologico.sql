USE [master]
GO
/****** Object:  Database [zoologico]    Script Date: 11/12/2023 18:06:38 ******/
CREATE DATABASE [zoologico]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'zoologico', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\zoologico.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'zoologico_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\zoologico_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [zoologico] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [zoologico].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [zoologico] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [zoologico] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [zoologico] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [zoologico] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [zoologico] SET ARITHABORT OFF 
GO
ALTER DATABASE [zoologico] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [zoologico] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [zoologico] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [zoologico] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [zoologico] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [zoologico] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [zoologico] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [zoologico] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [zoologico] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [zoologico] SET  ENABLE_BROKER 
GO
ALTER DATABASE [zoologico] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [zoologico] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [zoologico] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [zoologico] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [zoologico] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [zoologico] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [zoologico] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [zoologico] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [zoologico] SET  MULTI_USER 
GO
ALTER DATABASE [zoologico] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [zoologico] SET DB_CHAINING OFF 
GO
ALTER DATABASE [zoologico] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [zoologico] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [zoologico] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [zoologico] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [zoologico] SET QUERY_STORE = ON
GO
ALTER DATABASE [zoologico] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [zoologico]
GO
/****** Object:  Table [dbo].[animales]    Script Date: 11/12/2023 18:06:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[animales](
	[id] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[edad] [int] NULL,
	[especie] [varchar](50) NOT NULL,
	[raza_id] [int] NOT NULL,
	[estado] [char](1) NOT NULL,
	[fecha_estado] [date] NOT NULL,
	[observacion_estado] [nvarchar](2000) NULL,
	[usuario_ingreso] [bigint] NOT NULL,
	[fecha_ingreso] [date] NOT NULL,
	[ubicacion_ingreso] [nvarchar](200) NOT NULL,
	[usuario_modificacion] [bigint] NULL,
	[fecha_modificacion] [date] NULL,
	[ubicacion_modificacion] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[razas]    Script Date: 11/12/2023 18:06:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[razas](
	[id] [int] NOT NULL,
	[nombre] [varchar](50) NOT NULL,
	[descripcion] [text] NOT NULL,
	[origen_geografico] [varchar](50) NULL,
	[estado] [char](1) NOT NULL,
	[fecha_estado] [date] NOT NULL,
	[observacion_estado] [nvarchar](2000) NULL,
	[usuario_ingreso] [bigint] NOT NULL,
	[fecha_ingreso] [date] NOT NULL,
	[ubicacion_ingreso] [nvarchar](200) NOT NULL,
	[usuario_modificacion] [bigint] NULL,
	[fecha_modificacion] [date] NULL,
	[ubicacion_modificacion] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[zoologico_secuencias_primarias]    Script Date: 11/12/2023 18:06:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[zoologico_secuencias_primarias](
	[codigo] [int] NOT NULL,
	[descripcion] [nvarchar](200) NOT NULL,
	[valor_inicial] [int] NOT NULL,
	[incrementa_en] [int] NOT NULL,
	[valor_actual] [int] NOT NULL,
	[estado] [char](1) NOT NULL,
	[fecha_estado] [date] NOT NULL,
	[observacion_estado] [nvarchar](2000) NULL,
	[usuario_ingreso] [bigint] NOT NULL,
	[fecha_ingreso] [date] NOT NULL,
	[ubicacion_ingreso] [nvarchar](200) NOT NULL,
	[usuario_modificacion] [bigint] NULL,
	[fecha_modificacion] [date] NULL,
	[ubicacion_modificacion] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[zoologico_secuencias_primarias] ADD  DEFAULT ((0)) FOR [valor_inicial]
GO
ALTER TABLE [dbo].[zoologico_secuencias_primarias] ADD  DEFAULT ((1)) FOR [incrementa_en]
GO
ALTER TABLE [dbo].[animales]  WITH CHECK ADD FOREIGN KEY([raza_id])
REFERENCES [dbo].[razas] ([id])
GO
USE [master]
GO
ALTER DATABASE [zoologico] SET  READ_WRITE 
GO
