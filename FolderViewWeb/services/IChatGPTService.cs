namespace FolderView.Services
{
    public interface IChatGPTService
    {
        Task<string> SendPromptAsync(string prompt, string? systemMessage = null);
        Task<string> SendPromptWithHistoryAsync(List<ChatMessage> messages);
        Task<ChatGPTResponse> SendPromptDetailedAsync(string prompt, string? systemMessage = null, string? model = null, double? temperature = null, int? maxTokens = null);
    }

    public class ChatMessage
    {
        public string Role { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }

    public class ChatGPTResponse
    {
        public string Content { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int PromptTokens { get; set; }
        public int CompletionTokens { get; set; }
        public int TotalTokens { get; set; }
        public string FinishReason { get; set; } = string.Empty;
    }
}
