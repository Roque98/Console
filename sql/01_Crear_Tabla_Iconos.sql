-- =============================================
-- Script: Crear tabla Iconos y poblar con Font Awesome 4
-- Base de datos: abcmasplus
-- Descripción: Tabla para almacenar iconos disponibles para selección en módulos
-- =============================================

USE abcmasplus;
GO

-- Eliminar tabla si existe (para desarrollo/testing)
IF OBJECT_ID('dbo.Iconos', 'U') IS NOT NULL
    DROP TABLE dbo.Iconos;
GO

-- Crear tabla Iconos
CREATE TABLE dbo.Iconos (
    idIcono INT IDENTITY(1,1) PRIMARY KEY,
    nombre VARCHAR(50) NOT NULL UNIQUE,
    clase VARCHAR(100) NOT NULL,
    categoria VARCHAR(50) NOT NULL,
    descripcion VARCHAR(200) NULL,
    activo BIT NOT NULL DEFAULT 1,
    fechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
GO

-- Crear índice para búsquedas rápidas
CREATE INDEX IX_Iconos_Categoria ON dbo.Iconos(categoria);
CREATE INDEX IX_Iconos_Activo ON dbo.Iconos(activo);
GO

-- =============================================
-- Insertar iconos de Font Awesome 4
-- Categorías: General, Negocios, Tecnología, Comunicación, Archivos, Usuarios, Sistema
-- =============================================

-- Categoría: General
INSERT INTO dbo.Iconos (nombre, clase, categoria, descripcion) VALUES
('Home', 'fa-home', 'General', 'Inicio'),
('Dashboard', 'fa-dashboard', 'General', 'Tablero de control'),
('Tachometer', 'fa-tachometer', 'General', 'Velocímetro / Dashboard'),
('Star', 'fa-star', 'General', 'Estrella'),
('Heart', 'fa-heart', 'General', 'Corazón / Favoritos'),
('Cog', 'fa-cog', 'General', 'Configuración'),
('Wrench', 'fa-wrench', 'General', 'Herramientas'),
('Settings', 'fa-gear', 'General', 'Configuración alternativa'),
('Flag', 'fa-flag', 'General', 'Bandera / Marcador'),
('Bell', 'fa-bell', 'General', 'Notificaciones'),
('Calendar', 'fa-calendar', 'General', 'Calendario'),
('Clock', 'fa-clock-o', 'General', 'Reloj'),
('Globe', 'fa-globe', 'General', 'Mundo / Global'),
('Map', 'fa-map', 'General', 'Mapa'),
('Location', 'fa-map-marker', 'General', 'Ubicación'),
('Search', 'fa-search', 'General', 'Búsqueda');

-- Categoría: Negocios
INSERT INTO dbo.Iconos (nombre, clase, categoria, descripcion) VALUES
('Briefcase', 'fa-briefcase', 'Negocios', 'Maletín / Portafolio'),
('Building', 'fa-building', 'Negocios', 'Edificio / Empresa'),
('Shopping Cart', 'fa-shopping-cart', 'Negocios', 'Carrito de compras'),
('Money', 'fa-money', 'Negocios', 'Dinero'),
('Credit Card', 'fa-credit-card', 'Negocios', 'Tarjeta de crédito'),
('Dollar', 'fa-dollar', 'Negocios', 'Dólar / Finanzas'),
('Bar Chart', 'fa-bar-chart', 'Negocios', 'Gráfico de barras'),
('Line Chart', 'fa-line-chart', 'Negocios', 'Gráfico de líneas'),
('Pie Chart', 'fa-pie-chart', 'Negocios', 'Gráfico circular'),
('Calculator', 'fa-calculator', 'Negocios', 'Calculadora'),
('Suitcase', 'fa-suitcase', 'Negocios', 'Maleta'),
('Balance Scale', 'fa-balance-scale', 'Negocios', 'Balanza / Justicia');

-- Categoría: Tecnología
INSERT INTO dbo.Iconos (nombre, clase, categoria, descripcion) VALUES
('Code', 'fa-code', 'Tecnología', 'Código / Programación'),
('Terminal', 'fa-terminal', 'Tecnología', 'Terminal / Consola'),
('Database', 'fa-database', 'Tecnología', 'Base de datos'),
('Server', 'fa-server', 'Tecnología', 'Servidor'),
('Desktop', 'fa-desktop', 'Tecnología', 'Computadora de escritorio'),
('Laptop', 'fa-laptop', 'Tecnología', 'Laptop'),
('Mobile', 'fa-mobile', 'Tecnología', 'Móvil / Celular'),
('Tablet', 'fa-tablet', 'Tecnología', 'Tableta'),
('Keyboard', 'fa-keyboard-o', 'Tecnología', 'Teclado'),
('Plug', 'fa-plug', 'Tecnología', 'Conector / Plugin'),
('Microchip', 'fa-microchip', 'Tecnología', 'Microchip / Hardware'),
('USB', 'fa-usb', 'Tecnología', 'USB'),
('Wifi', 'fa-wifi', 'Tecnología', 'WiFi / Conexión'),
('Cloud', 'fa-cloud', 'Tecnología', 'Nube'),
('Cloud Upload', 'fa-cloud-upload', 'Tecnología', 'Subir a la nube'),
('Cloud Download', 'fa-cloud-download', 'Tecnología', 'Descargar de la nube');

-- Categoría: Comunicación
INSERT INTO dbo.Iconos (nombre, clase, categoria, descripcion) VALUES
('Envelope', 'fa-envelope', 'Comunicación', 'Correo electrónico'),
('Comment', 'fa-comment', 'Comunicación', 'Comentario'),
('Comments', 'fa-comments', 'Comunicación', 'Comentarios / Chat'),
('Phone', 'fa-phone', 'Comunicación', 'Teléfono'),
('Fax', 'fa-fax', 'Comunicación', 'Fax'),
('Bullhorn', 'fa-bullhorn', 'Comunicación', 'Megáfono / Anuncios'),
('Rss', 'fa-rss', 'Comunicación', 'RSS / Noticias'),
('Inbox', 'fa-inbox', 'Comunicación', 'Bandeja de entrada'),
('Paperclip', 'fa-paperclip', 'Comunicación', 'Adjunto'),
('Share', 'fa-share', 'Comunicación', 'Compartir');

-- Categoría: Archivos
INSERT INTO dbo.Iconos (nombre, clase, categoria, descripcion) VALUES
('File', 'fa-file', 'Archivos', 'Archivo'),
('File Text', 'fa-file-text', 'Archivos', 'Archivo de texto'),
('File PDF', 'fa-file-pdf-o', 'Archivos', 'Archivo PDF'),
('File Excel', 'fa-file-excel-o', 'Archivos', 'Archivo Excel'),
('File Word', 'fa-file-word-o', 'Archivos', 'Archivo Word'),
('File Image', 'fa-file-image-o', 'Archivos', 'Archivo de imagen'),
('File Archive', 'fa-file-archive-o', 'Archivos', 'Archivo comprimido'),
('File Code', 'fa-file-code-o', 'Archivos', 'Archivo de código'),
('Folder', 'fa-folder', 'Archivos', 'Carpeta'),
('Folder Open', 'fa-folder-open', 'Archivos', 'Carpeta abierta'),
('Copy', 'fa-copy', 'Archivos', 'Copiar'),
('Paste', 'fa-paste', 'Archivos', 'Pegar'),
('Save', 'fa-save', 'Archivos', 'Guardar'),
('Download', 'fa-download', 'Archivos', 'Descargar'),
('Upload', 'fa-upload', 'Archivos', 'Subir');

-- Categoría: Usuarios
INSERT INTO dbo.Iconos (nombre, clase, categoria, descripcion) VALUES
('User', 'fa-user', 'Usuarios', 'Usuario'),
('Users', 'fa-users', 'Usuarios', 'Usuarios / Grupo'),
('User Circle', 'fa-user-circle', 'Usuarios', 'Usuario con círculo'),
('User Plus', 'fa-user-plus', 'Usuarios', 'Agregar usuario'),
('User Times', 'fa-user-times', 'Usuarios', 'Eliminar usuario'),
('Id Card', 'fa-id-card', 'Usuarios', 'Identificación'),
('Address Card', 'fa-address-card', 'Usuarios', 'Tarjeta de contacto'),
('Child', 'fa-child', 'Usuarios', 'Niño / Dependiente'),
('Group', 'fa-group', 'Usuarios', 'Grupo / Team');

-- Categoría: Sistema
INSERT INTO dbo.Iconos (nombre, clase, categoria, descripcion) VALUES
('Puzzle Piece', 'fa-puzzle-piece', 'Sistema', 'Módulo / Plugin'),
('Th', 'fa-th', 'Sistema', 'Cuadrícula / Grid'),
('Th List', 'fa-th-list', 'Sistema', 'Lista'),
('List', 'fa-list', 'Sistema', 'Lista simple'),
('Bars', 'fa-bars', 'Sistema', 'Menú hamburguesa'),
('Lock', 'fa-lock', 'Sistema', 'Candado / Seguridad'),
('Unlock', 'fa-unlock', 'Sistema', 'Desbloquear'),
('Key', 'fa-key', 'Sistema', 'Llave / Autenticación'),
('Shield', 'fa-shield', 'Sistema', 'Escudo / Protección'),
('Power Off', 'fa-power-off', 'Sistema', 'Apagar / Cerrar sesión'),
('Sign In', 'fa-sign-in', 'Sistema', 'Iniciar sesión'),
('Sign Out', 'fa-sign-out', 'Sistema', 'Cerrar sesión'),
('Eye', 'fa-eye', 'Sistema', 'Ver / Mostrar'),
('Eye Slash', 'fa-eye-slash', 'Sistema', 'Ocultar'),
('Print', 'fa-print', 'Sistema', 'Imprimir'),
('Trash', 'fa-trash', 'Sistema', 'Eliminar / Papelera'),
('Edit', 'fa-edit', 'Sistema', 'Editar'),
('Plus', 'fa-plus', 'Sistema', 'Añadir / Más'),
('Minus', 'fa-minus', 'Sistema', 'Menos / Quitar'),
('Check', 'fa-check', 'Sistema', 'Verificar / OK'),
('Times', 'fa-times', 'Sistema', 'Cerrar / Cancelar'),
('Refresh', 'fa-refresh', 'Sistema', 'Actualizar'),
('Sync', 'fa-sync', 'Sistema', 'Sincronizar'),
('Filter', 'fa-filter', 'Sistema', 'Filtrar'),
('Sort', 'fa-sort', 'Sistema', 'Ordenar'),
('Question Circle', 'fa-question-circle', 'Sistema', 'Ayuda'),
('Info Circle', 'fa-info-circle', 'Sistema', 'Información'),
('Exclamation Triangle', 'fa-exclamation-triangle', 'Sistema', 'Advertencia'),
('Bookmark', 'fa-bookmark', 'Sistema', 'Marcador'),
('Tag', 'fa-tag', 'Sistema', 'Etiqueta'),
('Tags', 'fa-tags', 'Sistema', 'Etiquetas');

GO

-- Verificar la inserción
SELECT
    categoria,
    COUNT(*) as CantidadIconos
FROM dbo.Iconos
GROUP BY categoria
ORDER BY categoria;
GO

-- Mostrar todos los iconos insertados
SELECT * FROM dbo.Iconos ORDER BY categoria, nombre;
GO

PRINT 'Tabla Iconos creada exitosamente con ' + CAST((SELECT COUNT(*) FROM dbo.Iconos) AS VARCHAR(10)) + ' iconos.';
GO
