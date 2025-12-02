using Microsoft.AspNetCore.Mvc;
using FolderView.Dapper.AdministracionBot.Interfaces;

namespace FolderView.Controllers.MiCuenta
{
    public class MisPermisosController : Controller
    {
        private readonly IOperacionRepository _operacionRepository;
        private readonly IRolRepository _rolRepository;

        public MisPermisosController(
            IOperacionRepository operacionRepository,
            IRolRepository rolRepository)
        {
            _operacionRepository = operacionRepository;
            _rolRepository = rolRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/mis-permisos/operaciones")]
        public async Task<IActionResult> GetMisOperacionesPermitidas()
        {
            // TODO: Obtener el ID del usuario actual
            int idUsuarioActual = 1; // Temporal - reemplazar con ID real

            var operaciones = await _operacionRepository.GetOperacionesByUsuarioAsync(idUsuarioActual);

            var agrupadas = operaciones.GroupBy(o => o.Modulo).Select(g => new
            {
                Modulo = g.Key,
                ModuloIcono = g.First().ModuloIcono,
                Operaciones = g.Select(o => new
                {
                    o.IdOperacion,
                    o.Nombre,
                    o.Comando,
                    o.Descripcion,
                    o.RequiereParametros,
                    o.TipoPermiso,
                    o.TienePoder
                }).OrderBy(o => o.Nombre)
            }).OrderBy(g => g.Modulo);

            return Json(agrupadas);
        }

        [HttpGet("api/mis-permisos/roles")]
        public async Task<IActionResult> GetMisRoles()
        {
            // TODO: Obtener roles del usuario actual desde la base de datos
            // Ejemplo temporal
            var roles = new[]
            {
                new
                {
                    IdRol = 1,
                    Nombre = "Usuario Estándar",
                    Descripcion = "Acceso básico al sistema",
                    Nivel = 1,
                    TotalPermisos = 10
                }
            };

            return Json(roles);
        }

        [HttpGet("api/mis-permisos/verificar/{comando}")]
        public async Task<IActionResult> VerificarPermiso(string comando)
        {
            // TODO: Obtener el ID del usuario actual
            int idUsuarioActual = 1; // Temporal

            var operaciones = await _operacionRepository.GetOperacionesByUsuarioAsync(idUsuarioActual);
            var tienePermiso = operaciones.Any(o => o.Comando.Equals(comando, StringComparison.OrdinalIgnoreCase));

            return Json(new
            {
                Comando = comando,
                TienePermiso = tienePermiso,
                Operacion = operaciones.FirstOrDefault(o => o.Comando.Equals(comando, StringComparison.OrdinalIgnoreCase))
            });
        }
    }
}
