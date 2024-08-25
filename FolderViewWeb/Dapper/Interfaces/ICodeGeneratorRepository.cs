using FolderView.Dapper.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FolderView.Dapper.Interfaces
{
    public interface ICodeGeneratorRepository
    {
        Task<List<ReadTipoProyectoEntidad>> CreateTipoProyectoAsync(CreateTipoProyectoEntidad dto);
        Task<ReadTipoProyectoEntidad> GetTipoProyectoByIdAsync(int id);
        Task<List<ReadTipoProyectoEntidad>> GetAllTipoProyectoAsync();
        Task<List<ReadTipoProyectoEntidad>> UpdateTipoProyectoAsync(UpdateTipoProyectoEntidad dto);
        Task<bool> DeleteTipoProyectoAsync(int id);

        Task<List<ReadTipoArchivosEntidad>> CreateTipoArchivosAsync(CreateTipoArchivosEntidad dto);
        Task<ReadTipoArchivosEntidad> GetTipoArchivosByIdAsync(int id);
        Task<List<ReadTipoArchivosEntidad>> GetAllTipoArchivosAsync();
        Task<List<ReadTipoArchivosEntidad>> UpdateTipoArchivosAsync(UpdateTipoArchivosEntidad dto);
        Task<bool> DeleteTipoArchivosAsync(int id);

        Task<List<ReadTipoArchivoTipoProyectoEntidad>> CreateTipoArchivoTipoProyectoAsync(CreateTipoArchivoTipoProyectoEntidad dto);
        Task<ReadTipoArchivoTipoProyectoEntidad> GetTipoArchivoTipoProyectoByIdAsync(int id);
        Task<List<ReadTipoArchivoTipoProyectoEntidad>> GetAllTipoArchivoTipoProyectoAsync();
        Task<List<ReadTipoArchivoTipoProyectoEntidad>> UpdateTipoArchivoTipoProyectoAsync(UpdateTipoArchivoTipoProyectoEntidad dto);
        Task<bool> DeleteTipoArchivoTipoProyectoAsync(int id);

        Task<List<ReadProyectoEntidad>> CreateProyectoAsync(CreateProyectoEntidad dto);
        Task<ReadProyectoEntidad> GetProyectoByIdAsync(int id);
        Task<List<ReadProyectoEntidad>> GetAllProyectoAsync();
        Task<List<ReadProyectoEntidad>> UpdateProyectoAsync(UpdateProyectoEntidad dto);
        Task<bool> DeleteProyectoAsync(int id);

        Task<List<ReadFolderEntidad>> CreateFolderAsync(CreateFolderEntidad dto);
        Task<ReadFolderEntidad> GetFolderByIdAsync(int id);
        Task<List<ReadFolderEntidad>> GetAllFolderAsync();
        Task<List<ReadFolderEntidad>> UpdateFolderAsync(UpdateFolderEntidad dto);
        Task<bool> DeleteFolderAsync(int id);

        Task<List<ReadArchivoEntidad>> CreateArchivoAsync(CreateArchivoEntidad dto);
        Task<ReadArchivoEntidad> GetArchivoByIdAsync(int id);
        Task<List<ReadArchivoEntidad>> GetAllArchivoAsync();
        Task<List<ReadArchivoEntidad>> UpdateArchivoAsync(UpdateArchivoEntidad dto);
        Task<bool> DeleteArchivoAsync(int id);
    }
}
