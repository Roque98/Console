using Dapper;
using FolderView.Dapper.Entidades;
using FolderView.Dapper.Interfaces;
using System.Data;

namespace FolderView.Dapper.Repositorios
{
    public class CodeGeneratorRepository : ICodeGeneratorRepository
    {
        private readonly DapperContext _context;
        public CodeGeneratorRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<CreateTipoProyectoEntidad>> CreateTipoProyectoAsync(CreateTipoProyectoEntidad dto)
        {
            var query = $"consolaMonitoreo..[FolderView_CreateTipoProyecto]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<CreateTipoProyectoEntidad>(query, dto, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<List<ReadTipoProyectoEntidad>> GetTipoProyectoByIdAsync(int id)
        {
            var query = $"consolaMonitoreo..[FolderView_GetTipoProyectoById]";
            var connection = _context.CreateConnection();
            var param = new { Id = id };
            var resultado = await connection.QueryAsync<ReadTipoProyectoEntidad>(query, param, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<List<UpdateTipoProyectoEntidad>> UpdateTipoProyectoAsync(UpdateTipoProyectoEntidad dto)
        {
            var query = $"consolaMonitoreo..[FolderView_UpdateTipoProyecto]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<UpdateTipoProyectoEntidad>(query, dto, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<bool> DeleteTipoProyectoAsync(int id)
        {
            var query = $"consolaMonitoreo..[FolderView_DeleteTipoProyecto]";
            var connection = _context.CreateConnection();
            var param = new { Id = id };
            var resultado = await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return resultado > 0;
        }

        public async Task<List<CreateTipoArchivosEntidad>> CreateTipoArchivosAsync(CreateTipoArchivosEntidad dto)
        {
            var query = $"consolaMonitoreo..[FolderView_CreateTipoArchivos]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<CreateTipoArchivosEntidad>(query, dto, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<List<ReadTipoArchivosEntidad>> GetTipoArchivosByIdAsync(int id)
        {
            var query = $"consolaMonitoreo..[FolderView_GetTipoArchivosById]";
            var connection = _context.CreateConnection();
            var param = new { Id = id };
            var resultado = await connection.QueryAsync<ReadTipoArchivosEntidad>(query, param, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<List<UpdateTipoArchivosEntidad>> UpdateTipoArchivosAsync(UpdateTipoArchivosEntidad dto)
        {
            var query = $"consolaMonitoreo..[FolderView_UpdateTipoArchivos]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<UpdateTipoArchivosEntidad>(query, dto, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<bool> DeleteTipoArchivosAsync(int id)
        {
            var query = $"consolaMonitoreo..[FolderView_DeleteTipoArchivos]";
            var connection = _context.CreateConnection();
            var param = new { Id = id };
            var resultado = await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return resultado > 0;
        }

        public async Task<List<CreateTipoArchivoTipoProyectoEntidad>> CreateTipoArchivoTipoProyectoAsync(CreateTipoArchivoTipoProyectoEntidad dto)
        {
            var query = $"consolaMonitoreo..[FolderView_CreateTipoArchivoTipoProyecto]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<CreateTipoArchivoTipoProyectoEntidad>(query, dto, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<List<ReadTipoArchivoTipoProyectoEntidad>> GetTipoArchivoTipoProyectoByIdAsync(int id)
        {
            var query = $"consolaMonitoreo..[FolderView_GetTipoArchivoTipoProyectoById]";
            var connection = _context.CreateConnection();
            var param = new { Id = id };
            var resultado = await connection.QueryAsync<ReadTipoArchivoTipoProyectoEntidad>(query, param, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<List<UpdateTipoArchivoTipoProyectoEntidad>> UpdateTipoArchivoTipoProyectoAsync(UpdateTipoArchivoTipoProyectoEntidad dto)
        {
            var query = $"consolaMonitoreo..[FolderView_UpdateTipoArchivoTipoProyecto]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<UpdateTipoArchivoTipoProyectoEntidad>(query, dto, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<bool> DeleteTipoArchivoTipoProyectoAsync(int id)
        {
            var query = $"consolaMonitoreo..[FolderView_DeleteTipoArchivoTipoProyecto]";
            var connection = _context.CreateConnection();
            var param = new { Id = id };
            var resultado = await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return resultado > 0;
        }

        public async Task<List<CreateProyectoEntidad>> CreateProyectoAsync(CreateProyectoEntidad dto)
        {
            var query = $"consolaMonitoreo..[FolderView_CreateProyecto]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<CreateProyectoEntidad>(query, dto, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<List<ReadProyectoEntidad>> GetProyectoByIdAsync(int id)
        {
            var query = $"consolaMonitoreo..[FolderView_GetProyectoById]";
            var connection = _context.CreateConnection();
            var param = new { Id = id };
            var resultado = await connection.QueryAsync<ReadProyectoEntidad>(query, param, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<List<UpdateProyectoEntidad>> UpdateProyectoAsync(UpdateProyectoEntidad dto)
        {
            var query = $"consolaMonitoreo..[FolderView_UpdateProyecto]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<UpdateProyectoEntidad>(query, dto, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<bool> DeleteProyectoAsync(int id)
        {
            var query = $"consolaMonitoreo..[FolderView_DeleteProyecto]";
            var connection = _context.CreateConnection();
            var param = new { Id = id };
            var resultado = await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return resultado > 0;
        }

        public async Task<List<CreateFolderEntidad>> CreateFolderAsync(CreateFolderEntidad dto)
        {
            var query = $"consolaMonitoreo..[FolderView_CreateFolder]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<CreateFolderEntidad>(query, dto, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<List<ReadFolderEntidad>> GetFolderByIdAsync(int id)
        {
            var query = $"consolaMonitoreo..[FolderView_GetFolderById]";
            var connection = _context.CreateConnection();
            var param = new { Id = id };
            var resultado = await connection.QueryAsync<ReadFolderEntidad>(query, param, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<List<UpdateFolderEntidad>> UpdateFolderAsync(UpdateFolderEntidad dto)
        {
            var query = $"consolaMonitoreo..[FolderView_UpdateFolder]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<UpdateFolderEntidad>(query, dto, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<bool> DeleteFolderAsync(int id)
        {
            var query = $"consolaMonitoreo..[FolderView_DeleteFolder]";
            var connection = _context.CreateConnection();
            var param = new { Id = id };
            var resultado = await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return resultado > 0;
        }

        public async Task<List<CreateArchivoEntidad>> CreateArchivoAsync(CreateArchivoEntidad dto)
        {
            var query = $"consolaMonitoreo..[FolderView_CreateArchivo]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<CreateArchivoEntidad>(query, dto, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<List<ReadArchivoEntidad>> GetArchivoByIdAsync(int id)
        {
            var query = $"consolaMonitoreo..[FolderView_GetArchivoById]";
            var connection = _context.CreateConnection();
            var param = new { Id = id };
            var resultado = await connection.QueryAsync<ReadArchivoEntidad>(query, param, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<List<UpdateArchivoEntidad>> UpdateArchivoAsync(UpdateArchivoEntidad dto)
        {
            var query = $"consolaMonitoreo..[FolderView_UpdateArchivo]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<UpdateArchivoEntidad>(query, dto, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<bool> DeleteArchivoAsync(int id)
        {
            var query = $"consolaMonitoreo..[FolderView_DeleteArchivo]";
            var connection = _context.CreateConnection();
            var param = new { Id = id };
            var resultado = await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return resultado > 0;
        }

        public async Task<List<ReadTipoArchivosEntidad>> GetAllTipoArchivos()
        {
            var query = $"consolaMonitoreo..[CodeGenerator_TipoProyecto_GetAll]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<ReadTipoArchivosEntidad>(query, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }
    }
}
