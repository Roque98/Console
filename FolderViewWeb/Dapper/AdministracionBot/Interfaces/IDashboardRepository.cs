using FolderView.Dapper.AdministracionBot.Entidades;

namespace FolderView.Dapper.AdministracionBot.Interfaces
{
    public interface IDashboardRepository
    {
        Task<DashboardStatsEntidad> GetStatsAsync();
        Task<List<OperacionResultadoEntidad>> GetOperacionesPorResultadoHoyAsync();
        Task<List<TopOperacionEntidad>> GetTopOperacionesAsync(int top = 5);
        Task<List<TopUsuarioEntidad>> GetTopUsuariosAsync(int top = 5);
        Task<List<TasaExitoEntidad>> GetTasaExitoUltimos7DiasAsync();
    }
}
