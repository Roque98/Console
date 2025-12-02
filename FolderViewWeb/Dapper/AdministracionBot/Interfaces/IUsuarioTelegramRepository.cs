using FolderView.Dapper.AdministracionBot.Entidades;

namespace FolderView.Dapper.AdministracionBot.Interfaces
{
    public interface IUsuarioTelegramRepository
    {
        Task<List<UsuarioTelegramEntidad>> GetAllAsync();
        Task<UsuarioTelegramEntidad> GetByIdAsync(int id);
        Task<List<UsuarioTelegramEntidad>> GetByUsuarioAsync(int idUsuario);
        Task<List<UsuarioTelegramEntidad>> GetPendientesVerificacionAsync();
        Task<bool> VerificarCuentaAsync(int idUsuarioTelegram);
        Task<bool> EstablecerPrincipalAsync(int idUsuarioTelegram, int idUsuario);
        Task<bool> CambiarEstadoAsync(int idUsuarioTelegram, string estado);
        Task<List<UsuarioTelegramEntidad>> GetUsuariosConMultiplesCuentasAsync();
        Task<List<UsuarioTelegramEntidad>> GetCuentasByUsuarioIdAsync(int idUsuario);
        Task<bool> SetCuentaPrincipalAsync(int idUsuarioTelegram);
        Task<List<LogOperacionEntidad>> GetHistorialActividadAsync(int idUsuarioTelegram);
    }
}
