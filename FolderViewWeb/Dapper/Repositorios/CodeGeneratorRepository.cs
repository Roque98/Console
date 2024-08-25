using Dapper;
using FolderView.Dapper.Entidades;
using FolderView.Dapper.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FolderView.Dapper.Repositorios
{
    public class CodeGeneratorRepository : ICodeGeneratorRepository
    {
        private readonly DapperContext _context;
        public CodeGeneratorRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<ReadTipoProyectoEntidad>> CreateTipoProyectoAsync(CreateTipoProyectoEntidad dto)
        {
            var query = "consolaMonitoreo..CodeGenerator_TipoProyecto_Add";
            var connection = _context.CreateConnection();
            var param = new { dto.Nombre, dto.Descripcion, dto.UrlImagen };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return await GetAllTipoProyectoAsync();
        }

        public async Task<ReadTipoProyectoEntidad> GetTipoProyectoByIdAsync(int id)
        {
            var query = "consolaMonitoreo..CodeGenerator_TipoProyecto_GetById";
            var connection = _context.CreateConnection();
            var param = new { id };
            var resultado = await connection.QuerySingleOrDefaultAsync<ReadTipoProyectoEntidad>(query, param, commandType: CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<List<ReadTipoProyectoEntidad>> GetAllTipoProyectoAsync()
        {
            var query = "consolaMonitoreo..CodeGenerator_TipoProyecto_GetAll";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<ReadTipoProyectoEntidad>(query, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<List<ReadTipoProyectoEntidad>> UpdateTipoProyectoAsync(UpdateTipoProyectoEntidad dto)
        {
            var query = "consolaMonitoreo..CodeGenerator_TipoProyecto_Update";
            var connection = _context.CreateConnection();
            var param = new { dto.Id, dto.Nombre, dto.Descripcion, dto.UrlImagen };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return await GetAllTipoProyectoAsync();
        }

        public async Task<bool> DeleteTipoProyectoAsync(int id)
        {
            var query = "consolaMonitoreo..CodeGenerator_TipoProyecto_Delete";
            var connection = _context.CreateConnection();
            var param = new { id };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return true;
        }

        public async Task<List<ReadTipoArchivosEntidad>> CreateTipoArchivosAsync(CreateTipoArchivosEntidad dto)
        {
            var query = "consolaMonitoreo..CodeGenerator_TipoArchivos_Add";
            var connection = _context.CreateConnection();
            var param = new { dto.Nombre, dto.Descripcion };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return await GetAllTipoArchivosAsync();
        }

        public async Task<ReadTipoArchivosEntidad> GetTipoArchivosByIdAsync(int id)
        {
            var query = "consolaMonitoreo..CodeGenerator_TipoArchivos_GetById";
            var connection = _context.CreateConnection();
            var param = new { id };
            var resultado = await connection.QuerySingleOrDefaultAsync<ReadTipoArchivosEntidad>(query, param, commandType: CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<List<ReadTipoArchivosEntidad>> GetAllTipoArchivosAsync()
        {
            var query = "consolaMonitoreo..CodeGenerator_TipoArchivos_GetAll";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<ReadTipoArchivosEntidad>(query, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<List<ReadTipoArchivosEntidad>> UpdateTipoArchivosAsync(UpdateTipoArchivosEntidad dto)
        {
            var query = "consolaMonitoreo..CodeGenerator_TipoArchivos_Update";
            var connection = _context.CreateConnection();
            var param = new { dto.Id, dto.Nombre, dto.Descripcion };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return await GetAllTipoArchivosAsync();
        }

        public async Task<bool> DeleteTipoArchivosAsync(int id)
        {
            var query = "consolaMonitoreo..CodeGenerator_TipoArchivos_Delete";
            var connection = _context.CreateConnection();
            var param = new { id };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return true;
        }

        public async Task<List<ReadTipoArchivoTipoProyectoEntidad>> CreateTipoArchivoTipoProyectoAsync(CreateTipoArchivoTipoProyectoEntidad dto)
        {
            var query = "consolaMonitoreo..CodeGenerator_TipoArchivo_TipoProyecto_Add";
            var connection = _context.CreateConnection();
            var param = new { dto.IdTipoProyecto, dto.IdTipoArchivo, dto.IdPadre };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return await GetAllTipoArchivoTipoProyectoAsync();
        }

        public async Task<ReadTipoArchivoTipoProyectoEntidad> GetTipoArchivoTipoProyectoByIdAsync(int id)
        {
            var query = "consolaMonitoreo..CodeGenerator_TipoArchivo_TipoProyecto_GetById";
            var connection = _context.CreateConnection();
            var param = new { id };
            var resultado = await connection.QuerySingleOrDefaultAsync<ReadTipoArchivoTipoProyectoEntidad>(query, param, commandType: CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<List<ReadTipoArchivoTipoProyectoEntidad>> GetAllTipoArchivoTipoProyectoAsync()
        {
            var query = "consolaMonitoreo..CodeGenerator_TipoArchivo_TipoProyecto_GetAll";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<ReadTipoArchivoTipoProyectoEntidad>(query, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<List<ReadTipoArchivoTipoProyectoEntidad>> UpdateTipoArchivoTipoProyectoAsync(UpdateTipoArchivoTipoProyectoEntidad dto)
        {
            var query = "consolaMonitoreo..CodeGenerator_TipoArchivo_TipoProyecto_Update";
            var connection = _context.CreateConnection();
            var param = new { dto.Id, dto.IdTipoProyecto, dto.IdTipoArchivo, dto.IdPadre };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return await GetAllTipoArchivoTipoProyectoAsync();
        }

        public async Task<bool> DeleteTipoArchivoTipoProyectoAsync(int id)
        {
            var query = "consolaMonitoreo..CodeGenerator_TipoArchivo_TipoProyecto_Delete";
            var connection = _context.CreateConnection();
            var param = new { id };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return true;
        }

        public async Task<List<ReadProyectoEntidad>> CreateProyectoAsync(CreateProyectoEntidad dto)
        {
            var query = "consolaMonitoreo..CodeGenerator_Proyecto_Add";
            var connection = _context.CreateConnection();
            var param = new { dto.IdTipoProyecto, dto.NombreProyecto };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return await GetAllProyectoAsync();
        }

        public async Task<ReadProyectoEntidad> GetProyectoByIdAsync(int id)
        {
            var query = "consolaMonitoreo..CodeGenerator_Proyecto_GetById";
            var connection = _context.CreateConnection();
            var param = new { id };
            var resultado = await connection.QuerySingleOrDefaultAsync<ReadProyectoEntidad>(query, param, commandType: CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<List<ReadProyectoEntidad>> GetAllProyectoAsync()
        {
            var query = "consolaMonitoreo..CodeGenerator_Proyecto_GetAll";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<ReadProyectoEntidad>(query, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<List<ReadProyectoEntidad>> UpdateProyectoAsync(UpdateProyectoEntidad dto)
        {
            var query = "consolaMonitoreo..CodeGenerator_Proyecto_Update";
            var connection = _context.CreateConnection();
            var param = new { dto.Id, dto.IdTipoProyecto, dto.NombreProyecto };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return await GetAllProyectoAsync();
        }

        public async Task<bool> DeleteProyectoAsync(int id)
        {
            var query = "consolaMonitoreo..CodeGenerator_Proyecto_Delete";
            var connection = _context.CreateConnection();
            var param = new { id };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return true;
        }

        public async Task<List<ReadFolderEntidad>> CreateFolderAsync(CreateFolderEntidad dto)
        {
            var query = "consolaMonitoreo..CodeGenerator_Folder_Add";
            var connection = _context.CreateConnection();
            var param = new { dto.IdTipoArchivo, dto.IdProyecto, dto.NumeroVersion, dto.IdFolderPadre };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return await GetAllFolderAsync();
        }

        public async Task<ReadFolderEntidad> GetFolderByIdAsync(int id)
        {
            var query = "consolaMonitoreo..CodeGenerator_Folder_GetById";
            var connection = _context.CreateConnection();
            var param = new { id };
            var resultado = await connection.QuerySingleOrDefaultAsync<ReadFolderEntidad>(query, param, commandType: CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<List<ReadFolderEntidad>> GetAllFolderAsync()
        {
            var query = "consolaMonitoreo..CodeGenerator_Folder_GetAll";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<ReadFolderEntidad>(query, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<List<ReadFolderEntidad>> UpdateFolderAsync(UpdateFolderEntidad dto)
        {
            var query = "consolaMonitoreo..CodeGenerator_Folder_Update";
            var connection = _context.CreateConnection();
            var param = new { dto.Id, dto.IdTipoArchivo, dto.IdProyecto, dto.NumeroVersion, dto.IdFolderPadre };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return await GetAllFolderAsync();
        }

        public async Task<bool> DeleteFolderAsync(int id)
        {
            var query = "consolaMonitoreo..CodeGenerator_Folder_Delete";
            var connection = _context.CreateConnection();
            var param = new { id };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return true;
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