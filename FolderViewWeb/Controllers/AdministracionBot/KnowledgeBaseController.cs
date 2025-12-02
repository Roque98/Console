using FolderView.Dapper.AdministracionBot.Entidades;
using FolderView.Dapper.AdministracionBot.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FolderView.Controllers.AdministracionBot
{
    public class KnowledgeBaseController : Controller
    {
        private readonly IKnowledgeRepository _knowledgeRepository;

        public KnowledgeBaseController(IKnowledgeRepository knowledgeRepository)
        {
            _knowledgeRepository = knowledgeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Categorías
        [HttpGet("api/knowledge/categories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _knowledgeRepository.GetAllCategoriesAsync();
            return Json(result);
        }

        [HttpGet("api/knowledge/categories/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _knowledgeRepository.GetCategoryByIdAsync(id);
            return Json(result);
        }

        [HttpPost("api/knowledge/categories")]
        public async Task<IActionResult> CreateCategory([FromBody] KnowledgeCategoryEntidad category)
        {
            var id = await _knowledgeRepository.CreateCategoryAsync(category);
            return Json(new { success = true, id });
        }

        [HttpPut("api/knowledge/categories/{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] KnowledgeCategoryEntidad category)
        {
            category.Id = id;
            var result = await _knowledgeRepository.UpdateCategoryAsync(category);
            return Json(new { success = result });
        }

        // Entradas
        [HttpGet("api/knowledge/entries")]
        public async Task<IActionResult> GetAllEntries()
        {
            var result = await _knowledgeRepository.GetAllEntriesAsync();
            return Json(result);
        }

        [HttpGet("api/knowledge/entries/category/{categoryId}")]
        public async Task<IActionResult> GetEntriesByCategory(int categoryId)
        {
            var result = await _knowledgeRepository.GetEntriesByCategoryAsync(categoryId);
            return Json(result);
        }

        [HttpGet("api/knowledge/entries/{id}")]
        public async Task<IActionResult> GetEntryById(int id)
        {
            var result = await _knowledgeRepository.GetEntryByIdAsync(id);
            return Json(result);
        }

        [HttpPost("api/knowledge/entries")]
        public async Task<IActionResult> CreateEntry([FromBody] KnowledgeEntryEntidad entry)
        {
            var id = await _knowledgeRepository.CreateEntryAsync(entry);
            return Json(new { success = true, id });
        }

        [HttpPut("api/knowledge/entries/{id}")]
        public async Task<IActionResult> UpdateEntry(int id, [FromBody] KnowledgeEntryEntidad entry)
        {
            entry.Id = id;
            var result = await _knowledgeRepository.UpdateEntryAsync(entry);
            return Json(new { success = result });
        }

        [HttpDelete("api/knowledge/entries/{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            var result = await _knowledgeRepository.DeleteEntryAsync(id);
            return Json(new { success = result });
        }
    }
}
