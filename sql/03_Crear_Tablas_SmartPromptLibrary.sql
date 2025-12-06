-- =============================================
-- Script: SmartPromptLibrary - Tablas
-- Descripción: Módulo para gestión, refinamiento y prueba de prompts de IA
-- Fecha: 2025-12-03
-- =============================================

USE abcmasplus;
GO

-- =============================================
-- Tabla: Categorías de Prompts
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CategoriasPrompt]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[CategoriasPrompt]
    (
        idCategoriaPrompt INT IDENTITY(1,1) PRIMARY KEY,
        nombre NVARCHAR(100) NOT NULL,
        descripcion NVARCHAR(500),
        icono NVARCHAR(50),
        color NVARCHAR(20),
        activo BIT NOT NULL DEFAULT 1,
        fechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
        fechaActualizacion DATETIME
    );
    PRINT 'Tabla CategoriasPrompt creada exitosamente';
END
GO

-- =============================================
-- Tabla: Etiquetas de Prompts
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EtiquetasPrompt]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[EtiquetasPrompt]
    (
        idEtiquetaPrompt INT IDENTITY(1,1) PRIMARY KEY,
        nombre NVARCHAR(50) NOT NULL UNIQUE,
        color NVARCHAR(20),
        activo BIT NOT NULL DEFAULT 1,
        fechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
    );
    PRINT 'Tabla EtiquetasPrompt creada exitosamente';
END
GO

-- =============================================
-- Tabla: Biblioteca de Prompts
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BibliotecaPrompts]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[BibliotecaPrompts]
    (
        idPrompt INT IDENTITY(1,1) PRIMARY KEY,
        idCategoriaPrompt INT NOT NULL,
        titulo NVARCHAR(200) NOT NULL,
        descripcion NVARCHAR(1000),
        contenidoMarkdown NVARCHAR(MAX) NOT NULL,
        mensajeSistema NVARCHAR(MAX),
        version INT NOT NULL DEFAULT 1,
        activo BIT NOT NULL DEFAULT 1,
        favorito BIT NOT NULL DEFAULT 0,
        cantidadEjecuciones INT NOT NULL DEFAULT 0,
        ultimaEjecucion DATETIME,
        fechaCreacion DATETIME NOT NULL DEFAULT GETDATE(),
        fechaActualizacion DATETIME,
        creadoPorUsuario NVARCHAR(100),
        CONSTRAINT FK_BibliotecaPrompts_Categoria FOREIGN KEY (idCategoriaPrompt)
            REFERENCES [dbo].[CategoriasPrompt](idCategoriaPrompt)
    );
    PRINT 'Tabla BibliotecaPrompts creada exitosamente';
END
GO

-- =============================================
-- Tabla: Relación Prompts - Etiquetas (N:M)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PromptEtiquetas]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[PromptEtiquetas]
    (
        idPromptEtiqueta INT IDENTITY(1,1) PRIMARY KEY,
        idPrompt INT NOT NULL,
        idEtiquetaPrompt INT NOT NULL,
        CONSTRAINT FK_PromptEtiquetas_Prompt FOREIGN KEY (idPrompt)
            REFERENCES [dbo].[BibliotecaPrompts](idPrompt) ON DELETE CASCADE,
        CONSTRAINT FK_PromptEtiquetas_Etiqueta FOREIGN KEY (idEtiquetaPrompt)
            REFERENCES [dbo].[EtiquetasPrompt](idEtiquetaPrompt) ON DELETE CASCADE,
        CONSTRAINT UQ_PromptEtiqueta UNIQUE (idPrompt, idEtiquetaPrompt)
    );
    PRINT 'Tabla PromptEtiquetas creada exitosamente';
END
GO

