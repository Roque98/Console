using FolderView.Dapper.AdministracionBot.Entidades;

namespace FolderView.Dapper.AdministracionBot.Interfaces
{
    public interface ILogOperacionRepository
    {
        Task<List<LogOperacionEntidad>> GetAllAsync(int offset = 0, int pageSize = 50);
        Task<LogOperacionEntidad> GetByIdAsync(int id);
        Task<List<LogOperacionEntidad>> GetConFiltrosAsync(int? idUsuario = null, int? idOperacion = null,
            string resultado = null, DateTime? fechaDesde = null, DateTime? fechaHasta = null);
        Task<List<LogOperacionEntidad>> GetOperacionesfallidasRecientesAsync(int horas = 24, int top = 100);
        Task<List<LogOperacionEntidad>> GetOperacionesPorDiaAsync(int dias = 30);
        Task<List<LogOperacionEntidad>> GetByUsuarioAsync(int idUsuario, DateTime fechaInicio, DateTime fechaFin);
    }
}
