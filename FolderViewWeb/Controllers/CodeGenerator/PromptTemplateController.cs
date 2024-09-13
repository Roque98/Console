using FolderView.Dapper.Entidades;
using FolderView.Dapper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FolderView.Controllers
{
    public class PromptTemplateController : Controller
    {
        private readonly IPromptTemplateRepository _promptTemplateRepositorio;

        public PromptTemplateController(IPromptTemplateRepository promptTemplateRepositorio)
        {
            _promptTemplateRepositorio = promptTemplateRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PromptTemplateEntidad dto)
        {
            var result = await _promptTemplateRepositorio.CreateAsync(dto);
            return Json(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _promptTemplateRepositorio.GetByIdAsync(id);
            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(PromptTemplateEntidad dto)
        {
            var result = await _promptTemplateRepositorio.UpdateAsync(dto);
            return Json(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _promptTemplateRepositorio.DeleteAsync(id);
            return Json(result);
        }

        [HttpGet("GetAllByIdTipoProyecto/{id}")]
        public async Task<IActionResult> GetAllByIdTipoProyecto(int id)
        {
            var result = await _promptTemplateRepositorio.GetAllByIdTipoProyectoAsync(id);
            return Json(result);
        }
    }
}
