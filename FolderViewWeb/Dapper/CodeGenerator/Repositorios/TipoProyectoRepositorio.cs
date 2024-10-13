using Dapper;
using FolderView.Dapper.Entidades;
using FolderView.Dapper.Interfaces;
using System.Data;

namespace FolderView.Dapper.Repositorios
{
    public class TipoProyectoRepositorio : ITipoProyectoRepository
    {
        private readonly DapperContext _context;
        public TipoProyectoRepositorio(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<TipoProyectoEntidad>> CreateAsync(TipoProyectoEntidad dto)
        {
            var query = "consolaMonitoreo..[CodeGenerator_Tipo_proyecto_Add]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<TipoProyectoEntidad>(query, new { dto.nombre, dto.descripcion, dto.urlImagen }, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<TipoProyectoEntidad> GetByIdAsync(int id)
        {
            var query = "consolaMonitoreo..[CodeGenerator_Tipo_proyecto_GetById]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QuerySingleOrDefaultAsync<TipoProyectoEntidad>(query, new { id }, commandType: CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<TipoProyectoEntidad> UpdateAsync(TipoProyectoEntidad dto)
        {
            var query = "consolaMonitoreo..[CodeGenerator_Tipo_proyecto_Update]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QuerySingleOrDefaultAsync<TipoProyectoEntidad>(query, new { dto.id, dto.nombre, dto.descripcion, dto.urlImagen }, commandType: CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<TipoProyectoEntidad> DeleteAsync(int id)
        {
            var query = "consolaMonitoreo..[CodeGenerator_Tipo_proyecto_Delete]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QuerySingleOrDefaultAsync<TipoProyectoEntidad>(query, new { id }, commandType: CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<List<TipoProyectoEntidad>> GetAllAsync()
        {
            var query = "consolaMonitoreo..[CodeGenerator_Tipo_proyecto_GetAll]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<TipoProyectoEntidad>(query, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }
    }

}
