using FolderView.Dapper.Entidades;
using FolderView.Dapper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FolderView.Controllers
{
    public class InputPromptController : Controller
    {
        private readonly IInputPromptRepository _inputPromptRepository;

        public InputPromptController(IInputPromptRepository inputPromptRepository)
        {
            _inputPromptRepository = inputPromptRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> inputprompt()
        {
            return View();
        }

        [HttpGet("api/inputprompt/{id}")]
        public async Task<IActionResult> GetInputPromptById(int id)
        {
            var result = await _inputPromptRepository.GetInputPromptByIdAsync(id);
          
            return Ok(result);
        }

        [HttpGet("api/inputprompt")]
        public async Task<IActionResult> GetAllInputPrompts()
        {
            var result = await _inputPromptRepository.GetAllInputPromptsAsync();
            
            return Ok(result);
        }

        [HttpPost("api/inputprompt")]
        public async Task<IActionResult> CreateInputPrompt([FromBody] InputPromptEntidad dto)
        {
            var result = await _inputPromptRepository.CreateInputPromptAsync(dto);
            return Ok(result);
        }

        [HttpPut("api/inputprompt/{id}")]
        public async Task<IActionResult> UpdateInputPrompt(int id, [FromBody] InputPromptEntidad dto)
        {
            dto.Id = id;
            var result = await _inputPromptRepository.UpdateInputPromptAsync(dto);
            return Ok(result);
        }

        [HttpDelete("api/inputprompt/{id}")]
        public async Task<IActionResult> DeleteInputPrompt(int id)
        {
            var success = await _inputPromptRepository.DeleteInputPromptAsync(id);
            return Ok(success);
        }
    }
}
