using Microsoft.AspNetCore.Mvc;
using FolderView.Dapper.AdministracionBot.Interfaces;
using FolderView.Dapper.AdministracionBot.Entidades;

namespace FolderView.Controllers.MiCuenta
{
    public class MisCuentasTelegramController : Controller
    {
        private readonly IUsuarioTelegramRepository _usuarioTelegramRepository;

        public MisCuentasTelegramController(IUsuarioTelegramRepository usuarioTelegramRepository)
        {
            _usuarioTelegramRepository = usuarioTelegramRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/mis-cuentas-telegram")]
        public async Task<IActionResult> GetMisCuentas()
        {
            // TODO: Obtener el ID del usuario actual desde la sesión/autenticación
            int idUsuarioActual = 1; // Temporal - reemplazar con ID de usuario real

            var cuentas = await _usuarioTelegramRepository.GetCuentasByUsuarioIdAsync(idUsuarioActual);
            return Json(cuentas);
        }

        [HttpPost("api/mis-cuentas-telegram/{id}/principal")]
        public async Task<IActionResult> SetCuentaPrincipal(int id)
        {
            // TODO: Verificar que la cuenta pertenece al usuario actual
            var result = await _usuarioTelegramRepository.SetCuentaPrincipalAsync(id);
            return Json(new { success = result });
        }

        [HttpGet("api/mis-cuentas-telegram/{id}/actividad")]
        public async Task<IActionResult> GetActividadCuenta(int id)
        {
            // TODO: Verificar que la cuenta pertenece al usuario actual
            var actividad = await _usuarioTelegramRepository.GetHistorialActividadAsync(id);
            return Json(actividad);
        }
    }
}
