using FolderView.Dapper.Entidades;

namespace FolderView.Dapper.Interfaces
{
    public interface IProyectoRepository
    {
        Task<List<ReadProyectoEntidad>> CreateProyectoAsync(CreateProyectoEntidad dto);
        Task<ReadProyectoEntidad> GetProyectoByIdAsync(int id);
        Task<List<ReadProyectoEntidad>> GetAllProyectoAsync();
        Task<List<ReadProyectoEntidad>> UpdateProyectoAsync(UpdateProyectoEntidad dto);
        Task<bool> DeleteProyectoAsync(int id);
    }
}
