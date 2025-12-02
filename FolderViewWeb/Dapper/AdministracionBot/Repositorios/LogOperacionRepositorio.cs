using Dapper;
using FolderView.Dapper.AdministracionBot.Entidades;
using FolderView.Dapper.AdministracionBot.Interfaces;

namespace FolderView.Dapper.AdministracionBot.Repositorios
{
    public class LogOperacionRepositorio : ILogOperacionRepository
    {
        private readonly DapperContext _context;

        public LogOperacionRepositorio(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<LogOperacionEntidad>> GetAllAsync(int offset = 0, int pageSize = 50)
        {
            var query = @"
                SELECT
                    l.idLog as IdLog,
                    l.fechaEjecucion as FechaEjecucion,
                    u.nombre + ' ' + u.apellido as Usuario,
                    o.nombre as Operacion,
                    o.comando as Comando,
                    m.nombre as Modulo,
                    l.resultado as Resultado,
                    l.duracionMs as DuracionMs,
                    l.telegramChatId as TelegramChatId,
                    l.telegramUsername as TelegramUsername
                FROM abcmasplus..LogOperaciones l
                INNER JOIN abcmasplus..Usuarios u ON l.idUsuario = u.idUsuario
                INNER JOIN abcmasplus..Operaciones o ON l.idOperacion = o.idOperacion
                INNER JOIN abcmasplus..Modulos m ON o.idModulo = m.idModulo
                ORDER BY l.fechaEjecucion DESC
                OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<LogOperacionEntidad>(query, new { offset, pageSize });
            return result.ToList();
        }

        public async Task<LogOperacionEntidad> GetByIdAsync(int id)
        {
            var query = @"
                SELECT
                    l.idLog as IdLog,
                    l.fechaEjecucion as FechaHora,
                    u.idUsuario as IdUsuario,
                    u.nombre + ' ' + u.apellido as Usuario,
                    u.email as Email,
                    o.idOperacion as IdOperacion,
                    o.nombre as Operacion,
                    o.comando as Comando,
                    o.descripcion as DescripcionOperacion,
                    m.nombre as Modulo,
                    l.telegramChatId as TelegramChatId,
                    l.telegramUsername as TelegramUsername,
                    l.parametros as Parametros,
                    l.resultado as Resultado,
                    CASE WHEN l.resultado = 'EXITOSO' THEN 1 ELSE 0 END as Exito,
                    l.mensajeError as ErrorDetalle,
                    l.duracionMs as TiempoEjecucion,
                    l.ipOrigen as IpAddress
                FROM abcmasplus..LogOperaciones l
                INNER JOIN abcmasplus..Usuarios u ON l.idUsuario = u.idUsuario
                INNER JOIN abcmasplus..Operaciones o ON l.idOperacion = o.idOperacion
                INNER JOIN abcmasplus..Modulos m ON o.idModulo = m.idModulo
                WHERE l.idLog = @id";

            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<LogOperacionEntidad>(query, new { id });
        }

        public async Task<List<LogOperacionEntidad>> GetConFiltrosAsync(int? idUsuario = null, int? idOperacion = null,
            string resultado = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null)
        {
            var query = @"
                SELECT
                    l.idLog as IdLog,
                    l.fechaEjecucion as FechaEjecucion,
                    u.nombre + ' ' + u.apellido as Usuario,
                    o.nombre as Operacion,
                    o.comando as Comando,
                    m.nombre as Modulo,
                    l.resultado as Resultado,
                    l.duracionMs as DuracionMs,
                    l.parametros as Parametros,
                    l.mensajeError as MensajeError
                FROM abcmasplus..LogOperaciones l
                INNER JOIN abcmasplus..Usuarios u ON l.idUsuario = u.idUsuario
                INNER JOIN abcmasplus..Operaciones o ON l.idOperacion = o.idOperacion
                INNER JOIN abcmasplus..Modulos m ON o.idModulo = m.idModulo
                WHERE
                    (@idUsuario IS NULL OR l.idUsuario = @idUsuario)
                    AND (@idOperacion IS NULL OR l.idOperacion = @idOperacion)
                    AND (@resultado IS NULL OR l.resultado = @resultado)
                    AND (@fechaDesde IS NULL OR l.fechaEjecucion >= @fechaDesde)
                    AND (@fechaHasta IS NULL OR l.fechaEjecucion <= @fechaHasta)
                ORDER BY l.fechaEjecucion DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<LogOperacionEntidad>(query,
                new { idUsuario, idOperacion, resultado, fechaDesde, fechaHasta });
            return result.ToList();
        }

        public async Task<List<LogOperacionEntidad>> GetOperacionesfallidasRecientesAsync(int horas = 24, int top = 100)
        {
            var query = $@"
                SELECT TOP {top}
                    l.idLog as IdLog,
                    l.fechaEjecucion as FechaEjecucion,
                    u.nombre + ' ' + u.apellido as Usuario,
                    o.comando as Comando,
                    l.mensajeError as MensajeError,
                    l.telegramUsername as TelegramUsername
                FROM abcmasplus..LogOperaciones l
                INNER JOIN abcmasplus..Usuarios u ON l.idUsuario = u.idUsuario
                INNER JOIN abcmasplus..Operaciones o ON l.idOperacion = o.idOperacion
                WHERE l.resultado IN ('ERROR', 'DENEGADO')
                  AND l.fechaEjecucion >= DATEADD(hour, -@horas, GETDATE())
                ORDER BY l.fechaEjecucion DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<LogOperacionEntidad>(query, new { horas });
            return result.ToList();
        }

        public async Task<List<LogOperacionEntidad>> GetOperacionesPorDiaAsync(int dias = 30)
        {
            var query = @"
                SELECT
                    CAST(l.fechaEjecucion AS DATE) as FechaEjecucion,
                    COUNT(*) as Total,
                    SUM(CASE WHEN l.resultado = 'EXITOSO' THEN 1 ELSE 0 END) as Exitosas,
                    SUM(CASE WHEN l.resultado = 'ERROR' THEN 1 ELSE 0 END) as Errores
                FROM abcmasplus..LogOperaciones l
                WHERE l.fechaEjecucion >= DATEADD(day, -@dias, GETDATE())
                GROUP BY CAST(l.fechaEjecucion AS DATE)
                ORDER BY CAST(l.fechaEjecucion AS DATE) DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<LogOperacionEntidad>(query, new { dias });
            return result.ToList();
        }

        public async Task<List<LogOperacionEntidad>> GetByUsuarioAsync(int idUsuario, DateTime fechaInicio, DateTime fechaFin)
        {
            var query = @"
                SELECT
                    l.idLog as IdLog,
                    l.fechaEjecucion as FechaHora,
                    u.nombre + ' ' + u.apellido as Usuario,
                    o.nombre as Operacion,
                    o.comando as Comando,
                    m.nombre as Modulo,
                    l.resultado as Resultado,
                    CASE WHEN l.resultado = 'EXITOSO' THEN 1 ELSE 0 END as Exito,
                    l.duracionMs as TiempoEjecucion,
                    l.telegramUsername as TelegramUsername,
                    l.ipOrigen as IpAddress,
                    l.parametros as Parametros,
                    l.mensajeError as ErrorDetalle
                FROM abcmasplus..LogOperaciones l
                INNER JOIN abcmasplus..Usuarios u ON l.idUsuario = u.idUsuario
                INNER JOIN abcmasplus..Operaciones o ON l.idOperacion = o.idOperacion
                INNER JOIN abcmasplus..Modulos m ON o.idModulo = m.idModulo
                WHERE l.idUsuario = @idUsuario
                    AND l.fechaEjecucion >= @fechaInicio
                    AND l.fechaEjecucion <= @fechaFin
                ORDER BY l.fechaEjecucion DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<LogOperacionEntidad>(query,
                new { idUsuario, fechaInicio, fechaFin });
            return result.ToList();
        }
    }
}
