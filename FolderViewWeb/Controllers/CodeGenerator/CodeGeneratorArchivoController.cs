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

        [HttpGet("api/archivo/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _archivoRepositorio.GetByIdAsync(id);
            return Json(result);
        }

        [HttpGet("api/archivo/all/proyecto/{id}")]
        public async Task<IActionResult> GetAllByIdProyecto(int id)
        {
            var result = await _archivoRepositorio.GetAllByIdProyectoAsync(id);
            return Json(result);
        }

        [HttpPost("api/archivo/create")]
        public async Task<IActionResult> Create(CodeGeneratorArchivoEntidad dto)
        {
            var result = await _archivoRepositorio.CreateAsync(dto);
            return Json(result);
        }

        [HttpPut("api/archivo/update")]
        public async Task<IActionResult> Update(CodeGeneratorArchivoEntidad dto)
        {
            var result = await _archivoRepositorio.UpdateAsync(dto);
            return Json(result);
        }

        [HttpDelete("api/archivo/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _archivoRepositorio.DeleteAsync(id);
            return Json(result);
        }
    }

}
