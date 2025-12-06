using Dapper;
using FolderView.Dapper.SmartPromptLibrary.Entidades;
using FolderView.Dapper.SmartPromptLibrary.Interfaces;
using System.Text.RegularExpressions;

namespace FolderView.Dapper.SmartPromptLibrary.Repositorios
{
    public class PromptRepositorio : IPromptRepository
    {
        private readonly DapperContext _context;
        private readonly IEtiquetaPromptRepository _etiquetaRepository;

        public PromptRepositorio(DapperContext context, IEtiquetaPromptRepository etiquetaRepository)
        {
            _context = context;
            _etiquetaRepository = etiquetaRepository;
        }

        public async Task<List<PromptEntidad>> GetAllAsync()
        {
            var query = @"
                SELECT
                    p.idPrompt as IdPrompt,
                    p.idCategoriaPrompt as IdCategoriaPrompt,
                    p.titulo as Titulo,
                    p.descripcion as Descripcion,
                    p.contenidoMarkdown as ContenidoMarkdown,
                    p.mensajeSistema as MensajeSistema,
                    p.version as Version,
                    p.activo as Activo,
                    p.favorito as Favorito,
                    p.cantidadEjecuciones as CantidadEjecuciones,
                    p.ultimaEjecucion as UltimaEjecucion,
                    p.fechaCreacion as FechaCreacion,
                    p.fechaActualizacion as FechaActualizacion,
                    p.creadoPorUsuario as CreadoPorUsuario,
                    c.nombre as CategoriaNombre,
                    c.icono as CategoriaIcono,
                    c.color as CategoriaColor
                FROM abcmasplus..BibliotecaPrompts p
                INNER JOIN abcmasplus..CategoriasPrompt c ON p.idCategoriaPrompt = c.idCategoriaPrompt
                ORDER BY p.fechaCreacion DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<PromptEntidad>(query);
            return result.ToList();
        }

        public async Task<List<PromptEntidad>> GetAllActivosAsync()
        {
            var query = @"
                SELECT
                    p.idPrompt as IdPrompt,
                    p.idCategoriaPrompt as IdCategoriaPrompt,
                    p.titulo as Titulo,
                    p.descripcion as Descripcion,
                    p.contenidoMarkdown as ContenidoMarkdown,
                    p.mensajeSistema as MensajeSistema,
                    p.version as Version,
                    p.activo as Activo,
                    p.favorito as Favorito,
                    p.cantidadEjecuciones as CantidadEjecuciones,
                    p.ultimaEjecucion as UltimaEjecucion,
                    p.fechaCreacion as FechaCreacion,
                    p.fechaActualizacion as FechaActualizacion,
                    p.creadoPorUsuario as CreadoPorUsuario,
                    c.nombre as CategoriaNombre,
                    c.icono as CategoriaIcono,
                    c.color as CategoriaColor
                FROM abcmasplus..BibliotecaPrompts p
                INNER JOIN abcmasplus..CategoriasPrompt c ON p.idCategoriaPrompt = c.idCategoriaPrompt
                WHERE p.activo = 1
                ORDER BY p.fechaCreacion DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<PromptEntidad>(query);
            return result.ToList();
        }

        public async Task<List<PromptEntidad>> GetByCategoriaAsync(int idCategoria)
        {
            var query = @"
                SELECT
                    p.idPrompt as IdPrompt,
                    p.idCategoriaPrompt as IdCategoriaPrompt,
                    p.titulo as Titulo,
                    p.descripcion as Descripcion,
                    p.contenidoMarkdown as ContenidoMarkdown,
                    p.mensajeSistema as MensajeSistema,
                    p.version as Version,
                    p.activo as Activo,
                    p.favorito as Favorito,
                    p.cantidadEjecuciones as CantidadEjecuciones,
                    p.ultimaEjecucion as UltimaEjecucion,
                    p.fechaCreacion as FechaCreacion,
                    p.fechaActualizacion as FechaActualizacion,
                    p.creadoPorUsuario as CreadoPorUsuario,
                    c.nombre as CategoriaNombre,
                    c.icono as CategoriaIcono,
                    c.color as CategoriaColor
                FROM abcmasplus..BibliotecaPrompts p
                INNER JOIN abcmasplus..CategoriasPrompt c ON p.idCategoriaPrompt = c.idCategoriaPrompt
                WHERE p.idCategoriaPrompt = @idCategoria AND p.activo = 1
                ORDER BY p.fechaCreacion DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<PromptEntidad>(query, new { idCategoria });
            return result.ToList();
        }

