using Newtonsoft.Json;
using System.Text;

namespace FolderView.Services
{
    public class ChatGPTService : IChatGPTService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ChatGPTService> _logger;
        private readonly string _apiKey;
        private readonly string _apiUrl;
        private readonly string _defaultModel;
        private readonly double _defaultTemperature;
        private readonly int _defaultMaxTokens;
        private readonly bool _enableLogging;

        public ChatGPTService(IConfiguration configuration, HttpClient httpClient, ILogger<ChatGPTService> logger)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _logger = logger;

            // Leer todas las configuraciones desde appsettings.json
            _apiKey = configuration["ChatGPT:ApiKey"] ?? throw new ArgumentNullException("ChatGPT:ApiKey not configured");
            _apiUrl = configuration["ChatGPT:ApiUrl"] ?? "https://api.openai.com/v1/chat/completions";
            _defaultModel = configuration["ChatGPT:DefaultModel"] ?? "gpt-4";
            _defaultTemperature = double.Parse(configuration["ChatGPT:DefaultTemperature"] ?? "0.7");
            _defaultMaxTokens = int.Parse(configuration["ChatGPT:DefaultMaxTokens"] ?? "1000");
            _enableLogging = bool.Parse(configuration["ChatGPT:EnableLogging"] ?? "true");

            var timeoutSeconds = int.Parse(configuration["ChatGPT:RequestTimeoutSeconds"] ?? "120");
            _httpClient.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            if (_enableLogging)
            {
                _logger.LogInformation("ChatGPT Service initialized with model: {Model}, Temperature: {Temperature}, MaxTokens: {MaxTokens}",
                    _defaultModel, _defaultTemperature, _defaultMaxTokens);
            }
        }

        public async Task<string> SendPromptAsync(string prompt, string? systemMessage = null)
        {
            var messages = new List<ChatMessage>();

            if (!string.IsNullOrEmpty(systemMessage))
            {
                messages.Add(new ChatMessage { Role = "system", Content = systemMessage });
            }

            messages.Add(new ChatMessage { Role = "user", Content = prompt });

            var response = await SendPromptDetailedAsync(prompt, systemMessage);
            return response.Content;
        }

        public async Task<string> SendPromptWithHistoryAsync(List<ChatMessage> messages)
        {
            if (_enableLogging)
            {
                _logger.LogInformation("Sending prompt with history. Messages count: {Count}", messages.Count);
            }

            var requestBody = new
            {
                model = _defaultModel,
                messages = messages.Select(m => new { role = m.Role, content = m.Content }).ToArray(),
                temperature = _defaultTemperature,
                max_tokens = _defaultMaxTokens
            };

            var jsonContent = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                if (_enableLogging)
                {
                    _logger.LogError("OpenAI API Error: {StatusCode} - {Error}", response.StatusCode, errorContent);
                }
                throw new HttpRequestException($"OpenAI API Error: {response.StatusCode} - {errorContent}");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(responseString);

            var responseContent = result?.choices[0]?.message?.content?.ToString() ?? string.Empty;

            if (_enableLogging)
            {
                _logger.LogInformation("ChatGPT response received. Length: {Length}", responseContent.Length);
            }

            return responseContent;
        }

        public async Task<ChatGPTResponse> SendPromptDetailedAsync(
            string prompt,
            string? systemMessage = null,
            string? model = null,
            double? temperature = null,
            int? maxTokens = null)
        {
            // Usar valores de configuración si no se especifican
            var actualModel = model ?? _defaultModel;
            var actualTemperature = temperature ?? _defaultTemperature;
            var actualMaxTokens = maxTokens ?? _defaultMaxTokens;
            var actualSystemMessage = systemMessage ?? _configuration["ChatGPT:DefaultSystemMessage"];

            if (_enableLogging)
            {
                _logger.LogInformation("Sending prompt. Model: {Model}, Temperature: {Temperature}, MaxTokens: {MaxTokens}",
                    actualModel, actualTemperature, actualMaxTokens);
            }

            var messages = new List<object>();

            if (!string.IsNullOrEmpty(actualSystemMessage))
            {
                messages.Add(new { role = "system", content = actualSystemMessage });
            }

            messages.Add(new { role = "user", content = prompt });

            var requestBody = new
            {
                model = actualModel,
                messages = messages,
                temperature = actualTemperature,
                max_tokens = actualMaxTokens
            };

            var jsonContent = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_apiUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                if (_enableLogging)
                {
                    _logger.LogError("OpenAI API Error: {StatusCode} - {Error}", response.StatusCode, errorContent);
                }
                throw new HttpRequestException($"OpenAI API Error: {response.StatusCode} - {errorContent}");
            }

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<dynamic>(responseString);

            var chatResponse = new ChatGPTResponse
            {
                Content = result?.choices[0]?.message?.content?.ToString() ?? string.Empty,
                Model = result?.model?.ToString() ?? actualModel,
                PromptTokens = result?.usage?.prompt_tokens ?? 0,
                CompletionTokens = result?.usage?.completion_tokens ?? 0,
                TotalTokens = result?.usage?.total_tokens ?? 0,
                FinishReason = result?.choices[0]?.finish_reason?.ToString() ?? string.Empty
            };

            if (_enableLogging)
            {
                _logger.LogInformation("ChatGPT response received. Tokens used: {TotalTokens}", chatResponse.TotalTokens);
            }

            return chatResponse;
        }
    }
}
