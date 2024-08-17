using FolderView.Dapper.Entidades;

namespace FolderView.Dapper.Interfaces
{
    public interface IArchivoRepository
    {
        Task<List<ArchivoEntidad>> GetByDirectorioIdAsync(int directorioId);
    }
}
