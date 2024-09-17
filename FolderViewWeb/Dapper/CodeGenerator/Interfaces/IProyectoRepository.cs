using FolderView.Dapper.Entidades;

namespace FolderView.Dapper.Interfaces
{
    public interface IProyectoRepository
    {
        Task<List<ProyectoEntidad>> CreateAsync(ProyectoEntidad dto);
        Task<ProyectoEntidad> GetByIdAsync(int id);
        Task<ProyectoEntidad> UpdateAsync(ProyectoEntidad dto);
        Task<ProyectoEntidad> DeleteAsync(int id);

        Task<List<ProyectoEntidad>> GetAllByIdTipoProyectoAsync(int id);
        //Task<List<ProyectoEntidad>> GetAllByIdArchivoAsync(int id);
    }
}