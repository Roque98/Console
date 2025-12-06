using Dapper;
using FolderView.Dapper;
using FolderView.Dapper.SmartPromptLibrary.Entidades;
using FolderView.Dapper.SmartPromptLibrary.Interfaces;
using System.Data;

namespace FolderView.Dapper.SmartPromptLibrary.Repositorios
{
    public class VersionPromptRepositorio : IVersionPromptRepository
    {
        private readonly DapperContext _context;

        public VersionPromptRepositorio(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<VersionPromptEntidad>> GetVersionesByPromptIdAsync(int idPrompt)
        {
            var query = @"
                SELECT
                    v.idVersionPrompt AS IdVersionPrompt,
                    v.idPrompt AS IdPrompt,
                    v.numeroVersion AS NumeroVersion,
                    v.titulo AS Titulo,
                    v.descripcion AS Descripcion,
                    v.contenidoMarkdown AS ContenidoMarkdown,
                    v.mensajeSistema AS MensajeSistema,
                    v.mensajeCambio AS MensajeCambio,
                    v.esVersionActual AS EsVersionActual,
                    v.fechaCreacion AS FechaCreacion,
                    v.creadoPorUsuario AS CreadoPorUsuario,
                    p.titulo AS TituloPrompt
                FROM VersionesPrompt v
                INNER JOIN BibliotecaPrompts p ON v.idPrompt = p.idPrompt
                WHERE v.idPrompt = @IdPrompt
                ORDER BY v.numeroVersion DESC";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.QueryAsync<VersionPromptEntidad>(query, new { IdPrompt = idPrompt });
                return result.ToList();
            }
        }

        public async Task<VersionPromptEntidad> GetVersionByIdAsync(int idVersionPrompt)
        {
            var query = @"
                SELECT
                    v.idVersionPrompt AS IdVersionPrompt,
                    v.idPrompt AS IdPrompt,
                    v.numeroVersion AS NumeroVersion,
                    v.titulo AS Titulo,
                    v.descripcion AS Descripcion,
                    v.contenidoMarkdown AS ContenidoMarkdown,
                    v.mensajeSistema AS MensajeSistema,
                    v.mensajeCambio AS MensajeCambio,
                    v.esVersionActual AS EsVersionActual,
                    v.fechaCreacion AS FechaCreacion,
                    v.creadoPorUsuario AS CreadoPorUsuario,
                    p.titulo AS TituloPrompt
                FROM VersionesPrompt v
                INNER JOIN BibliotecaPrompts p ON v.idPrompt = p.idPrompt
                WHERE v.idVersionPrompt = @IdVersionPrompt";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<VersionPromptEntidad>(query, new { IdVersionPrompt = idVersionPrompt });
            }
        }

