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

        [HttpGet("api/tipoproyecto/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _tipoProyectoRepositorio.GetByIdAsync(id);
            return Json(result);
        }

        [HttpGet("api/tipoproyecto/all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _tipoProyectoRepositorio.GetAllAsync();
            return Json(result);
        }

        [HttpPost("api/tipoproyecto/create")]
        public async Task<IActionResult> Create([FromBody] TipoProyectoEntidad dto)
        {
            var result = await _tipoProyectoRepositorio.CreateAsync(dto);
            return Json(result);
        }

        [HttpPut("api/tipoproyecto/update")]
        public async Task<IActionResult> Update([FromBody] TipoProyectoEntidad dto)
        {
            var result = await _tipoProyectoRepositorio.UpdateAsync(dto);
            return Json(result);
        }

        [HttpDelete("api/tipoproyecto/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _tipoProyectoRepositorio.DeleteAsync(id);
            return Json(result);
        }
    }

}