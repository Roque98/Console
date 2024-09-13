using FolderView.Dapper.Entidades;
using FolderView.Dapper.Interfaces;
using FolderView.Dapper.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FolderView.Controllers
{
    public class CodeGeneratorArchivoController : Controller
    {
        private readonly ICodeGeneratorIArchivoRepository _archivoRepositorio;

        public CodeGeneratorArchivoController(ICodeGeneratorIArchivoRepository archivoRepositorio)
        {
            _archivoRepositorio = archivoRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CodeGeneratorArchivoEntidad dto)
        {
            var result = await _archivoRepositorio.CreateAsync(dto);
            return Json(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _archivoRepositorio.GetByIdAsync(id);
            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CodeGeneratorArchivoEntidad dto)
        {
            var result = await _archivoRepositorio.UpdateAsync(dto);
            return Json(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _archivoRepositorio.DeleteAsync(id);
            return Json(result);
        }

        [HttpGet("GetAllByIdProyecto/{id}")]
        public async Task<IActionResult> GetAllByIdProyecto(int id)
        {
            var result = await _archivoRepositorio.GetAllByIdProyectoAsync(id);
            return Json(result);
        }

        [HttpGet("GetAllByIdArchivoPadre/{id}")]
        public async Task<IActionResult> GetAllByIdArchivoPadre(int id)
        {
            var result = await _archivoRepositorio.GetAllByIdArchivoPadreAsync(id);
            return Json(result);
        }
    }
}
