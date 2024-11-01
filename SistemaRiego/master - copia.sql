USE [master]

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
