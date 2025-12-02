//using Dapper;
//using FolderView.Dapper.Entidades;
//using FolderView.Dapper.Interfaces;
//using System.Data;

//namespace FolderView.Dapper.Repositorios
//{
//    public class TipoArchivoRepository: ITipoArchivoRepository
//    {
//        private readonly DapperContext _context;

//        public TipoArchivoRepository(
//            DapperContext context
//        )
//        {
//            _context = context;
//        }

//        public async Task<List<ReadTipoArchivosEntidad>> CreateTipoArchivosAsync(CreateTipoArchivosEntidad dto)
//        {
//            var query = "consolaMonitoreo..CodeGenerator_TipoArchivos_Add";
//            var connection = _context.CreateConnection();
//            var param = new { dto.Nombre, dto.Descripcion };
//            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
//            return await GetAllTipoArchivosAsync();
//        }

//        public async Task<ReadTipoArchivosEntidad> GetTipoArchivosByIdAsync(int id)
//        {
//            var query = "consolaMonitoreo..CodeGenerator_TipoArchivos_GetById";
//            var connection = _context.CreateConnection();
//            var param = new { id };
//            var resultado = await connection.QuerySingleOrDefaultAsync<ReadTipoArchivosEntidad>(query, param, commandType: CommandType.StoredProcedure);
//            return resultado;
//        }

//        public async Task<List<ReadTipoArchivosEntidad>> GetAllTipoArchivosAsync()
//        {
//            var query = "consolaMonitoreo..CodeGenerator_TipoArchivos_GetAll";
//            var connection = _context.CreateConnection();
//            var resultado = await connection.QueryAsync<ReadTipoArchivosEntidad>(query, commandType: CommandType.StoredProcedure);
//            return resultado.ToList();
//        }

//        public async Task<List<ReadTipoArchivosEntidad>> UpdateTipoArchivosAsync(UpdateTipoArchivosEntidad dto)
//        {
//            var query = "consolaMonitoreo..CodeGenerator_TipoArchivos_Update";
//            var connection = _context.CreateConnection();
//            var param = new { dto.Id, dto.Nombre, dto.Descripcion };
//            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
//            return await GetAllTipoArchivosAsync();
//        }

//        public async Task<bool> DeleteTipoArchivosAsync(int id)
//        {
//            var query = "consolaMonitoreo..CodeGenerator_TipoArchivos_Delete";
//            var connection = _context.CreateConnection();
//            var param = new { id };
//            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
//            return true;
//        }
//    }
//}
