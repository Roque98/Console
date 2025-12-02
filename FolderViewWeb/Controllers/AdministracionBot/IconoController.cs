using FolderView.Dapper.AdministracionBot.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FolderView.Controllers.AdministracionBot
{
    public class IconoController : Controller
    {
        private readonly IIconoRepository _iconoRepository;

        public IconoController(IIconoRepository iconoRepository)
        {
            _iconoRepository = iconoRepository;
        }

        [HttpGet("api/iconos")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _iconoRepository.GetAllAsync();
            return Json(result);
        }

        [HttpGet("api/iconos/categorias")]
        public async Task<IActionResult> GetCategorias()
        {
            var result = await _iconoRepository.GetCategoriasAsync();
            return Json(result);
        }

        [HttpGet("api/iconos/categoria/{categoria}")]
        public async Task<IActionResult> GetByCategoria(string categoria)
        {
            var result = await _iconoRepository.GetByCategoriaAsync(categoria);
            return Json(result);
        }
    }
}
