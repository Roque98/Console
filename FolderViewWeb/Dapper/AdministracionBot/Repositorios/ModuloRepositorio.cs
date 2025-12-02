using Dapper;
using FolderView.Dapper.AdministracionBot.Entidades;
using FolderView.Dapper.AdministracionBot.Interfaces;

namespace FolderView.Dapper.AdministracionBot.Repositorios
{
    public class ModuloRepositorio : IModuloRepository
    {
        private readonly DapperContext _context;

        public ModuloRepositorio(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<ModuloEntidad>> GetAllAsync()
        {
            var query = @"
                SELECT
                    m.idModulo as IdModulo,
                    m.nombre as Nombre,
                    m.descripcion as Descripcion,
                    m.icono as Icono,
                    m.orden as Orden,
                    m.activo as Activo,
                    m.fechaCreacion as FechaCreacion,
                    (SELECT COUNT(*) FROM abcmasplus..Operaciones o WHERE o.idModulo = m.idModulo AND o.activo = 1) as TotalOperaciones
                FROM abcmasplus..Modulos m
                ORDER BY m.orden, m.nombre";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<ModuloEntidad>(query);
            return result.ToList();
        }

        public async Task<ModuloEntidad> GetByIdAsync(int id)
        {
            var query = @"
                SELECT
                    m.idModulo as IdModulo,
                    m.nombre as Nombre,
                    m.descripcion as Descripcion,
                    m.icono as Icono,
                    m.orden as Orden,
                    m.activo as Activo,
                    m.fechaCreacion as FechaCreacion
                FROM abcmasplus..Modulos m
                WHERE m.idModulo = @id";

            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<ModuloEntidad>(query, new { id });
        }

        public async Task<int> CreateAsync(ModuloEntidad modulo)
        {
            var query = @"
                INSERT INTO abcmasplus..Modulos (nombre, descripcion, icono, orden, activo)
                VALUES (@Nombre, @Descripcion, @Icono, @Orden, 1);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(query, modulo);
        }

        public async Task<bool> UpdateAsync(ModuloEntidad modulo)
        {
            var query = @"
                UPDATE abcmasplus..Modulos
                SET
                    nombre = @Nombre,
                    descripcion = @Descripcion,
                    icono = @Icono,
                    orden = @Orden,
                    activo = @Activo
                WHERE idModulo = @IdModulo";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, modulo);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var query = "UPDATE abcmasplus..Modulos SET activo = 0 WHERE idModulo = @id";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, new { id });
            return affected > 0;
        }
    }
}
