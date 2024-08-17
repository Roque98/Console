using Dapper;
using FolderView.Dapper.Entidades;
using FolderView.Dapper.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace FolderView.Dapper.Repositorios
{
    public class DirectorioRepositorio : IDirectoryRepository
    {
        private readonly DapperContext _context;
        public DirectorioRepositorio(DapperContext context)
        {
            _context = context;
        }

        public async Task<DirectorioEntidad> GetByIdAsync(int directorioId)
        {
            var query = $"consolaMonitoreo..[FolderView_Directorio_GetById]";
            var connection = _context.CreateConnection();
            var param = new { directorioId };
            var resultado = await connection.QueryFirstOrDefaultAsync<DirectorioEntidad>(query,
                                                                                param,
                                                                                commandType: CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<List<DirectorioEntidad>> GetByParentIdAsync(int directorioId)
        {
            var query = $"consolaMonitoreo..[FolderView_Directorio_GetAllByParentId]";
            var connection = _context.CreateConnection();
            var param = new { directorioId };
            var resultado = await connection.QueryAsync<DirectorioEntidad>(query,
                                                                                param,
                                                                                commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }
    }
}
