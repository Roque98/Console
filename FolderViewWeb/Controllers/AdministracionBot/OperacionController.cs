using FolderView.Dapper.AdministracionBot.Entidades;
using FolderView.Dapper.AdministracionBot.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FolderView.Controllers.AdministracionBot
{
    public class OperacionController : Controller
    {
        private readonly IOperacionRepository _operacionRepository;

        public OperacionController(IOperacionRepository operacionRepository)
        {
            _operacionRepository = operacionRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/operaciones")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _operacionRepository.GetAllAsync();
            return Json(result);
        }

        [HttpGet("api/operaciones/estadisticas")]
        public async Task<IActionResult> GetAllConEstadisticas()
        {
            var result = await _operacionRepository.GetAllConEstadisticasAsync();
            return Json(result);
        }

        [HttpGet("api/operaciones/modulo/{idModulo}")]
        public async Task<IActionResult> GetByModulo(int idModulo)
        {
            var result = await _operacionRepository.GetByModuloAsync(idModulo);
            return Json(result);
        }

        [HttpGet("api/operaciones/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _operacionRepository.GetByIdAsync(id);
            return Json(result);
        }

        [HttpPost("api/operaciones")]
        public async Task<IActionResult> Create([FromBody] OperacionEntidad operacion)
        {
            var id = await _operacionRepository.CreateAsync(operacion);
            return Json(new { success = true, id });
        }

        [HttpPut("api/operaciones/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] OperacionEntidad operacion)
        {
            operacion.IdOperacion = id;
            var result = await _operacionRepository.UpdateAsync(operacion);
            return Json(new { success = result });
        }

        [HttpDelete("api/operaciones/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _operacionRepository.DeleteAsync(id);
            return Json(new { success = result });
        }
    }
}
