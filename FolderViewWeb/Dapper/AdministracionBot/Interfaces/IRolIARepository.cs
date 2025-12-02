using FolderView.Dapper.AdministracionBot.Entidades;

namespace FolderView.Dapper.AdministracionBot.Interfaces
{
    public interface IRolIARepository
    {
        Task<List<RolIAEntidad>> GetAllAsync();
        Task<RolIAEntidad> GetByIdAsync(int id);
        Task<int> CreateAsync(RolIAEntidad rol);
        Task<bool> UpdateAsync(RolIAEntidad rol);
        Task<bool> AsignarRolAUsuarioAsync(int idRol, int idUsuario);
        Task<bool> RemoverRolDeUsuarioAsync(int idRol, int idUsuario);
    }
}
