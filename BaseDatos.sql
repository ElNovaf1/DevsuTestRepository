USE [master]
GO
/****** Object:  Database [devsu]    Script Date: 19/9/2023 18:00:55 ******/
CREATE DATABASE [devsu]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'devsu', FILENAME = N'F:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\devsu.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'devsu_log', FILENAME = N'F:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\devsu_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [devsu] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [devsu].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [devsu] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [devsu] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [devsu] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [devsu] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [devsu] SET ARITHABORT OFF 
GO
ALTER DATABASE [devsu] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [devsu] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [devsu] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [devsu] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [devsu] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [devsu] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [devsu] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [devsu] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [devsu] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [devsu] SET  DISABLE_BROKER 
GO
ALTER DATABASE [devsu] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [devsu] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [devsu] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [devsu] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [devsu] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [devsu] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [devsu] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [devsu] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [devsu] SET  MULTI_USER 
GO
ALTER DATABASE [devsu] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [devsu] SET DB_CHAINING OFF 
GO
ALTER DATABASE [devsu] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [devsu] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [devsu] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [devsu] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [devsu] SET QUERY_STORE = ON
GO
ALTER DATABASE [devsu] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [devsu]
GO
/****** Object:  Table [dbo].[cliente]    Script Date: 19/9/2023 18:00:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cliente](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[edad] [tinyint] NULL,
	[direccion] [varchar](500) NULL,
	[telefono] [varchar](20) NULL,
	[genero] [varchar](20) NULL,
	[contraseña] [varchar](200) NOT NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_cliente] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_Nombre] UNIQUE NONCLUSTERED 
(
	[nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cuenta]    Script Date: 19/9/2023 18:00:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cuenta](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[numero] [varchar](20) NOT NULL,
	[tipo_cuenta] [varchar](20) NOT NULL,
	[cliente_id] [int] NOT NULL,
	[saldo_inicial] [decimal](13, 4) NOT NULL,
	[estado] [bit] NOT NULL,
 CONSTRAINT [PK_cuenta] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [AK_Numero] UNIQUE NONCLUSTERED 
(
	[numero] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[movimiento]    Script Date: 19/9/2023 18:00:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[movimiento](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[cuenta_id] [int] NOT NULL,
	[tipo_movimiento] [varchar](50) NOT NULL,
	[fecha] [datetime] NOT NULL,
	[valor] [decimal](13, 4) NOT NULL,
	[saldo] [decimal](13, 4) NOT NULL,
 CONSTRAINT [PK_movimiento] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[movimiento] ADD  CONSTRAINT [DF_movimiento_fecha]  DEFAULT (getdate()) FOR [fecha]
GO
ALTER TABLE [dbo].[cuenta]  WITH CHECK ADD  CONSTRAINT [FK_cuenta_cliente] FOREIGN KEY([cliente_id])
REFERENCES [dbo].[cliente] ([id])
GO
ALTER TABLE [dbo].[cuenta] CHECK CONSTRAINT [FK_cuenta_cliente]
GO
ALTER TABLE [dbo].[movimiento]  WITH CHECK ADD  CONSTRAINT [FK_movimiento_cuenta] FOREIGN KEY([cuenta_id])
REFERENCES [dbo].[cuenta] ([id])
GO
ALTER TABLE [dbo].[movimiento] CHECK CONSTRAINT [FK_movimiento_cuenta]
GO
/****** Object:  StoredProcedure [dbo].[uspActualizarSaldos]    Script Date: 19/9/2023 18:00:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspActualizarSaldos] 
	@IDCUENTA BIGINT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT ROW_NUMBER() OVER(ORDER BY fecha) row, * 
	INTO #TempDestinationTable
	FROM movimiento WHERE cuenta_id = @IDCUENTA

	UPDATE movs SET movs.saldo = tmovs.saldo
	FROM
	movimiento movs INNER JOIN 
	(
		SELECT a.row, a.id, a.tipo_movimiento,a.fecha,a.valor, a.valor + ISNULL(b.saldo,0) saldo
		FROM #TempDestinationTable a
		LEFT JOIN #TempDestinationTable b ON a.row = b.row + 1
	) as tmovs ON movs.id = tmovs.id WHERE movs.cuenta_id = @IDCUENTA
	DROP TABLE #TempDestinationTable;
END
GO
USE [master]
GO
ALTER DATABASE [devsu] SET  READ_WRITE 
GO
