using FolderView.Dapper.AdministracionBot.Entidades;
using FolderView.Dapper.AdministracionBot.Interfaces;
using FolderView.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FolderView.Controllers.AdministracionBot
{
    public class KnowledgeBaseController : Controller
    {
        private readonly IKnowledgeRepository _knowledgeRepository;
        private readonly IPdfService _pdfService;
        private readonly ILlmService _llmService;
        private readonly ILogger<KnowledgeBaseController> _logger;

        public KnowledgeBaseController(
            IKnowledgeRepository knowledgeRepository,
            IPdfService pdfService,
            ILlmService llmService,
            ILogger<KnowledgeBaseController> logger)
        {
            _knowledgeRepository = knowledgeRepository;
            _pdfService = pdfService;
            _llmService = llmService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Categorías
        [HttpGet("api/knowledge/categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _knowledgeRepository.GetAllCategoriesAsync();
            return Json(result);
        }

        [HttpGet("api/knowledge/categories/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _knowledgeRepository.GetCategoryByIdAsync(id);
            return Json(result);
        }

        [HttpPost("api/knowledge/categories")]
        public async Task<IActionResult> CreateCategory([FromBody] KnowledgeCategoryEntidad category)
        {
            var id = await _knowledgeRepository.CreateCategoryAsync(category);
            return Json(new { success = true, id });
        }

        [HttpPut("api/knowledge/categories/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] KnowledgeCategoryEntidad category)
        {
            category.Id = id;
            var result = await _knowledgeRepository.UpdateCategoryAsync(category);
            return Json(new { success = result });
        }

        // Entradas
        [HttpGet("api/knowledge/entries")]
        public async Task<IActionResult> GetAllEntries()
        {
            var result = await _knowledgeRepository.GetAllEntriesAsync();
            return Json(result);
        }

        [HttpGet("api/knowledge/entries/category/{categoryId}")]
        public async Task<IActionResult> GetEntriesByCategory(int categoryId)
        {
            var result = await _knowledgeRepository.GetEntriesByCategoryAsync(categoryId);
            return Json(result);
        }

        [HttpGet("api/knowledge/entries/{id}")]
        public async Task<IActionResult> GetEntryById(int id)
        {
            var result = await _knowledgeRepository.GetEntryByIdAsync(id);
            return Json(result);
        }

        [HttpPost("api/knowledge/entries")]
        public async Task<IActionResult> CreateEntry([FromBody] KnowledgeEntryEntidad entry)
        {
            var id = await _knowledgeRepository.CreateEntryAsync(entry);
            return Json(new { success = true, id });
        }

        [HttpPut("api/knowledge/entries/{id}")]
        public async Task<IActionResult> UpdateEntry(int id, [FromBody] KnowledgeEntryEntidad entry)
        {
            entry.Id = id;
            var result = await _knowledgeRepository.UpdateEntryAsync(entry);
            return Json(new { success = result });
        }

        [HttpDelete("api/knowledge/entries/{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            var result = await _knowledgeRepository.DeleteEntryAsync(id);
            return Json(new { success = result });
        }

        [HttpPost("api/knowledge/process-pdf")]
        public async Task<IActionResult> ProcessPdfWithAI(IFormFile pdfFile, [FromForm] int categoryId)
        {
            try
            {
                // Validar archivo
                if (pdfFile == null || pdfFile.Length == 0)
                {
                    return Json(new { success = false, message = "No se recibió ningún archivo PDF" });
                }

                if (!pdfFile.ContentType.Equals("application/pdf", StringComparison.OrdinalIgnoreCase) &&
                    !pdfFile.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    return Json(new { success = false, message = "El archivo debe ser un PDF" });
                }

                // Validar categoría
                var category = await _knowledgeRepository.GetCategoryByIdAsync(categoryId);
                if (category == null)
                {
                    return Json(new { success = false, message = "La categoría seleccionada no existe" });
                }

                _logger.LogInformation($"Procesando PDF: {pdfFile.FileName} para categoría {category.DisplayName}");

                // Extraer texto del PDF
                string extractedText;
                using (var stream = pdfFile.OpenReadStream())
                {
                    extractedText = await _pdfService.ExtractTextFromPdfAsync(stream);
                }

                if (string.IsNullOrWhiteSpace(extractedText))
                {
                    return Json(new { success = false, message = "No se pudo extraer texto del PDF" });
                }

                _logger.LogInformation($"Texto extraído: {extractedText.Length} caracteres");

                // Crear prompt para el LLM
                var systemMessage = @"Eres un experto en análisis de documentación y creación de bases de conocimiento.
Tu tarea es analizar el texto proporcionado y extraer información útil para una base de conocimiento.

Identifica preguntas frecuentes, conceptos importantes, procedimientos y cualquier información relevante.
Para cada elemento identificado, genera:
- question: Una pregunta clara y específica
- answer: Una respuesta detallada y precisa
- keywords: Palabras clave separadas por comas (3-5 palabras)
- priority: Un número del 1 al 10 (1 = baja prioridad, 10 = alta prioridad)

IMPORTANTE: Responde ÚNICAMENTE con un JSON válido en el siguiente formato:
{
  ""entries"": [
    {
      ""question"": ""¿Pregunta aquí?"",
      ""answer"": ""Respuesta detallada aquí"",
      ""keywords"": ""palabra1, palabra2, palabra3"",
      ""priority"": 5
    }
  ]
}

No incluyas ningún texto adicional fuera del JSON. No uses markdown code blocks.";

                var userPrompt = $@"Analiza el siguiente texto extraído de un documento PDF y genera entradas para la base de conocimiento:

--- INICIO DEL TEXTO ---
{extractedText}
--- FIN DEL TEXTO ---

Genera entre 5 y 20 entradas relevantes basadas en el contenido. Asegúrate de cubrir los temas más importantes.";

                // Enviar al LLM (usando el módulo de Smart Prompt Library - ID 8)
                var llmResponse = await _llmService.SendPromptAsync(8, userPrompt, systemMessage);

                if (!llmResponse.Success)
                {
                    return Json(new { success = false, message = $"Error al procesar con IA: {llmResponse.Error}" });
                }

                _logger.LogInformation($"Respuesta del LLM: {llmResponse.Content}");

                // Parsear la respuesta JSON del LLM
                var cleanedResponse = llmResponse.Content.Trim();

                // Remover markdown code blocks si existen
                if (cleanedResponse.StartsWith("```json"))
                {
                    cleanedResponse = cleanedResponse.Substring(7);
                }
                if (cleanedResponse.StartsWith("```"))
                {
                    cleanedResponse = cleanedResponse.Substring(3);
                }
                if (cleanedResponse.EndsWith("```"))
                {
                    cleanedResponse = cleanedResponse.Substring(0, cleanedResponse.Length - 3);
                }
                cleanedResponse = cleanedResponse.Trim();

                JsonDocument jsonDoc;
                try
                {
                    jsonDoc = JsonDocument.Parse(cleanedResponse);
                }
                catch (JsonException ex)
                {
                    _logger.LogError($"Error al parsear JSON del LLM: {ex.Message}. Respuesta: {cleanedResponse}");
                    return Json(new {
                        success = false,
                        message = $"La IA generó una respuesta inválida. Por favor intenta nuevamente.",
                        llmResponse = cleanedResponse.Substring(0, Math.Min(500, cleanedResponse.Length))
                    });
                }

                // Extraer entradas del JSON
                if (!jsonDoc.RootElement.TryGetProperty("entries", out var entriesElement))
                {
                    return Json(new { success = false, message = "La respuesta de la IA no contiene entradas válidas" });
                }

                var entriesCreated = 0;
                foreach (var entryElement in entriesElement.EnumerateArray())
                {
                    try
                    {
                        var entry = new KnowledgeEntryEntidad
                        {
                            CategoryId = categoryId,
                            Question = entryElement.GetProperty("question").GetString(),
                            Answer = entryElement.GetProperty("answer").GetString(),
                            Keywords = entryElement.GetProperty("keywords").GetString(),
                            Priority = entryElement.TryGetProperty("priority", out var priorityProp)
                                ? priorityProp.GetInt32()
                                : 5,
                            Active = true,
                            CreatedAt = DateTime.Now
                        };

                        await _knowledgeRepository.CreateEntryAsync(entry);
                        entriesCreated++;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning($"Error al crear entrada individual: {ex.Message}");
                        // Continuar con las demás entradas
                    }
                }

                if (entriesCreated == 0)
                {
                    return Json(new { success = false, message = "No se pudo crear ninguna entrada. Revisa el formato del PDF." });
                }

                _logger.LogInformation($"PDF procesado exitosamente. {entriesCreated} entradas creadas.");

                return Json(new {
                    success = true,
                    entriesCreated = entriesCreated,
                    message = $"Se crearon {entriesCreated} entradas exitosamente"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar PDF con IA");
                return Json(new { success = false, message = $"Error al procesar el PDF: {ex.Message}" });
            }
        }
    }
}
