using Dapper;
using FolderView.Dapper.AdministracionBot.Entidades;
using FolderView.Dapper.AdministracionBot.Interfaces;

namespace FolderView.Dapper.AdministracionBot.Repositorios
{
    public class DashboardRepositorio : IDashboardRepository
    {
        private readonly DapperContext _context;

        public DashboardRepositorio(DapperContext context)
        {
            _context = context;
        }

        public async Task<DashboardStatsEntidad> GetStatsAsync()
        {
            var query = @"
                SELECT
                    (SELECT COUNT(*) FROM abcmasplus..Usuarios WHERE activo = 1) as TotalUsuarios,
                    (SELECT COUNT(DISTINCT ut.idUsuario) FROM abcmasplus..UsuariosTelegram ut WHERE ut.activo = 1 AND ut.verificado = 1) as UsuariosConTelegram,
                    (SELECT COUNT(*) FROM abcmasplus..LogOperaciones WHERE CAST(fechaEjecucion AS DATE) = CAST(GETDATE() AS DATE)) as OperacionesHoy,
                    (SELECT COUNT(*) FROM abcmasplus..Operaciones WHERE activo = 1) as OperacionesDisponibles,
                    (SELECT COUNT(*) FROM abcmasplus..knowledge_entries WHERE active = 1) as EntradasConocimiento,
                    (SELECT TOP 1 fechaEjecucion FROM abcmasplus..LogOperaciones ORDER BY fechaEjecucion DESC) as UltimaActividad";

            using var connection = _context.CreateConnection();
            return await connection.QuerySingleAsync<DashboardStatsEntidad>(query);
        }

        public async Task<List<OperacionResultadoEntidad>> GetOperacionesPorResultadoHoyAsync()
        {
            var query = @"
                SELECT resultado as Resultado, COUNT(*) as Cantidad
                FROM abcmasplus..LogOperaciones
                WHERE CAST(fechaEjecucion AS DATE) = CAST(GETDATE() AS DATE)
                GROUP BY resultado";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<OperacionResultadoEntidad>(query);
            return result.ToList();
        }

        public async Task<List<TopOperacionEntidad>> GetTopOperacionesAsync(int top = 5)
        {
            var query = $@"
                SELECT TOP {top}
                    o.nombre as Operacion,
                    o.comando as Comando,
                    COUNT(*) as TotalEjecuciones
                FROM abcmasplus..LogOperaciones l
                INNER JOIN abcmasplus..Operaciones o ON l.idOperacion = o.idOperacion
                GROUP BY o.idOperacion, o.nombre, o.comando
                ORDER BY TotalEjecuciones DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<TopOperacionEntidad>(query);
            return result.ToList();
        }

        public async Task<List<TopUsuarioEntidad>> GetTopUsuariosAsync(int top = 5)
        {
            var query = $@"
                SELECT TOP {top}
                    u.nombre + ' ' + u.apellido as Usuario,
                    COUNT(*) as TotalOperaciones,
                    MAX(l.fechaEjecucion) as UltimaActividad
                FROM abcmasplus..LogOperaciones l
                INNER JOIN abcmasplus..Usuarios u ON l.idUsuario = u.idUsuario
                GROUP BY u.idUsuario, u.nombre, u.apellido
                ORDER BY TotalOperaciones DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<TopUsuarioEntidad>(query);
            return result.ToList();
        }

        public async Task<List<TasaExitoEntidad>> GetTasaExitoUltimos7DiasAsync()
        {
            var query = @"
                SELECT
                    CAST(fechaEjecucion AS DATE) as Fecha,
                    COUNT(*) as Total,
                    SUM(CASE WHEN resultado = 'EXITOSO' THEN 1 ELSE 0 END) as Exitosas,
                    CAST(SUM(CASE WHEN resultado = 'EXITOSO' THEN 1.0 ELSE 0 END) / COUNT(*) * 100 AS DECIMAL(5,2)) as PorcentajeExito
                FROM abcmasplus..LogOperaciones
                WHERE fechaEjecucion >= DATEADD(day, -7, GETDATE())
                GROUP BY CAST(fechaEjecucion AS DATE)
                ORDER BY Fecha DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<TasaExitoEntidad>(query);
            return result.ToList();
        }
    }
}
