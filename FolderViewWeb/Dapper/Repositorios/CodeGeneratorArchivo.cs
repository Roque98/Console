using Dapper;
using FolderView.Dapper.Entidades;
using FolderView.Dapper.Interfaces;
using System.Data;

namespace FolderView.Dapper.Repositorios
{
    public class CodeGeneratorArchivo: ICodeGeneratorArchivo
    {

        private readonly DapperContext _context;

        public CodeGeneratorArchivo(
            DapperContext context
        )
        {
            _context = context;
        }

        public async Task<List<ReadArchivoEntidad>> CreateArchivoAsync(CreateArchivoEntidad dto)
        {
            var query = "consolaMonitoreo..CodeGenerator_Archivo_Add";
            var connection = _context.CreateConnection();
            var param = new { dto.IdFolder, dto.Extension, dto.Documentacion, dto.Contenido, dto.Path };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return await GetAllArchivoAsync();
        }

        public async Task<ReadArchivoEntidad> GetArchivoByIdAsync(int id)
        {
            var query = "consolaMonitoreo..CodeGenerator_Archivo_GetById";
            var connection = _context.CreateConnection();
            var param = new { id };
            var resultado = await connection.QuerySingleOrDefaultAsync<ReadArchivoEntidad>(query, param, commandType: CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<List<ReadArchivoEntidad>> GetAllArchivoAsync()
        {
            var query = "consolaMonitoreo..CodeGenerator_Archivo_GetAll";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<ReadArchivoEntidad>(query, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<List<ReadArchivoEntidad>> UpdateArchivoAsync(UpdateArchivoEntidad dto)
        {
            var query = "consolaMonitoreo..CodeGenerator_Archivo_Update";
            var connection = _context.CreateConnection();
            var param = new { dto.Id, dto.IdFolder, dto.Extension, dto.Documentacion, dto.Contenido, dto.Path };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return await GetAllArchivoAsync();
        }

        public async Task<bool> DeleteArchivoAsync(int id)
        {
            var query = "consolaMonitoreo..CodeGenerator_Archivo_Delete";
            var connection = _context.CreateConnection();
            var param = new { id };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return true;
        }
    }
}
