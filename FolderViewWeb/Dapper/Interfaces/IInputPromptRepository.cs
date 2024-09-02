using FolderView.Dapper.Entidades;

namespace FolderView.Dapper.Interfaces
{
    public interface IInputPromptRepository
    {
        Task<List<InputPromptEntidad>> GetAllInputPromptsAsync();
        Task<InputPromptEntidad> GetInputPromptByIdAsync(int id);
        Task<List<InputPromptEntidad>> CreateInputPromptAsync(InputPromptEntidad dto);
        Task<List<InputPromptEntidad>> UpdateInputPromptAsync(InputPromptEntidad dto);
        Task<bool> DeleteInputPromptAsync(int id);
        Task<List<InputPromptEntidad>> GetAllInputPromptsByIdPromptAsync(int idPrompt);

    }
}
