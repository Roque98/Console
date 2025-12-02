using Microsoft.AspNetCore.Mvc;
using FolderView.Dapper.AdministracionBot.Interfaces;

namespace FolderView.Controllers.MiCuenta
{
    public class DashboardPersonalController : Controller
    {
        private readonly ILogOperacionRepository _logOperacionRepository;
        private readonly IOperacionRepository _operacionRepository;
        private readonly IUsuarioTelegramRepository _usuarioTelegramRepository;

        public DashboardPersonalController(
            ILogOperacionRepository logOperacionRepository,
            IOperacionRepository operacionRepository,
            IUsuarioTelegramRepository usuarioTelegramRepository)
        {
            _logOperacionRepository = logOperacionRepository;
            _operacionRepository = operacionRepository;
            _usuarioTelegramRepository = usuarioTelegramRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/dashboard-personal/resumen")]
        public async Task<IActionResult> GetResumen()
        {
            // TODO: Obtener el usuario e ID actual desde la sesión
            string usuarioActual = "admin"; // Temporal
            int idUsuarioActual = 1; // Temporal

            var logs = await _logOperacionRepository.GetByUsuarioAsync(
                idUsuarioActual,
                DateTime.Now.AddMonths(-1),
                DateTime.Now
            );

            var operaciones = await _operacionRepository.GetOperacionesByUsuarioAsync(idUsuarioActual);
            var cuentas = await _usuarioTelegramRepository.GetCuentasByUsuarioIdAsync(idUsuarioActual);

            var resumen = new
            {
                Usuario = usuarioActual,
                OperacionesUltimos30Dias = logs.Count,
                OperacionesExitosas = logs.Count(l => l.Exito),
                OperacionesFallidas = logs.Count(l => !l.Exito),
                PorcentajeExito = logs.Count > 0 ? Math.Round(logs.Count(l => l.Exito) * 100.0 / logs.Count, 2) : 0,
                TotalPermisos = operaciones.Count,
                CuentasTelegram = cuentas.Count,
                CuentasActivas = cuentas.Count(c => c.Estado == "ACTIVO"),
                UltimaOperacion = logs.OrderByDescending(l => l.FechaHora).FirstOrDefault()?.FechaHora
            };

            return Json(resumen);
        }

        [HttpGet("api/dashboard-personal/operaciones-por-dia")]
        public async Task<IActionResult> GetOperacionesPorDia()
        {
            // TODO: Obtener el ID del usuario actual
            int idUsuarioActual = 1; // Temporal

            var logs = await _logOperacionRepository.GetByUsuarioAsync(
                idUsuarioActual,
                DateTime.Now.AddDays(-30),
                DateTime.Now
            );

            var porDia = logs.GroupBy(l => l.FechaHora.Date)
                .Select(g => new
                {
                    Fecha = g.Key,
                    Total = g.Count(),
                    Exitosas = g.Count(l => l.Exito),
                    Fallidas = g.Count(l => !l.Exito)
                })
                .OrderBy(x => x.Fecha)
                .ToList();

            return Json(porDia);
        }

        [HttpGet("api/dashboard-personal/operaciones-mas-usadas")]
        public async Task<IActionResult> GetOperacionesMasUsadas()
        {
            // TODO: Obtener el ID del usuario actual
            int idUsuarioActual = 1; // Temporal

            var logs = await _logOperacionRepository.GetByUsuarioAsync(
                idUsuarioActual,
                DateTime.Now.AddMonths(-1),
                DateTime.Now
            );

            var masUsadas = logs.GroupBy(l => l.Operacion)
                .Select(g => new
                {
                    Operacion = g.Key,
                    Total = g.Count(),
                    Exitosas = g.Count(l => l.Exito),
                    Fallidas = g.Count(l => !l.Exito),
                    UltimoUso = g.Max(l => l.FechaHora)
                })
                .OrderByDescending(x => x.Total)
                .Take(10)
                .ToList();

            return Json(masUsadas);
        }

        [HttpGet("api/dashboard-personal/actividad-reciente")]
        public async Task<IActionResult> GetActividadReciente()
        {
            // TODO: Obtener el ID del usuario actual
            int idUsuarioActual = 1; // Temporal

            var logs = await _logOperacionRepository.GetByUsuarioAsync(
                idUsuarioActual,
                DateTime.Now.AddDays(-7),
                DateTime.Now
            );

            var recientes = logs.OrderByDescending(l => l.FechaHora)
                .Take(20)
                .Select(l => new
                {
                    l.IdLog,
                    l.Operacion,
                    l.FechaHora,
                    l.Exito,
                    l.TiempoEjecucion,
                    l.TelegramUsername
                })
                .ToList();

            return Json(recientes);
        }
    }
}
