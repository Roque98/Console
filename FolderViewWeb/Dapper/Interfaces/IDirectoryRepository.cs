using FolderView.Dapper.Entidades;

namespace FolderView.Dapper.Interfaces
{
    public interface IDirectoryRepository
    {
        Task<DirectorioEntidad> GetByIdAsync(int directorioId);
        Task<List<DirectorioEntidad>> GetByParentIdAsync(int directorioId);
    }
}
