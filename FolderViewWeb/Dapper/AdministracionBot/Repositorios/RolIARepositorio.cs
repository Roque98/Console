using Dapper;
using FolderView.Dapper.AdministracionBot.Entidades;
using FolderView.Dapper.AdministracionBot.Interfaces;

namespace FolderView.Dapper.AdministracionBot.Repositorios
{
    public class RolIARepositorio : IRolIARepository
    {
        private readonly DapperContext _context;

        public RolIARepositorio(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<RolIAEntidad>> GetAllAsync()
        {
            var query = @"
                SELECT
                    ri.idRol as IdRol,
                    ri.nombre as Nombre,
                    ri.descripcion as Descripcion,
                    ri.fechaCreacion as FechaCreacion,
                    ri.activo as Activo,
                    (SELECT COUNT(*) FROM abcmasplus..UsuariosRolesIA uri WHERE uri.idRol = ri.idRol AND uri.activo = 1) as TotalUsuarios,
                    (SELECT COUNT(*) FROM abcmasplus..GerenciasRolesIA gri WHERE gri.idRol = ri.idRol AND gri.activo = 1) as TotalGerencias
                FROM abcmasplus..RolesIA ri
                ORDER BY ri.nombre";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<RolIAEntidad>(query);
            return result.ToList();
        }

        public async Task<RolIAEntidad> GetByIdAsync(int id)
        {
            var query = @"
                SELECT
                    ri.idRol as IdRol,
                    ri.nombre as Nombre,
                    ri.descripcion as Descripcion,
                    ri.fechaCreacion as FechaCreacion,
                    ri.activo as Activo
                FROM abcmasplus..RolesIA ri
                WHERE ri.idRol = @id";

            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<RolIAEntidad>(query, new { id });
        }

        public async Task<int> CreateAsync(RolIAEntidad rol)
        {
            var query = @"
                INSERT INTO abcmasplus..RolesIA (nombre, descripcion, activo)
                VALUES (@Nombre, @Descripcion, 1);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(query, rol);
        }

        public async Task<bool> UpdateAsync(RolIAEntidad rol)
        {
            var query = @"
                UPDATE abcmasplus..RolesIA
                SET
                    nombre = @Nombre,
                    descripcion = @Descripcion,
                    activo = @Activo
                WHERE idRol = @IdRol";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, rol);
            return affected > 0;
        }

        public async Task<bool> AsignarRolAUsuarioAsync(int idRol, int idUsuario)
        {
            var query = @"
                INSERT INTO abcmasplus..UsuariosRolesIA (idRol, idUsuario, activo)
                VALUES (@idRol, @idUsuario, 1)";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, new { idRol, idUsuario });
            return affected > 0;
        }

        public async Task<bool> RemoverRolDeUsuarioAsync(int idRol, int idUsuario)
        {
            var query = @"
                UPDATE abcmasplus..UsuariosRolesIA
                SET activo = 0
                WHERE idRol = @idRol AND idUsuario = @idUsuario";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, new { idRol, idUsuario });
            return affected > 0;
        }
    }
}
