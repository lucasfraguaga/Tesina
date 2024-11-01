USE [master]
GO
/****** Object:  User [##MS_PolicyEventProcessingLogin##]    Script Date: 3/10/2024 23:37:18 ******/
CREATE USER [##MS_PolicyEventProcessingLogin##] FOR LOGIN [##MS_PolicyEventProcessingLogin##] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [##MS_AgentSigningCertificate##]    Script Date: 3/10/2024 23:37:18 ******/
CREATE USER [##MS_AgentSigningCertificate##] FOR LOGIN [##MS_AgentSigningCertificate##]
GO
/****** Object:  Table [dbo].[permiso]    Script Date: 3/10/2024 23:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[permiso](
	[nombre] [varchar](100) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[permiso] [varchar](50) NULL,
 CONSTRAINT [PK_permiso_1] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[permiso_permiso]    Script Date: 3/10/2024 23:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[permiso_permiso](
	[id_permiso_padre] [int] NULL,
	[id_permiso_hijo] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[prueba]    Script Date: 3/10/2024 23:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[prueba](
	[id] [int] NOT NULL,
	[dni] [int] NULL,
 CONSTRAINT [PK_prueba] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 3/10/2024 23:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarios](
	[id_usuario] [int] NOT NULL,
	[nombre] [varchar](100) NULL,
 CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuarios_permisos]    Script Date: 3/10/2024 23:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarios_permisos](
	[id_usuario] [int] NOT NULL,
	[id_permiso] [int] NOT NULL,
 CONSTRAINT [PK_usuarios_permisos] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC,
	[id_permiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[permiso_permiso]  WITH CHECK ADD  CONSTRAINT [FK_permiso_permiso_permiso] FOREIGN KEY([id_permiso_padre])
REFERENCES [dbo].[permiso] ([id])
GO
ALTER TABLE [dbo].[permiso_permiso] CHECK CONSTRAINT [FK_permiso_permiso_permiso]
GO
ALTER TABLE [dbo].[permiso_permiso]  WITH CHECK ADD  CONSTRAINT [FK_permiso_permiso_permiso1] FOREIGN KEY([id_permiso_hijo])
REFERENCES [dbo].[permiso] ([id])
GO
ALTER TABLE [dbo].[permiso_permiso] CHECK CONSTRAINT [FK_permiso_permiso_permiso1]
GO
ALTER TABLE [dbo].[usuarios_permisos]  WITH CHECK ADD  CONSTRAINT [FK_usuarios_permisos_permiso] FOREIGN KEY([id_permiso])
REFERENCES [dbo].[permiso] ([id])
GO
ALTER TABLE [dbo].[usuarios_permisos] CHECK CONSTRAINT [FK_usuarios_permisos_permiso]
GO
ALTER TABLE [dbo].[usuarios_permisos]  WITH CHECK ADD  CONSTRAINT [FK_usuarios_permisos_usuarios] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuarios] ([id_usuario])
GO
ALTER TABLE [dbo].[usuarios_permisos] CHECK CONSTRAINT [FK_usuarios_permisos_usuarios]
GO
/****** Object:  StoredProcedure [dbo].[RestoreDatabase]    Script Date: 3/10/2024 23:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RestoreDatabase]
    @backupPath NVARCHAR(500)
AS
BEGIN
    DECLARE @restoreCommand NVARCHAR(1000);

    -- Asegurarse de que la base de datos está en modo de un solo usuario para la restauración
    SET @restoreCommand = 'ALTER DATABASE [web] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;';
    EXEC sp_executesql @restoreCommand;

    -- Comando de restauración
    SET @restoreCommand = 'RESTORE DATABASE [web] FROM DISK = ''' + @backupPath + ''' WITH REPLACE;';
    EXEC sp_executesql @restoreCommand;

    -- Volver a modo multiusuario
    SET @restoreCommand = 'ALTER DATABASE [web] SET MULTI_USER;';
    EXEC sp_executesql @restoreCommand;
END;

GO
/****** Object:  StoredProcedure [dbo].[RestoreRiegoAutonomo]    Script Date: 3/10/2024 23:37:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RestoreRiegoAutonomo]
    @RutaBackup NVARCHAR(255)   -- Ruta del archivo .bak del backup
AS
BEGIN
    DECLARE @ComandoRestore NVARCHAR(MAX)
    
    -- Cerramos las conexiones activas en la base de datos si es necesario
    DECLARE @KillConnections NVARCHAR(MAX)
    SET @KillConnections = 'DECLARE @spid INT; ' +
                           'DECLARE cur CURSOR FOR SELECT spid FROM sys.sysprocesses WHERE dbid = DB_ID(''RiegoAutonomo''); ' +
                           'OPEN cur; ' +
                           'FETCH NEXT FROM cur INTO @spid; ' +
                           'WHILE @@FETCH_STATUS = 0 BEGIN EXEC(''KILL '' + @spid); FETCH NEXT FROM cur INTO @spid; END; ' +
                           'CLOSE cur; ' +
                           'DEALLOCATE cur;'
    
    EXEC sp_executesql @KillConnections

    -- Comando de restore
    SET @ComandoRestore = 'RESTORE DATABASE [RiegoAutonomo] FROM DISK = ''' + @RutaBackup + ''' WITH REPLACE'
    
    -- Ejecutamos el comando de restore
    EXEC sp_executesql @ComandoRestore
END
GO
