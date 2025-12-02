using Dapper;
using FolderView.Dapper.AdministracionBot.Entidades;
using FolderView.Dapper.AdministracionBot.Interfaces;

namespace FolderView.Dapper.AdministracionBot.Repositorios
{
    public class RolRepositorio : IRolRepository
    {
        private readonly DapperContext _context;

        public RolRepositorio(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<RolEntidad>> GetAllAsync()
        {
            var query = @"
                SELECT
                    r.idRol as IdRol,
                    r.nombre as Nombre,
                    r.fechaCreacion as FechaCreacion,
                    r.activo as Activo,
                    (SELECT COUNT(*) FROM abcmasplus..Usuarios u WHERE u.rol = r.idRol AND u.activo = 1) as TotalUsuarios,
                    (SELECT COUNT(*) FROM abcmasplus..RolesOperaciones ro WHERE ro.idRol = r.idRol AND ro.permitido = 1 AND ro.activo = 1) as PermisosAsignados
                FROM abcmasplus..Roles r
                ORDER BY r.nombre";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<RolEntidad>(query);
            return result.ToList();
        }

        public async Task<RolEntidad> GetByIdAsync(int id)
        {
            var query = @"
                SELECT
                    r.idRol as IdRol,
                    r.nombre as Nombre,
                    r.fechaCreacion as FechaCreacion,
                    r.activo as Activo
                FROM abcmasplus..Roles r
                WHERE r.idRol = @id";

            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<RolEntidad>(query, new { id });
        }

        public async Task<List<RolOperacionEntidad>> GetPermisosByRolAsync(int idRol)
        {
            var query = @"
                SELECT
                    m.nombre as Modulo,
                    m.icono as IconoModulo,
                    o.idOperacion as IdOperacion,
                    o.nombre as NombreOperacion,
                    o.comando as Comando,
                    o.descripcion as Descripcion,
                    o.nivelCriticidad as NivelCriticidad,
                    ISNULL(ro.permitido, 0) as Permitido,
                    ro.fechaAsignacion as FechaAsignacion
                FROM abcmasplus..Operaciones o
                INNER JOIN abcmasplus..Modulos m ON o.idModulo = m.idModulo
                LEFT JOIN abcmasplus..RolesOperaciones ro ON o.idOperacion = ro.idOperacion AND ro.idRol = @idRol AND ro.activo = 1
                WHERE o.activo = 1 AND m.activo = 1
                ORDER BY m.orden, o.orden";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<RolOperacionEntidad>(query, new { idRol });
            return result.ToList();
        }

        public async Task<bool> AsignarPermisoAsync(int idRol, int idOperacion, bool permitido, int? usuarioAsignacion)
        {
            var query = @"
                IF NOT EXISTS (SELECT 1 FROM abcmasplus..RolesOperaciones WHERE idRol = @idRol AND idOperacion = @idOperacion)
                BEGIN
                    INSERT INTO abcmasplus..RolesOperaciones (idRol, idOperacion, permitido, usuarioAsignacion)
                    VALUES (@idRol, @idOperacion, @permitido, @usuarioAsignacion);
                END
                ELSE
                BEGIN
                    UPDATE abcmasplus..RolesOperaciones
                    SET permitido = @permitido,
                        fechaAsignacion = GETDATE(),
                        usuarioAsignacion = @usuarioAsignacion,
                        activo = 1
                    WHERE idRol = @idRol AND idOperacion = @idOperacion;
                END";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, new { idRol, idOperacion, permitido, usuarioAsignacion });
            return affected > 0;
        }

        public async Task<bool> RevocarPermisoAsync(int idRol, int idOperacion)
        {
            var query = @"
                UPDATE abcmasplus..RolesOperaciones
                SET activo = 0
                WHERE idRol = @idRol AND idOperacion = @idOperacion";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, new { idRol, idOperacion });
            return affected > 0;
        }

        public async Task<List<UsuarioOperacionEntidad>> GetPermisosEspecificosUsuarioAsync()
        {
            var query = @"
                SELECT
                    u.idUsuario as IdUsuario,
                    u.nombre + ' ' + u.apellido as Usuario,
                    r.nombre as Rol,
                    o.nombre as Operacion,
                    o.comando as Comando,
                    uo.permitido as Permitido,
                    uo.fechaAsignacion as FechaAsignacion,
                    uo.fechaExpiracion as FechaExpiracion,
                    uo.observaciones as Observaciones
                FROM abcmasplus..UsuariosOperaciones uo
                INNER JOIN abcmasplus..Usuarios u ON uo.idUsuario = u.idUsuario
                INNER JOIN abcmasplus..Roles r ON u.rol = r.idRol
                INNER JOIN abcmasplus..Operaciones o ON uo.idOperacion = o.idOperacion
                WHERE uo.activo = 1
                  AND (uo.fechaExpiracion IS NULL OR uo.fechaExpiracion > GETDATE())
                ORDER BY u.nombre, u.apellido, o.nombre";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<UsuarioOperacionEntidad>(query);
            return result.ToList();
        }

        public async Task<bool> AsignarPermisoEspecificoUsuarioAsync(UsuarioOperacionEntidad permiso)
        {
            var query = @"
                INSERT INTO abcmasplus..UsuariosOperaciones
                    (idUsuario, idOperacion, permitido, fechaExpiracion, usuarioAsignacion, observaciones)
                VALUES
                    (@IdUsuario, @IdOperacion, @Permitido, @FechaExpiracion, @UsuarioAsignacion, @Observaciones)";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, permiso);
            return affected > 0;
        }
    }
}
