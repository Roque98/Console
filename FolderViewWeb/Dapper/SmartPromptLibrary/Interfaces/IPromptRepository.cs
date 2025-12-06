using FolderView.Dapper.SmartPromptLibrary.Entidades;

namespace FolderView.Dapper.SmartPromptLibrary.Interfaces
{
    public interface IPromptRepository
    {
        Task<List<PromptEntidad>> GetAllAsync();
        Task<List<PromptEntidad>> GetAllActivosAsync();
        Task<List<PromptEntidad>> GetByCategoriaAsync(int idCategoria);
        Task<List<PromptEntidad>> GetFavoritosAsync();
        Task<List<PromptEntidad>> SearchAsync(string searchTerm, int? idCategoria = null, List<int> idsEtiquetas = null);
        Task<PromptEntidad> GetByIdAsync(int id);
        Task<PromptEntidad> GetByIdConDetallesAsync(int id);
        Task<int> CreateAsync(PromptEntidad prompt);
        Task<bool> UpdateAsync(PromptEntidad prompt);
        Task<bool> DeleteAsync(int id);
        Task<bool> ToggleFavoritoAsync(int id);
        Task<bool> IncrementarEjecucionesAsync(int id);
        Task<bool> AsignarEtiquetasAsync(int idPrompt, List<int> idsEtiquetas);
        Task<List<string>> DetectarVariablesAsync(string contenidoMarkdown);
    }
}
