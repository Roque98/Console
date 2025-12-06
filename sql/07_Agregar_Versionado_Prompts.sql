-- =============================================
-- Script: Versionado de Prompts
-- Descripción: Agrega capacidad de versionado completo a SmartPromptLibrary
-- Fecha: 2025-12-05
-- =============================================

USE abcmasplus;
GO

-- =============================================
-- Tabla: Historial de Versiones de Prompts
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VersionesPrompt]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[VersionesPrompt]
    (
        idVersionPrompt INT IDENTITY(1,1) PRIMARY KEY,
        idPrompt INT NOT NULL,
        numeroVersion INT NOT NULL,
        titulo NVARCHAR(200) NOT NULL,
        descripcion NVARCHAR(1000),
        contenidoMarkdown NVARCHAR(MAX) NOT NULL,
        mensajeSistema NVARCHAR(MAX),
        mensajeCambio NVARCHAR(500),  -- Descripción de qué cambió en esta versión
        esVersionActual BIT NOT NULL DEFAULT 0,  -- TRUE si esta es la versión actualmente en uso
        fechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
        creadoPorUsuario NVARCHAR(100),
        CONSTRAINT FK_VersionesPrompt_Prompt FOREIGN KEY (idPrompt)
            REFERENCES [dbo].[BibliotecaPrompts](idPrompt) ON DELETE CASCADE,
        CONSTRAINT UQ_VersionPrompt_Numero UNIQUE (idPrompt, numeroVersion)
    );
    PRINT 'Tabla VersionesPrompt creada exitosamente';
END
GO

-- =============================================
-- Índices para optimización
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_VersionesPrompt_Prompt')
    CREATE INDEX IX_VersionesPrompt_Prompt ON [dbo].[VersionesPrompt](idPrompt);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_VersionesPrompt_Actual')
    CREATE INDEX IX_VersionesPrompt_Actual ON [dbo].[VersionesPrompt](esVersionActual);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_VersionesPrompt_Fecha')
    CREATE INDEX IX_VersionesPrompt_Fecha ON [dbo].[VersionesPrompt](fechaCreacion DESC);

PRINT 'Índices de versionado creados exitosamente';
GO

-- =============================================
-- Migrar versiones existentes al historial
-- =============================================
-- Crear versiones iniciales para todos los prompts existentes
IF EXISTS (SELECT * FROM [dbo].[BibliotecaPrompts])
BEGIN
    INSERT INTO [dbo].[VersionesPrompt]
        (idPrompt, numeroVersion, titulo, descripcion, contenidoMarkdown,
         mensajeSistema, mensajeCambio, esVersionActual, fechaCreacion, creadoPorUsuario)
    SELECT
        idPrompt,
        version AS numeroVersion,
        titulo,
        descripcion,
        contenidoMarkdown,
        mensajeSistema,
        'Versión inicial migrada del sistema existente' AS mensajeCambio,
        1 AS esVersionActual,  -- Marcar como versión actual
        fechaCreacion,
        creadoPorUsuario
    FROM [dbo].[BibliotecaPrompts];

    PRINT 'Versiones iniciales migradas al historial';
END
GO

-- =============================================
-- Stored Procedure: Crear nueva versión de un prompt
-- =============================================
IF OBJECT_ID('[dbo].[sp_CrearVersionPrompt]', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].[sp_CrearVersionPrompt];
GO