        public async Task<VersionPromptEntidad> GetVersionByNumeroAsync(int idPrompt, int numeroVersion)
        {
            var query = @"
                SELECT
                    v.idVersionPrompt AS IdVersionPrompt,
                    v.idPrompt AS IdPrompt,
                    v.numeroVersion AS NumeroVersion,
                    v.titulo AS Titulo,
                    v.descripcion AS Descripcion,
                    v.contenidoMarkdown AS ContenidoMarkdown,
                    v.mensajeSistema AS MensajeSistema,
                    v.mensajeCambio AS MensajeCambio,
                    v.esVersionActual AS EsVersionActual,
                    v.fechaCreacion AS FechaCreacion,
                    v.creadoPorUsuario AS CreadoPorUsuario,
                    p.titulo AS TituloPrompt
                FROM VersionesPrompt v
                INNER JOIN BibliotecaPrompts p ON v.idPrompt = p.idPrompt
                WHERE v.idPrompt = @IdPrompt AND v.numeroVersion = @NumeroVersion";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<VersionPromptEntidad>(
                    query,
                    new { IdPrompt = idPrompt, NumeroVersion = numeroVersion });
            }
        }

        public async Task<VersionPromptEntidad> GetVersionActualAsync(int idPrompt)
        {
            var query = @"
                SELECT
                    v.idVersionPrompt AS IdVersionPrompt,
                    v.idPrompt AS IdPrompt,
                    v.numeroVersion AS NumeroVersion,
                    v.titulo AS Titulo,
                    v.descripcion AS Descripcion,
                    v.contenidoMarkdown AS ContenidoMarkdown,
                    v.mensajeSistema AS MensajeSistema,
                    v.mensajeCambio AS MensajeCambio,
                    v.esVersionActual AS EsVersionActual,
                    v.fechaCreacion AS FechaCreacion,
                    v.creadoPorUsuario AS CreadoPorUsuario,
                    p.titulo AS TituloPrompt
                FROM VersionesPrompt v
                INNER JOIN BibliotecaPrompts p ON v.idPrompt = p.idPrompt
                WHERE v.idPrompt = @IdPrompt AND v.esVersionActual = 1";

            using (var connection = _context.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<VersionPromptEntidad>(query, new { IdPrompt = idPrompt });
            }
        }

        public async Task<(bool success, int numeroVersion, string error)> CrearNuevaVersionAsync(
            int idPrompt,
            string titulo,
            string descripcion,
            string contenidoMarkdown,
            string mensajeSistema,
            string mensajeCambio,
            string usuario)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@idPrompt", idPrompt);
                parameters.Add("@titulo", titulo);
                parameters.Add("@descripcion", descripcion);
                parameters.Add("@contenidoMarkdown", contenidoMarkdown);
                parameters.Add("@mensajeSistema", mensajeSistema);
                parameters.Add("@mensajeCambio", mensajeCambio);
                parameters.Add("@usuario", usuario);

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "sp_CrearVersionPrompt",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                if (result != null)
                {
                    bool success = result.Success == 1;
                    int numeroVersion = success ? (int)result.NumeroVersion : 0;
                    string error = success ? null : (string)result.ErrorMessage;
                    return (success, numeroVersion, error);
                }

                return (false, 0, "Error al ejecutar stored procedure");
            }
        }

        public async Task<(bool success, int numeroVersion, string error)> RestaurarVersionAsync(
            int idPrompt,
            int numeroVersion,
            string usuario)
        {
            using (var connection = _context.CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@idPrompt", idPrompt);
                parameters.Add("@numeroVersion", numeroVersion);
                parameters.Add("@usuario", usuario);

                var result = await connection.QueryFirstOrDefaultAsync<dynamic>(
                    "sp_RestaurarVersionPrompt",
                    parameters,
                    commandType: CommandType.StoredProcedure);

                if (result != null)
                {
                    bool success = result.Success == 1;
                    int nuevoNumeroVersion = success ? (int)result.NumeroVersion : 0;
                    string error = success ? null : (string)result.ErrorMessage;
                    return (success, nuevoNumeroVersion, error);
                }

                return (false, 0, "Error al ejecutar stored procedure");
            }
        }

        public async Task<(VersionPromptEntidad version1, VersionPromptEntidad version2)> CompararVersionesAsync(
            int idPrompt,
            int numeroVersion1,
            int numeroVersion2)
        {
            var version1 = await GetVersionByNumeroAsync(idPrompt, numeroVersion1);
            var version2 = await GetVersionByNumeroAsync(idPrompt, numeroVersion2);
            return (version1, version2);
        }

        public async Task<int> LimpiarVersionesAntiguasAsync(int idPrompt, int mantenerUltimas = 10)
        {
            var query = @"
                DELETE FROM VersionesPrompt
                WHERE idPrompt = @IdPrompt
                AND idVersionPrompt NOT IN (
                    SELECT TOP (@MantenerUltimas) idVersionPrompt
                    FROM VersionesPrompt
                    WHERE idPrompt = @IdPrompt
                    ORDER BY numeroVersion DESC
                )";

            using (var connection = _context.CreateConnection())
            {
                return await connection.ExecuteAsync(query, new { IdPrompt = idPrompt, MantenerUltimas = mantenerUltimas });
            }
        }
    }
}
