//using Dapper;
//using FolderView.Dapper.Entidades;
//using FolderView.Dapper.Interfaces;
//using System.Data;

//namespace FolderView.Dapper.Repositorios
//{
//    public class ProyectoRepository: IProyectoRepository
//    {
//        private readonly DapperContext _context;
//        private readonly ITipoProyectoRepository _tipoProyectoRepository;

//        public ProyectoRepository(
//            DapperContext context,
//            ITipoProyectoRepository tipoProyectoRepository
//        )
//        {
//            _context = context;
//            _tipoProyectoRepository = tipoProyectoRepository;
//        }

//        public async Task<List<ReadProyectoEntidad>> CreateProyectoAsync(CreateProyectoEntidad dto)
//        {
//            var query = "consolaMonitoreo..CodeGenerator_Proyecto_Add";
//            var connection = _context.CreateConnection();
//            var param = new { dto.IdTipoProyecto, dto.NombreProyecto };
//            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
//            return await GetAllProyectoAsync();
//        }

//        public async Task<ReadProyectoEntidad> GetProyectoByIdAsync(int id)
//        {
//            var query = "consolaMonitoreo..CodeGenerator_Proyecto_GetById";
//            var connection = _context.CreateConnection();
//            var param = new { id };
//            var resultado = await connection.QuerySingleOrDefaultAsync<ReadProyectoEntidad>(query, param, commandType: CommandType.StoredProcedure);

//            if (resultado != null)
//            {
//                resultado.TipoProyecto = await _tipoProyectoRepository.GetTipoProyectoByIdAsync(resultado.IdTipoProyecto);
//            }

//            return resultado;
//        }

//        public async Task<List<ReadProyectoEntidad>> GetAllProyectoAsync()
//        {
//            var query = "consolaMonitoreo..CodeGenerator_Proyecto_GetAll";
//            var connection = _context.CreateConnection();
//            var resultado = await connection.QueryAsync<ReadProyectoEntidad>(query, commandType: CommandType.StoredProcedure);
//            return resultado.ToList();
//        }

//        public async Task<List<ReadProyectoEntidad>> UpdateProyectoAsync(UpdateProyectoEntidad dto)
//        {
//            var query = "consolaMonitoreo..CodeGenerator_Proyecto_Update";
//            var connection = _context.CreateConnection();
//            var param = new { dto.Id, dto.IdTipoProyecto, dto.NombreProyecto };
//            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
//            return await GetAllProyectoAsync();
//        }

//        public async Task<bool> DeleteProyectoAsync(int id)
//        {
//            var query = "consolaMonitoreo..CodeGenerator_Proyecto_Delete";
//            var connection = _context.CreateConnection();
//            var param = new { id };
//            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
//            return true;
//        }

//    }
//}
