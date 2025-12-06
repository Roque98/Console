/*
==============================================================================
SCRIPT: AGREGAR SISTEMA BIBLIOTECA PROMPTS
==============================================================================
Fecha: 2024-12-05
Descripción: Este script agrega el sistema "Biblioteca Prompts" a la tabla
             SistemasAplicaciones para que pueda usar configuraciones de IA
             desde ConfiguracionIASistemas.

Propósito: La SmartPromptLibrary necesita su propia configuración de IA
           independiente para generar y ejecutar prompts.
==============================================================================
*/

USE abcmasplus;
GO

PRINT '============================================';
PRINT 'AGREGANDO SISTEMA: BIBLIOTECA PROMPTS';
PRINT '============================================';
PRINT '';

-- ============================================
-- 1. INSERTAR SISTEMA BIBLIOTECA PROMPTS
-- ============================================
PRINT 'Paso 1: Insertando sistema Biblioteca Prompts...';

IF NOT EXISTS (SELECT 1 FROM [dbo].[SistemasAplicaciones] WHERE nombre = 'Biblioteca Prompts')
BEGIN
    INSERT INTO [dbo].[SistemasAplicaciones]
        (nombre, descripcion, icono, color, activo)
    VALUES
        (
            'Biblioteca Prompts',
            'Sistema de gestión y ejecución de prompts con IA',
            'fa fa-book',
            '#605ca8',
            1
        );

    DECLARE @idSistema INT = SCOPE_IDENTITY();
    PRINT '  ✓ Sistema "Biblioteca Prompts" creado con ID: ' + CAST(@idSistema AS VARCHAR);
END
ELSE
BEGIN
    PRINT '  ℹ El sistema "Biblioteca Prompts" ya existe.';

    -- Mostrar el ID existente
    DECLARE @idExistente INT;
    SELECT @idExistente = idSistemaAplicacion
    FROM [dbo].[SistemasAplicaciones]
    WHERE nombre = 'Biblioteca Prompts';

    PRINT '  ℹ ID del sistema existente: ' + CAST(@idExistente AS VARCHAR);
END
GO

-- ============================================
-- 2. VERIFICAR CONFIGURACIÓN DE IA
-- ============================================
PRINT '';
PRINT 'Paso 2: Verificando configuración de IA...';

DECLARE @idSistemaPrompts INT;
SELECT @idSistemaPrompts = idSistemaAplicacion
FROM [dbo].[SistemasAplicaciones]
WHERE nombre = 'Biblioteca Prompts';

IF EXISTS (
    SELECT 1
    FROM [dbo].[ConfiguracionIASistemas]
    WHERE idSistemaAplicacion = @idSistemaPrompts
)
BEGIN
    PRINT '  ✓ Ya existe una configuración de IA para Biblioteca Prompts.';

    -- Mostrar configuraciones existentes
    SELECT
        c.idConfiguracionIA,
        p.nombre as Proveedor,
        m.nombreCompleto as Modelo,
        c.creditosDisponibles,
        c.activo
    FROM [dbo].[ConfiguracionIASistemas] c
    INNER JOIN [dbo].[ProveedoresIA] p ON c.idProveedorIA = p.idProveedorIA
    INNER JOIN [dbo].[ModelosIA] m ON c.idModeloIA = m.idModeloIA
    WHERE c.idSistemaAplicacion = @idSistemaPrompts;
END
ELSE
BEGIN
    PRINT '  ⚠ NO existe configuración de IA para Biblioteca Prompts.';
    PRINT '';
    PRINT '  ACCIÓN REQUERIDA:';
    PRINT '  -----------------';
    PRINT '  1. Acceder a http://localhost:5035/ConfiguracionIA';
    PRINT '  2. En la pestaña "Configuraciones por Sistema"';
    PRINT '  3. Hacer clic en "Nueva Configuración"';
    PRINT '  4. Seleccionar "Biblioteca Prompts" como sistema';
    PRINT '  5. Configurar proveedor, modelo y API Key';
    PRINT '';
END
GO

-- ============================================
-- 3. RESUMEN Y PRÓXIMOS PASOS
-- ============================================
PRINT '';
PRINT '============================================';
PRINT 'SCRIPT COMPLETADO EXITOSAMENTE';
PRINT '============================================';
PRINT '';

DECLARE @idSistema INT;
SELECT @idSistema = idSistemaAplicacion
FROM [dbo].[SistemasAplicaciones]
WHERE nombre = 'Biblioteca Prompts';

PRINT 'Sistema: Biblioteca Prompts';
PRINT 'ID del Sistema: ' + CAST(@idSistema AS VARCHAR);
PRINT '';
PRINT 'PRÓXIMOS PASOS:';
PRINT '---------------';
PRINT '1. ✓ Sistema creado en SistemasAplicaciones';
PRINT '2. → Configurar credenciales de IA en /ConfiguracionIA';
PRINT '3. → El controlador SmartPromptLibrary usará automáticamente las credenciales';
PRINT '';
PRINT 'NOTA: El sistema ahora puede usar diferentes proveedores de IA:';
PRINT '  - OpenAI (GPT-4, GPT-3.5)';
PRINT '  - Anthropic (Claude)';
PRINT '  - Google (Gemini)';
PRINT '  - Mistral AI';
PRINT '';
PRINT '============================================';
GO
