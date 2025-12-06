using FolderView.Dapper.SmartPromptLibrary.Entidades;

namespace FolderView.Dapper.SmartPromptLibrary.Interfaces
{
    public interface ICategoriaPromptRepository
    {
        Task<List<CategoriaPromptEntidad>> GetAllAsync();
        Task<List<CategoriaPromptEntidad>> GetAllActivasAsync();
        Task<CategoriaPromptEntidad> GetByIdAsync(int id);
        Task<int> CreateAsync(CategoriaPromptEntidad categoria);
        Task<bool> UpdateAsync(CategoriaPromptEntidad categoria);
        Task<bool> DeleteAsync(int id);
    }
}
