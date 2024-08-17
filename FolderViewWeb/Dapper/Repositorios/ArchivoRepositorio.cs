using Dapper;
using FolderView.Dapper.Entidades;
using FolderView.Dapper.Interfaces;
using System.Data;

namespace FolderView.Dapper.Repositorios
{
    public class ArchivoRepositorio : IArchivoRepository
    {
        private readonly DapperContext _context;
        public ArchivoRepositorio(DapperContext context)
        {
            _context = context;
        }
        public async Task<List<ArchivoEntidad>> GetByDirectorioIdAsync(int directorioId)
        {
            var query = $"consolaMonitoreo..[FolderView_Archivo_GetAllByParentDirectoryId]";
            var connection = _context.CreateConnection();
            var param = new { directorioId };
            var resultado = await connection.QueryAsync<ArchivoEntidad>(query,
                                                                                param,
                                                                                commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }
    }
}
