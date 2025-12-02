using FolderView.Dapper.AdministracionBot.Entidades;
using FolderView.Dapper.AdministracionBot.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FolderView.Controllers.AdministracionBot
{
    public class ModuloController : Controller
    {
        private readonly IModuloRepository _moduloRepository;

        public ModuloController(IModuloRepository moduloRepository)
        {
            _moduloRepository = moduloRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/modulos")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _moduloRepository.GetAllAsync();
            return Json(result);
        }

        [HttpGet("api/modulos/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _moduloRepository.GetByIdAsync(id);
            return Json(result);
        }

        [HttpPost("api/modulos")]
        public async Task<IActionResult> Create([FromBody] ModuloEntidad modulo)
        {
            var id = await _moduloRepository.CreateAsync(modulo);
            return Json(new { success = true, id });
        }

        [HttpPut("api/modulos/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ModuloEntidad modulo)
        {
            modulo.IdModulo = id;
            var result = await _moduloRepository.UpdateAsync(modulo);
            return Json(new { success = result });
        }

        [HttpDelete("api/modulos/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _moduloRepository.DeleteAsync(id);
            return Json(new { success = result });
        }
    }
}
