using Dapper;
using FolderView.Dapper.AdministracionBot.Entidades;
using FolderView.Dapper.AdministracionBot.Interfaces;

namespace FolderView.Dapper.AdministracionBot.Repositorios
{
    public class IconoRepositorio : IIconoRepository
    {
        private readonly DapperContext _context;

        public IconoRepositorio(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<IconoEntidad>> GetAllAsync()
        {
            var query = @"
                SELECT
                    i.idIcono as IdIcono,
                    i.nombre as Nombre,
                    i.clase as Clase,
                    i.categoria as Categoria,
                    i.descripcion as Descripcion,
                    i.activo as Activo,
                    i.fechaCreacion as FechaCreacion
                FROM abcmasplus..Iconos i
                WHERE i.activo = 1
                ORDER BY i.categoria, i.nombre";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<IconoEntidad>(query);
            return result.ToList();
        }

        public async Task<List<IconoEntidad>> GetByCategoriaAsync(string categoria)
        {
            var query = @"
                SELECT
                    i.idIcono as IdIcono,
                    i.nombre as Nombre,
                    i.clase as Clase,
                    i.categoria as Categoria,
                    i.descripcion as Descripcion,
                    i.activo as Activo,
                    i.fechaCreacion as FechaCreacion
                FROM abcmasplus..Iconos i
                WHERE i.activo = 1 AND i.categoria = @categoria
                ORDER BY i.nombre";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<IconoEntidad>(query, new { categoria });
            return result.ToList();
        }

        public async Task<List<string>> GetCategoriasAsync()
        {
            var query = @"
                SELECT DISTINCT i.categoria
                FROM abcmasplus..Iconos i
                WHERE i.activo = 1
                ORDER BY i.categoria";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<string>(query);
            return result.ToList();
        }
    }
}
