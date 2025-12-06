using Dapper;
using FolderView.Dapper.SmartPromptLibrary.Entidades;
using FolderView.Dapper.SmartPromptLibrary.Interfaces;

namespace FolderView.Dapper.SmartPromptLibrary.Repositorios
{
    public class EtiquetaPromptRepositorio : IEtiquetaPromptRepository
    {
        private readonly DapperContext _context;

        public EtiquetaPromptRepositorio(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<EtiquetaPromptEntidad>> GetAllAsync()
        {
            var query = @"
                SELECT
                    e.idEtiquetaPrompt as IdEtiquetaPrompt,
                    e.nombre as Nombre,
                    e.color as Color,
                    e.activo as Activo,
                    e.fechaCreacion as FechaCreacion,
                    (SELECT COUNT(*) FROM abcmasplus..PromptEtiquetas pe
                     INNER JOIN abcmasplus..BibliotecaPrompts p ON pe.idPrompt = p.idPrompt
                     WHERE pe.idEtiquetaPrompt = e.idEtiquetaPrompt AND p.activo = 1) as TotalPrompts
                FROM abcmasplus..EtiquetasPrompt e
                ORDER BY e.nombre";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<EtiquetaPromptEntidad>(query);
            return result.ToList();
        }

        public async Task<List<EtiquetaPromptEntidad>> GetAllActivasAsync()
        {
            var query = @"
                SELECT
                    e.idEtiquetaPrompt as IdEtiquetaPrompt,
                    e.nombre as Nombre,
                    e.color as Color,
                    e.activo as Activo,
                    e.fechaCreacion as FechaCreacion,
                    (SELECT COUNT(*) FROM abcmasplus..PromptEtiquetas pe
                     INNER JOIN abcmasplus..BibliotecaPrompts p ON pe.idPrompt = p.idPrompt
                     WHERE pe.idEtiquetaPrompt = e.idEtiquetaPrompt AND p.activo = 1) as TotalPrompts
                FROM abcmasplus..EtiquetasPrompt e
                WHERE e.activo = 1
                ORDER BY e.nombre";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<EtiquetaPromptEntidad>(query);
            return result.ToList();
        }

        public async Task<EtiquetaPromptEntidad> GetByIdAsync(int id)
        {
            var query = @"
                SELECT
                    e.idEtiquetaPrompt as IdEtiquetaPrompt,
                    e.nombre as Nombre,
                    e.color as Color,
                    e.activo as Activo,
                    e.fechaCreacion as FechaCreacion
                FROM abcmasplus..EtiquetasPrompt e
                WHERE e.idEtiquetaPrompt = @id";

            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<EtiquetaPromptEntidad>(query, new { id });
        }

        public async Task<EtiquetaPromptEntidad> GetByNombreAsync(string nombre)
        {
            var query = @"
                SELECT
                    e.idEtiquetaPrompt as IdEtiquetaPrompt,
                    e.nombre as Nombre,
                    e.color as Color,
                    e.activo as Activo,
                    e.fechaCreacion as FechaCreacion
                FROM abcmasplus..EtiquetasPrompt e
                WHERE e.nombre = @nombre";

            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<EtiquetaPromptEntidad>(query, new { nombre });
        }

        public async Task<int> CreateAsync(EtiquetaPromptEntidad etiqueta)
        {
            var query = @"
                INSERT INTO abcmasplus..EtiquetasPrompt
                    (nombre, color, activo)
                VALUES
                    (@Nombre, @Color, 1);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(query, etiqueta);
        }

        public async Task<bool> UpdateAsync(EtiquetaPromptEntidad etiqueta)
        {
            var query = @"
                UPDATE abcmasplus..EtiquetasPrompt
                SET
                    nombre = @Nombre,
                    color = @Color,
                    activo = @Activo
                WHERE idEtiquetaPrompt = @IdEtiquetaPrompt";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, etiqueta);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var query = @"
                UPDATE abcmasplus..EtiquetasPrompt
                SET activo = 0
                WHERE idEtiquetaPrompt = @id";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, new { id });
            return affected > 0;
        }

        public async Task<List<EtiquetaPromptEntidad>> GetEtiquetasByPromptAsync(int idPrompt)
        {
            var query = @"
                SELECT
                    e.idEtiquetaPrompt as IdEtiquetaPrompt,
                    e.nombre as Nombre,
                    e.color as Color,
                    e.activo as Activo,
                    e.fechaCreacion as FechaCreacion
                FROM abcmasplus..EtiquetasPrompt e
                INNER JOIN abcmasplus..PromptEtiquetas pe ON e.idEtiquetaPrompt = pe.idEtiquetaPrompt
                WHERE pe.idPrompt = @idPrompt AND e.activo = 1
                ORDER BY e.nombre";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<EtiquetaPromptEntidad>(query, new { idPrompt });
            return result.ToList();
        }
    }
}
