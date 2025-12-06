using FolderView.Dapper.SmartPromptLibrary.Entidades;
using FolderView.Dapper.SmartPromptLibrary.Interfaces;
using FolderView.Dapper.ConfiguracionIA.Interfaces;
using FolderView.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace FolderView.Controllers.SmartPromptLibrary
{
    public class SmartPromptLibraryController : Controller
    {
        private readonly ICategoriaPromptRepository _categoriaRepository;
        private readonly IEtiquetaPromptRepository _etiquetaRepository;
        private readonly IPromptRepository _promptRepository;
        private readonly IEjecucionPromptRepository _ejecucionRepository;
        private readonly IVersionPromptRepository _versionRepository;
        private readonly ILlmService _llmService;
        private readonly ISistemaAplicacionRepository _sistemaRepository;
        private readonly ILogger<SmartPromptLibraryController> _logger;

        // Nombre del sistema en la tabla SistemasAplicaciones
        private const string NOMBRE_SISTEMA = "Biblioteca Prompts";
        private int? _idSistema = null;

        public SmartPromptLibraryController(
            ICategoriaPromptRepository categoriaRepository,
            IEtiquetaPromptRepository etiquetaRepository,
            IPromptRepository promptRepository,
            IEjecucionPromptRepository ejecucionRepository,
            IVersionPromptRepository versionRepository,
            ILlmService llmService,
            ISistemaAplicacionRepository sistemaRepository,
            ILogger<SmartPromptLibraryController> logger)
        {
            _categoriaRepository = categoriaRepository;
            _etiquetaRepository = etiquetaRepository;
            _promptRepository = promptRepository;
            _ejecucionRepository = ejecucionRepository;
            _versionRepository = versionRepository;
            _llmService = llmService;
            _sistemaRepository = sistemaRepository;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el ID del sistema "Biblioteca Prompts" de forma dinámica
        /// </summary>
        private async Task<int?> GetIdSistemaAsync()
        {
            if (_idSistema.HasValue)
                return _idSistema;

            try
            {
                var sistemas = await _sistemaRepository.GetAllAsync();
                var sistema = sistemas.FirstOrDefault(s => s.Nombre == NOMBRE_SISTEMA);

                if (sistema != null)
                {
                    _idSistema = sistema.IdSistemaAplicacion;
                    return _idSistema;
                }

                _logger.LogWarning($"Sistema '{NOMBRE_SISTEMA}' no encontrado en SistemasAplicaciones");
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener ID del sistema '{NOMBRE_SISTEMA}'");
                return null;
            }
        }

        #region Vistas

        public IActionResult Index()
        {
            return View();
        }

        #endregion

        #region Categorías

        [HttpGet("api/smart-prompt-library/categorias")]
        public async Task<IActionResult> GetCategorias()
        {
            var result = await _categoriaRepository.GetAllActivasAsync();
            return Json(result);
        }

        [HttpGet("api/smart-prompt-library/categorias/{id}")]
        public async Task<IActionResult> GetCategoriaById(int id)
        {
            var result = await _categoriaRepository.GetByIdAsync(id);
            return Json(result);
        }

        [HttpPost("api/smart-prompt-library/categorias")]
        public async Task<IActionResult> CreateCategoria([FromBody] CategoriaPromptEntidad categoria)
        {
            try
            {
                var id = await _categoriaRepository.CreateAsync(categoria);
                return Json(new { success = true, id });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("api/smart-prompt-library/categorias/{id}")]
        public async Task<IActionResult> UpdateCategoria(int id, [FromBody] CategoriaPromptEntidad categoria)
        {
            try
            {
                categoria.IdCategoriaPrompt = id;
                var result = await _categoriaRepository.UpdateAsync(categoria);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("api/smart-prompt-library/categorias/{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            try
            {
                var result = await _categoriaRepository.DeleteAsync(id);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region Etiquetas

        [HttpGet("api/smart-prompt-library/etiquetas")]
        public async Task<IActionResult> GetEtiquetas()
        {
            var result = await _etiquetaRepository.GetAllActivasAsync();
            return Json(result);
        }

        [HttpGet("api/smart-prompt-library/etiquetas/{id}")]
        public async Task<IActionResult> GetEtiquetaById(int id)
        {
            var result = await _etiquetaRepository.GetByIdAsync(id);
            return Json(result);
        }

        [HttpPost("api/smart-prompt-library/etiquetas")]
        public async Task<IActionResult> CreateEtiqueta([FromBody] EtiquetaPromptEntidad etiqueta)
        {
            try
            {
                var id = await _etiquetaRepository.CreateAsync(etiqueta);
                return Json(new { success = true, id });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("api/smart-prompt-library/etiquetas/{id}")]
        public async Task<IActionResult> UpdateEtiqueta(int id, [FromBody] EtiquetaPromptEntidad etiqueta)
        {
            try
            {
                etiqueta.IdEtiquetaPrompt = id;
                var result = await _etiquetaRepository.UpdateAsync(etiqueta);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("api/smart-prompt-library/etiquetas/{id}")]
        public async Task<IActionResult> DeleteEtiqueta(int id)
        {
            try
            {
                var result = await _etiquetaRepository.DeleteAsync(id);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region Prompts

        [HttpGet("api/smart-prompt-library/prompts")]
        public async Task<IActionResult> GetPrompts()
        {
            var result = await _promptRepository.GetAllActivosAsync();
            return Json(result);
        }

        [HttpGet("api/smart-prompt-library/prompts/favoritos")]
        public async Task<IActionResult> GetPromptsFavoritos()
        {
            var result = await _promptRepository.GetFavoritosAsync();
            return Json(result);
        }

        [HttpGet("api/smart-prompt-library/prompts/categoria/{idCategoria}")]
        public async Task<IActionResult> GetPromptsByCategoria(int idCategoria)
        {
            var result = await _promptRepository.GetByCategoriaAsync(idCategoria);
            return Json(result);
        }

        [HttpGet("api/smart-prompt-library/prompts/buscar")]
        public async Task<IActionResult> SearchPrompts([FromQuery] string search, [FromQuery] int? categoria, [FromQuery] string etiquetas)
        {
            List<int> idsEtiquetas = null;
            if (!string.IsNullOrWhiteSpace(etiquetas))
            {
                idsEtiquetas = etiquetas.Split(',').Select(int.Parse).ToList();
            }

            var result = await _promptRepository.SearchAsync(search, categoria, idsEtiquetas);
            return Json(result);
        }

        [HttpGet("api/smart-prompt-library/prompts/{id}")]
        public async Task<IActionResult> GetPromptById(int id)
        {
            var result = await _promptRepository.GetByIdConDetallesAsync(id);
            return Json(result);
        }

        [HttpGet("api/smart-prompt-library/prompts/{id}/variables")]
        public async Task<IActionResult> DetectarVariables(int id)
        {
            try
            {
                var prompt = await _promptRepository.GetByIdAsync(id);
                if (prompt == null)
                {
                    return Json(new { success = false, message = "Prompt no encontrado" });
                }

                var variables = await _promptRepository.DetectarVariablesAsync(prompt.ContenidoMarkdown);
                return Json(new { success = true, variables });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("api/smart-prompt-library/prompts")]
        public async Task<IActionResult> CreatePrompt([FromBody] CreatePromptRequest request)
        {
            try
            {
                var prompt = new PromptEntidad
                {
                    IdCategoriaPrompt = request.IdCategoriaPrompt,
                    Titulo = request.Titulo,
                    Descripcion = request.Descripcion,
                    ContenidoMarkdown = request.ContenidoMarkdown,
                    MensajeSistema = request.MensajeSistema,
                    CreadoPorUsuario = "Sistema" // Actualizar con usuario actual si aplica
                };

                var id = await _promptRepository.CreateAsync(prompt);

                // Asignar etiquetas si se proporcionaron
                if (request.IdsEtiquetas != null && request.IdsEtiquetas.Any())
                {
                    await _promptRepository.AsignarEtiquetasAsync(id, request.IdsEtiquetas);
                }

                return Json(new { success = true, id });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("api/smart-prompt-library/prompts/generar")]
        public async Task<IActionResult> GenerarPromptConIA([FromBody] GenerarPromptRequest request)
        {
            try
            {
                var idSistema = await GetIdSistemaAsync();
                if (!idSistema.HasValue)
                {
                    return Json(new { success = false, message = $"Sistema '{NOMBRE_SISTEMA}' no encontrado en la base de datos" });
                }

                // Verificar configuración de IA
                if (!await _llmService.HasActiveConfigurationAsync(idSistema.Value))
                {
                    return Json(new { success = false, message = "No hay configuración de IA activa para este módulo. Configure las credenciales en /ConfiguracionIA" });
                }

                // Crear prompt para generar el prompt optimizado
                var systemMessage = "Eres un experto en ingeniería de prompts. Tu trabajo es tomar ideas generales y convertirlas en prompts estructurados, claros y efectivos en formato Markdown.";
                var userPrompt = $@"Genera un prompt profesional y estructurado en formato Markdown basado en la siguiente idea:

{request.IdeaGeneral}

El prompt debe:
1. Estar en formato Markdown con títulos, secciones y listas
2. Incluir variables dinámicas usando la sintaxis {{{{nombre_variable}}}}
3. Ser claro, específico y profesional
4. Incluir contexto y requisitos necesarios

Genera SOLO el contenido del prompt en Markdown, sin explicaciones adicionales.";

                var response = await _llmService.SendPromptAsync(idSistema.Value, userPrompt, systemMessage);

                if (!response.Success)
                {
                    return Json(new { success = false, message = response.Error });
                }

                return Json(new { success = true, contenidoGenerado = response.Content });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("api/smart-prompt-library/prompts/sugerir-etiquetas")]
        public async Task<IActionResult> SugerirEtiquetas([FromBody] SugerirEtiquetasRequest request)
        {
            try
            {
                var idSistema = await GetIdSistemaAsync();
                if (!idSistema.HasValue)
                {
                    return Json(new { success = false, message = $"Sistema '{NOMBRE_SISTEMA}' no encontrado en la base de datos" });
                }

                // Verificar configuración de IA
                if (!await _llmService.HasActiveConfigurationAsync(idSistema.Value))
                {
                    return Json(new { success = false, message = "No hay configuración de IA activa para este módulo. Configure las credenciales en /ConfiguracionIA" });
                }

                // Obtener etiquetas existentes
                var etiquetasExistentes = await _etiquetaRepository.GetAllActivasAsync();
                var nombresEtiquetas = string.Join(", ", etiquetasExistentes.Select(e => e.Nombre));

                var systemMessage = "Eres un asistente que analiza prompts y sugiere etiquetas relevantes.";
                var userPrompt = $@"Analiza el siguiente prompt y sugiere entre 2 y 5 etiquetas relevantes:

**Título:** {request.Titulo}
**Descripción:** {request.Descripcion}
**Contenido:**
{request.ContenidoMarkdown}

**Etiquetas existentes en el sistema:** {nombresEtiquetas}

Responde SOLO con una lista de etiquetas separadas por comas, sin explicaciones adicionales.
Prioriza usar etiquetas existentes, pero puedes sugerir nuevas si son realmente necesarias.";

                var response = await _llmService.SendPromptAsync(idSistema.Value, userPrompt, systemMessage);

                if (!response.Success)
                {
                    return Json(new { success = false, message = response.Error });
                }

                // Parsear las etiquetas sugeridas
                var etiquetasSugeridas = response.Content
                    .Split(',')
                    .Select(e => e.Trim().ToLower())
                    .Where(e => !string.IsNullOrWhiteSpace(e))
                    .Distinct()
                    .ToList();

                return Json(new { success = true, etiquetasSugeridas });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("api/smart-prompt-library/prompts/{id}")]
        public async Task<IActionResult> UpdatePrompt(int id, [FromBody] UpdatePromptRequest request)
        {
            try
            {
                var prompt = await _promptRepository.GetByIdAsync(id);
                if (prompt == null)
                {
                    return Json(new { success = false, message = "Prompt no encontrado" });
                }

                prompt.IdCategoriaPrompt = request.IdCategoriaPrompt;
                prompt.Titulo = request.Titulo;
                prompt.Descripcion = request.Descripcion;
                prompt.ContenidoMarkdown = request.ContenidoMarkdown;
                prompt.MensajeSistema = request.MensajeSistema;
                prompt.Activo = request.Activo;
                prompt.Favorito = request.Favorito;

                var result = await _promptRepository.UpdateAsync(prompt);

                // Actualizar etiquetas
                if (request.IdsEtiquetas != null)
                {
                    await _promptRepository.AsignarEtiquetasAsync(id, request.IdsEtiquetas);
                }

                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPut("api/smart-prompt-library/prompts/{id}/favorito")]
        public async Task<IActionResult> ToggleFavorito(int id)
        {
            try
            {
                var result = await _promptRepository.ToggleFavoritoAsync(id);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("api/smart-prompt-library/prompts/{id}")]
        public async Task<IActionResult> DeletePrompt(int id)
        {
            try
            {
                var result = await _promptRepository.DeleteAsync(id);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region Ejecución de Prompts

        [HttpPost("api/smart-prompt-library/prompts/{id}/ejecutar")]
        public async Task<IActionResult> EjecutarPrompt(int id, [FromBody] EjecutarPromptRequest request)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                // Obtener el prompt
                var prompt = await _promptRepository.GetByIdAsync(id);
                if (prompt == null)
                {
                    return Json(new { success = false, message = "Prompt no encontrado" });
                }

                var idSistema = await GetIdSistemaAsync();
                if (!idSistema.HasValue)
                {
                    return Json(new { success = false, message = $"Sistema '{NOMBRE_SISTEMA}' no encontrado en la base de datos" });
                }

                // Verificar configuración de IA
                if (!await _llmService.HasActiveConfigurationAsync(idSistema.Value))
                {
                    return Json(new { success = false, message = "No hay configuración de IA activa para este módulo. Configure las credenciales en /ConfiguracionIA" });
                }

                // Reemplazar variables en el contenido
                var promptFinal = prompt.ContenidoMarkdown;
                var parametros = new List<ParametroEjecucionEntidad>();

                if (request.Parametros != null)
                {
                    foreach (var param in request.Parametros)
                    {
                        // Reemplazar {{variable}}
                        promptFinal = Regex.Replace(
                            promptFinal,
                            $@"\{{\{{{Regex.Escape(param.Key)}\}}\}}",
                            param.Value,
                            RegexOptions.IgnoreCase
                        );

                        // Reemplazar [VARIABLE] o [INSERT VARIABLE]
                        promptFinal = Regex.Replace(
                            promptFinal,
                            $@"\[(?:INSERT\s+)?{Regex.Escape(param.Key)}\]",
                            param.Value,
                            RegexOptions.IgnoreCase
                        );

                        parametros.Add(new ParametroEjecucionEntidad
                        {
                            NombreParametro = param.Key,
                            ValorParametro = param.Value
                        });
                    }
                }

                // Ejecutar con el servicio LLM
                var response = await _llmService.SendPromptAsync(
                    idSistema.Value,
                    promptFinal,
                    prompt.MensajeSistema
                );

                stopwatch.Stop();

                // Guardar la ejecución en el historial
                var ejecucion = new EjecucionPromptEntidad
                {
                    IdPrompt = id,
                    PromptFinal = promptFinal,
                    RespuestaIA = response.Content,
                    Exitoso = response.Success,
                    MensajeError = response.Error,
                    TokensUsados = response.TokensUsed,
                    CostoEstimado = response.CostEstimated,
                    TiempoRespuestaMs = (int)stopwatch.ElapsedMilliseconds,
                    EjecutadoPorUsuario = "Sistema" // Actualizar con usuario actual si aplica
                };

                var idEjecucion = await _ejecucionRepository.CreateAsync(ejecucion, parametros);

                // Incrementar contador de ejecuciones del prompt
                if (response.Success)
                {
                    await _promptRepository.IncrementarEjecucionesAsync(id);
                }

                return Json(new
                {
                    success = response.Success,
                    respuesta = response.Content,
                    error = response.Error,
                    tokensUsados = response.TokensUsed,
                    costoEstimado = response.CostEstimated,
                    tiempoMs = stopwatch.ElapsedMilliseconds,
                    idEjecucion
                });
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                return Json(new
                {
                    success = false,
                    message = $"Error al ejecutar el prompt: {ex.Message}",
                    tiempoMs = stopwatch.ElapsedMilliseconds
                });
            }
        }

        [HttpGet("api/smart-prompt-library/ejecuciones")]
        public async Task<IActionResult> GetEjecuciones([FromQuery] int? limit)
        {
            var result = limit.HasValue
                ? await _ejecucionRepository.GetUltimasEjecucionesAsync(limit.Value)
                : await _ejecucionRepository.GetAllAsync();
            return Json(result);
        }

        [HttpGet("api/smart-prompt-library/ejecuciones/prompt/{idPrompt}")]
        public async Task<IActionResult> GetEjecucionesByPrompt(int idPrompt)
        {
            var result = await _ejecucionRepository.GetByPromptAsync(idPrompt);
            return Json(result);
        }

        [HttpGet("api/smart-prompt-library/ejecuciones/{id}")]
        public async Task<IActionResult> GetEjecucionById(int id)
        {
            var result = await _ejecucionRepository.GetByIdConDetallesAsync(id);
            return Json(result);
        }

        [HttpDelete("api/smart-prompt-library/ejecuciones/{id}")]
        public async Task<IActionResult> DeleteEjecucion(int id)
        {
            try
            {
                var result = await _ejecucionRepository.DeleteAsync(id);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion

        #region Versiones

        [HttpGet("api/smart-prompt-library/prompts/{idPrompt}/versiones")]
        public async Task<IActionResult> GetVersionesByPromptId(int idPrompt)
        {
            var result = await _versionRepository.GetVersionesByPromptIdAsync(idPrompt);
            return Json(result);
        }

        [HttpGet("api/smart-prompt-library/versiones/{id}")]
        public async Task<IActionResult> GetVersionById(int id)
        {
            var result = await _versionRepository.GetVersionByIdAsync(id);
            return Json(result);
        }

        [HttpGet("api/smart-prompt-library/prompts/{idPrompt}/versiones/{numeroVersion}")]
        public async Task<IActionResult> GetVersionByNumero(int idPrompt, int numeroVersion)
        {
            var result = await _versionRepository.GetVersionByNumeroAsync(idPrompt, numeroVersion);
            return Json(result);
        }

        [HttpGet("api/smart-prompt-library/prompts/{idPrompt}/version-actual")]
        public async Task<IActionResult> GetVersionActual(int idPrompt)
        {
            var result = await _versionRepository.GetVersionActualAsync(idPrompt);
            return Json(result);
        }

        [HttpPost("api/smart-prompt-library/prompts/{idPrompt}/crear-version")]
        public async Task<IActionResult> CrearNuevaVersion(int idPrompt, [FromBody] CrearVersionRequest request)
        {
            try
            {
                var (success, numeroVersion, error) = await _versionRepository.CrearNuevaVersionAsync(
                    idPrompt,
                    request.Titulo,
                    request.Descripcion,
                    request.ContenidoMarkdown,
                    request.MensajeSistema,
                    request.MensajeCambio,
                    request.Usuario ?? "Sistema");

                if (success)
                {
                    // Actualizar etiquetas si se proporcionaron
                    if (request.IdsEtiquetas != null && request.IdsEtiquetas.Any())
                    {
                        await _promptRepository.AsignarEtiquetasAsync(idPrompt, request.IdsEtiquetas);
                    }

                    return Json(new { success = true, numeroVersion, message = $"Versión {numeroVersion} creada exitosamente" });
                }
                else
                {
                    return Json(new { success = false, message = error });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al crear nueva versión para prompt {idPrompt}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost("api/smart-prompt-library/prompts/{idPrompt}/restaurar-version")]
        public async Task<IActionResult> RestaurarVersion(int idPrompt, [FromBody] RestaurarVersionRequest request)
        {
            try
            {
                var (success, numeroVersion, error) = await _versionRepository.RestaurarVersionAsync(
                    idPrompt,
                    request.NumeroVersion,
                    request.Usuario ?? "Sistema");

                if (success)
                {
                    return Json(new { success = true, numeroVersion, message = $"Versión {request.NumeroVersion} restaurada como versión {numeroVersion}" });
                }
                else
                {
                    return Json(new { success = false, message = error });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al restaurar versión {request.NumeroVersion} del prompt {idPrompt}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("api/smart-prompt-library/prompts/{idPrompt}/comparar-versiones")]
        public async Task<IActionResult> CompararVersiones(int idPrompt, [FromQuery] int version1, [FromQuery] int version2)
        {
            try
            {
                var (v1, v2) = await _versionRepository.CompararVersionesAsync(idPrompt, version1, version2);

                if (v1 == null || v2 == null)
                {
                    return Json(new { success = false, message = "Una o ambas versiones no existen" });
                }

                return Json(new
                {
                    success = true,
                    version1 = v1,
                    version2 = v2
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al comparar versiones {version1} y {version2} del prompt {idPrompt}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("api/smart-prompt-library/prompts/{idPrompt}/limpiar-versiones")]
        public async Task<IActionResult> LimpiarVersionesAntiguas(int idPrompt, [FromQuery] int mantenerUltimas = 10)
        {
            try
            {
                var deletedCount = await _versionRepository.LimpiarVersionesAntiguasAsync(idPrompt, mantenerUltimas);
                return Json(new { success = true, deletedCount, message = $"{deletedCount} versiones antiguas eliminadas" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al limpiar versiones antiguas del prompt {idPrompt}");
                return Json(new { success = false, message = ex.Message });
            }
        }

        #endregion
    }

    #region Request Models

    public class CreatePromptRequest
    {
        public int IdCategoriaPrompt { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string ContenidoMarkdown { get; set; }
        public string MensajeSistema { get; set; }
        public List<int> IdsEtiquetas { get; set; }
    }

    public class UpdatePromptRequest
    {
        public int IdCategoriaPrompt { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string ContenidoMarkdown { get; set; }
        public string MensajeSistema { get; set; }
        public bool Activo { get; set; }
        public bool Favorito { get; set; }
        public List<int> IdsEtiquetas { get; set; }
    }

    public class GenerarPromptRequest
    {
        public string IdeaGeneral { get; set; }
    }

    public class SugerirEtiquetasRequest
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string ContenidoMarkdown { get; set; }
    }

    public class EjecutarPromptRequest
    {
        public Dictionary<string, string> Parametros { get; set; }
    }

    public class CrearVersionRequest
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string ContenidoMarkdown { get; set; }
        public string MensajeSistema { get; set; }
        public string MensajeCambio { get; set; }
        public string Usuario { get; set; }
        public List<int> IdsEtiquetas { get; set; }
    }

    public class RestaurarVersionRequest
    {
        public int NumeroVersion { get; set; }
        public string Usuario { get; set; }
    }

    #endregion
}
