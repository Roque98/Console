using FolderView.Dapper.SmartPromptLibrary.Entidades;

namespace FolderView.Dapper.SmartPromptLibrary.Interfaces
{
    public interface IEjecucionPromptRepository
    {
        Task<List<EjecucionPromptEntidad>> GetAllAsync();
        Task<List<EjecucionPromptEntidad>> GetByPromptAsync(int idPrompt);
        Task<EjecucionPromptEntidad> GetByIdAsync(int id);
        Task<EjecucionPromptEntidad> GetByIdConDetallesAsync(int id);
        Task<int> CreateAsync(EjecucionPromptEntidad ejecucion, List<ParametroEjecucionEntidad> parametros);
        Task<bool> DeleteAsync(int id);
        Task<List<EjecucionPromptEntidad>> GetUltimasEjecucionesAsync(int limit = 10);
    }
}
