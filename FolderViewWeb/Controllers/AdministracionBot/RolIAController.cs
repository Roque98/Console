using FolderView.Dapper.AdministracionBot.Entidades;
using FolderView.Dapper.AdministracionBot.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FolderView.Controllers.AdministracionBot
{
    public class RolIAController : Controller
    {
        private readonly IRolIARepository _rolIARepository;

        public RolIAController(IRolIARepository rolIARepository)
        {
            _rolIARepository = rolIARepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/roles-ia")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _rolIARepository.GetAllAsync();
            return Json(result);
        }

        [HttpGet("api/roles-ia/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _rolIARepository.GetByIdAsync(id);
            return Json(result);
        }

        [HttpPost("api/roles-ia")]
        public async Task<IActionResult> Create([FromBody] RolIAEntidad rol)
        {
            var id = await _rolIARepository.CreateAsync(rol);
            return Json(new { success = true, id });
        }

        [HttpPut("api/roles-ia/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolIAEntidad rol)
        {
            rol.IdRol = id;
            var result = await _rolIARepository.UpdateAsync(rol);
            return Json(new { success = result });
        }

        [HttpPost("api/roles-ia/{idRol}/usuarios/{idUsuario}")]
        public async Task<IActionResult> AsignarRolAUsuario(int idRol, int idUsuario)
        {
            var result = await _rolIARepository.AsignarRolAUsuarioAsync(idRol, idUsuario);
            return Json(new { success = result });
        }

        [HttpDelete("api/roles-ia/{idRol}/usuarios/{idUsuario}")]
        public async Task<IActionResult> RemoverRolDeUsuario(int idRol, int idUsuario)
        {
            var result = await _rolIARepository.RemoverRolDeUsuarioAsync(idRol, idUsuario);
            return Json(new { success = result });
        }
    }
}
