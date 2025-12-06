using Dapper;
using FolderView.Dapper.SmartPromptLibrary.Entidades;
using FolderView.Dapper.SmartPromptLibrary.Interfaces;

namespace FolderView.Dapper.SmartPromptLibrary.Repositorios
{
    public class EjecucionPromptRepositorio : IEjecucionPromptRepository
    {
        private readonly DapperContext _context;

        public EjecucionPromptRepositorio(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<EjecucionPromptEntidad>> GetAllAsync()
        {
            var query = @"
                SELECT
                    e.idEjecucionPrompt as IdEjecucionPrompt,
                    e.idPrompt as IdPrompt,
                    e.promptFinal as PromptFinal,
                    e.respuestaIA as RespuestaIA,
                    e.exitoso as Exitoso,
                    e.mensajeError as MensajeError,
                    e.tokensUsados as TokensUsados,
                    e.costoEstimado as CostoEstimado,
                    e.tiempoRespuestaMs as TiempoRespuestaMs,
                    e.idProveedorIA as IdProveedorIA,
                    e.idModeloIA as IdModeloIA,
                    e.temperatura as Temperatura,
                    e.maxTokens as MaxTokens,
                    e.fechaEjecucion as FechaEjecucion,
                    e.ejecutadoPorUsuario as EjecutadoPorUsuario,
                    p.titulo as PromptTitulo,
                    prov.nombre as ProveedorNombre,
                    m.nombre as ModeloNombre
                FROM abcmasplus..EjecucionesPrompt e
                INNER JOIN abcmasplus..BibliotecaPrompts p ON e.idPrompt = p.idPrompt
                LEFT JOIN abcmasplus..ProveedoresIA prov ON e.idProveedorIA = prov.idProveedorIA
                LEFT JOIN abcmasplus..ModelosIA m ON e.idModeloIA = m.idModeloIA
                ORDER BY e.fechaEjecucion DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<EjecucionPromptEntidad>(query);
            return result.ToList();
        }

        public async Task<List<EjecucionPromptEntidad>> GetByPromptAsync(int idPrompt)
        {
            var query = @"
                SELECT
                    e.idEjecucionPrompt as IdEjecucionPrompt,
                    e.idPrompt as IdPrompt,
                    e.promptFinal as PromptFinal,
                    e.respuestaIA as RespuestaIA,
                    e.exitoso as Exitoso,
                    e.mensajeError as MensajeError,
                    e.tokensUsados as TokensUsados,
                    e.costoEstimado as CostoEstimado,
                    e.tiempoRespuestaMs as TiempoRespuestaMs,
                    e.idProveedorIA as IdProveedorIA,
                    e.idModeloIA as IdModeloIA,
                    e.temperatura as Temperatura,
                    e.maxTokens as MaxTokens,
                    e.fechaEjecucion as FechaEjecucion,
                    e.ejecutadoPorUsuario as EjecutadoPorUsuario,
                    p.titulo as PromptTitulo,
                    prov.nombre as ProveedorNombre,
                    m.nombre as ModeloNombre
                FROM abcmasplus..EjecucionesPrompt e
                INNER JOIN abcmasplus..BibliotecaPrompts p ON e.idPrompt = p.idPrompt
                LEFT JOIN abcmasplus..ProveedoresIA prov ON e.idProveedorIA = prov.idProveedorIA
                LEFT JOIN abcmasplus..ModelosIA m ON e.idModeloIA = m.idModeloIA
                WHERE e.idPrompt = @idPrompt
                ORDER BY e.fechaEjecucion DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<EjecucionPromptEntidad>(query, new { idPrompt });
            return result.ToList();
        }

        public async Task<EjecucionPromptEntidad> GetByIdAsync(int id)
        {
            var query = @"
                SELECT
                    e.idEjecucionPrompt as IdEjecucionPrompt,
                    e.idPrompt as IdPrompt,
                    e.promptFinal as PromptFinal,
                    e.respuestaIA as RespuestaIA,
                    e.exitoso as Exitoso,
                    e.mensajeError as MensajeError,
                    e.tokensUsados as TokensUsados,
                    e.costoEstimado as CostoEstimado,
                    e.tiempoRespuestaMs as TiempoRespuestaMs,
                    e.idProveedorIA as IdProveedorIA,
                    e.idModeloIA as IdModeloIA,
                    e.temperatura as Temperatura,
                    e.maxTokens as MaxTokens,
                    e.fechaEjecucion as FechaEjecucion,
                    e.ejecutadoPorUsuario as EjecutadoPorUsuario,
                    p.titulo as PromptTitulo,
                    prov.nombre as ProveedorNombre,
                    m.nombre as ModeloNombre
                FROM abcmasplus..EjecucionesPrompt e
                INNER JOIN abcmasplus..BibliotecaPrompts p ON e.idPrompt = p.idPrompt
                LEFT JOIN abcmasplus..ProveedoresIA prov ON e.idProveedorIA = prov.idProveedorIA
                LEFT JOIN abcmasplus..ModelosIA m ON e.idModeloIA = m.idModeloIA
                WHERE e.idEjecucionPrompt = @id";

            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<EjecucionPromptEntidad>(query, new { id });
        }

        public async Task<EjecucionPromptEntidad> GetByIdConDetallesAsync(int id)
        {
            var ejecucion = await GetByIdAsync(id);
            if (ejecucion != null)
            {
                var queryParametros = @"
                    SELECT
                        idParametroEjecucion as IdParametroEjecucion,
                        idEjecucionPrompt as IdEjecucionPrompt,
                        nombreParametro as NombreParametro,
                        valorParametro as ValorParametro
                    FROM abcmasplus..ParametrosEjecucion
                    WHERE idEjecucionPrompt = @id";

                using var connection = _context.CreateConnection();
                var parametros = await connection.QueryAsync<ParametroEjecucionEntidad>(queryParametros, new { id });
                ejecucion.Parametros = parametros.ToList();
            }
            return ejecucion;
        }

        public async Task<int> CreateAsync(EjecucionPromptEntidad ejecucion, List<ParametroEjecucionEntidad> parametros)
        {
            using var connection = _context.CreateConnection();
            connection.Open();
            using var transaction = connection.BeginTransaction();

            try
            {
                // Insertar la ejecución
                var queryEjecucion = @"
                    INSERT INTO abcmasplus..EjecucionesPrompt
                        (idPrompt, promptFinal, respuestaIA, exitoso, mensajeError,
                         tokensUsados, costoEstimado, tiempoRespuestaMs, idProveedorIA, idModeloIA,
                         temperatura, maxTokens, ejecutadoPorUsuario)
                    VALUES
                        (@IdPrompt, @PromptFinal, @RespuestaIA, @Exitoso, @MensajeError,
                         @TokensUsados, @CostoEstimado, @TiempoRespuestaMs, @IdProveedorIA, @IdModeloIA,
                         @Temperatura, @MaxTokens, @EjecutadoPorUsuario);
                    SELECT CAST(SCOPE_IDENTITY() as int)";

                var idEjecucion = await connection.ExecuteScalarAsync<int>(queryEjecucion, ejecucion, transaction);

                // Insertar los parámetros
                if (parametros != null && parametros.Any())
                {
                    var queryParametros = @"
                        INSERT INTO abcmasplus..ParametrosEjecucion
                            (idEjecucionPrompt, nombreParametro, valorParametro)
                        VALUES
                            (@IdEjecucionPrompt, @NombreParametro, @ValorParametro)";

                    foreach (var parametro in parametros)
                    {
                        parametro.IdEjecucionPrompt = idEjecucion;
                        await connection.ExecuteAsync(queryParametros, parametro, transaction);
                    }
                }

                transaction.Commit();
                return idEjecucion;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var query = @"
                DELETE FROM abcmasplus..EjecucionesPrompt
                WHERE idEjecucionPrompt = @id";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, new { id });
            return affected > 0;
        }

        public async Task<List<EjecucionPromptEntidad>> GetUltimasEjecucionesAsync(int limit = 10)
        {
            var query = @"
                SELECT TOP (@limit)
                    e.idEjecucionPrompt as IdEjecucionPrompt,
                    e.idPrompt as IdPrompt,
                    e.promptFinal as PromptFinal,
                    e.respuestaIA as RespuestaIA,
                    e.exitoso as Exitoso,
                    e.mensajeError as MensajeError,
                    e.tokensUsados as TokensUsados,
                    e.costoEstimado as CostoEstimado,
                    e.tiempoRespuestaMs as TiempoRespuestaMs,
                    e.idProveedorIA as IdProveedorIA,
                    e.idModeloIA as IdModeloIA,
                    e.temperatura as Temperatura,
                    e.maxTokens as MaxTokens,
                    e.fechaEjecucion as FechaEjecucion,
                    e.ejecutadoPorUsuario as EjecutadoPorUsuario,
                    p.titulo as PromptTitulo,
                    prov.nombre as ProveedorNombre,
                    m.nombre as ModeloNombre
                FROM abcmasplus..EjecucionesPrompt e
                INNER JOIN abcmasplus..BibliotecaPrompts p ON e.idPrompt = p.idPrompt
                LEFT JOIN abcmasplus..ProveedoresIA prov ON e.idProveedorIA = prov.idProveedorIA
                LEFT JOIN abcmasplus..ModelosIA m ON e.idModeloIA = m.idModeloIA
                ORDER BY e.fechaEjecucion DESC";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<EjecucionPromptEntidad>(query, new { limit });
            return result.ToList();
        }
    }
}
