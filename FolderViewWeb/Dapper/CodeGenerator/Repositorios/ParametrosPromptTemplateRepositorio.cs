using Dapper;
using FolderView.Dapper.Entidades;
using FolderView.Dapper.Interfaces;
using System.Data;

namespace FolderView.Dapper.CodeGenerator.Repositorios
{
    public class ParametrosPromptTemplateRepositorio : IParametrosPromptTemplateRepository
    {
        private readonly DapperContext _context;
        public ParametrosPromptTemplateRepositorio(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<ParametrosPromptTemplateEntidad>> CreateAsync(ParametrosPromptTemplateEntidad dto)
        {
            var query = "consolaMonitoreo..[CodeGenerator_ParametrosPromptTemplate_Add]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<ParametrosPromptTemplateEntidad>(query, new { dto.nombre_parametro, dto.IdPromptTemplate, dto.IdPromptTemplateEntrada, dto.label, dto.placeholder }, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<ParametrosPromptTemplateEntidad> GetByIdAsync(int id)
        {
            var query = "consolaMonitoreo..[CodeGenerator_ParametrosPromptTemplate_GetById]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QuerySingleOrDefaultAsync<ParametrosPromptTemplateEntidad>(query, new { id }, commandType: CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<ParametrosPromptTemplateEntidad> UpdateAsync(ParametrosPromptTemplateEntidad dto)
        {
            var query = "consolaMonitoreo..[CodeGenerator_ParametrosPromptTemplate_Update]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QuerySingleOrDefaultAsync<ParametrosPromptTemplateEntidad>(query, new { dto.Id, dto.nombre_parametro, dto.IdPromptTemplate, dto.IdPromptTemplateEntrada, dto.label, dto.placeholder }, commandType: CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<ParametrosPromptTemplateEntidad> DeleteAsync(int id)
        {
            var query = "consolaMonitoreo..[CodeGenerator_ParametrosPromptTemplate_Delete]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QuerySingleOrDefaultAsync<ParametrosPromptTemplateEntidad>(query, new { id }, commandType: CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<List<ParametrosPromptTemplateEntidad>> GetAllByIdPromptTemplateAsync(int idPromptTemplate)
        {
            var query = "consolaMonitoreo..[CodeGenerator_ParametrosPromptTemplate_GetAllByPromptTemplate]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<ParametrosPromptTemplateEntidad>(query, new { idPromptTemplate }, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }
    }
}
