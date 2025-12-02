using FolderView.Dapper.AdministracionBot.Entidades;
using FolderView.Dapper.AdministracionBot.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FolderView.Controllers.AdministracionBot
{
    public class RolPermisoController : Controller
    {
        private readonly IRolRepository _rolRepository;

        public RolPermisoController(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/roles")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _rolRepository.GetAllAsync();
            return Json(result);
        }

        [HttpGet("api/roles/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _rolRepository.GetByIdAsync(id);
            return Json(result);
        }

        [HttpGet("api/roles/{id}/permisos")]
        public async Task<IActionResult> GetPermisos(int id)
        {
            var result = await _rolRepository.GetPermisosByRolAsync(id);
            return Json(result);
        }

        [HttpPost("api/roles/{idRol}/permisos/{idOperacion}")]
        public async Task<IActionResult> AsignarPermiso(int idRol, int idOperacion, [FromBody] AsignarPermisoRequest request)
        {
            var result = await _rolRepository.AsignarPermisoAsync(idRol, idOperacion, request.Permitido, request.UsuarioAsignacion);
            return Json(new { success = result });
        }

        [HttpDelete("api/roles/{idRol}/permisos/{idOperacion}")]
        public async Task<IActionResult> RevocarPermiso(int idRol, int idOperacion)
        {
            var result = await _rolRepository.RevocarPermisoAsync(idRol, idOperacion);
            return Json(new { success = result });
        }

        [HttpGet("api/usuarios/permisos-especificos")]
        public async Task<IActionResult> GetPermisosEspecificosUsuario()
        {
            var result = await _rolRepository.GetPermisosEspecificosUsuarioAsync();
            return Json(result);
        }

        [HttpPost("api/usuarios/permisos-especificos")]
        public async Task<IActionResult> AsignarPermisoEspecificoUsuario([FromBody] UsuarioOperacionEntidad permiso)
        {
            var result = await _rolRepository.AsignarPermisoEspecificoUsuarioAsync(permiso);
            return Json(new { success = result });
        }
    }

    public class AsignarPermisoRequest
    {
        public bool Permitido { get; set; }
        public int? UsuarioAsignacion { get; set; }
    }
}
