using Dapper;
using FolderView.Dapper.AdministracionBot.Entidades;
using FolderView.Dapper.AdministracionBot.Interfaces;

namespace FolderView.Dapper.AdministracionBot.Repositorios
{
    public class UsuarioTelegramRepositorio : IUsuarioTelegramRepository
    {
        private readonly DapperContext _context;

        public UsuarioTelegramRepositorio(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<UsuarioTelegramEntidad>> GetAllAsync()
        {
            var query = @"
                SELECT
                    ut.idUsuarioTelegram as IdUsuarioTelegram,
                    u.nombre + ' ' + u.apellido as Usuario,
                    ut.telegramChatId as TelegramChatId,
                    ut.telegramUsername as TelegramUsername,
                    ut.telegramFirstName as TelegramFirstName,
                    ut.telegramLastName as TelegramLastName,
                    ut.alias as Alias,
                    ut.esPrincipal as EsPrincipal,
                    ut.estado as Estado,
                    ut.verificado as Verificado,
                    ut.fechaRegistro as FechaRegistro,
                    ut.fechaUltimaActividad as FechaUltimaActividad,
                    DATEDIFF(hour, ut.fechaUltimaActividad, GETDATE()) as HorasInactivo
                FROM abcmasplus..UsuariosTelegram ut
                INNER JOIN abcmasplus..Usuarios u ON ut.idUsuario = u.idUsuario
                WHERE ut.activo = 1
                ORDER BY ut.fechaUltimaActividad DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<UsuarioTelegramEntidad>(query);
            return result.ToList();
        }

        public async Task<UsuarioTelegramEntidad> GetByIdAsync(int id)
        {
            var query = @"
                SELECT
                    ut.idUsuarioTelegram as IdUsuarioTelegram,
                    ut.idUsuario as IdUsuario,
                    ut.telegramChatId as TelegramChatId,
                    ut.telegramUsername as TelegramUsername,
                    ut.telegramFirstName as TelegramFirstName,
                    ut.telegramLastName as TelegramLastName,
                    ut.alias as Alias,
                    ut.esPrincipal as EsPrincipal,
                    ut.estado as Estado,
                    ut.verificado as Verificado,
                    ut.fechaRegistro as FechaRegistro,
                    ut.fechaVerificacion as FechaVerificacion,
                    ut.fechaUltimaActividad as FechaUltimaActividad,
                    ut.notificacionesActivas as NotificacionesActivas,
                    ut.activo as Activo
                FROM abcmasplus..UsuariosTelegram ut
                WHERE ut.idUsuarioTelegram = @id";

            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<UsuarioTelegramEntidad>(query, new { id });
        }

        public async Task<List<UsuarioTelegramEntidad>> GetByUsuarioAsync(int idUsuario)
        {
            var query = @"
                SELECT
                    ut.idUsuarioTelegram as IdUsuarioTelegram,
                    ut.telegramChatId as TelegramChatId,
                    ut.telegramUsername as TelegramUsername,
                    ut.telegramFirstName as TelegramFirstName,
                    ut.telegramLastName as TelegramLastName,
                    ut.alias as Alias,
                    ut.esPrincipal as EsPrincipal,
                    ut.estado as Estado,
                    ut.verificado as Verificado,
                    ut.fechaRegistro as FechaRegistro,
                    ut.fechaUltimaActividad as FechaUltimaActividad,
                    ut.notificacionesActivas as NotificacionesActivas
                FROM abcmasplus..UsuariosTelegram ut
                WHERE ut.idUsuario = @idUsuario AND ut.activo = 1
                ORDER BY ut.esPrincipal DESC, ut.fechaRegistro DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<UsuarioTelegramEntidad>(query, new { idUsuario });
            return result.ToList();
        }

        public async Task<List<UsuarioTelegramEntidad>> GetPendientesVerificacionAsync()
        {
            var query = @"
                SELECT
                    ut.idUsuarioTelegram as IdUsuarioTelegram,
                    u.nombre + ' ' + u.apellido as Usuario,
                    u.email as Email,
                    ut.telegramChatId as TelegramChatId,
                    ut.telegramUsername as TelegramUsername,
                    ut.codigoVerificacion as CodigoVerificacion,
                    ut.intentosVerificacion as IntentosVerificacion,
                    ut.fechaRegistro as FechaRegistro,
                    DATEDIFF(hour, ut.fechaRegistro, GETDATE()) as HorasSinVerificar
                FROM abcmasplus..UsuariosTelegram ut
                INNER JOIN abcmasplus..Usuarios u ON ut.idUsuario = u.idUsuario
                WHERE ut.verificado = 0
                  AND ut.activo = 1
                  AND ut.estado = 'ACTIVO'
                ORDER BY ut.fechaRegistro DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<UsuarioTelegramEntidad>(query);
            return result.ToList();
        }

        public async Task<bool> VerificarCuentaAsync(int idUsuarioTelegram)
        {
            var query = @"
                UPDATE abcmasplus..UsuariosTelegram
                SET
                    verificado = 1,
                    fechaVerificacion = GETDATE(),
                    codigoVerificacion = NULL
                WHERE idUsuarioTelegram = @idUsuarioTelegram";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, new { idUsuarioTelegram });
            return affected > 0;
        }

        public async Task<bool> EstablecerPrincipalAsync(int idUsuarioTelegram, int idUsuario)
        {
            var query1 = @"
                UPDATE abcmasplus..UsuariosTelegram
                SET esPrincipal = 0
                WHERE idUsuario = @idUsuario AND activo = 1";

            var query2 = @"
                UPDATE abcmasplus..UsuariosTelegram
                SET esPrincipal = 1
                WHERE idUsuarioTelegram = @idUsuarioTelegram";

            using var connection = _context.CreateConnection();
            await connection.ExecuteAsync(query1, new { idUsuario });
            var affected = await connection.ExecuteAsync(query2, new { idUsuarioTelegram });
            return affected > 0;
        }

        public async Task<bool> CambiarEstadoAsync(int idUsuarioTelegram, string estado)
        {
            var query = @"
                UPDATE abcmasplus..UsuariosTelegram
                SET estado = @estado
                WHERE idUsuarioTelegram = @idUsuarioTelegram";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, new { idUsuarioTelegram, estado });
            return affected > 0;
        }

        public async Task<List<UsuarioTelegramEntidad>> GetUsuariosConMultiplesCuentasAsync()
        {
            var query = @"
                SELECT
                    u.idUsuario as IdUsuario,
                    u.nombre + ' ' + u.apellido as Usuario,
                    COUNT(ut.idUsuarioTelegram) as TotalCuentas
                FROM abcmasplus..Usuarios u
                INNER JOIN abcmasplus..UsuariosTelegram ut ON u.idUsuario = ut.idUsuario
                WHERE u.activo = 1
                GROUP BY u.idUsuario, u.nombre, u.apellido
                HAVING COUNT(ut.idUsuarioTelegram) > 1
                ORDER BY COUNT(ut.idUsuarioTelegram) DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<UsuarioTelegramEntidad>(query);
            return result.ToList();
        }

        public async Task<List<UsuarioTelegramEntidad>> GetCuentasByUsuarioIdAsync(int idUsuario)
        {
            var query = @"
                SELECT
                    ut.idUsuarioTelegram as IdUsuarioTelegram,
                    ut.telegramChatId as TelegramChatId,
                    ut.telegramUsername as TelegramUsername,
                    ut.telegramFirstName as TelegramFirstName,
                    ut.telegramLastName as TelegramLastName,
                    ut.alias as Alias,
                    ut.esPrincipal as EsPrincipal,
                    ut.estado as Estado,
                    ut.verificado as Verificado,
                    ut.fechaRegistro as FechaCreacion,
                    ut.fechaUltimaActividad as FechaUltimaActividad,
                    ut.notificacionesActivas as NotificacionesActivas
                FROM abcmasplus..UsuariosTelegram ut
                WHERE ut.idUsuario = @idUsuario AND ut.activo = 1
                ORDER BY ut.esPrincipal DESC, ut.fechaRegistro DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<UsuarioTelegramEntidad>(query, new { idUsuario });
            return result.ToList();
        }

        public async Task<bool> SetCuentaPrincipalAsync(int idUsuarioTelegram)
        {
            // Primero obtenemos el idUsuario de la cuenta
            var queryGetUsuario = @"
                SELECT idUsuario
                FROM abcmasplus..UsuariosTelegram
                WHERE idUsuarioTelegram = @idUsuarioTelegram";

            using var connection = _context.CreateConnection();
            var idUsuario = await connection.ExecuteScalarAsync<int>(queryGetUsuario, new { idUsuarioTelegram });

            if (idUsuario == 0)
                return false;

            // Desmarcar todas las cuentas del usuario como principal
            var query1 = @"
                UPDATE abcmasplus..UsuariosTelegram
                SET esPrincipal = 0
                WHERE idUsuario = @idUsuario AND activo = 1";

            // Marcar la cuenta específica como principal
            var query2 = @"
                UPDATE abcmasplus..UsuariosTelegram
                SET esPrincipal = 1
                WHERE idUsuarioTelegram = @idUsuarioTelegram";

            await connection.ExecuteAsync(query1, new { idUsuario });
            var affected = await connection.ExecuteAsync(query2, new { idUsuarioTelegram });
            return affected > 0;
        }

        public async Task<List<LogOperacionEntidad>> GetHistorialActividadAsync(int idUsuarioTelegram)
        {
            var query = @"
                SELECT TOP 50
                    l.idLog as IdLog,
                    l.fechaEjecucion as FechaHora,
                    o.nombre as Operacion,
                    CASE WHEN l.resultado = 'EXITOSO' THEN 1 ELSE 0 END as Exito
                FROM abcmasplus..LogOperaciones l
                INNER JOIN abcmasplus..Operaciones o ON l.idOperacion = o.idOperacion
                INNER JOIN abcmasplus..UsuariosTelegram ut ON l.idUsuario = ut.idUsuario
                WHERE ut.idUsuarioTelegram = @idUsuarioTelegram
                ORDER BY l.fechaEjecucion DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<LogOperacionEntidad>(query, new { idUsuarioTelegram });
            return result.ToList();
        }
    }
}