-- =============================================
-- Tabla: Historial de Ejecuciones de Prompts
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EjecucionesPrompt]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[EjecucionesPrompt]
    (
        idEjecucionPrompt INT IDENTITY(1,1) PRIMARY KEY,
        idPrompt INT NOT NULL,
        promptFinal NVARCHAR(MAX) NOT NULL,
        respuestaIA NVARCHAR(MAX),
        exitoso BIT NOT NULL DEFAULT 0,
        mensajeError NVARCHAR(MAX),
        tokensUsados INT,
        costoEstimado DECIMAL(10, 6),
        tiempoRespuestaMs INT,
        idProveedorIA INT,
        idModeloIA INT,
        temperatura DECIMAL(3, 2),
        maxTokens INT,
        fechaEjecucion DATETIME NOT NULL DEFAULT GETDATE(),
        ejecutadoPorUsuario NVARCHAR(100),
        CONSTRAINT FK_EjecucionesPrompt_Prompt FOREIGN KEY (idPrompt)
            REFERENCES [dbo].[BibliotecaPrompts](idPrompt)
    );
    PRINT 'Tabla EjecucionesPrompt creada exitosamente';
END
GO

-- =============================================
-- Tabla: Parámetros de Ejecución
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ParametrosEjecucion]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[ParametrosEjecucion]
    (
        idParametroEjecucion INT IDENTITY(1,1) PRIMARY KEY,
        idEjecucionPrompt INT NOT NULL,
        nombreParametro NVARCHAR(100) NOT NULL,
        valorParametro NVARCHAR(MAX) NOT NULL,
        CONSTRAINT FK_ParametrosEjecucion_Ejecucion FOREIGN KEY (idEjecucionPrompt)
            REFERENCES [dbo].[EjecucionesPrompt](idEjecucionPrompt) ON DELETE CASCADE
    );
    PRINT 'Tabla ParametrosEjecucion creada exitosamente';
END
GO

-- =============================================
-- Índices para optimización
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_BibliotecaPrompts_Categoria')
    CREATE INDEX IX_BibliotecaPrompts_Categoria ON [dbo].[BibliotecaPrompts](idCategoriaPrompt);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_BibliotecaPrompts_Activo')
    CREATE INDEX IX_BibliotecaPrompts_Activo ON [dbo].[BibliotecaPrompts](activo);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_BibliotecaPrompts_Favorito')
    CREATE INDEX IX_BibliotecaPrompts_Favorito ON [dbo].[BibliotecaPrompts](favorito);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_EjecucionesPrompt_Prompt')
    CREATE INDEX IX_EjecucionesPrompt_Prompt ON [dbo].[EjecucionesPrompt](idPrompt);

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_EjecucionesPrompt_Fecha')
    CREATE INDEX IX_EjecucionesPrompt_Fecha ON [dbo].[EjecucionesPrompt](fechaEjecucion DESC);

PRINT 'Índices creados exitosamente';
GO

-- =============================================
-- Datos iniciales: Categorías de ejemplo
-- =============================================
IF NOT EXISTS (SELECT * FROM [dbo].[CategoriasPrompt])
BEGIN
    INSERT INTO [dbo].[CategoriasPrompt] (nombre, descripcion, icono, color)
    VALUES
        ('General', 'Prompts de propósito general', 'bi-star-fill', '#6366f1'),
        ('Desarrollo de Software', 'Prompts para generación y revisión de código', 'bi-code-slash', '#10b981'),
        ('Escritura y Contenido', 'Prompts para creación de contenido y redacción', 'bi-pencil-square', '#f59e0b'),
        ('Análisis de Datos', 'Prompts para análisis y procesamiento de datos', 'bi-graph-up', '#3b82f6'),
        ('Marketing', 'Prompts para estrategias de marketing y publicidad', 'bi-megaphone-fill', '#ec4899'),
        ('Educación', 'Prompts para enseñanza y aprendizaje', 'bi-book-fill', '#8b5cf6'),
        ('Traducción', 'Prompts para traducción de idiomas', 'bi-translate', '#14b8a6'),
        ('Atención al Cliente', 'Prompts para servicio y soporte al cliente', 'bi-headset', '#f97316');

    PRINT 'Categorías de ejemplo insertadas';
END
GO

-- =============================================
-- Datos iniciales: Etiquetas de ejemplo
-- =============================================
IF NOT EXISTS (SELECT * FROM [dbo].[EtiquetasPrompt])
BEGIN
    INSERT INTO [dbo].[EtiquetasPrompt] (nombre, color)
    VALUES
        ('producción', '#ef4444'),
        ('desarrollo', '#3b82f6'),
        ('testing', '#f59e0b'),
        ('documentación', '#8b5cf6'),
        ('optimizado', '#10b981'),
        ('experimental', '#ec4899'),
        ('aprobado', '#14b8a6'),
        ('revisión', '#f97316');

    PRINT 'Etiquetas de ejemplo insertadas';
