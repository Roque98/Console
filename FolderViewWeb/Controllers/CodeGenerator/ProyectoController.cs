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
        private readonly ITipoProyectoRepository _tipoProyectoRepository;

        public ProyectoController(
            IProyectoRepository proyectoRepositorio,
            ITipoProyectoRepository tipoProyectoRepository
        )
        {
            _proyectoRepositorio = proyectoRepositorio;
            _tipoProyectoRepository = tipoProyectoRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/proyecto/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _proyectoRepositorio.GetByIdAsync(id);
            return Json(result);
        }

        [HttpGet("api/proyecto/all/tipoproyecto/{id}")]
        public async Task<IActionResult> GetAllByIdTipoProyecto(int id)
        {
            var result = await _proyectoRepositorio.GetAllByIdTipoProyectoAsync(id);
            foreach (var proyecto in result)
            {
                proyecto.TipoProyecto = await _tipoProyectoRepository.GetByIdAsync(proyecto.idTipoProyecto);
            }
            return Json(result);
        }

        [HttpPost("api/proyecto/create")]
        public async Task<IActionResult> Create([FromBody] ProyectoEntidad dto)
        {
            var result = await _proyectoRepositorio.CreateAsync(dto);
            return Json(result);
        }

        [HttpPut("api/proyecto/update")]
        public async Task<IActionResult> Update([FromBody] ProyectoEntidad dto)
        {
            var result = await _proyectoRepositorio.UpdateAsync(dto);
            return Json(result);
        }

        [HttpDelete("api/proyecto/delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _proyectoRepositorio.DeleteAsync(id);
            return Json(result);
        }
    }

}
