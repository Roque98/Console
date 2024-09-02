using FolderView.Dapper.Entidades;

namespace FolderView.Dapper.Interfaces
{
    public interface ICodeGeneratorArchivo
    {
        Task<List<ReadArchivoEntidad>> CreateArchivoAsync(CreateArchivoEntidad dto);
        Task<ReadArchivoEntidad> GetArchivoByIdAsync(int id);
        Task<List<ReadArchivoEntidad>> GetAllArchivoAsync();
        Task<List<ReadArchivoEntidad>> UpdateArchivoAsync(UpdateArchivoEntidad dto);
        Task<bool> DeleteArchivoAsync(int id);

    }
}
