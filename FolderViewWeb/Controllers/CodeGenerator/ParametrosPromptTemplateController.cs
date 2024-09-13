using FolderView.Dapper.Entidades;
using FolderView.Dapper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FolderView.Controllers
{
    public class ParametrosPromptTemplateController : Controller
    {
        private readonly IParametrosPromptTemplateRepository _parametrosPromptTemplateRepositorio;

        public ParametrosPromptTemplateController(IParametrosPromptTemplateRepository parametrosPromptTemplateRepositorio)
        {
            _parametrosPromptTemplateRepositorio = parametrosPromptTemplateRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ParametrosPromptTemplateEntidad dto)
        {
            var result = await _parametrosPromptTemplateRepositorio.CreateAsync(dto);
            return Json(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _parametrosPromptTemplateRepositorio.GetByIdAsync(id);
            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ParametrosPromptTemplateEntidad dto)
        {
            var result = await _parametrosPromptTemplateRepositorio.UpdateAsync(dto);
            return Json(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _parametrosPromptTemplateRepositorio.DeleteAsync(id);
            return Json(result);
        }

        [HttpGet("GetAllByIdPromptTemplate/{id}")]
        public async Task<IActionResult> GetAllByIdPromptTemplate(int id)
        {
            var result = await _parametrosPromptTemplateRepositorio.GetAllByIdPromptTemplateAsync(id);
            return Json(result);
        }
    }
}