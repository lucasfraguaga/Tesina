USE [master]
GO
/****** Object:  Database [RiegoAutonomo]    Script Date: 3/10/2024 23:36:47 ******/
CREATE DATABASE [RiegoAutonomo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RiegoAutonomo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\RiegoAutonomo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'RiegoAutonomo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\RiegoAutonomo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [RiegoAutonomo] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RiegoAutonomo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RiegoAutonomo] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RiegoAutonomo] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RiegoAutonomo] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RiegoAutonomo] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RiegoAutonomo] SET ARITHABORT OFF 
GO
ALTER DATABASE [RiegoAutonomo] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RiegoAutonomo] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RiegoAutonomo] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RiegoAutonomo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RiegoAutonomo] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RiegoAutonomo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RiegoAutonomo] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RiegoAutonomo] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RiegoAutonomo] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RiegoAutonomo] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RiegoAutonomo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RiegoAutonomo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RiegoAutonomo] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RiegoAutonomo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RiegoAutonomo] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RiegoAutonomo] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RiegoAutonomo] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RiegoAutonomo] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RiegoAutonomo] SET  MULTI_USER 
GO
ALTER DATABASE [RiegoAutonomo] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RiegoAutonomo] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RiegoAutonomo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RiegoAutonomo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [RiegoAutonomo] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [RiegoAutonomo] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [RiegoAutonomo] SET QUERY_STORE = ON
GO
ALTER DATABASE [RiegoAutonomo] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [RiegoAutonomo]
GO
/****** Object:  UserDefinedTableType [dbo].[ItemIdiomaNuevoDisplayType]    Script Date: 3/10/2024 23:36:47 ******/
CREATE TYPE [dbo].[ItemIdiomaNuevoDisplayType] AS TABLE(
	[Id] [int] NULL,
	[Traduccion] [nvarchar](max) NULL
)
GO
/****** Object:  UserDefinedFunction [dbo].[CalcularDigitoVerificador]    Script Date: 3/10/2024 23:36:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[CalcularDigitoVerificador]
(
    @id INT,
    @idUsuario INT,
    @fecha DATETIME,
    @mensaje NVARCHAR(400)
)
RETURNS CHAR(1)
AS
BEGIN
    DECLARE @checksum CHAR(1)

    -- Puedes usar HASHBYTES para generar un hash basado en los datos
    SET @checksum = CONVERT(CHAR(1), 
                      (ABS(CHECKSUM(@id, @idUsuario, @fecha, @mensaje)) % 10))

    RETURN @checksum
END
GO
/****** Object:  UserDefinedFunction [dbo].[fn_CalcularDigitoVerificadorVertical]    Script Date: 3/10/2024 23:36:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_CalcularDigitoVerificadorVertical]()
RETURNS NVARCHAR(32)
AS
BEGIN
    DECLARE @hash NVARCHAR(32);

    SELECT @hash = HASHBYTES('MD5', (
        SELECT
            CAST(id AS NVARCHAR(50)) + CAST(idUsuario AS NVARCHAR(50)) + CAST(fecha AS NVARCHAR(50)) + CAST(mensaje AS NVARCHAR(400))
        FROM Bitacora
        FOR XML PATH('')
    ));

    RETURN CONVERT(NVARCHAR(32), @hash, 2);
END
GO
/****** Object:  Table [dbo].[Bitacora]    Script Date: 3/10/2024 23:36:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bitacora](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [int] NOT NULL,
	[fecha] [datetime] NOT NULL,
	[mensaje] [nvarchar](400) NOT NULL,
	[digitoVerificador] [char](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BitacoraCambios]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BitacoraCambios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NULL,
	[Mensaje] [nvarchar](255) NULL,
	[Fecha] [datetime] NULL,
	[FechaCambio] [datetime] NULL,
	[Version] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[id] [int] NOT NULL,
	[nombre] [nvarchar](50) NULL,
	[apellido] [nvarchar](50) NULL,
	[mail] [nvarchar](100) NULL,
	[telefono] [int] NULL,
	[dni] [int] NULL,
	[idUsuario] [int] NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Content]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Content](
	[id] [int] NOT NULL,
	[ContentId] [nvarchar](100) NULL,
 CONSTRAINT [PK_Content] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cultivo]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cultivo](
	[id] [int] NOT NULL,
	[descripcion] [nvarchar](max) NULL,
	[tipo] [nvarchar](100) NULL,
 CONSTRAINT [PK_Cultivo] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cultivo_Formulario]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cultivo_Formulario](
	[idCultivo] [int] NOT NULL,
	[idFormulario] [int] NOT NULL,
	[cantidad] [int] NULL,
 CONSTRAINT [PK_Cultivo_Formulario] PRIMARY KEY CLUSTERED 
(
	[idCultivo] ASC,
	[idFormulario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DigitoVerificadorVertical]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DigitoVerificadorVertical](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tabla] [nvarchar](50) NULL,
	[digitoVerificador] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DispositivoAgua]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DispositivoAgua](
	[id] [int] NOT NULL,
	[descripcion] [nvarchar](max) NULL,
	[nombre] [nvarchar](50) NULL,
	[precio] [float] NULL,
 CONSTRAINT [PK_DispositivoAgua] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DispositivoAgua_Formulario]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DispositivoAgua_Formulario](
	[idDispositivoAgua] [int] NOT NULL,
	[idFormulario] [int] NOT NULL,
	[cantidad] [int] NULL,
 CONSTRAINT [PK_DispositivoAgua_Formulario] PRIMARY KEY CLUSTERED 
(
	[idDispositivoAgua] ASC,
	[idFormulario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Fecha_Instalacion]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fecha_Instalacion](
	[id_Instalacion] [int] NOT NULL,
	[id_Formulario] [int] NULL,
	[fecha] [datetime] NULL,
	[descripcion] [nvarchar](max) NOT NULL,
	[estado] [nvarchar](50) NULL,
 CONSTRAINT [PK_FechaInstalacion] PRIMARY KEY CLUSTERED 
(
	[id_Instalacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Formulario]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Formulario](
	[id] [int] NOT NULL,
	[descripcionAgua] [nvarchar](max) NULL,
	[descripcionZona] [nvarchar](max) NULL,
	[disponibilidadAgua] [bit] NULL,
	[direccion] [nvarchar](max) NULL,
	[distanciaCubrir] [nvarchar](max) NULL,
	[estadoFabricacion] [nvarchar](100) NULL,
	[estadoPago] [nvarchar](100) NULL,
	[viabilidad] [bit] NULL,
	[idCliente] [int] NULL,
	[descripcionViabilidad] [nvarchar](max) NULL,
	[viabilidadEquipo] [bit] NULL,
	[descripcionViabilidadEquipo] [nvarchar](max) NULL,
	[estadoInstalacion] [nvarchar](100) NULL,
 CONSTRAINT [PK_Formulario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Languages]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Languages](
	[LanguageId] [int] NOT NULL,
	[LanguageCode] [nvarchar](10) NOT NULL,
	[LanguageName] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Materiales]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Materiales](
	[id] [int] NOT NULL,
	[comentario] [nvarchar](max) NULL,
	[cantEquipos] [int] NULL,
	[conductoAgua] [nvarchar](50) NULL,
	[precioConducto] [float] NULL,
	[precioEquipo] [float] NULL,
	[idFormulario] [int] NOT NULL,
 CONSTRAINT [PK_Materiales] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PedidoCreacion]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PedidoCreacion](
	[id] [int] NOT NULL,
	[idFormulario] [int] NULL,
	[contraseña] [nvarchar](max) NULL,
	[usuario] [nvarchar](max) NULL,
	[estado] [nvarchar](100) NULL,
 CONSTRAINT [PK_PedidoCreacion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[permiso]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[permiso](
	[nombre] [varchar](100) NULL,
	[id] [int] NOT NULL,
	[permiso] [varchar](50) NULL,
 CONSTRAINT [PK_permiso] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[permiso_permiso]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[permiso_permiso](
	[id_permiso_padre] [int] NULL,
	[id_permiso_hijo] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sensor]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sensor](
	[id] [int] NOT NULL,
	[descripcion] [nvarchar](max) NULL,
	[nombre] [nvarchar](max) NULL,
	[precio] [float] NULL,
 CONSTRAINT [PK_Sensor] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sensor_Formulario]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sensor_Formulario](
	[idSensor] [int] NOT NULL,
	[idFormulario] [int] NOT NULL,
	[cantidad] [nchar](10) NULL,
 CONSTRAINT [PK_Sensor_Formulario] PRIMARY KEY CLUSTERED 
(
	[idSensor] ASC,
	[idFormulario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[nombre] [varchar](50) NOT NULL,
	[contrasena] [nvarchar](255) NULL,
	[id_usuario] [int] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuarios_permisos]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarios_permisos](
	[id_usuario] [int] NOT NULL,
	[id_permiso] [int] NOT NULL,
 CONSTRAINT [PK_usuario_permisos] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC,
	[id_permiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VersionContent]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VersionContent](
	[ContentId] [int] NOT NULL,
	[LanguageId] [int] NOT NULL,
	[Content] [nvarchar](max) NULL,
 CONSTRAINT [PK_VersionContent] PRIMARY KEY CLUSTERED 
(
	[ContentId] ASC,
	[LanguageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Bitacora] ON 

INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (6, 2, CAST(N'2024-09-30T10:17:17.000' AS DateTime), N'Ingreso a la pantalla admin', N'4')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (7, 1, CAST(N'2024-09-30T10:17:52.000' AS DateTime), N'Ingreso de usuario', N'2')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (8, 1, CAST(N'2024-09-30T10:17:52.000' AS DateTime), N'Ingreso a la pantalla admin', N'2')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (9, 1, CAST(N'2024-09-30T10:19:12.000' AS DateTime), N'Ingreso de usuario', N'0')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (10, 1, CAST(N'2024-09-30T10:19:12.000' AS DateTime), N'Ingreso a la pantalla admin', N'8')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (11, 1, CAST(N'2024-09-30T10:23:18.000' AS DateTime), N'Ingreso de usuario', N'4')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (12, 1, CAST(N'2024-09-30T10:23:19.000' AS DateTime), N'Ingreso a la pantalla admin', N'6')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (13, 1, CAST(N'2024-09-30T10:25:20.000' AS DateTime), N'Ingreso de usuario', N'0')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (14, 1, CAST(N'2024-09-30T10:25:20.000' AS DateTime), N'Ingreso a la pantalla admin', N'6')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (15, 1, CAST(N'2024-09-30T10:25:58.000' AS DateTime), N'Ingreso de usuario', N'6')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (16, 1, CAST(N'2024-09-30T10:25:58.000' AS DateTime), N'Ingreso a la pantalla admin', N'6')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (17, 1, CAST(N'2024-09-30T10:41:28.000' AS DateTime), N'Ingreso de usuario', N'8')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (18, 1, CAST(N'2024-09-30T10:41:28.000' AS DateTime), N'Ingreso a la pantalla admin', N'6')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (19, 1, CAST(N'2024-09-30T10:41:34.000' AS DateTime), N'La tabla Bitacora tiene registros corruptos y son: 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18 ', N'3')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (20, 1, CAST(N'2024-09-30T10:42:14.000' AS DateTime), N'Ingreso de usuario', N'8')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (21, 1, CAST(N'2024-09-30T10:42:14.000' AS DateTime), N'Ingreso a la pantalla admin', N'0')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (22, 1, CAST(N'2024-09-30T10:42:15.000' AS DateTime), N'Ingreso a la pantalla bitacora', N'5')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (23, 1, CAST(N'2024-09-30T10:45:17.000' AS DateTime), N'Ingreso de usuario', N'2')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (24, 1, CAST(N'2024-09-30T10:45:17.000' AS DateTime), N'Ingreso a la pantalla admin', N'6')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (25, 1, CAST(N'2024-09-30T10:45:51.000' AS DateTime), N'Ingreso de usuario', N'0')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (26, 1, CAST(N'2024-09-30T10:45:51.000' AS DateTime), N'Ingreso a la pantalla admin', N'8')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (27, 1, CAST(N'2024-09-30T10:45:57.000' AS DateTime), N'La tabla Bitacora tiene registros corruptos y son: 2 ', N'9')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (28, 1, CAST(N'2024-09-30T10:57:25.000' AS DateTime), N'Ingreso de usuario', N'6')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (29, 1, CAST(N'2024-09-30T10:58:16.000' AS DateTime), N'Ingreso de usuario', N'8')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (30, 1, CAST(N'2024-09-30T11:01:07.000' AS DateTime), N'Ingreso de usuario', N'6')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (31, 1, CAST(N'2024-09-30T11:01:07.000' AS DateTime), N'Ingreso a la pantalla admin', N'4')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (32, 1, CAST(N'2024-09-30T11:01:10.000' AS DateTime), N'Ingreso a la pantalla bitacora', N'3')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (33, 1, CAST(N'2024-09-30T11:03:08.000' AS DateTime), N'Ingreso de usuario', N'6')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (34, 1, CAST(N'2024-09-30T11:03:08.000' AS DateTime), N'Ingreso a la pantalla admin', N'6')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (35, 1, CAST(N'2024-09-30T11:03:09.000' AS DateTime), N'Ingreso a la pantalla bitacora', N'7')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (36, 1, CAST(N'2024-09-30T11:11:40.000' AS DateTime), N'Ingreso de usuario', N'8')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (37, 1, CAST(N'2024-09-30T11:11:40.000' AS DateTime), N'Ingreso a la pantalla admin', N'8')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (38, 1, CAST(N'2024-09-30T11:12:00.000' AS DateTime), N'Ingreso de usuario', N'0')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (39, 1, CAST(N'2024-09-30T11:12:00.000' AS DateTime), N'Ingreso a la pantalla admin', N'0')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (40, 1, CAST(N'2024-09-30T11:12:59.000' AS DateTime), N'Ingreso de usuario', N'2')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (41, 1, CAST(N'2024-09-30T11:12:59.000' AS DateTime), N'Ingreso a la pantalla admin', N'8')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (42, 1, CAST(N'2024-09-30T11:14:46.000' AS DateTime), N'Ingreso de usuario', N'4')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (43, 1, CAST(N'2024-09-30T11:14:46.000' AS DateTime), N'Ingreso a la pantalla admin', N'2')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (44, 1, CAST(N'2024-09-30T11:15:04.000' AS DateTime), N'Ingreso de usuario', N'0')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (45, 1, CAST(N'2024-09-30T11:15:04.000' AS DateTime), N'Ingreso a la pantalla admin', N'8')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (46, 1, CAST(N'2024-09-30T11:15:07.000' AS DateTime), N'La tabla Bitacora tiene registros corruptos y son: 2 ', N'1')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (47, 1, CAST(N'2024-09-30T11:15:29.000' AS DateTime), N'Ingreso de usuario', N'2')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (48, 1, CAST(N'2024-09-30T11:15:29.000' AS DateTime), N'Ingreso a la pantalla admin', N'2')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (49, 1, CAST(N'2024-09-30T11:17:09.000' AS DateTime), N'Ingreso de usuario', N'8')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (50, 1, CAST(N'2024-09-30T11:17:09.000' AS DateTime), N'Ingreso a la pantalla admin', N'6')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (51, 1, CAST(N'2024-09-30T11:17:10.000' AS DateTime), N'La tabla Bitacora tiene registros corruptos y son: 3 ', N'5')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (53, 0, CAST(N'2024-09-30T11:30:00.000' AS DateTime), N'Se forzo solucion de digito verificador vertical', N'4')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (54, 0, CAST(N'2024-09-30T11:30:39.000' AS DateTime), N'Se forzo solucion de digito verificador vertical', N'2')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (55, 1, CAST(N'2024-09-30T11:30:47.000' AS DateTime), N'Ingreso de usuario', N'4')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (56, 1, CAST(N'2024-09-30T11:30:47.000' AS DateTime), N'Ingreso a la pantalla admin', N'0')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (57, 1, CAST(N'2024-09-30T11:30:48.000' AS DateTime), N'Ingreso a la pantalla bitacora', N'5')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (58, 0, CAST(N'2024-09-30T11:31:12.000' AS DateTime), N'Se forzo solucion de digito verificador vertical', N'0')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (59, 1, CAST(N'2024-09-30T11:31:14.000' AS DateTime), N'Ingreso de usuario', N'6')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (60, 1, CAST(N'2024-09-30T11:31:14.000' AS DateTime), N'Ingreso a la pantalla admin', N'0')
INSERT [dbo].[Bitacora] ([id], [idUsuario], [fecha], [mensaje], [digitoVerificador]) VALUES (61, 1, CAST(N'2024-09-30T11:31:16.000' AS DateTime), N'La tabla Bitacora tiene registros corruptos y son: 6 ', N'1')
SET IDENTITY_INSERT [dbo].[Bitacora] OFF
GO
SET IDENTITY_INSERT [dbo].[BitacoraCambios] ON 

INSERT [dbo].[BitacoraCambios] ([Id], [IdUsuario], [Mensaje], [Fecha], [FechaCambio], [Version]) VALUES (1, 1, N'cambio', CAST(N'2024-09-30T14:02:41.750' AS DateTime), CAST(N'2024-09-30T14:02:41.753' AS DateTime), 1)
INSERT [dbo].[BitacoraCambios] ([Id], [IdUsuario], [Mensaje], [Fecha], [FechaCambio], [Version]) VALUES (2, 2, N'cambio 2', CAST(N'2024-09-30T14:03:51.783' AS DateTime), CAST(N'2024-09-30T14:03:51.787' AS DateTime), 2)
SET IDENTITY_INSERT [dbo].[BitacoraCambios] OFF
GO
INSERT [dbo].[Cliente] ([id], [nombre], [apellido], [mail], [telefono], [dni], [idUsuario]) VALUES (1, N'lucas', N'fraguaga', N'lucasfraguaga@live.com', 1162045499, 41130624, 9)
INSERT [dbo].[Cliente] ([id], [nombre], [apellido], [mail], [telefono], [dni], [idUsuario]) VALUES (2, N'prueba', N'', N'', 0, 0, NULL)
INSERT [dbo].[Cliente] ([id], [nombre], [apellido], [mail], [telefono], [dni], [idUsuario]) VALUES (3, N'pablo', N'roldan', N'pablo@pablo', 1111111, 11111111, 10)
GO
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (1, N'ABM Roles')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (115, N'Aceptado')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (2, N'Aceptar fecha')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (3, N'Aceptar ficha')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (4, N'Aceptar formulario')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (82, N'Actualizar formulario')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (51, N'Agregar >>')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (60, N'Agregar Familias')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (58, N'Agregar patentes')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (66, N'Analizar farbricacion')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (65, N'Analizar materiales de formulario')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (110, N'Apellido')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (5, N'Arbol usuario seleccionado')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (126, N'Calcular presupuesto')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (118, N'Cambiar a aceptado')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (119, N'Cambiar a finalizado')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (139, N'Cambiar estado a aceptado')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (135, N'Cambiar estado a fabricacio')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (141, N'Cambiar estado a fabricacion')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (136, N'Cambiar estado a terminado')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (6, N'Cambiar estado de fabricacion')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (138, N'Cambiar fecha')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (117, N'Cambiar fecha propuesta')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (7, N'Cambiar usuario')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (80, N'Cantidad')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (94, N'Cantidad equipos')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (84, N'Cargar cultivo')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (102, N'Cargar dipositivo agua')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (8, N'Cargar fecha')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (129, N'Cargar fecha instalacion')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (9, N'Cargar formulario')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (106, N'Cargar materiales')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (124, N'Cargar pago de seña')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (125, N'Cargar pago total')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (104, N'Cargar sensor')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (10, N'Categoria')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (44, N'Codigo de lenguaje')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (98, N'Comentarios')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (11, N'Completar instalacion')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (96, N'Conductor agua')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (12, N'Conectar')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (52, N'Configurar')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (56, N'Configurar Familia')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (13, N'Contraseña')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (14, N'Creacion de usuario')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (15, N'Crear formulario')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (43, N'Crear Idioma')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (16, N'Crear pedido')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (130, N'Crear pedido de creacion usuario')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (17, N'Crear pedido premium')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (18, N'Crear permiso')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (19, N'Crear rama permiso')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (20, N'Crear Usuario')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (83, N'Crear y cargar cultivo')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (105, N'Crear y cargar dipositivo agua')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (103, N'Crear y cargar sensor')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (75, N'Cultivo cargados')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (86, N'Cultivos')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (81, N'Cultivos precargados')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (109, N'Datos cliente')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (68, N'Datos formulario')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (21, N'Desconectarse')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (22, N'Descripcion')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (99, N'Descripcion agua')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (78, N'Descripcion cultivo')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (87, N'Descripcion sensor')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (69, N'DescripcionAgua')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (74, N'DescripcionViabilidad')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (140, N'DescripcionViabilidadEquipo')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (71, N'DescripcionZona')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (134, N'DipositivosAgua')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (72, N'Direccion')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (70, N'DisponibilidadAgua')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (101, N'Dispositivos agua precargados')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (93, N'Dispositivos de agua cargados')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (73, N'Distancia a cubrir')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (85, N'DistanciaCubrir')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (111, N'DNI')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (23, N'Enviar a fabricacion')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (55, N'Familia')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (120, N'Fecha')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (137, N'Fecha cargada')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (116, N'Finalizado')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (24, N'Finalizar pago')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (131, N'Formulario')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (77, N'Formulario seleccionado')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (25, N'Generar ficha tecnica')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (26, N'Gestiones')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (50, N'Guardar')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (61, N'Guardar cambios')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (53, N'Guardar familia')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (27, N'Ingrese el usuario')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (28, N'Ingrese la contraseña')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (29, N'Lenguaje')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (112, N'Mail')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (132, N'Materiales')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (91, N'Materiales cargados')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (30, N'Modificar usuario')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (122, N'Monto a pagar')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (123, N'Monto de la seña')
GO
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (31, N'MostrarContraseña')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (49, N'Nombre')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (45, N'Nombre de lenguaje')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (54, N'Nueva')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (128, N'Pagos de formulario')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (62, N'Patente y Familias')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (57, N'Patentes')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (64, N'Pedido creacion cliente')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (127, N'Pedido creacion de usuario')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (32, N'Pedidos')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (47, N'Permiso')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (33, N'Permiso 1')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (34, N'Permiso 2')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (89, N'Precio')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (97, N'Precio conductor agua')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (95, N'Precio equipo')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (114, N'Propuesto')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (35, N'Recetear contraseña')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (36, N'Rechazar fecha')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (37, N'Rechazar formulario')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (38, N'Registrar usuario')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (63, N'Roles Usuarios')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (39, N'Roll')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (67, N'Seleccionar formulario')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (108, N'Seleccionar pedido')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (133, N'Sensores')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (92, N'Sensores cargados')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (90, N'Sensores precargados')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (113, N'Telefono')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (79, N'Tipo cultivo')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (100, N'Tipo dispositivo agua')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (88, N'Tipo sensor')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (48, N'Todas las patentes')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (107, N'Todos los pedidos')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (59, N'Todos los usuarios')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (40, N'Usuario')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (41, N'Usuarios')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (121, N'Ver fechas instalacion')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (76, N'Viabilidad')
INSERT [dbo].[Content] ([id], [ContentId]) VALUES (42, N'Volver')
GO
INSERT [dbo].[Cultivo] ([id], [descripcion], [tipo]) VALUES (1, N'Tiene hojas fuertemente aromáticas con bordes dentados. Florece con abundancia y sus flores pequeñas y amarillas producen frutos muy coloreados —de tonos que van del amarillento al rojo—, debido a la presencia de pigmentos como el licopeno y los carotenos.', N'tomate')
INSERT [dbo].[Cultivo] ([id], [descripcion], [tipo]) VALUES (2, N'árbol siempreverde de hasta 15 metros de altura, de tronco recto, corto y corteza rugosa. Hojas grandes, verdes, simples, alternas, de 6 - 30 cm de largo, que forman un ramaje denso y muy abundante. Flores pequeñas, arracimadas, fragantes, blanco-verdosas, 1 - 3 cm de ancho.', N'palta')
INSERT [dbo].[Cultivo] ([id], [descripcion], [tipo]) VALUES (3, N'crece abajo de la tierra', N'papa')
GO
INSERT [dbo].[Cultivo_Formulario] ([idCultivo], [idFormulario], [cantidad]) VALUES (1, 1, 4)
INSERT [dbo].[Cultivo_Formulario] ([idCultivo], [idFormulario], [cantidad]) VALUES (1, 2, 2)
INSERT [dbo].[Cultivo_Formulario] ([idCultivo], [idFormulario], [cantidad]) VALUES (1, 3, 12)
INSERT [dbo].[Cultivo_Formulario] ([idCultivo], [idFormulario], [cantidad]) VALUES (2, 2, 10000)
INSERT [dbo].[Cultivo_Formulario] ([idCultivo], [idFormulario], [cantidad]) VALUES (2, 3, 1)
GO
SET IDENTITY_INSERT [dbo].[DigitoVerificadorVertical] ON 

INSERT [dbo].[DigitoVerificadorVertical] ([id], [tabla], [digitoVerificador]) VALUES (1, N'Bitacora', N'ፐ䩙賿�丁慲戢')
SET IDENTITY_INSERT [dbo].[DigitoVerificadorVertical] OFF
GO
INSERT [dbo].[DispositivoAgua] ([id], [descripcion], [nombre], [precio]) VALUES (1, N'motor para agua en deposito', N'deposito', 5)
INSERT [dbo].[DispositivoAgua] ([id], [descripcion], [nombre], [precio]) VALUES (2, N'manejador de canillas', N'canilla', 10)
GO
INSERT [dbo].[DispositivoAgua_Formulario] ([idDispositivoAgua], [idFormulario], [cantidad]) VALUES (1, 3, 2)
GO
INSERT [dbo].[Fecha_Instalacion] ([id_Instalacion], [id_Formulario], [fecha], [descripcion], [estado]) VALUES (1, 1, CAST(N'2024-07-28T00:00:00.000' AS DateTime), N'', N'finalizado')
INSERT [dbo].[Fecha_Instalacion] ([id_Instalacion], [id_Formulario], [fecha], [descripcion], [estado]) VALUES (2, 2, CAST(N'2024-07-28T00:00:00.000' AS DateTime), N'', N'aceptado')
INSERT [dbo].[Fecha_Instalacion] ([id_Instalacion], [id_Formulario], [fecha], [descripcion], [estado]) VALUES (3, 3, CAST(N'2024-09-24T00:00:00.000' AS DateTime), N'el cliente quiere esa fecha', N'finalizado')
GO
INSERT [dbo].[Formulario] ([id], [descripcionAgua], [descripcionZona], [disponibilidadAgua], [direccion], [distanciaCubrir], [estadoFabricacion], [estadoPago], [viabilidad], [idCliente], [descripcionViabilidad], [viabilidadEquipo], [descripcionViabilidadEquipo], [estadoInstalacion]) VALUES (1, N'llega la lluvia, hay que poner un tacho con agua', N'es un balcon', 1, N'asd', N'1 metro por 8 metros', N'terminado', N'pago total', 1, 1, N'es viabl', 1, N'es viable hacer el trabajo', NULL)
INSERT [dbo].[Formulario] ([id], [descripcionAgua], [descripcionZona], [disponibilidadAgua], [direccion], [distanciaCubrir], [estadoFabricacion], [estadoPago], [viabilidad], [idCliente], [descripcionViabilidad], [viabilidadEquipo], [descripcionViabilidadEquipo], [estadoInstalacion]) VALUES (2, N'fdghdfghdf', N'dfghdfghdfg', 1, N'dfghdfghdf', N'hdfghdfghdfgh', N'en fabricacion', NULL, 1, 2, N'dfghdfghdfgh', 0, N'no es viable', NULL)
INSERT [dbo].[Formulario] ([id], [descripcionAgua], [descripcionZona], [disponibilidadAgua], [direccion], [distanciaCubrir], [estadoFabricacion], [estadoPago], [viabilidad], [idCliente], [descripcionViabilidad], [viabilidadEquipo], [descripcionViabilidadEquipo], [estadoInstalacion]) VALUES (3, N'llega lluvia, hay que poner un tacho', N'balcon', 1, N'asd', N'1 metro por 8 metros', N'terminado', N'pago total', 1, 3, N'es viable', 1, N'yo digo que va a funcionar', NULL)
GO
INSERT [dbo].[Languages] ([LanguageId], [LanguageCode], [LanguageName]) VALUES (1, N'ES', N'ESPAÑOL')
INSERT [dbo].[Languages] ([LanguageId], [LanguageCode], [LanguageName]) VALUES (2, N'EN', N'INGLES')
GO
INSERT [dbo].[Materiales] ([id], [comentario], [cantEquipos], [conductoAgua], [precioConducto], [precioEquipo], [idFormulario]) VALUES (1, N'va a funcionar', 2, N'mangera 4 metros', 8, 30, 1)
INSERT [dbo].[Materiales] ([id], [comentario], [cantEquipos], [conductoAgua], [precioConducto], [precioEquipo], [idFormulario]) VALUES (2, N'tercer intento', 4, N'manguera', 8, 20, 3)
GO
INSERT [dbo].[PedidoCreacion] ([id], [idFormulario], [contraseña], [usuario], [estado]) VALUES (1, 1, N'1234', N'usuario1', N'pedido')
INSERT [dbo].[PedidoCreacion] ([id], [idFormulario], [contraseña], [usuario], [estado]) VALUES (2, 3, N'1234', N'usuario2', N'pedido')
GO
INSERT [dbo].[permiso] ([nombre], [id], [permiso]) VALUES (N'administrador', 1, N'PuedeHacerTodo')
INSERT [dbo].[permiso] ([nombre], [id], [permiso]) VALUES (N'admin', 2, NULL)
INSERT [dbo].[permiso] ([nombre], [id], [permiso]) VALUES (N'vendedor', 3, N'PuedeVender')
INSERT [dbo].[permiso] ([nombre], [id], [permiso]) VALUES (N'instalador', 4, N'PuedeInstalar')
INSERT [dbo].[permiso] ([nombre], [id], [permiso]) VALUES (N'analistaEquipo', 5, N'PuedeVerEquipos')
INSERT [dbo].[permiso] ([nombre], [id], [permiso]) VALUES (N'analistaCampo', 6, N'PuedeAnalizarCampo')
INSERT [dbo].[permiso] ([nombre], [id], [permiso]) VALUES (N'usuario', 7, N'PuedeHacerTodo')
INSERT [dbo].[permiso] ([nombre], [id], [permiso]) VALUES (N'cliente', 8, N'PuedeHacerTodo')
GO
INSERT [dbo].[permiso_permiso] ([id_permiso_padre], [id_permiso_hijo]) VALUES (2, 1)
INSERT [dbo].[permiso_permiso] ([id_permiso_padre], [id_permiso_hijo]) VALUES (2, 4)
INSERT [dbo].[permiso_permiso] ([id_permiso_padre], [id_permiso_hijo]) VALUES (2, 5)
INSERT [dbo].[permiso_permiso] ([id_permiso_padre], [id_permiso_hijo]) VALUES (2, 6)
GO
INSERT [dbo].[Sensor] ([id], [descripcion], [nombre], [precio]) VALUES (1, N'sensor de humerdad tierra', N'humeda', 15)
INSERT [dbo].[Sensor] ([id], [descripcion], [nombre], [precio]) VALUES (2, N'sensor de ph tierra', N'ph tierra', 20)
INSERT [dbo].[Sensor] ([id], [descripcion], [nombre], [precio]) VALUES (3, N'sensor de lluvia', N'lluvia', 10)
INSERT [dbo].[Sensor] ([id], [descripcion], [nombre], [precio]) VALUES (4, N'sensor que mide el sol', N'sol', 10)
GO
INSERT [dbo].[Sensor_Formulario] ([idSensor], [idFormulario], [cantidad]) VALUES (1, 1, N'1         ')
INSERT [dbo].[Sensor_Formulario] ([idSensor], [idFormulario], [cantidad]) VALUES (1, 3, N'4         ')
INSERT [dbo].[Sensor_Formulario] ([idSensor], [idFormulario], [cantidad]) VALUES (2, 1, N'1         ')
INSERT [dbo].[Sensor_Formulario] ([idSensor], [idFormulario], [cantidad]) VALUES (3, 1, N'2         ')
INSERT [dbo].[Sensor_Formulario] ([idSensor], [idFormulario], [cantidad]) VALUES (3, 3, N'1         ')
INSERT [dbo].[Sensor_Formulario] ([idSensor], [idFormulario], [cantidad]) VALUES (4, 1, N'1         ')
INSERT [dbo].[Sensor_Formulario] ([idSensor], [idFormulario], [cantidad]) VALUES (4, 3, N'1         ')
GO
INSERT [dbo].[Usuario] ([nombre], [contrasena], [id_usuario]) VALUES (N'Bitacora', N'123', 0)
INSERT [dbo].[Usuario] ([nombre], [contrasena], [id_usuario]) VALUES (N'lucas', N'$2a$10$nXd36HrdiiTvPDBZGgGtV.vETXEZ6mK7rYp.qXLkjWefFUSlnry/K', 1)
INSERT [dbo].[Usuario] ([nombre], [contrasena], [id_usuario]) VALUES (N'lucasPrueba2', N'$2a$10$WkgXt/xflEpC.czbgtn2.etSyK8fGDIPVzXAB2KjoYsaWQ2IWbVJW', 2)
INSERT [dbo].[Usuario] ([nombre], [contrasena], [id_usuario]) VALUES (N'vendedor', N'$2a$10$ksmkpHt9OVFc42dg5K2cs.Imsm0PO3LAccSZuMKqCXc7DTOs5LWTO', 3)
INSERT [dbo].[Usuario] ([nombre], [contrasena], [id_usuario]) VALUES (N'vendedor2', N'$2a$10$ksmkpHt9OVFc42dg5K2cs.Imsm0PO3LAccSZuMKqCXc7DTOs5LWTO', 4)
INSERT [dbo].[Usuario] ([nombre], [contrasena], [id_usuario]) VALUES (N'equipo', N'$2a$10$wO5Hg9Mthpam3lQIgA4qO.TKZWXB0Md2i3lswQ5LmHwgpmn2t9qqi', 5)
INSERT [dbo].[Usuario] ([nombre], [contrasena], [id_usuario]) VALUES (N'campo', N'$2a$10$NEkotBB5Q3DbnlnPHk/czeEH0rdWr.4O/74K4NBIVqNn3XDENjvXy', 6)
INSERT [dbo].[Usuario] ([nombre], [contrasena], [id_usuario]) VALUES (N'instalador', N'$2a$10$Hs8EShmQa6J/Kfd0VUzcMuDU.Q6EhnRGgAh2x.fnKees3GzV6j7dG', 7)
INSERT [dbo].[Usuario] ([nombre], [contrasena], [id_usuario]) VALUES (N'lucass', N'$2a$10$ULuMuZ7drt/DKzk8T10wPu5FUy6kvwHpml2I/k2lf.rnv.65Cz7Wu', 8)
INSERT [dbo].[Usuario] ([nombre], [contrasena], [id_usuario]) VALUES (N'usuario1', N'$2a$10$Pdd3jUlWcy6FRrpi6npWAuj9K0f0V49gKjtHCi6oDYS5ixxjRrvN6', 9)
INSERT [dbo].[Usuario] ([nombre], [contrasena], [id_usuario]) VALUES (N'usuario2', N'$2a$10$GeRKTnHnNkcIXmx90rpONOgjDKfJapLC5SEPecMQq9Z.78BQ1D2Zq', 10)
GO
INSERT [dbo].[usuarios_permisos] ([id_usuario], [id_permiso]) VALUES (1, 1)
INSERT [dbo].[usuarios_permisos] ([id_usuario], [id_permiso]) VALUES (1, 2)
INSERT [dbo].[usuarios_permisos] ([id_usuario], [id_permiso]) VALUES (1, 3)
INSERT [dbo].[usuarios_permisos] ([id_usuario], [id_permiso]) VALUES (3, 3)
INSERT [dbo].[usuarios_permisos] ([id_usuario], [id_permiso]) VALUES (5, 5)
INSERT [dbo].[usuarios_permisos] ([id_usuario], [id_permiso]) VALUES (6, 6)
INSERT [dbo].[usuarios_permisos] ([id_usuario], [id_permiso]) VALUES (7, 4)
INSERT [dbo].[usuarios_permisos] ([id_usuario], [id_permiso]) VALUES (8, 3)
INSERT [dbo].[usuarios_permisos] ([id_usuario], [id_permiso]) VALUES (9, 8)
INSERT [dbo].[usuarios_permisos] ([id_usuario], [id_permiso]) VALUES (10, 8)
GO
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (1, 1, N'ABM Roles')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (1, 2, N'ABM Roles')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (2, 1, N'Aceptar fecha')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (2, 2, N'Accept date')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (3, 1, N'Aceptar ficha')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (3, 2, N'Accept file')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (4, 1, N'Aceptar formulario')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (4, 2, N'Accept form')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (5, 1, N'Argol usuario seleccionado')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (5, 2, N'Selected user tree')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (6, 1, N'Cambiar estado de fabricacion')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (6, 2, N'Change manufacturing status')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (7, 1, N'Cambiar usuario')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (7, 2, N'Change user')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (8, 1, N'Cargar fecha')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (8, 2, N'Load date')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (9, 1, N'Cargar formulario')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (9, 2, N'Load form')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (10, 1, N'Categoria')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (10, 2, N'Category')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (11, 1, N'Completar instalacion')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (11, 2, N'Complete installation')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (12, 1, N'Conectar')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (12, 2, N'Connect')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (13, 1, N'Contraseña')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (13, 2, N'Password')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (14, 1, N'Creacion de usuario')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (14, 2, N'User creation')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (15, 1, N'Crear formulario')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (15, 2, N'Create form')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (16, 1, N'Crear pedido')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (16, 2, N'Create order')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (17, 1, N'Crear pedido premium')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (17, 2, N'Create premium order')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (18, 1, N'Crear permiso')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (18, 2, N'Create permission')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (19, 1, N'Crear rama permiso')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (19, 2, N'Create permission branch')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (20, 1, N'Crear Usuario')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (20, 2, N'Create User')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (21, 1, N'Desconectarse')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (21, 2, N'Disconnect')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (22, 1, N'Descripcion')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (22, 2, N'Description')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (23, 1, N'Enviar a fabricacion')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (23, 2, N'Send to manufacturing')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (24, 1, N'Finalizar pago')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (24, 2, N'Finish payment')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (25, 1, N'Generar ficha tecnica')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (25, 2, N'Generate technical sheet')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (26, 1, N'Gestiones')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (26, 2, N'Management')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (27, 1, N'Ingrese el usuario')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (27, 2, N'Enter the user')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (28, 1, N'Ingrese la contraseña')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (28, 2, N'Enter password')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (29, 1, N'Lenguaje')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (29, 2, N'Language')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (30, 1, N'Modificar usuario')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (30, 2, N'Modify user')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (31, 1, N'MostrarContraseña')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (31, 2, N'ShowPassword')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (32, 1, N'Pedidos')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (32, 2, N'Orders')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (33, 1, N'Permiso 1')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (33, 2, N'Permission 1')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (34, 1, N'Permiso 2')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (34, 2, N'Permission 2')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (35, 1, N'Recetear contraseña')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (35, 2, N'Reset password')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (36, 1, N'Rechazar fecha')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (36, 2, N'Reject date')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (37, 1, N'Rechazar formulario')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (37, 2, N'Reject form')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (38, 1, N'Registrar usuario')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (38, 2, N'Register user')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (39, 1, N'Roll')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (39, 2, N'Roll')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (40, 1, N'Usuario')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (40, 2, N'User')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (41, 1, N'Usuarios')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (41, 2, N'Users')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (42, 1, N'Volver')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (42, 2, N'Return')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (43, 1, N'Crear idioma')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (43, 2, N'Create language')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (44, 1, N'Codigo de lenguaje')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (44, 2, N'language code')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (45, 1, N'Nombre de lenguaje')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (45, 2, N'Language name')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (47, 1, N'Permiso')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (47, 2, N'Permission')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (48, 1, N'Todas las patentes')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (48, 2, N'All patents')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (49, 1, N'Nombre')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (49, 2, N'Name')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (50, 1, N'Guardar')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (50, 2, N'Save')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (51, 1, N'Agregar >>')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (51, 2, N'Add >>')
GO
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (52, 1, N'Configurar')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (52, 2, N'Set up')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (53, 1, N'Guardar familia')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (53, 2, N'Save family')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (54, 1, N'Nueva')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (54, 2, N'New')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (55, 1, N'Familia')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (55, 2, N'Family')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (56, 1, N'Configurar Familia')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (56, 2, N'Set up Family')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (57, 1, N'Patentes')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (57, 2, N'Patents')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (58, 1, N'Agregar patentes')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (58, 2, N'Add patents')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (59, 1, N'Todos los usuarios')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (59, 2, N'All users')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (60, 1, N'Agregar Familias')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (60, 2, N'Add Families')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (61, 1, N'Guardar cambios')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (61, 2, N'Save Changes')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (62, 1, N'Patente y Familias')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (62, 2, N'Patent and Families')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (63, 1, N'Roles Usuarios')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (63, 2, N'User Roles')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (64, 1, N'Pedido creacion cliente')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (64, 2, N'Customer creation order')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (65, 1, N'Analizar materiales de formulario')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (65, 2, N'Analyze form materials')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (66, 1, N'Analizar farbricacion')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (66, 2, N'Analyze manufacturing')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (67, 1, N'Seleccionar formulario')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (67, 2, N'Select form')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (68, 1, N'Datos formulario')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (68, 2, N'Form data')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (69, 1, N'DescripcionAgua')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (69, 2, N'DescriptionWater')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (70, 1, N'DisponibilidadAgua')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (70, 2, N'Water Availability')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (71, 1, N'DescripcionZona')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (71, 2, N'DescriptionZone')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (72, 1, N'Direccion')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (72, 2, N'Address')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (73, 1, N'Distancia a cubrir')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (73, 2, N'Distance to cover')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (74, 1, N'DescripcionViabilidad')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (74, 2, N'DescriptionViability')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (75, 1, N'Cultivo cargados')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (75, 2, N'Crop loaded')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (76, 1, N'Viabilidad')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (76, 2, N'Viability')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (77, 1, N'Formulario seleccionado')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (77, 2, N'Selected form')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (78, 1, N'Descripcion cultivo')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (78, 2, N'Crop description')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (79, 1, N'Tipo cultivo')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (79, 2, N'Type of crop')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (80, 1, N'Cantidad')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (80, 2, N'Amount')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (81, 1, N'Cultivos precargados')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (81, 2, N'Preloaded cultures')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (82, 1, N'Actualizar formulario')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (82, 2, N'Update form')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (83, 1, N'Crear y cargar cultivo')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (83, 2, N'Create and load culture')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (84, 1, N'Cargar cultivo')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (84, 2, N'Load crop')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (85, 1, N'DistanciaCubrir')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (85, 2, N'DistanceCover')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (86, 1, N'Cultivos')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (86, 2, N'Crops')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (87, 1, N'Descripcion sensor')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (87, 2, N'Sensor description')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (88, 1, N'Tipo sensor')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (88, 2, N'Sensor type')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (89, 1, N'Precio')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (89, 2, N'Price')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (90, 1, N'Sensores precargados')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (90, 2, N'Preloaded sensors')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (91, 1, N'Materiales cargados')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (91, 2, N'Uploaded materials')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (92, 1, N'Sensores cargados')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (92, 2, N'Sensors charged')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (93, 1, N'Dispositivos de agua cargados')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (93, 2, N'Water charged devices')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (94, 1, N'Cantidad equipos')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (94, 2, N'Number of teams')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (95, 1, N'Precio equipo')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (95, 2, N'Equipment price')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (96, 1, N'Conductor agua')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (96, 2, N'Water driver')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (97, 1, N'Precio conductor agua')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (97, 2, N'Water driver price')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (98, 1, N'Comentarios')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (98, 2, N'Comments')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (99, 1, N'Descripcion agua')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (99, 2, N'Description of water')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (100, 1, N'Tipo dispositivo agua')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (100, 2, N'Device type water')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (101, 1, N'Dispositivos agua precargados')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (101, 2, N'Pre-charged water devices')
GO
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (102, 1, N'Cargar dipositivo agua')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (102, 2, N'Charge water device')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (103, 1, N'Crear y cargar sensor')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (103, 2, N'Create and upload sensor')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (104, 1, N'Cargar sensor')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (104, 2, N'Load sensor')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (105, 1, N'Crear y cargar dipositivo agua')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (105, 2, N'Create and charge water device')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (106, 1, N'Cargar materiales')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (106, 2, N'Loading materials')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (107, 1, N'Todos los pedidos')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (107, 2, N'All orders')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (108, 1, N'Seleccionar pedido')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (108, 2, N'Select order')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (109, 1, N'Datos cliente')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (109, 2, N'Customer data')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (110, 1, N'Apellido')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (110, 2, N'Last name')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (111, 1, N'DNI')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (111, 2, N'DNI')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (112, 1, N'Mail')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (112, 2, N'Mail')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (113, 1, N'Telefono')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (113, 2, N'Phone')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (114, 1, N'Propuesto')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (114, 2, N'Proposed')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (115, 1, N'Aceptado')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (115, 2, N'Accepted')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (116, 1, N'Finalizado')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (116, 2, N'Finalized')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (117, 1, N'Cambiar fecha propuesta')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (117, 2, N'Change proposed date')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (118, 1, N'Cambiar a aceptado')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (118, 2, N'Change to accepted')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (119, 1, N'Cambiar a finalizado')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (119, 2, N'Change to finished')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (120, 1, N'Fecha')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (120, 2, N'Date')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (121, 1, N'Ver fechas instalacion')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (121, 2, N'See installation dates')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (122, 1, N'Monto a pagar')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (122, 2, N'Amount payable')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (123, 1, N'Monto de la seña')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (123, 2, N'Amount of the deposit')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (124, 1, N'Cargar pago de seña')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (124, 2, N'Load deposit')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (125, 1, N'Cargar pago total')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (125, 2, N'Load full payment')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (126, 1, N'Calcular presupuesto')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (126, 2, N'Calculate budget')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (127, 1, N'Pedido creacion de usuario')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (127, 2, N'Request for user creation')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (128, 1, N'Pagos de formulario')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (128, 2, N'Form payments')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (129, 1, N'Cargar fecha instalacion')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (129, 2, N'Load installation date')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (130, 1, N'Crear pedido de creacion usuario')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (130, 2, N'Create user creation request')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (131, 1, N'Formulario')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (131, 2, N'Form')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (132, 1, N'Materiales')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (132, 2, N'Materials')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (133, 1, N'Sensores')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (133, 2, N'Sensors')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (134, 1, N'DipositivosAgua')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (134, 2, N'Water Devices')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (135, 1, N'Cambiar estado a fabricacio')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (135, 2, N'Change status to manufacturing')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (136, 1, N'Cambiar estado a terminado')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (136, 2, N'Change status to finished')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (137, 1, N'Fecha cargada')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (137, 2, N'Date loaded')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (138, 1, N'Cambiar fecha')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (138, 2, N'Change date')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (139, 1, N'Cambiar estado a aceptado')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (139, 2, N'Change status to accepted')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (140, 1, N'DescripcionViabilidadEquipo')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (140, 2, N'DescriptionViabilityEquipment')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (141, 1, N'Cambiar estado a fabricacion')
INSERT [dbo].[VersionContent] ([ContentId], [LanguageId], [Content]) VALUES (141, 2, N'Change status to manufacturing')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_content_content]    Script Date: 3/10/2024 23:36:48 ******/
ALTER TABLE [dbo].[Content] ADD  CONSTRAINT [UQ_content_content] UNIQUE NONCLUSTERED 
(
	[ContentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ_Usuario]    Script Date: 3/10/2024 23:36:48 ******/
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [UQ_Usuario] UNIQUE NONCLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Bitacora] ADD  DEFAULT (getdate()) FOR [fecha]
GO
ALTER TABLE [dbo].[BitacoraCambios] ADD  DEFAULT (getdate()) FOR [FechaCambio]
GO
ALTER TABLE [dbo].[Bitacora]  WITH CHECK ADD  CONSTRAINT [FK_Registro_Usuario] FOREIGN KEY([idUsuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Bitacora] CHECK CONSTRAINT [FK_Registro_Usuario]
GO
ALTER TABLE [dbo].[Cliente]  WITH CHECK ADD  CONSTRAINT [FK_usuario_cliente] FOREIGN KEY([id])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[Cliente] CHECK CONSTRAINT [FK_usuario_cliente]
GO
ALTER TABLE [dbo].[Cultivo_Formulario]  WITH CHECK ADD  CONSTRAINT [FK_Cultivo_formulario] FOREIGN KEY([idCultivo])
REFERENCES [dbo].[Cultivo] ([id])
GO
ALTER TABLE [dbo].[Cultivo_Formulario] CHECK CONSTRAINT [FK_Cultivo_formulario]
GO
ALTER TABLE [dbo].[Cultivo_Formulario]  WITH CHECK ADD  CONSTRAINT [FK_Cultivo_formulario_formulario] FOREIGN KEY([idFormulario])
REFERENCES [dbo].[Formulario] ([id])
GO
ALTER TABLE [dbo].[Cultivo_Formulario] CHECK CONSTRAINT [FK_Cultivo_formulario_formulario]
GO
ALTER TABLE [dbo].[DispositivoAgua_Formulario]  WITH CHECK ADD  CONSTRAINT [FK_DispositivoAgua_formulario] FOREIGN KEY([idDispositivoAgua])
REFERENCES [dbo].[DispositivoAgua] ([id])
GO
ALTER TABLE [dbo].[DispositivoAgua_Formulario] CHECK CONSTRAINT [FK_DispositivoAgua_formulario]
GO
ALTER TABLE [dbo].[DispositivoAgua_Formulario]  WITH CHECK ADD  CONSTRAINT [FK_DispositivoAgua_formulario_formulario] FOREIGN KEY([idFormulario])
REFERENCES [dbo].[Formulario] ([id])
GO
ALTER TABLE [dbo].[DispositivoAgua_Formulario] CHECK CONSTRAINT [FK_DispositivoAgua_formulario_formulario]
GO
ALTER TABLE [dbo].[Fecha_Instalacion]  WITH CHECK ADD  CONSTRAINT [FK_fecha_formulario] FOREIGN KEY([id_Instalacion])
REFERENCES [dbo].[Formulario] ([id])
GO
ALTER TABLE [dbo].[Fecha_Instalacion] CHECK CONSTRAINT [FK_fecha_formulario]
GO
ALTER TABLE [dbo].[Formulario]  WITH CHECK ADD  CONSTRAINT [FK_cliente_formulario] FOREIGN KEY([idCliente])
REFERENCES [dbo].[Cliente] ([id])
GO
ALTER TABLE [dbo].[Formulario] CHECK CONSTRAINT [FK_cliente_formulario]
GO
ALTER TABLE [dbo].[Materiales]  WITH CHECK ADD  CONSTRAINT [FK_materiales_formulario] FOREIGN KEY([id])
REFERENCES [dbo].[Formulario] ([id])
GO
ALTER TABLE [dbo].[Materiales] CHECK CONSTRAINT [FK_materiales_formulario]
GO
ALTER TABLE [dbo].[PedidoCreacion]  WITH CHECK ADD  CONSTRAINT [FK_pedidoCreacion_formulario] FOREIGN KEY([idFormulario])
REFERENCES [dbo].[Formulario] ([id])
GO
ALTER TABLE [dbo].[PedidoCreacion] CHECK CONSTRAINT [FK_pedidoCreacion_formulario]
GO
ALTER TABLE [dbo].[permiso_permiso]  WITH CHECK ADD  CONSTRAINT [FK_permiso_permiso_permiso1] FOREIGN KEY([id_permiso_padre])
REFERENCES [dbo].[permiso] ([id])
GO
ALTER TABLE [dbo].[permiso_permiso] CHECK CONSTRAINT [FK_permiso_permiso_permiso1]
GO
ALTER TABLE [dbo].[permiso_permiso]  WITH CHECK ADD  CONSTRAINT [FK_permiso_permiso_permiso2] FOREIGN KEY([id_permiso_hijo])
REFERENCES [dbo].[permiso] ([id])
GO
ALTER TABLE [dbo].[permiso_permiso] CHECK CONSTRAINT [FK_permiso_permiso_permiso2]
GO
ALTER TABLE [dbo].[Sensor_Formulario]  WITH CHECK ADD  CONSTRAINT [FK_sensor_formulario] FOREIGN KEY([idSensor])
REFERENCES [dbo].[Sensor] ([id])
GO
ALTER TABLE [dbo].[Sensor_Formulario] CHECK CONSTRAINT [FK_sensor_formulario]
GO
ALTER TABLE [dbo].[Sensor_Formulario]  WITH CHECK ADD  CONSTRAINT [FK_sensor_formulario_formulario] FOREIGN KEY([idFormulario])
REFERENCES [dbo].[Formulario] ([id])
GO
ALTER TABLE [dbo].[Sensor_Formulario] CHECK CONSTRAINT [FK_sensor_formulario_formulario]
GO
ALTER TABLE [dbo].[usuarios_permisos]  WITH CHECK ADD  CONSTRAINT [FK_usuario_permisos_permiso] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id_usuario])
GO
ALTER TABLE [dbo].[usuarios_permisos] CHECK CONSTRAINT [FK_usuario_permisos_permiso]
GO
ALTER TABLE [dbo].[usuarios_permisos]  WITH CHECK ADD  CONSTRAINT [FK_usuario_permisos_usuario] FOREIGN KEY([id_permiso])
REFERENCES [dbo].[permiso] ([id])
GO
ALTER TABLE [dbo].[usuarios_permisos] CHECK CONSTRAINT [FK_usuario_permisos_usuario]
GO
ALTER TABLE [dbo].[VersionContent]  WITH CHECK ADD  CONSTRAINT [FK_Content_ConterVersion] FOREIGN KEY([ContentId])
REFERENCES [dbo].[Content] ([id])
GO
ALTER TABLE [dbo].[VersionContent] CHECK CONSTRAINT [FK_Content_ConterVersion]
GO
/****** Object:  StoredProcedure [dbo].[ActualizarContraseñaYUsuario]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  CREATE PROCEDURE [dbo].[ActualizarContraseñaYUsuario]
    @idPedido INT,
    @contrasena NVARCHAR(MAX),
    @usuario NVARCHAR(MAX)
AS
BEGIN
    -- Actualizar los campos contraseña y usuario del registro con el id proporcionado
    UPDATE dbo.PedidoCreacion
    SET contraseña = @contrasena,
        usuario = @usuario
    WHERE id = @idPedido;
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarEstadoFabricacion]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarEstadoFabricacion]
    @id INT,
    @estadoFabricacion NVARCHAR(100)
AS
BEGIN
    -- Actualizar el campo estadoFabricacion en la tabla Formulario
    UPDATE dbo.Formulario
    SET estadoFabricacion = @estadoFabricacion
    WHERE id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarEstadoFechaInstalacion]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ActualizarEstadoFechaInstalacion]
    @id_Formulario INT,
    @nuevoEstado NVARCHAR(50)
AS
BEGIN
    -- Actualizar el campo estado en la tabla Fecha_Instalacion
    UPDATE dbo.Fecha_Instalacion
    SET estado = @nuevoEstado
    WHERE id_Formulario = @id_Formulario;
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarEstadoPago]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ActualizarEstadoPago]
    @id INT,
    @estadoPago NVARCHAR(100)
AS
BEGIN
    -- Actualizar el campo estadoPago en la tabla Formulario
    UPDATE dbo.Formulario
    SET estadoPago = @estadoPago
    WHERE id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarEstadoYFechaInstalacion]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarEstadoYFechaInstalacion]
    @id_Formulario INT,
    @nuevoEstado NVARCHAR(50),
    @nuevaFecha DATETIME
AS
BEGIN
    -- Actualizar el campo estado y fecha en la tabla Fecha_Instalacion
    UPDATE dbo.Fecha_Instalacion
    SET estado = @nuevoEstado,
        fecha = @nuevaFecha
    WHERE id_Formulario = @id_Formulario;
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarFormulario]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ActualizarFormulario]
    @id INT,
    @descripcionAgua NVARCHAR(MAX) = NULL,
    @descripcionZona NVARCHAR(MAX) = NULL,
    @disponibilidadAgua BIT = NULL,
    @direccion NVARCHAR(MAX) = NULL,
    @distanciaCubrir NVARCHAR(MAX) = NULL,
	@descripcionViabilidad  NVARCHAR(MAX) = NULL,
	@viabilidad  BIT = NULL
AS
BEGIN
    UPDATE Formulario
    SET 
        descripcionAgua = ISNULL(@descripcionAgua, descripcionAgua),
        descripcionZona = ISNULL(@descripcionZona, descripcionZona),
        disponibilidadAgua = ISNULL(@disponibilidadAgua, disponibilidadAgua),
        direccion = ISNULL(@direccion, direccion),
        distanciaCubrir = ISNULL(@distanciaCubrir, distanciaCubrir),
		descripcionViabilidad = ISNULL(@descripcionViabilidad, descripcionViabilidad),
		viabilidad = ISNULL(@viabilidad, viabilidad)
    WHERE id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarIdUsuarioCliente]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  CREATE PROCEDURE [dbo].[ActualizarIdUsuarioCliente]
    @idCliente INT,
    @idUsuario INT
AS
BEGIN
    -- Actualizar el idUsuario del cliente con el idCliente proporcionado
    UPDATE dbo.Cliente
    SET idUsuario = @idUsuario
    WHERE id = @idCliente;
END
GO
/****** Object:  StoredProcedure [dbo].[ActualizarViabilidadEquipo]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ActualizarViabilidadEquipo]
    @id INT,
    @nuevaDescripcionViabilidadEquipo NVARCHAR(MAX),
    @nuevaViabilidadEquipo BIT
AS
BEGIN
    -- Actualiza la descripción y la viabilidad del equipo para el formulario con el ID dado
    UPDATE dbo.Formulario
    SET 
        descripcionViabilidadEquipo = @nuevaDescripcionViabilidadEquipo,
        viabilidadEquipo = @nuevaViabilidadEquipo
    WHERE 
        id = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarCliente]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AgregarCliente]
    @nombre NVARCHAR(50),
    @apellido NVARCHAR(50),
    @mail NVARCHAR(100),
    @telefono INT,
    @dni INT
AS
BEGIN
    -- Declara una variable para almacenar el nuevo ID
    DECLARE @nuevoId INT;

    -- Encuentra el último ID en la tabla y asigna el siguiente ID
    SELECT @nuevoId = ISNULL(MAX([id]), 0) + 1
    FROM [dbo].[Cliente];

    -- Inserta el nuevo cliente con el ID calculado y idUsuario en NULL
    INSERT INTO [dbo].[Cliente] ([id], [nombre], [apellido], [mail], [telefono], [dni], [idUsuario])
    VALUES (@nuevoId, @nombre, @apellido, @mail, @telefono, @dni, NULL);
END;
GO
/****** Object:  StoredProcedure [dbo].[AgregarCultivo]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AgregarCultivo]
    @descripcion NVARCHAR(MAX),
    @tipo NVARCHAR(100)
AS
BEGIN
    DECLARE @newId INT

    -- Obtener el último id y sumar 1
    SELECT @newId = ISNULL(MAX(id), 0) + 1 FROM Cultivo

    -- Insertar el nuevo cultivo
    INSERT INTO Cultivo (id, descripcion, tipo)
    VALUES (@newId, @descripcion, @tipo)
    
    -- Retornar el nuevo ID si es necesario
    SELECT @newId AS NewId
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarDispositivoAgua]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AgregarDispositivoAgua]
    @descripcion NVARCHAR(MAX) = NULL,
    @nombre NVARCHAR(50) = NULL,
    @precio FLOAT = NULL
AS
BEGIN
    DECLARE @newId INT

    -- Obtener el último id y sumar 1
    SELECT @newId = ISNULL(MAX(id), 0) + 1 FROM dbo.DispositivoAgua

    -- Insertar el nuevo dispositivo de agua
    INSERT INTO dbo.DispositivoAgua (id, descripcion, nombre, precio)
    VALUES (@newId, @descripcion, @nombre, @precio)
    
    -- Retornar el nuevo ID si es necesario
    SELECT @newId AS NewId
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarFechaInstalacion]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AgregarFechaInstalacion]
    @id_Formulario INT,
    @fecha DATETIME,
    @descripcion NVARCHAR(MAX),
    @estado NVARCHAR(50)
AS
BEGIN
    DECLARE @newId INT;

    -- Obtener el último id_Instalacion y sumar 1
    SELECT @newId = ISNULL(MAX(id_Instalacion), 0) + 1 FROM Fecha_Instalacion;

    -- Insertar el nuevo registro en Fecha_Instalacion
    INSERT INTO Fecha_Instalacion (id_Instalacion, id_Formulario, fecha, descripcion, estado)
    VALUES (@newId, @id_Formulario, @fecha, @descripcion, @estado);
    
    -- Retornar el nuevo ID si es necesario
    SELECT @newId AS NewId;
END

GO
/****** Object:  StoredProcedure [dbo].[AgregarFormulario]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AgregarFormulario]
AS
BEGIN
    -- Declara variables para almacenar el nuevo ID del formulario y el ID del último cliente
    DECLARE @nuevoId INT;
    DECLARE @ultimoIdCliente INT;

    -- Encuentra el último ID en la tabla Formulario y asigna el siguiente ID
    SELECT @nuevoId = ISNULL(MAX([id]), 0) + 1
    FROM [dbo].[Formulario];

    -- Encuentra el último ID en la tabla Cliente para asignar al idCliente
    SELECT TOP 1 @ultimoIdCliente = [id]
    FROM [dbo].[Cliente]
    ORDER BY [id] DESC;

    -- Inserta el nuevo formulario con el ID calculado y el último ID de cliente
    INSERT INTO [dbo].[Formulario] 
        ([id], [descripcionAgua], [descripcionZona], [disponibilidadAgua], [direccion], 
         [distanciaCubrir], [estadoInstalacion],[estadoFabricacion], [estadoPago], [viabilidad], [idCliente])
    VALUES 
        (@nuevoId, NULL, NULL, NULL, NULL, NULL,NULL, NULL, NULL, NULL, @ultimoIdCliente);
END;

GO
/****** Object:  StoredProcedure [dbo].[AgregarPedidoCreacion]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AgregarPedidoCreacion]
    @idFormulario INT,
    @estado NVARCHAR(100)
AS
BEGIN
    DECLARE @nuevoId INT

    -- Obtener el último id y sumar 1
    SELECT @nuevoId = ISNULL(MAX(id), 0) + 1 FROM dbo.PedidoCreacion

    -- Insertar el nuevo registro en la tabla PedidoCreacion
    INSERT INTO dbo.PedidoCreacion (id, idFormulario, contraseña, usuario, estado)
    VALUES (@nuevoId, @idFormulario, NULL, NULL, @estado)
    
    -- Retornar el nuevo ID si es necesario
    SELECT @nuevoId AS NuevoId
END
GO
/****** Object:  StoredProcedure [dbo].[AgregarSensor]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AgregarSensor]
    @descripcion NVARCHAR(MAX) = NULL,
    @nombre NVARCHAR(MAX) = NULL,
    @precio FLOAT = NULL
AS
BEGIN
    DECLARE @newId INT

    -- Obtener el último id y sumar 1
    SELECT @newId = ISNULL(MAX(id), 0) + 1 FROM dbo.Sensor

    -- Insertar el nuevo sensor
    INSERT INTO dbo.Sensor (id, descripcion, nombre, precio)
    VALUES (@newId, @descripcion, @nombre, @precio)
    
    -- Retornar el nuevo ID si es necesario
    SELECT @newId AS NewId
END
GO
/****** Object:  StoredProcedure [dbo].[AsignarCultivoAFormulario]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AsignarCultivoAFormulario]
    @idCultivo INT,
    @idFormulario INT,
    @cantidad INT = NULL
AS
BEGIN
    -- Insertar la relación entre cultivo y formulario
    INSERT INTO Cultivo_Formulario (idCultivo, idFormulario, cantidad)
    VALUES (@idCultivo, @idFormulario, @cantidad);
END
GO
/****** Object:  StoredProcedure [dbo].[BackupBD]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BackupBD]
    @RutaBackup NVARCHAR(255)   -- Ruta completa donde se guardará el backup
AS
BEGIN
    DECLARE @ComandoBackup NVARCHAR(MAX)
    
    -- Construimos el comando de backup para la base de datos RiegoAutonomo
    SET @ComandoBackup = 'BACKUP DATABASE [RiegoAutonomo] TO DISK = ''' + @RutaBackup + ''''
    
    -- Ejecutamos el comando de backup
    EXEC sp_executesql @ComandoBackup
END
GO
/****** Object:  StoredProcedure [dbo].[BuscarUsuario]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BuscarUsuario]
    @nombre NVARCHAR(50)
AS
BEGIN
    SELECT nombre, contrasena, id_usuario
    FROM Usuario
    WHERE nombre = @nombre;
END;
GO
/****** Object:  StoredProcedure [dbo].[CambiarContrasenaUsuario]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CambiarContrasenaUsuario]
    @id int,
    @nuevaContrasena NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el usuario existe
    IF EXISTS (SELECT 1 FROM Usuario WHERE id_usuario = @id)
    BEGIN
        -- Actualizar la contraseña del usuario
        UPDATE Usuario
        SET contrasena = @nuevaContrasena
        WHERE id_usuario = @id;

        SELECT 'Contraseña actualizada correctamente.' AS Mensaje;
    END
    ELSE
    BEGIN
        SELECT 'El usuario especificado no existe.' AS Mensaje;
    END
END;

GO
/****** Object:  StoredProcedure [dbo].[CambiarUsuario]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CambiarUsuario]
    @id int,
    @nuevoUsuario NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    -- Verificar si el usuario existe
    IF EXISTS (SELECT 1 FROM Usuario WHERE id_usuario = @id)
    BEGIN
        -- Actualizar la contraseña del usuario
        UPDATE Usuario
        SET nombre = @nuevoUsuario
        WHERE id_usuario = @id;

        SELECT 'usuario actualizada correctamente.' AS Mensaje;
    END
    ELSE
    BEGIN
        SELECT 'El usuario especificado no existe.' AS Mensaje;
    END
END;

GO
/****** Object:  StoredProcedure [dbo].[GetAllContent]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

  CREATE PROCEDURE [dbo].[GetAllContent]
AS
BEGIN
    SELECT * FROM Content;
END;
GO
/****** Object:  StoredProcedure [dbo].[GetContentByLanguage]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetContentByLanguage]
    @id INT
AS
BEGIN
    SELECT  c.ContentId, vc.Content
    FROM VersionContent vc
    INNER JOIN Content c ON vc.ContentId = c.id
    WHERE vc.LanguageId = @id;
END;

GO
/****** Object:  StoredProcedure [dbo].[GetLanguages]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetLanguages]
AS
BEGIN
    SELECT LanguageId, LanguageName FROM Languages;
END
GO
/****** Object:  StoredProcedure [dbo].[getLenguajeComponent]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[getLenguajeComponent]
    @id INT
AS
BEGIN
    SELECT ContentId, Content FROM ContentVersion WHERE LanguageId = @id
END;
GO
/****** Object:  StoredProcedure [dbo].[GuardarUsuario]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GuardarUsuario]
    @usuario VARCHAR(50),
    @contrasena NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @id INT;

    -- Obtén el último id y suma uno
    SELECT @id = ISNULL(MAX(id_usuario), 0) + 1 FROM [dbo].[Usuario];

    -- Inserta el nuevo usuario
    INSERT INTO [dbo].[Usuario] (id_usuario, nombre, contrasena)
    VALUES (@id, @usuario, @contrasena);

    -- Devuelve el id generado
    SELECT @id AS NuevoID;
END;

GO
/****** Object:  StoredProcedure [dbo].[InsertarBitacora]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertarBitacora]
    @idUsuario INT,
    @fecha DATETIME,
    @mensaje NVARCHAR(400)
AS
BEGIN
    DECLARE @id INT;
    DECLARE @digitoVerificador CHAR(1);
    DECLARE @digitoVerificadorVertical NVARCHAR(32);

    -- Insertar el nuevo registro
    INSERT INTO Bitacora (idUsuario, fecha, mensaje)
    VALUES (@idUsuario, @fecha, @mensaje);

    -- Obtener el id del último registro insertado
    SET @id = SCOPE_IDENTITY();

    -- Calcular el dígito verificador horizontal (por registro)
    SET @digitoVerificador = dbo.CalcularDigitoVerificador(@id, @idUsuario, @fecha, @mensaje);

    -- Actualizar el registro con el dígito verificador
    UPDATE Bitacora
    SET digitoVerificador = @digitoVerificador
    WHERE id = @id;

    -- Calcular el dígito verificador vertical para toda la tabla Bitacora
    SET @digitoVerificadorVertical = dbo.fn_CalcularDigitoVerificadorVertical();

    -- Actualizar el valor del dígito verificador vertical en la tabla auxiliar
    UPDATE DigitoVerificadorVertical
    SET digitoVerificador = @digitoVerificadorVertical
    WHERE tabla = 'Bitacora';
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertarNuevoDispositivoAguaFormulario]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertarNuevoDispositivoAguaFormulario]
    @idDispositivoAgua INT,
    @idFormulario INT,
    @cantidad INT = NULL
AS
BEGIN
    -- Inserta un nuevo registro en la tabla DispositivoAgua_Formulario
    INSERT INTO dbo.DispositivoAgua_Formulario
    (
        idDispositivoAgua,
        idFormulario,
        cantidad
    )
    VALUES
    (
        @idDispositivoAgua,
        @idFormulario,
        @cantidad
    );
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarNuevoMaterial]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertarNuevoMaterial]
    @comentario NVARCHAR(MAX) = NULL,
    @cantEquipos INT = NULL,
    @conductoAgua NVARCHAR(50) = NULL,
    @precioConducto FLOAT = NULL,
    @precioEquipo FLOAT = NULL,
    @idFormulario INT
AS
BEGIN
    -- Variable para almacenar el nuevo ID
    DECLARE @nuevoId INT;

    -- Obtener el último ID (máximo) y calcular el nuevo ID
    SELECT @nuevoId = ISNULL(MAX(id), 0) + 1 FROM dbo.Materiales;

    -- Insertar el nuevo registro con el nuevo ID
    INSERT INTO dbo.Materiales
    (
        id,
        comentario,
        cantEquipos,
        conductoAgua,
        precioConducto,
        precioEquipo,
        idFormulario
    )
    VALUES
    (
        @nuevoId,
        @comentario,
        @cantEquipos,
        @conductoAgua,
        @precioConducto,
        @precioEquipo,
        @idFormulario
    );
END
GO
/****** Object:  StoredProcedure [dbo].[InsertarNuevoSensorFormulario]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertarNuevoSensorFormulario]
    @idSensor INT,
    @idFormulario INT,
    @cantidad NCHAR(10) = NULL
AS
BEGIN
    -- Inserta un nuevo registro en la tabla Sensor_Formulario
    INSERT INTO dbo.Sensor_Formulario
    (
        idSensor,
        idFormulario,
        cantidad
    )
    VALUES
    (
        @idSensor,
        @idFormulario,
        @cantidad
    );
END
GO
/****** Object:  StoredProcedure [dbo].[InsertLanguage]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  CREATE PROCEDURE [dbo].[InsertLanguage]
    @LanguageCode NVARCHAR(50),
    @LanguageName NVARCHAR(100)
AS
BEGIN
    -- Variable to store the max LanguageId
    DECLARE @MaxLanguageId INT;

    -- Get the current maximum LanguageId
    SELECT @MaxLanguageId = ISNULL(MAX(LanguageId), 0) FROM Languages;

    -- Insert the new record with the new LanguageId
    INSERT INTO Languages (LanguageId, LanguageCode, LanguageName)
    VALUES (@MaxLanguageId + 1, @LanguageCode, @LanguageName);
END;
GO
/****** Object:  StoredProcedure [dbo].[InsertVersionContent]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InsertVersionContent]
    @Items ItemIdiomaNuevoDisplayType READONLY
AS
BEGIN
    DECLARE @LastLanguageId INT;

    -- Obtener el último LanguageId cargado en la tabla Languages
    SELECT @LastLanguageId = MAX(LanguageId) FROM Languages;

    -- Insertar los datos en la tabla VersionContent
    INSERT INTO VersionContent (ContentId, Content, LanguageId)
    SELECT Id, Traduccion, @LastLanguageId
    FROM @Items;
END;
GO
/****** Object:  StoredProcedure [dbo].[ListarRegistrosBitacora]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ListarRegistrosBitacora]
AS
BEGIN
    SELECT id, idUsuario, fecha, mensaje,
           CASE 
               WHEN digitoVerificador = dbo.CalcularDigitoVerificador(id, idUsuario, fecha, mensaje)
               THEN 'Valido'
               ELSE 'Corrupto'
           END AS Estado
    FROM Bitacora
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerCultivosPorFormulario]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerCultivosPorFormulario]
    @idFormulario INT
AS
BEGIN
    -- Selecciona los cultivos y su información asociada para el formulario dado
    SELECT 
        cf.idCultivo,
        cf.cantidad,
        c.descripcion,
        c.tipo
    FROM 
        dbo.Cultivo_Formulario cf
    INNER JOIN 
        dbo.Cultivo c ON cf.idCultivo = c.id
    WHERE 
        cf.idFormulario = @idFormulario;
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerDispositivosAguaConDetalles]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ObtenerDispositivosAguaConDetalles]
    @idFormulario INT
AS
BEGIN
    -- Seleccionar los dispositivos de agua con su descripción y precio
    SELECT
        da.id AS idDispositivoAgua,
        da.descripcion,
        da.precio,
		daf.cantidad,
		da.nombre
    FROM dbo.DispositivoAgua_Formulario daf
    JOIN dbo.DispositivoAgua da ON daf.idDispositivoAgua = da.id
    WHERE daf.idFormulario = @idFormulario;
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerFechasInstalacionPorEstado]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerFechasInstalacionPorEstado]
    @estado NVARCHAR(50)
AS
BEGIN
    -- Seleccionar registros de la tabla Fecha_Instalacion según el estado proporcionado
    SELECT id_Instalacion, id_Formulario, fecha, descripcion, estado
    FROM dbo.Fecha_Instalacion
    WHERE estado = @estado;
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerFechasInstalacionPorFormulario]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerFechasInstalacionPorFormulario]
    @id_Formulario INT
AS
BEGIN
    -- Seleccionar registros de la tabla Fecha_Instalacion según el id_Formulario
    SELECT id_Instalacion, id_Formulario, fecha, descripcion, estado
    FROM dbo.Fecha_Instalacion
    WHERE id_Formulario = @id_Formulario;

    -- Verificar si no se encontraron registros
    IF @@ROWCOUNT = 0
    BEGIN
        PRINT 'No se encontraron registros para el formulario especificado.';
    END
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerFormulario]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerFormulario]
    @id INT
AS
BEGIN
    -- Selecciona los datos del formulario con el ID especificado
    SELECT 
        [id], 
        [descripcionAgua], 
        [descripcionZona], 
        [disponibilidadAgua], 
        [direccion], 
        [distanciaCubrir], 
        [estadoFabricacion], 
        [estadoPago], 
        [viabilidad], 
        [idCliente],
		[descripcionViabilidad],
		[viabilidadEquipo],
		[descripcionViabilidadEquipo],
		[estadoInstalacion]
    FROM [dbo].[Formulario]
    WHERE [id] = @id;
END;

exec ObtenerFormulario 1
GO
/****** Object:  StoredProcedure [dbo].[ObtenerMaterialPorID]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerMaterialPorID]
    @id INT
AS
BEGIN
    -- Seleccionar el material según el ID
    SELECT
        id,
        comentario,
        cantEquipos,
        conductoAgua,
        precioConducto,
        precioEquipo,
        idFormulario
    FROM dbo.Materiales
    WHERE  idFormulario = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerSensoresConDetalles]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ObtenerSensoresConDetalles]
    @idFormulario INT
AS
BEGIN
    -- Seleccionar los sensores con su descripción y precio
    SELECT
        sf.idSensor,
        s.descripcion,
        s.precio,
		sf.cantidad,
		s.nombre
    FROM dbo.Sensor_Formulario sf
    JOIN dbo.Sensor s ON sf.idSensor = s.id
    WHERE sf.idFormulario = @idFormulario;
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerTodosLosCultivos]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerTodosLosCultivos]
AS
BEGIN
    SELECT [id], [descripcion], [tipo]
    FROM [dbo].[Cultivo]
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerTodosLosDispositivosAgua]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerTodosLosDispositivosAgua]
AS
BEGIN
    -- Selecciona todos los registros de la tabla DispositivosAgua
    SELECT 
        *
    FROM 
        dbo.DispositivoAgua;
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerTodosLosFormularios]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerTodosLosFormularios]
AS
BEGIN
    -- Selecciona todos los datos de la tabla Formulario
    SELECT 
        [id], 
        [descripcionAgua], 
        [descripcionZona], 
        [disponibilidadAgua], 
        [direccion], 
        [distanciaCubrir], 
        [estadoFabricacion], 
        [estadoPago], 
        [viabilidad], 
        [idCliente],
		[descripcionViabilidad],
		[viabilidadEquipo],
		[descripcionViabilidadEquipo],
		[estadoInstalacion]
    FROM [dbo].[Formulario];
END;
GO
/****** Object:  StoredProcedure [dbo].[ObtenerTodosLosPedidosCreacion]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerTodosLosPedidosCreacion]
AS
BEGIN
    -- Seleccionar todos los registros de la tabla PedidoCreacion
    SELECT id, idFormulario, contraseña, usuario, estado
    FROM dbo.PedidoCreacion;
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerTodosLosSensores]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ObtenerTodosLosSensores]
AS
BEGIN
    -- Selecciona todos los registros de la tabla Sensores
    SELECT 
        *
    FROM 
        dbo.Sensor;
END
GO
/****** Object:  StoredProcedure [dbo].[ObtenerTodosLosUsuarios]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerTodosLosUsuarios]
AS
BEGIN
    -- Selecciona todos los registros de la tabla usuario
    SELECT id_usuario, contrasena, nombre
    FROM usuario;
END;
GO
/****** Object:  StoredProcedure [dbo].[ObtenerUltimoFormulario]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ObtenerUltimoFormulario]
AS
BEGIN
    -- Selecciona los datos del formulario con el ID más alto
    SELECT TOP 1
        [id], 
        [descripcionAgua], 
        [descripcionZona], 
        [disponibilidadAgua], 
        [direccion], 
        [distanciaCubrir], 
        [estadoFabricacion], 
        [estadoPago], 
        [viabilidad], 
        [idCliente],
		[estadoInstalacion]
    FROM [dbo].[Formulario]
    ORDER BY [id] DESC;
END;
GO
/****** Object:  StoredProcedure [dbo].[VerificarIntegridadTabla]    Script Date: 3/10/2024 23:36:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[VerificarIntegridadTabla]
AS
BEGIN
    DECLARE @digitoVerificadorAlmacenado NVARCHAR(32);
    DECLARE @digitoVerificadorCalculado NVARCHAR(32);

    -- Obtener el dígito verificador almacenado
    SELECT @digitoVerificadorAlmacenado = digitoVerificador
    FROM DigitoVerificadorVertical
    WHERE tabla = 'Bitacora';

    -- Calcular el dígito verificador actual
    SET @digitoVerificadorCalculado = dbo.fn_CalcularDigitoVerificadorVertical();

    -- Comparar ambos valores
    IF @digitoVerificadorAlmacenado = @digitoVerificadorCalculado
    BEGIN
        SELECT 'La tabla no ha sido alterada.' AS Mensaje;
    END
    ELSE
    BEGIN
        SELECT 'La tabla ha sido alterada o se ha eliminado un registro.' AS Mensaje;
    END
END
GO
USE [master]
GO
ALTER DATABASE [RiegoAutonomo] SET  READ_WRITE 
GO
