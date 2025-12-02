using FolderView.Dapper.AdministracionBot.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FolderView.Controllers.AdministracionBot
{
    public class LogOperacionController : Controller
    {
        private readonly ILogOperacionRepository _logRepository;

        public LogOperacionController(ILogOperacionRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/logs")]
        public async Task<IActionResult> GetAll([FromQuery] int offset = 0, [FromQuery] int pageSize = 50)
        {
            var result = await _logRepository.GetAllAsync(offset, pageSize);
            return Json(result);
        }

        [HttpGet("api/logs/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _logRepository.GetByIdAsync(id);
            return Json(result);
        }

        [HttpGet("api/logs/filtros")]
        public async Task<IActionResult> GetConFiltros(
            [FromQuery] int? idUsuario = null,
            [FromQuery] int? idOperacion = null,
            [FromQuery] string resultado = null,
            [FromQuery] DateTime? fechaDesde = null,
            [FromQuery] DateTime? fechaHasta = null)
        {
            var result = await _logRepository.GetConFiltrosAsync(idUsuario, idOperacion, resultado, fechaDesde, fechaHasta);
            return Json(result);
        }

        [HttpGet("api/logs/fallidas-recientes")]
        public async Task<IActionResult> GetOperacionesFallidasRecientes([FromQuery] int horas = 24, [FromQuery] int top = 100)
        {
            var result = await _logRepository.GetOperacionesfallidasRecientesAsync(horas, top);
            return Json(result);
        }

        [HttpGet("api/logs/por-dia")]
        public async Task<IActionResult> GetOperacionesPorDia([FromQuery] int dias = 30)
        {
            var result = await _logRepository.GetOperacionesPorDiaAsync(dias);
            return Json(result);
        }
    }
}
