using Dapper;
using FolderView.Dapper.Entidades;
using FolderView.Dapper.Interfaces;
using System.Data;

namespace FolderView.Dapper.Repositorios
{
    public class InputPromptRepositorio : IInputPromptRepository
    {
        private readonly DapperContext _context;
        private readonly ITipoArchivoRepository _tipoArchivoRepository;

        public InputPromptRepositorio(
            DapperContext context,
            ITipoArchivoRepository tipoArchivoRepository
        )
        {
            _context = context;
            _tipoArchivoRepository = tipoArchivoRepository;
        }

        public async Task<List<InputPromptEntidad>> GetAllInputPromptsAsync()
        {
            var query = "CodeGenerator_InputPrompt_GetAll";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<InputPromptEntidad>(query, commandType: CommandType.StoredProcedure);

            foreach (var inputPrompt in resultado)
            {
                inputPrompt.TipoArchivoInput = await _tipoArchivoRepository.GetTipoArchivosByIdAsync(inputPrompt.IdTipoArchivoInput);
            }

            return resultado.ToList();
        }

        public async Task<InputPromptEntidad> GetInputPromptByIdAsync(int id)
        {
            var query = "CodeGenerator_InputPrompt_GetById";
            var connection = _context.CreateConnection();
            var param = new { id };
            var resultado = await connection.QuerySingleOrDefaultAsync<InputPromptEntidad>(query, param, commandType: CommandType.StoredProcedure);

            if (resultado != null)
            {
                resultado.TipoArchivoInput = await _tipoArchivoRepository.GetTipoArchivosByIdAsync(resultado.IdTipoArchivoInput);
            }

            return resultado;
        }

        public async Task<List<InputPromptEntidad>> CreateInputPromptAsync(InputPromptEntidad dto)
        {
            var query = "CodeGenerator_InputPrompt_Add";
            var connection = _context.CreateConnection();
            var param = new
            {
                dto.IdTipoArchivoInput,
                dto.IdPrompt
            };
            var resultado = await connection.QueryAsync<InputPromptEntidad>(query, param, commandType: CommandType.StoredProcedure);

            foreach (var inputPrompt in resultado)
            {
                inputPrompt.TipoArchivoInput = await _tipoArchivoRepository.GetTipoArchivosByIdAsync(inputPrompt.IdTipoArchivoInput);

            }

            return resultado.ToList();
        }

        public async Task<List<InputPromptEntidad>> UpdateInputPromptAsync(InputPromptEntidad dto)
        {
            var query = "CodeGenerator_InputPrompt_Update";
            var connection = _context.CreateConnection();
            var param = new
            {
                dto.Id,
                dto.IdTipoArchivoInput,
                dto.IdPrompt
            };
            var resultado = await connection.QueryAsync<InputPromptEntidad>(query, param, commandType: CommandType.StoredProcedure);

            foreach (var inputPrompt in resultado)
            {
                inputPrompt.TipoArchivoInput = await _tipoArchivoRepository.GetTipoArchivosByIdAsync(inputPrompt.IdTipoArchivoInput);
            }

            return resultado.ToList();
        }

        public async Task<bool> DeleteInputPromptAsync(int id)
        {
            var query = "CodeGenerator_InputPrompt_Delete";
            var connection = _context.CreateConnection();
            var param = new { id };
            var resultado = await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return resultado > 0;
        }

        public async Task<List<InputPromptEntidad>> GetAllInputPromptsByIdPromptAsync(int idPrompt)
        {
            var query = "[CodeGenerator_input_prompt_GetAllByIdPromt]";
            var connection = _context.CreateConnection();
            var param = new { idPrompt };
            var resultado = await connection.QueryAsync<InputPromptEntidad>(query, param, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
            
        }
    }
}
