using Microsoft.AspNetCore.Mvc;
using FolderView.Dapper.AdministracionBot.Interfaces;

namespace FolderView.Controllers.MiCuenta
{
    public class MisOperacionesController : Controller
    {
        private readonly ILogOperacionRepository _logOperacionRepository;

        public MisOperacionesController(ILogOperacionRepository logOperacionRepository)
        {
            _logOperacionRepository = logOperacionRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/mis-operaciones")]
        public async Task<IActionResult> GetMisOperaciones(
            [FromQuery] DateTime? fechaInicio,
            [FromQuery] DateTime? fechaFin)
        {
            // TODO: Obtener el ID del usuario actual desde la sesión/autenticación
            int idUsuarioActual = 1; // Temporal - reemplazar con ID real

            var logs = await _logOperacionRepository.GetByUsuarioAsync(
                idUsuarioActual,
                fechaInicio ?? DateTime.Now.AddMonths(-1),
                fechaFin ?? DateTime.Now
            );

            return Json(logs);
        }

        [HttpGet("api/mis-operaciones/{id}")]
        public async Task<IActionResult> GetDetalleOperacion(int id)
        {
            // TODO: Verificar que el log pertenece al usuario actual
            var log = await _logOperacionRepository.GetByIdAsync(id);
            return Json(log);
        }

        [HttpGet("api/mis-operaciones/estadisticas")]
        public async Task<IActionResult> GetEstadisticas()
        {
            // TODO: Obtener el ID del usuario actual
            int idUsuarioActual = 1; // Temporal

            var logs = await _logOperacionRepository.GetByUsuarioAsync(
                idUsuarioActual,
                DateTime.Now.AddMonths(-1),
                DateTime.Now
            );

            var stats = new
            {
                TotalOperaciones = logs.Count,
                Exitosas = logs.Count(l => l.Exito),
                Fallidas = logs.Count(l => !l.Exito),
                PorcentajeExito = logs.Count > 0 ? (logs.Count(l => l.Exito) * 100.0 / logs.Count) : 0,
                OperacionMasFrecuente = logs.GroupBy(l => l.Operacion)
                    .OrderByDescending(g => g.Count())
                    .FirstOrDefault()?.Key ?? "N/A"
            };

            return Json(stats);
        }
    }
}
