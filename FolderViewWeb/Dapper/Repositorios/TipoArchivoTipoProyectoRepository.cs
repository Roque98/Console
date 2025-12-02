//using Dapper;
//using FolderView.Dapper.Entidades;
//using FolderView.Dapper.Interfaces;
//using System.Data;

//namespace FolderView.Dapper.Repositorios
//{
//    public class TipoArchivoTipoProyectoRepository: ITipoArchivoTipoProyectoRepository
//    {

//        private readonly DapperContext _context;
//        private readonly ITipoArchivoRepository _TipoArchivoRepository;
//        private readonly IPromptsRepository _promptsRepository;

//        public TipoArchivoTipoProyectoRepository(
//            DapperContext context,
//            ITipoArchivoRepository tipoArchivoRepository,
//            IPromptsRepository promptsRepository
//        )
//        {
//            _context = context;
//            _TipoArchivoRepository = tipoArchivoRepository;
//            _promptsRepository = promptsRepository;
//        }

//        public async Task<List<ReadTipoArchivo_TipoProyectoEntidad>> CreateTipoArchivoTipoProyectoAsync(CreateTipoArchivo_TipoProyectoEntidad dto)
//        {
//            var query = "consolaMonitoreo..CodeGenerator_TipoArchivo_TipoProyecto_Add";
//            var connection = _context.CreateConnection();
//            var param = new { dto.IdTipoProyecto, dto.IdTipoArchivo, dto.IdPadre };
//            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
//            return await GetAllTipoArchivoTipoProyectoAsync();
//        }

//        public async Task<ReadTipoArchivo_TipoProyectoEntidad> GetTipoArchivoTipoProyectoByIdAsync(int id)
//        {
//            var query = "consolaMonitoreo..CodeGenerator_TipoArchivo_TipoProyecto_GetById";
//            var connection = _context.CreateConnection();
//            var param = new { id };
//            var resultado = await connection.QuerySingleOrDefaultAsync<ReadTipoArchivo_TipoProyectoEntidad>(query, param, commandType: CommandType.StoredProcedure);
//            return resultado;
//        }

//        public async Task<List<ReadTipoArchivo_TipoProyectoEntidad>> GetAllTipoArchivoTipoProyectoAsync()
//        {
//            var query = "consolaMonitoreo..CodeGenerator_TipoArchivo_TipoProyecto_GetAll";
//            var connection = _context.CreateConnection();
//            var resultado = await connection.QueryAsync<ReadTipoArchivo_TipoProyectoEntidad>(query, commandType: CommandType.StoredProcedure);
//            return resultado.ToList();
//        }

//        public async Task<List<ReadTipoArchivo_TipoProyectoEntidad>> GetAllByIdTipoProyectoTipoArchivoTipoProyectoAsync(int idTipoProyecto)
//        {
//            var query = "consolaMonitoreo..CodeGenerator_TipoArchivo_TipoProyecto_GetAllByIdTipoProyecto";
//            var connection = _context.CreateConnection();
//            var param = new { idTipoProyecto };
//            var resultado = await connection.QueryAsync<ReadTipoArchivo_TipoProyectoEntidad>(query, param, commandType: CommandType.StoredProcedure);

//            if (resultado != null)
//            {

//                foreach (var item in resultado)
//                {
//                    item.TipoArchivo = await _TipoArchivoRepository.GetTipoArchivosByIdAsync(item.IdTipoArchivo);
//                    var resultPromtps = await _promptsRepository.GetAllPromptsByIdTipoArchivoTipoProyectoAsync(item.Id);
//                    item.promptEntidad = resultPromtps.FirstOrDefault();
//                }
//            }

//            return resultado.ToList();
//        }

//        public async Task<List<ReadTipoArchivo_TipoProyectoEntidad>> UpdateTipoArchivoTipoProyectoAsync(UpdateTipoArchivo_TipoProyectoEntidad dto)
//        {
//            var query = "consolaMonitoreo..CodeGenerator_TipoArchivo_TipoProyecto_Update";
//            var connection = _context.CreateConnection();
//            var param = new { dto.Id, dto.IdTipoProyecto, dto.IdTipoArchivo, dto.IdPadre };
//            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
//            return await GetAllTipoArchivoTipoProyectoAsync();
//        }

//        public async Task<bool> DeleteTipoArchivoTipoProyectoAsync(int id)
//        {
//            var query = "consolaMonitoreo..CodeGenerator_TipoArchivo_TipoProyecto_Delete";
//            var connection = _context.CreateConnection();
//            var param = new { id };
//            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
//            return true;
//        }

       
//    }
//}
