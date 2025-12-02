using FolderView.Dapper.AdministracionBot.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FolderView.Controllers.AdministracionBot
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/dashboard/stats")]
        public async Task<IActionResult> GetStats()
        {
            var result = await _dashboardRepository.GetStatsAsync();
            return Json(result);
        }

        [HttpGet("api/dashboard/operaciones-resultado-hoy")]
        public async Task<IActionResult> GetOperacionesPorResultadoHoy()
        {
            var result = await _dashboardRepository.GetOperacionesPorResultadoHoyAsync();
            return Json(result);
        }

        [HttpGet("api/dashboard/top-operaciones")]
        public async Task<IActionResult> GetTopOperaciones([FromQuery] int top = 5)
        {
            var result = await _dashboardRepository.GetTopOperacionesAsync(top);
            return Json(result);
        }

        [HttpGet("api/dashboard/top-usuarios")]
        public async Task<IActionResult> GetTopUsuarios([FromQuery] int top = 5)
        {
            var result = await _dashboardRepository.GetTopUsuariosAsync(top);
            return Json(result);
        }

        [HttpGet("api/dashboard/tasa-exito")]
        public async Task<IActionResult> GetTasaExito()
        {
            var result = await _dashboardRepository.GetTasaExitoUltimos7DiasAsync();
            return Json(result);
        }
    }
}
