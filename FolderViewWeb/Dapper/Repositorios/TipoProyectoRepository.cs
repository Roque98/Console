//using Dapper;
//using FolderView.Dapper.Entidades;
//using FolderView.Dapper.Interfaces;
//using System.Data;

//namespace FolderView.Dapper.Repositorios
//{
//    public class TipoProyectoRepository: ITipoProyectoRepository
//    {
//        private readonly DapperContext _context;
//        private readonly ITipoArchivoTipoProyectoRepository _tipoArchivoTipoProyectoRepository;
//        public TipoProyectoRepository(
//            DapperContext context,
//            ITipoArchivoTipoProyectoRepository tipoArchivoTipoProyectoRepository
//        )
//        {
//            _context = context;
//            _tipoArchivoTipoProyectoRepository = tipoArchivoTipoProyectoRepository;
//        }

//        public async Task<List<ReadTipoProyectoEntidad>> CreateTipoProyectoAsync(CreateTipoProyectoEntidad dto)
//        {
//            var query = "consolaMonitoreo..CodeGenerator_TipoProyecto_Add";
//            var connection = _context.CreateConnection();
//            var param = new { dto.Nombre, dto.Descripcion, dto.UrlImagen };
//            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
//            return await GetAllTipoProyectoAsync();
//        }

//        public async Task<ReadTipoProyectoEntidad> GetTipoProyectoByIdAsync(int id)
//        {
//            var query = "consolaMonitoreo..CodeGenerator_TipoProyecto_GetById";
//            var connection = _context.CreateConnection();
//            var param = new { id };
//            var resultado = await connection.QuerySingleOrDefaultAsync<ReadTipoProyectoEntidad>(query, param, commandType: CommandType.StoredProcedure);

//            if (resultado != null)
//            {
//                resultado.TiposArchivosRelacion.AddRange(
//                    await _tipoArchivoTipoProyectoRepository.GetAllByIdTipoProyectoTipoArchivoTipoProyectoAsync(resultado.Id)
//                );
//            }

//            return resultado;
//        }

//        public async Task<List<ReadTipoProyectoEntidad>> GetAllTipoProyectoAsync()
//        {
//            var query = "consolaMonitoreo..CodeGenerator_TipoProyecto_GetAll";
//            var connection = _context.CreateConnection();
//            var resultado = await connection.QueryAsync<ReadTipoProyectoEntidad>(query, commandType: CommandType.StoredProcedure);
//            return resultado.ToList();
//        }

//        public async Task<List<ReadTipoProyectoEntidad>> UpdateTipoProyectoAsync(UpdateTipoProyectoEntidad dto)
//        {
//            var query = "consolaMonitoreo..CodeGenerator_TipoProyecto_Update";
//            var connection = _context.CreateConnection();
//            var param = new { dto.Id, dto.Nombre, dto.Descripcion, dto.UrlImagen };
//            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
//            return await GetAllTipoProyectoAsync();
//        }

//        public async Task<bool> DeleteTipoProyectoAsync(int id)
//        {
//            var query = "consolaMonitoreo..CodeGenerator_TipoProyecto_Delete";
//            var connection = _context.CreateConnection();
//            var param = new { id };
//            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
//            return true;
//        }
//    }
//}