END
GO

-- =============================================
-- Prompts de ejemplo
-- =============================================
IF NOT EXISTS (SELECT * FROM [dbo].[BibliotecaPrompts])
BEGIN
    -- Prompt de ejemplo 1: Generador de código
    INSERT INTO [dbo].[BibliotecaPrompts]
        (idCategoriaPrompt, titulo, descripcion, contenidoMarkdown, mensajeSistema, creadoPorUsuario)
    VALUES
        (2, 'Generador de API REST en C#',
         'Genera un controlador de API REST completo en C# con operaciones CRUD',
         '# Generador de API REST en C#

Por favor, genera un controlador de API REST en C# para la entidad **{{nombre_entidad}}**.

## Requisitos:
- Operaciones CRUD completas (Create, Read, Update, Delete)
- Usar **{{framework}}** como framework
- Incluir validaciones básicas
- Documentación con comentarios XML
- Manejo de errores apropiado

## Campos de la entidad:
{{campos_entidad}}

## Notas adicionales:
{{notas_adicionales}}',
         'Eres un experto desarrollador de C# y .NET. Genera código limpio, siguiendo las mejores prácticas y los principios SOLID.',
         'Sistema');

    -- Prompt de ejemplo 2: Revisor de código
    INSERT INTO [dbo].[BibliotecaPrompts]
        (idCategoriaPrompt, titulo, descripcion, contenidoMarkdown, mensajeSistema, creadoPorUsuario)
    VALUES
        (2, 'Revisor de Código y Buenas Prácticas',
         'Analiza código fuente y sugiere mejoras basadas en buenas prácticas',
         '# Revisión de Código

Por favor, analiza el siguiente código {{lenguaje}} y proporciona una revisión detallada:

```{{lenguaje}}
{{codigo}}
```

## Aspectos a revisar:
- ✅ Buenas prácticas del lenguaje
- ✅ Rendimiento y optimización
- ✅ Seguridad
- ✅ Legibilidad y mantenibilidad
- ✅ Posibles bugs o errores

## Contexto adicional:
{{contexto}}',
         'Eres un arquitecto de software senior con amplia experiencia en revisiones de código. Proporciona críticas constructivas y sugerencias prácticas.',
         'Sistema');

    -- Prompt de ejemplo 3: Generador de contenido
    INSERT INTO [dbo].[BibliotecaPrompts]
        (idCategoriaPrompt, titulo, descripcion, contenidoMarkdown, mensajeSistema, creadoPorUsuario)
    VALUES
        (3, 'Generador de Artículos de Blog',
         'Crea artículos de blog optimizados para SEO sobre cualquier tema',
         '# Generador de Artículo de Blog

Crea un artículo de blog profesional sobre el siguiente tema:

## Tema Principal:
**{{tema}}**

## Audiencia Objetivo:
{{audiencia}}

## Longitud deseada:
{{longitud}} palabras aproximadamente

## Tono del artículo:
{{tono}}

## Palabras clave SEO:
{{palabras_clave}}

## Estructura requerida:
1. Título atractivo
2. Introducción enganchante
3. 3-5 secciones con subtítulos
4. Conclusión con llamada a la acción
5. Meta descripción (150 caracteres)',
         'Eres un escritor profesional especializado en contenido para blogs y marketing digital. Crea contenido atractivo, informativo y optimizado para SEO.',
         'Sistema');

    PRINT 'Prompts de ejemplo insertados';

    -- Asignar etiquetas a los prompts de ejemplo
    INSERT INTO [dbo].[PromptEtiquetas] (idPrompt, idEtiquetaPrompt)
    VALUES
        (1, 5), -- optimizado
        (1, 7), -- aprobado
        (2, 4), -- documentación
        (2, 5), -- optimizado
        (3, 5), -- optimizado
        (3, 7); -- aprobado

    PRINT 'Etiquetas asignadas a prompts de ejemplo';
END
GO

PRINT '✓ Script de SmartPromptLibrary completado exitosamente';
GO
