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

        [HttpGet("api/parametrosprompttemplate/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _parametrosPromptTemplateRepositorio.GetByIdAsync(id);
            return Json(result);
        }

        [HttpGet("api/parametrosprompttemplate/all/prompttemplate/{id}")]
        public async Task<IActionResult> GetAllByIdPromptTemplate(int id)
        {
            var result = await _parametrosPromptTemplateRepositorio.GetAllByIdPromptTemplateAsync(id);
            return Json(result);
        }

        [HttpPost("api/parametrosprompttemplate/create")]
        public async Task<IActionResult> Create(ParametrosPromptTemplateEntidad dto)
        {
            var result = await _parametrosPromptTemplateRepositorio.CreateAsync(dto);
            return Json(result);
        }

        [HttpPut("api/parametrosprompttemplate/update")]
        public async Task<IActionResult> Update(ParametrosPromptTemplateEntidad dto)
        {
            var result = await _parametrosPromptTemplateRepositorio.UpdateAsync(dto);
            return Json(result);
        }

        [HttpDelete("api/parametrosprompttemplate/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _parametrosPromptTemplateRepositorio.DeleteAsync(id);
            return Json(result);
        }
    }

}