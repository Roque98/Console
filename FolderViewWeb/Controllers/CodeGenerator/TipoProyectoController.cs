using FolderView.Dapper.Entidades;
using FolderView.Dapper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FolderView.Controllers
{
    public class TipoProyectoController : Controller
    {
        private readonly ITipoProyectoRepository _tipoProyectoRepositorio;

        public TipoProyectoController(ITipoProyectoRepository tipoProyectoRepositorio)
        {
            _tipoProyectoRepositorio = tipoProyectoRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TipoProyectoEntidad dto)
        {
            var result = await _tipoProyectoRepositorio.CreateAsync(dto);
            return Json(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _tipoProyectoRepositorio.GetByIdAsync(id);
            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(TipoProyectoEntidad dto)
        {
            var result = await _tipoProyectoRepositorio.UpdateAsync(dto);
            return Json(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tipoProyectoRepositorio.DeleteAsync(id);
            return Json(result);
        }

        [HttpGet("GetAllByIdProyecto/{id}")]
        public async Task<IActionResult> GetAllByIdProyecto(int id)
        {
            var result = await _tipoProyectoRepositorio.GetAllByIdProyectoAsync(id);
            return Json(result);
        }
    }
}