        public async Task<List<PromptEntidad>> GetFavoritosAsync()
        {
            var query = @"
                SELECT
                    p.idPrompt as IdPrompt,
                    p.idCategoriaPrompt as IdCategoriaPrompt,
                    p.titulo as Titulo,
                    p.descripcion as Descripcion,
                    p.contenidoMarkdown as ContenidoMarkdown,
                    p.mensajeSistema as MensajeSistema,
                    p.version as Version,
                    p.activo as Activo,
                    p.favorito as Favorito,
                    p.cantidadEjecuciones as CantidadEjecuciones,
                    p.ultimaEjecucion as UltimaEjecucion,
                    p.fechaCreacion as FechaCreacion,
                    p.fechaActualizacion as FechaActualizacion,
                    p.creadoPorUsuario as CreadoPorUsuario,
                    c.nombre as CategoriaNombre,
                    c.icono as CategoriaIcono,
                    c.color as CategoriaColor
                FROM abcmasplus..BibliotecaPrompts p
                INNER JOIN abcmasplus..CategoriasPrompt c ON p.idCategoriaPrompt = c.idCategoriaPrompt
                WHERE p.favorito = 1 AND p.activo = 1
                ORDER BY p.fechaCreacion DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<PromptEntidad>(query);
            return result.ToList();
        }

        public async Task<List<PromptEntidad>> SearchAsync(string searchTerm, int? idCategoria = null, List<int> idsEtiquetas = null)
        {
            var conditions = new List<string> { "p.activo = 1" };
            var parameters = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                conditions.Add("(p.titulo LIKE @searchTerm OR p.descripcion LIKE @searchTerm OR p.contenidoMarkdown LIKE @searchTerm)");
                parameters.Add("searchTerm", $"%{searchTerm}%");
            }

            if (idCategoria.HasValue)
            {
                conditions.Add("p.idCategoriaPrompt = @idCategoria");
                parameters.Add("idCategoria", idCategoria.Value);
            }

            var whereClause = string.Join(" AND ", conditions);

            var query = $@"
                SELECT DISTINCT
                    p.idPrompt as IdPrompt,
                    p.idCategoriaPrompt as IdCategoriaPrompt,
                    p.titulo as Titulo,
                    p.descripcion as Descripcion,
                    p.contenidoMarkdown as ContenidoMarkdown,
                    p.mensajeSistema as MensajeSistema,
                    p.version as Version,
                    p.activo as Activo,
                    p.favorito as Favorito,
                    p.cantidadEjecuciones as CantidadEjecuciones,
                    p.ultimaEjecucion as UltimaEjecucion,
                    p.fechaCreacion as FechaCreacion,
                    p.fechaActualizacion as FechaActualizacion,
                    p.creadoPorUsuario as CreadoPorUsuario,
                    c.nombre as CategoriaNombre,
                    c.icono as CategoriaIcono,
                    c.color as CategoriaColor
                FROM abcmasplus..BibliotecaPrompts p
                INNER JOIN abcmasplus..CategoriasPrompt c ON p.idCategoriaPrompt = c.idCategoriaPrompt
                {(idsEtiquetas != null && idsEtiquetas.Any() ? "INNER JOIN abcmasplus..PromptEtiquetas pe ON p.idPrompt = pe.idPrompt" : "")}
                WHERE {whereClause}
                {(idsEtiquetas != null && idsEtiquetas.Any() ? $"AND pe.idEtiquetaPrompt IN ({string.Join(",", idsEtiquetas)})" : "")}
                ORDER BY p.fechaCreacion DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<PromptEntidad>(query, parameters);
            return result.ToList();
        }

        public async Task<PromptEntidad> GetByIdAsync(int id)
        {
            var query = @"
                SELECT
                    p.idPrompt as IdPrompt,
                    p.idCategoriaPrompt as IdCategoriaPrompt,
                    p.titulo as Titulo,
                    p.descripcion as Descripcion,
                    p.contenidoMarkdown as ContenidoMarkdown,
                    p.mensajeSistema as MensajeSistema,
                    p.version as Version,
                    p.activo as Activo,
                    p.favorito as Favorito,
                    p.cantidadEjecuciones as CantidadEjecuciones,
                    p.ultimaEjecucion as UltimaEjecucion,
                    p.fechaCreacion as FechaCreacion,
                    p.fechaActualizacion as FechaActualizacion,
                    p.creadoPorUsuario as CreadoPorUsuario,
                    c.nombre as CategoriaNombre,
                    c.icono as CategoriaIcono,
                    c.color as CategoriaColor
                FROM abcmasplus..BibliotecaPrompts p
                INNER JOIN abcmasplus..CategoriasPrompt c ON p.idCategoriaPrompt = c.idCategoriaPrompt
                WHERE p.idPrompt = @id";

            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<PromptEntidad>(query, new { id });
        }

