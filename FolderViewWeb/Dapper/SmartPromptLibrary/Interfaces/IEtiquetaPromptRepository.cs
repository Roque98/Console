using FolderView.Dapper.SmartPromptLibrary.Entidades;

namespace FolderView.Dapper.SmartPromptLibrary.Interfaces
{
    public interface IEtiquetaPromptRepository
    {
        Task<List<EtiquetaPromptEntidad>> GetAllAsync();
        Task<List<EtiquetaPromptEntidad>> GetAllActivasAsync();
        Task<EtiquetaPromptEntidad> GetByIdAsync(int id);
        Task<EtiquetaPromptEntidad> GetByNombreAsync(string nombre);
        Task<int> CreateAsync(EtiquetaPromptEntidad etiqueta);
        Task<bool> UpdateAsync(EtiquetaPromptEntidad etiqueta);
        Task<bool> DeleteAsync(int id);
        Task<List<EtiquetaPromptEntidad>> GetEtiquetasByPromptAsync(int idPrompt);
    }
}
