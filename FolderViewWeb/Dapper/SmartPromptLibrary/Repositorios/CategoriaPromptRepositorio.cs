using Dapper;
using FolderView.Dapper.SmartPromptLibrary.Entidades;
using FolderView.Dapper.SmartPromptLibrary.Interfaces;

namespace FolderView.Dapper.SmartPromptLibrary.Repositorios
{
    public class CategoriaPromptRepositorio : ICategoriaPromptRepository
    {
        private readonly DapperContext _context;

        public CategoriaPromptRepositorio(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<CategoriaPromptEntidad>> GetAllAsync()
        {
            var query = @"
                SELECT
                    c.idCategoriaPrompt as IdCategoriaPrompt,
                    c.nombre as Nombre,
                    c.descripcion as Descripcion,
                    c.icono as Icono,
                    c.color as Color,
                    c.activo as Activo,
                    c.fechaCreacion as FechaCreacion,
                    c.fechaActualizacion as FechaActualizacion,
                    (SELECT COUNT(*) FROM abcmasplus..BibliotecaPrompts p
                     WHERE p.idCategoriaPrompt = c.idCategoriaPrompt AND p.activo = 1) as TotalPrompts
                FROM abcmasplus..CategoriasPrompt c
                ORDER BY c.nombre";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<CategoriaPromptEntidad>(query);
            return result.ToList();
        }

        public async Task<List<CategoriaPromptEntidad>> GetAllActivasAsync()
        {
            var query = @"
                SELECT
                    c.idCategoriaPrompt as IdCategoriaPrompt,
                    c.nombre as Nombre,
                    c.descripcion as Descripcion,
                    c.icono as Icono,
                    c.color as Color,
                    c.activo as Activo,
                    c.fechaCreacion as FechaCreacion,
                    c.fechaActualizacion as FechaActualizacion,
                    (SELECT COUNT(*) FROM abcmasplus..BibliotecaPrompts p
                     WHERE p.idCategoriaPrompt = c.idCategoriaPrompt AND p.activo = 1) as TotalPrompts
                FROM abcmasplus..CategoriasPrompt c
                WHERE c.activo = 1
                ORDER BY c.nombre";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<CategoriaPromptEntidad>(query);
            return result.ToList();
        }

        public async Task<CategoriaPromptEntidad> GetByIdAsync(int id)
        {
            var query = @"
                SELECT
                    c.idCategoriaPrompt as IdCategoriaPrompt,
                    c.nombre as Nombre,
                    c.descripcion as Descripcion,
                    c.icono as Icono,
                    c.color as Color,
                    c.activo as Activo,
                    c.fechaCreacion as FechaCreacion,
                    c.fechaActualizacion as FechaActualizacion,
                    (SELECT COUNT(*) FROM abcmasplus..BibliotecaPrompts p
                     WHERE p.idCategoriaPrompt = c.idCategoriaPrompt AND p.activo = 1) as TotalPrompts
                FROM abcmasplus..CategoriasPrompt c
                WHERE c.idCategoriaPrompt = @id";

            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<CategoriaPromptEntidad>(query, new { id });
        }

        public async Task<int> CreateAsync(CategoriaPromptEntidad categoria)
        {
            var query = @"
                INSERT INTO abcmasplus..CategoriasPrompt
                    (nombre, descripcion, icono, color, activo)
                VALUES
                    (@Nombre, @Descripcion, @Icono, @Color, 1);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(query, categoria);
        }

        public async Task<bool> UpdateAsync(CategoriaPromptEntidad categoria)
        {
            var query = @"
                UPDATE abcmasplus..CategoriasPrompt
                SET
                    nombre = @Nombre,
                    descripcion = @Descripcion,
                    icono = @Icono,
                    color = @Color,
                    activo = @Activo,
                    fechaActualizacion = GETDATE()
                WHERE idCategoriaPrompt = @IdCategoriaPrompt";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, categoria);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var query = @"
                UPDATE abcmasplus..CategoriasPrompt
                SET activo = 0, fechaActualizacion = GETDATE()
                WHERE idCategoriaPrompt = @id";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, new { id });
            return affected > 0;
        }
    }
}
