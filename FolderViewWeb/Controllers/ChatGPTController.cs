using Microsoft.AspNetCore.Mvc;
using FolderView.Services;

namespace FolderView.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatGPTController : ControllerBase
    {
        private readonly IChatGPTService _chatGPTService;
        private readonly ILogger<ChatGPTController> _logger;

        public ChatGPTController(IChatGPTService chatGPTService, ILogger<ChatGPTController> logger)
        {
            _chatGPTService = chatGPTService;
            _logger = logger;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendPrompt([FromBody] PromptRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Prompt))
                {
                    return BadRequest(new { error = "El prompt no puede estar vacío" });
                }

                var response = await _chatGPTService.SendPromptAsync(request.Prompt, request.SystemMessage);

                return Ok(new
                {
                    success = true,
                    response = response
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar prompt a ChatGPT");
                return StatusCode(500, new
                {
                    success = false,
                    error = "Error al procesar la solicitud",
                    details = ex.Message
                });
            }
        }

        [HttpPost("send-detailed")]
        public async Task<IActionResult> SendPromptDetailed([FromBody] DetailedPromptRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Prompt))
                {
                    return BadRequest(new { error = "El prompt no puede estar vacío" });
                }

                var response = await _chatGPTService.SendPromptDetailedAsync(
                    request.Prompt,
                    request.SystemMessage,
                    request.Model ?? "gpt-4",
                    request.Temperature ?? 0.7,
                    request.MaxTokens ?? 1000
                );

                return Ok(new
                {
                    success = true,
                    response = new
                    {
                        content = response.Content,
                        model = response.Model,
                        usage = new
                        {
                            promptTokens = response.PromptTokens,
                            completionTokens = response.CompletionTokens,
                            totalTokens = response.TotalTokens
                        },
                        finishReason = response.FinishReason
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar prompt detallado a ChatGPT");
                return StatusCode(500, new
                {
                    success = false,
                    error = "Error al procesar la solicitud",
                    details = ex.Message
                });
            }
        }

        [HttpPost("send-with-history")]
        public async Task<IActionResult> SendPromptWithHistory([FromBody] HistoryPromptRequest request)
        {
            try
            {
                if (request.Messages == null || !request.Messages.Any())
                {
                    return BadRequest(new { error = "Debe proporcionar al menos un mensaje" });
                }

                var response = await _chatGPTService.SendPromptWithHistoryAsync(request.Messages);

                return Ok(new
                {
                    success = true,
                    response = response
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar prompt con historial a ChatGPT");
                return StatusCode(500, new
                {
                    success = false,
                    error = "Error al procesar la solicitud",
                    details = ex.Message
                });
            }
        }
    }

    public class PromptRequest
    {
        public string Prompt { get; set; } = string.Empty;
        public string? SystemMessage { get; set; }
    }

    public class DetailedPromptRequest
    {
        public string Prompt { get; set; } = string.Empty;
        public string? SystemMessage { get; set; }
        public string? Model { get; set; }
        public double? Temperature { get; set; }
        public int? MaxTokens { get; set; }
    }

    public class HistoryPromptRequest
    {
        public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();
    }
}
