using Dapper;
using FolderView.Dapper.Entidades;
using FolderView.Dapper.Interfaces;
using Microsoft.Identity.Client;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FolderView.Dapper.Repositorios
{
    public class PromptsRepositorio : IPromptsRepository
    {
        private readonly DapperContext _context;
        private readonly IInputPromptRepository _inputPromptRepository;
        private readonly ITipoArchivoTipoProyectoRepository _ITipoArchivoTipoProyectoRepository;

        public PromptsRepositorio(
            DapperContext context
            , IInputPromptRepository inputPromptRepository
            , ITipoArchivoTipoProyectoRepository iTipoArchivoTipoProyectoRepository

            )
        {
            _context = context;
            _inputPromptRepository = inputPromptRepository;
            _ITipoArchivoTipoProyectoRepository = iTipoArchivoTipoProyectoRepository;
        }

        public async Task<List<PromptsEntidad>> GetAllPromptsAsync()
        {
            var query = "CodeGenerator_Prompts_GetAll";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<PromptsEntidad>(query, commandType: CommandType.StoredProcedure);

            foreach (var prompt in resultado)
            {
                prompt.TipoArchivoTipoProyecto = await _ITipoArchivoTipoProyectoRepository.GetTipoArchivoTipoProyectoByIdAsync(prompt.IdTipoArchivoTipoProyecto);
                prompt.InputsPrompt = await _inputPromptRepository.GetAllInputPromptsByIdPromptAsync(prompt.Id);
            }

            return resultado.ToList();
        }

        public async Task<PromptsEntidad> GetPromptByIdAsync(int id)
        {
            var query = "CodeGenerator_Prompts_GetById";
            var connection = _context.CreateConnection();
            var param = new { id };
            var resultado = await connection.QuerySingleOrDefaultAsync<PromptsEntidad>(query, param, commandType: CommandType.StoredProcedure);

            if (resultado != null)
            {
                resultado.TipoArchivoTipoProyecto = await _ITipoArchivoTipoProyectoRepository.GetTipoArchivoTipoProyectoByIdAsync(resultado.IdTipoArchivoTipoProyecto);
                resultado.InputsPrompt = await _inputPromptRepository.GetAllInputPromptsByIdPromptAsync(resultado.Id);
            }
            return resultado;
        }

        public async Task<List<PromptsEntidad>> CreatePromptAsync(PromptsEntidad dto)
        {
            var query = "CodeGenerator_Prompts_Add";
            var connection = _context.CreateConnection();
            var param = new
            {
                dto.Prompt,
                dto.IdTipoArchivoTipoProyecto
            };
            var resultado = await connection.QueryAsync<PromptsEntidad>(query, param, commandType: CommandType.StoredProcedure);

            return resultado.ToList();
        }

        public async Task<List<PromptsEntidad>> UpdatePromptAsync(PromptsEntidad dto)
        {
            var query = "CodeGenerator_Prompts_Update";
            var connection = _context.CreateConnection();
            var param = new
            {
                dto.Id,
                dto.Prompt,
                dto.IdTipoArchivoTipoProyecto
            };
            var resultado = await connection.QueryAsync<PromptsEntidad>(query, param, commandType: CommandType.StoredProcedure);

            return resultado.ToList();
        }

        public async Task<bool> DeletePromptAsync(int id)
        {
            var query = "CodeGenerator_Prompts_Delete";
            var connection = _context.CreateConnection();
            var param = new { id };
            var resultado = await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return resultado > 0;
        }

        public async Task<List<PromptsEntidad>> GetAllPromptsByIdTipoArchivoTipoProyectoAsync(int idTipoArchivoTipoProyecto)
        {
            var query = "CodeGenerator_Prompts_GetAllByIdTipoArchivoTipoProyecto";
            var connection = _context.CreateConnection();
            var param = new { idTipoArchivoTipoProyecto };
            var resultado = await connection.QueryAsync<PromptsEntidad>(query, param, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }
    }

    
}
