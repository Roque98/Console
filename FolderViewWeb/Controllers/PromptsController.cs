//using FolderView.Dapper.Entidades;
//using FolderView.Dapper.Interfaces;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace FolderView.Controllers
//{
//    public class PromptsController : Controller
//    {
//        private readonly IPromptsRepository _promptsRepository;

//        public PromptsController(IPromptsRepository promptsRepository)
//        {
//            _promptsRepository = promptsRepository;
//        }

//        [HttpGet("")]
//        public async Task<IActionResult> prompts()
//        {
//            return View();
//        }

//        [HttpGet("api/prompts/{id}")]
//        public async Task<IActionResult> GetPromptById(int id)
//        {
//            var result = await _promptsRepository.GetPromptByIdAsync(id);
//            return Ok(result);
//        }

//        [HttpGet("api/prompts")]
//        public async Task<IActionResult> GetAllPrompts()
//        {
//            var result = await _promptsRepository.GetAllPromptsAsync();
           
//            return Ok(result);
//        }

//        [HttpPost("api/prompts")]
//        public async Task<IActionResult> CreatePrompt([FromBody] PromptsEntidad dto)
//        {
//            var result = await _promptsRepository.CreatePromptAsync(dto);
//            return Ok(result);
//        }

//        [HttpPut("api/prompts/{id}")]
//        public async Task<IActionResult> UpdatePrompt(int id, [FromBody] PromptsEntidad dto)
//        {
//            dto.Id = id;
//            var result = await _promptsRepository.UpdatePromptAsync(dto);
//            return Ok(result);
//        }

//        [HttpDelete("api/prompts/{id}")]
//        public async Task<IActionResult> DeletePrompt(int id)
//        {
//            var success = await _promptsRepository.DeletePromptAsync(id);
//            return Ok(success);
//        }
//    }

    
//}
