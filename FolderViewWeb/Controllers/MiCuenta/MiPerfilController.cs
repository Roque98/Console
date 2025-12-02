using Microsoft.AspNetCore.Mvc;
using FolderView.Dapper.AdministracionBot.Interfaces;

namespace FolderView.Controllers.MiCuenta
{
    public class MiPerfilController : Controller
    {
        private readonly IUsuarioTelegramRepository _usuarioTelegramRepository;
        private readonly IRolRepository _rolRepository;

        public MiPerfilController(
            IUsuarioTelegramRepository usuarioTelegramRepository,
            IRolRepository rolRepository)
        {
            _usuarioTelegramRepository = usuarioTelegramRepository;
            _rolRepository = rolRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/mi-perfil")]
        public async Task<IActionResult> GetMiPerfil()
        {
            // TODO: Obtener el ID del usuario actual desde la sesión/autenticación
            int idUsuarioActual = 1; // Temporal - reemplazar con ID de usuario real
            string usuarioActual = "admin"; // Temporal - reemplazar con usuario real

            var cuentasTelegram = await _usuarioTelegramRepository.GetCuentasByUsuarioIdAsync(idUsuarioActual);
            var cuentaPrincipal = cuentasTelegram.FirstOrDefault(c => c.EsPrincipal);

            var perfil = new
            {
                Usuario = usuarioActual,
                CuentaPrincipal = cuentaPrincipal,
                TotalCuentas = cuentasTelegram.Count,
                CuentasActivas = cuentasTelegram.Count(c => c.Estado == "ACTIVO"),
                FechaRegistro = cuentaPrincipal?.FechaCreacion,
                UltimaActividad = cuentaPrincipal?.FechaUltimaActividad
            };

            return Json(perfil);
        }

        [HttpGet("api/mi-perfil/roles")]
        public async Task<IActionResult> GetMisRoles()
        {
            // TODO: Obtener roles del usuario actual desde la base de datos
            // Por ahora retornamos un ejemplo
            var roles = new[]
            {
                new { IdRol = 1, Nombre = "Usuario Estándar", Descripcion = "Acceso básico al sistema" }
            };

            return Json(roles);
        }
    }
}
