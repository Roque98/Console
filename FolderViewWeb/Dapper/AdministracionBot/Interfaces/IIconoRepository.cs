using FolderView.Dapper.AdministracionBot.Entidades;

namespace FolderView.Dapper.AdministracionBot.Interfaces
{
    public interface IIconoRepository
    {
        Task<List<IconoEntidad>> GetAllAsync();
        Task<List<IconoEntidad>> GetByCategoriaAsync(string categoria);
        Task<List<string>> GetCategoriasAsync();
    }
}
