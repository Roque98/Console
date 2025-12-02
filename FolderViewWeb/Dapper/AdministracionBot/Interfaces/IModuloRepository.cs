using FolderView.Dapper.AdministracionBot.Entidades;

namespace FolderView.Dapper.AdministracionBot.Interfaces
{
    public interface IModuloRepository
    {
        Task<List<ModuloEntidad>> GetAllAsync();
        Task<ModuloEntidad> GetByIdAsync(int id);
        Task<int> CreateAsync(ModuloEntidad modulo);
        Task<bool> UpdateAsync(ModuloEntidad modulo);
        Task<bool> DeleteAsync(int id);
    }
}
