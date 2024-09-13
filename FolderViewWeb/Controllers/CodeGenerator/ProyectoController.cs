using FolderView.Dapper.Entidades;
using FolderView.Dapper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FolderView.Controllers
{
    public class ProyectoController : Controller
    {
        private readonly IProyectoRepository _proyectoRepositorio;

        public ProyectoController(IProyectoRepository proyectoRepositorio)
        {
            _proyectoRepositorio = proyectoRepositorio;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProyectoEntidad dto)
        {
            var result = await _proyectoRepositorio.CreateAsync(dto);
            return Json(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _proyectoRepositorio.GetByIdAsync(id);
            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProyectoEntidad dto)
        {
            var result = await _proyectoRepositorio.UpdateAsync(dto);
            return Json(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _proyectoRepositorio.DeleteAsync(id);
            return Json(result);
        }

        [HttpGet("GetAllByIdTipoProyecto/{id}")]
        public async Task<IActionResult> GetAllByIdTipoProyecto(int id)
        {
            var result = await _proyectoRepositorio.GetAllByIdTipoProyectoAsync(id);
            return Json(result);
        }

       
    }
}
