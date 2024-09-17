using FolderView.Dapper.Entidades;

namespace FolderView.Dapper.Interfaces
{
    public interface ITipoProyectoRepository
    {
        Task<List<TipoProyectoEntidad>> CreateAsync(TipoProyectoEntidad dto);
        Task<TipoProyectoEntidad> GetByIdAsync(int id);
        Task<TipoProyectoEntidad> UpdateAsync(TipoProyectoEntidad dto);
        Task<TipoProyectoEntidad> DeleteAsync(int id);
        Task<List<TipoProyectoEntidad>> GetAllAsync();
        
    }
}