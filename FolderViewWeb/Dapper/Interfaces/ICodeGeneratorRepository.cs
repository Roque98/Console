using FolderView.Dapper.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FolderView.Dapper.Interfaces
{
    public interface ICodeGeneratorRepository
    {
        public Task<List<CreateTipoProyectoEntidad>> CreateTipoProyectoAsync(CreateTipoProyectoEntidad dto);
        public Task<List<ReadTipoProyectoEntidad>> GetTipoProyectoByIdAsync(int id);
        public Task<List<UpdateTipoProyectoEntidad>> UpdateTipoProyectoAsync(UpdateTipoProyectoEntidad dto);
        public Task<bool> DeleteTipoProyectoAsync(int id);

        public Task<List<CreateTipoArchivosEntidad>> CreateTipoArchivosAsync(CreateTipoArchivosEntidad dto);
        public Task<List<ReadTipoArchivosEntidad>> GetTipoArchivosByIdAsync(int id);
        public Task<List<ReadTipoArchivosEntidad>> GetAllTipoArchivos();
        public Task<List<UpdateTipoArchivosEntidad>> UpdateTipoArchivosAsync(UpdateTipoArchivosEntidad dto);
        public Task<bool> DeleteTipoArchivosAsync(int id);

        public Task<List<CreateTipoArchivoTipoProyectoEntidad>> CreateTipoArchivoTipoProyectoAsync(CreateTipoArchivoTipoProyectoEntidad dto);
        public Task<List<ReadTipoArchivoTipoProyectoEntidad>> GetTipoArchivoTipoProyectoByIdAsync(int id);
        public Task<List<UpdateTipoArchivoTipoProyectoEntidad>> UpdateTipoArchivoTipoProyectoAsync(UpdateTipoArchivoTipoProyectoEntidad dto);
        public Task<bool> DeleteTipoArchivoTipoProyectoAsync(int id);

        public Task<List<CreateProyectoEntidad>> CreateProyectoAsync(CreateProyectoEntidad dto);
        public Task<List<ReadProyectoEntidad>> GetProyectoByIdAsync(int id);
        public Task<List<UpdateProyectoEntidad>> UpdateProyectoAsync(UpdateProyectoEntidad dto);
        public Task<bool> DeleteProyectoAsync(int id);

        public Task<List<CreateFolderEntidad>> CreateFolderAsync(CreateFolderEntidad dto);
        public Task<List<ReadFolderEntidad>> GetFolderByIdAsync(int id);
        public Task<List<UpdateFolderEntidad>> UpdateFolderAsync(UpdateFolderEntidad dto);
        public Task<bool> DeleteFolderAsync(int id);

        public Task<List<CreateArchivoEntidad>> CreateArchivoAsync(CreateArchivoEntidad dto);
        public Task<List<ReadArchivoEntidad>> GetArchivoByIdAsync(int id);
        public Task<List<UpdateArchivoEntidad>> UpdateArchivoAsync(UpdateArchivoEntidad dto);
        public Task<bool> DeleteArchivoAsync(int id);
    }
}
