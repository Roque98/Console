using Dapper;
using FolderView.Dapper.Entidades;
using FolderView.Dapper.Interfaces;
using System.Data;

namespace FolderView.Dapper.Repositorios
{
    public class PromptTemplateRepositorio : IPromptTemplateRepository
    {
        private readonly DapperContext _context;
        public PromptTemplateRepositorio(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<PromptTemplateEntidad>> CreateAsync(PromptTemplateEntidad dto)
        {
            var query = "consolaMonitoreo..[CodeGenerator_PromptTemplate_Add]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<PromptTemplateEntidad>(query, new { dto.Prompt, dto.Orden, dto.IdTipoProyecto }, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<PromptTemplateEntidad> GetByIdAsync(int id)
        {
            var query = "consolaMonitoreo..[CodeGenerator_PromptTemplate_GetById]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QuerySingleOrDefaultAsync<PromptTemplateEntidad>(query, new { id }, commandType: CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<PromptTemplateEntidad> UpdateAsync(PromptTemplateEntidad dto)
        {
            var query = "consolaMonitoreo..[CodeGenerator_PromptTemplate_Update]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QuerySingleOrDefaultAsync<PromptTemplateEntidad>(query, new { dto.Id, dto.Prompt, dto.Orden, dto.IdTipoProyecto }, commandType: CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<PromptTemplateEntidad> DeleteAsync(int id)
        {
            var query = "consolaMonitoreo..[CodeGenerator_PromptTemplate_Delete]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QuerySingleOrDefaultAsync<PromptTemplateEntidad>(query, new { id }, commandType: CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<List<PromptTemplateEntidad>> GetAllByIdTipoProyectoAsync(int idTipoProyecto)
        {
            var query = "consolaMonitoreo..[CodeGenerator_PromptTemplate_GetAllByTipoProyecto]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<PromptTemplateEntidad>(query, new { idTipoProyecto }, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }
    }

}
