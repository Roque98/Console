using FolderView.Dapper.AdministracionBot.Entidades;

namespace FolderView.Dapper.AdministracionBot.Interfaces
{
    public interface IRolRepository
    {
        Task<List<RolEntidad>> GetAllAsync();
        Task<RolEntidad> GetByIdAsync(int id);
        Task<List<RolOperacionEntidad>> GetPermisosByRolAsync(int idRol);
        Task<bool> AsignarPermisoAsync(int idRol, int idOperacion, bool permitido, int? usuarioAsignacion);
        Task<bool> RevocarPermisoAsync(int idRol, int idOperacion);
        Task<List<UsuarioOperacionEntidad>> GetPermisosEspecificosUsuarioAsync();
        Task<bool> AsignarPermisoEspecificoUsuarioAsync(UsuarioOperacionEntidad permiso);
    }
}
