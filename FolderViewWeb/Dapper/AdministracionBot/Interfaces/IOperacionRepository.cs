using FolderView.Dapper.AdministracionBot.Entidades;

namespace FolderView.Dapper.AdministracionBot.Interfaces
{
    public interface IOperacionRepository
    {
        Task<List<OperacionEntidad>> GetAllAsync();
        Task<List<OperacionEntidad>> GetByModuloAsync(int idModulo);
        Task<OperacionEntidad> GetByIdAsync(int id);
        Task<int> CreateAsync(OperacionEntidad operacion);
        Task<bool> UpdateAsync(OperacionEntidad operacion);
        Task<bool> DeleteAsync(int id);
        Task<List<OperacionEntidad>> GetAllConEstadisticasAsync();
        Task<List<OperacionEntidad>> GetOperacionesByUsuarioAsync(int idUsuario);
    }
}