        public async Task<PromptEntidad> GetByIdConDetallesAsync(int id)
        {
            var prompt = await GetByIdAsync(id);
            if (prompt != null)
            {
                prompt.Etiquetas = await _etiquetaRepository.GetEtiquetasByPromptAsync(id);
            }
            return prompt;
        }

        public async Task<int> CreateAsync(PromptEntidad prompt)
        {
            var query = @"
                INSERT INTO abcmasplus..BibliotecaPrompts
                    (idCategoriaPrompt, titulo, descripcion, contenidoMarkdown, mensajeSistema,
                     version, activo, favorito, creadoPorUsuario)
                VALUES
                    (@IdCategoriaPrompt, @Titulo, @Descripcion, @ContenidoMarkdown, @MensajeSistema,
                     1, 1, 0, @CreadoPorUsuario);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(query, prompt);
        }

        public async Task<bool> UpdateAsync(PromptEntidad prompt)
        {
            var query = @"
                UPDATE abcmasplus..BibliotecaPrompts
                SET
                    idCategoriaPrompt = @IdCategoriaPrompt,
                    titulo = @Titulo,
                    descripcion = @Descripcion,
                    contenidoMarkdown = @ContenidoMarkdown,
                    mensajeSistema = @MensajeSistema,
                    activo = @Activo,
                    favorito = @Favorito,
                    version = version + 1,
                    fechaActualizacion = GETDATE()
                WHERE idPrompt = @IdPrompt";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, prompt);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var query = @"
                UPDATE abcmasplus..BibliotecaPrompts
                SET activo = 0, fechaActualizacion = GETDATE()
                WHERE idPrompt = @id";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, new { id });
            return affected > 0;
        }

        public async Task<bool> ToggleFavoritoAsync(int id)
        {
            var query = @"
                UPDATE abcmasplus..BibliotecaPrompts
                SET favorito = CASE WHEN favorito = 1 THEN 0 ELSE 1 END,
                    fechaActualizacion = GETDATE()
                WHERE idPrompt = @id";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, new { id });
            return affected > 0;
        }

        public async Task<bool> IncrementarEjecucionesAsync(int id)
        {
            var query = @"
                UPDATE abcmasplus..BibliotecaPrompts
                SET cantidadEjecuciones = cantidadEjecuciones + 1,
                    ultimaEjecucion = GETDATE()
                WHERE idPrompt = @id";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, new { id });
            return affected > 0;
        }

        public async Task<bool> AsignarEtiquetasAsync(int idPrompt, List<int> idsEtiquetas)
        {
            using var connection = _context.CreateConnection();

            // Primero, eliminar todas las etiquetas existentes
            var deleteQuery = "DELETE FROM abcmasplus..PromptEtiquetas WHERE idPrompt = @idPrompt";
            await connection.ExecuteAsync(deleteQuery, new { idPrompt });

            // Luego, insertar las nuevas etiquetas
            if (idsEtiquetas != null && idsEtiquetas.Any())
            {
                var insertQuery = @"
                    INSERT INTO abcmasplus..PromptEtiquetas (idPrompt, idEtiquetaPrompt)
                    VALUES (@idPrompt, @idEtiqueta)";

                foreach (var idEtiqueta in idsEtiquetas)
                {
                    await connection.ExecuteAsync(insertQuery, new { idPrompt, idEtiqueta });
                }
            }

            return true;
        }

        public async Task<List<string>> DetectarVariablesAsync(string contenidoMarkdown)
        {
            if (string.IsNullOrWhiteSpace(contenidoMarkdown))
                return new List<string>();

            var variables = new HashSet<string>();

            // Patrón 1: {{variable}}
            var patron1 = new Regex(@"\{\{([^}]+)\}\}", RegexOptions.Compiled);
            var matches1 = patron1.Matches(contenidoMarkdown);
            foreach (Match match in matches1)
            {
                variables.Add(match.Groups[1].Value.Trim());
            }

            // Patrón 2: [INSERT VARIABLE] o [variable]
            var patron2 = new Regex(@"\[(?:INSERT\s+)?([A-Z_][A-Z0-9_]*)\]", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var matches2 = patron2.Matches(contenidoMarkdown);
            foreach (Match match in matches2)
            {
                variables.Add(match.Groups[1].Value.Trim());
            }

            return await Task.FromResult(variables.OrderBy(v => v).ToList());
        }
    }
}
