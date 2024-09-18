using FolderView.Dapper.Entidades;

namespace FolderView.Dapper.Interfaces
{
    public interface IFolderRepository
    {
        Task<List<ReadFolderEntidad>> CreateFolderAsync(CreateFolderEntidad dto);
        Task<ReadFolderEntidad> GetFolderByIdAsync(int id);
        Task<List<ReadFolderEntidad>> GetAllFolderAsync();
        Task<List<ReadFolderEntidad>> UpdateFolderAsync(UpdateFolderEntidad dto);
        Task<bool> DeleteFolderAsync(int id);
    }
}