CREATE PROCEDURE [dbo].[sp_CrearVersionPrompt]
    @idPrompt INT,
    @titulo NVARCHAR(200),
    @descripcion NVARCHAR(1000),
    @contenidoMarkdown NVARCHAR(MAX),
    @mensajeSistema NVARCHAR(MAX),
    @mensajeCambio NVARCHAR(500),
    @usuario NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    BEGIN TRY
        DECLARE @nuevoNumeroVersion INT;
        DECLARE @versionActual INT;

        -- Obtener el número de versión actual
        SELECT @versionActual = version FROM [dbo].[BibliotecaPrompts] WHERE idPrompt = @idPrompt;

        -- Calcular el nuevo número de versión
        SET @nuevoNumeroVersion = @versionActual + 1;

        -- Marcar la versión actual como no actual
        UPDATE [dbo].[VersionesPrompt]
        SET esVersionActual = 0
        WHERE idPrompt = @idPrompt AND esVersionActual = 1;

        -- Insertar la nueva versión en el historial
        INSERT INTO [dbo].[VersionesPrompt]
            (idPrompt, numeroVersion, titulo, descripcion, contenidoMarkdown,
             mensajeSistema, mensajeCambio, esVersionActual, fechaCreacion, creadoPorUsuario)
        VALUES
            (@idPrompt, @nuevoNumeroVersion, @titulo, @descripcion, @contenidoMarkdown,
             @mensajeSistema, @mensajeCambio, 1, GETDATE(), @usuario);

        -- Actualizar el prompt principal con la nueva versión
        UPDATE [dbo].[BibliotecaPrompts]
        SET
            titulo = @titulo,
            descripcion = @descripcion,
            contenidoMarkdown = @contenidoMarkdown,
            mensajeSistema = @mensajeSistema,
            version = @nuevoNumeroVersion,
            fechaActualizacion = GETDATE()
        WHERE idPrompt = @idPrompt;

        COMMIT TRANSACTION;
        SELECT 1 AS Success, @nuevoNumeroVersion AS NumeroVersion;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT 0 AS Success, ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

-- =============================================
-- Stored Procedure: Restaurar versión anterior
-- =============================================
IF OBJECT_ID('[dbo].[sp_RestaurarVersionPrompt]', 'P') IS NOT NULL
    DROP PROCEDURE [dbo].[sp_RestaurarVersionPrompt];
GO

CREATE PROCEDURE [dbo].[sp_RestaurarVersionPrompt]
    @idPrompt INT,
    @numeroVersion INT,
    @usuario NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;

    BEGIN TRY
        DECLARE @titulo NVARCHAR(200);
        DECLARE @descripcion NVARCHAR(1000);
        DECLARE @contenidoMarkdown NVARCHAR(MAX);
        DECLARE @mensajeSistema NVARCHAR(MAX);
        DECLARE @nuevoNumeroVersion INT;

        -- Verificar que la versión existe
        IF NOT EXISTS (SELECT 1 FROM [dbo].[VersionesPrompt] WHERE idPrompt = @idPrompt AND numeroVersion = @numeroVersion)
        BEGIN
            SELECT 0 AS Success, 'La versión especificada no existe' AS ErrorMessage;
            ROLLBACK TRANSACTION;
            RETURN;
        END

        -- Obtener los datos de la versión a restaurar
        SELECT
            @titulo = titulo,
            @descripcion = descripcion,
            @contenidoMarkdown = contenidoMarkdown,
            @mensajeSistema = mensajeSistema
        FROM [dbo].[VersionesPrompt]
        WHERE idPrompt = @idPrompt AND numeroVersion = @numeroVersion;

        -- Obtener el número de versión actual
        SELECT @nuevoNumeroVersion = MAX(numeroVersion) + 1
        FROM [dbo].[VersionesPrompt]
        WHERE idPrompt = @idPrompt;

        -- Marcar todas las versiones como no actuales
        UPDATE [dbo].[VersionesPrompt]
        SET esVersionActual = 0
        WHERE idPrompt = @idPrompt;

        -- Crear nueva versión (restauración)
        INSERT INTO [dbo].[VersionesPrompt]
            (idPrompt, numeroVersion, titulo, descripcion, contenidoMarkdown,
             mensajeSistema, mensajeCambio, esVersionActual, fechaCreacion, creadoPorUsuario)
        VALUES
            (@idPrompt, @nuevoNumeroVersion, @titulo, @descripcion, @contenidoMarkdown,
             @mensajeSistema, 'Restaurado desde versión ' + CAST(@numeroVersion AS NVARCHAR(10)), 1, GETDATE(), @usuario);

        -- Actualizar el prompt principal
        UPDATE [dbo].[BibliotecaPrompts]
        SET
            titulo = @titulo,
            descripcion = @descripcion,
            contenidoMarkdown = @contenidoMarkdown,
            mensajeSistema = @mensajeSistema,
            version = @nuevoNumeroVersion,
            fechaActualizacion = GETDATE()
        WHERE idPrompt = @idPrompt;

        COMMIT TRANSACTION;
        SELECT 1 AS Success, @nuevoNumeroVersion AS NumeroVersion;
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        SELECT 0 AS Success, ERROR_MESSAGE() AS ErrorMessage;
    END CATCH
END
GO

PRINT '✓ Script de versionado de prompts completado exitosamente';
GO
