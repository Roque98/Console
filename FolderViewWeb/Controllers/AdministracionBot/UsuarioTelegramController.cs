using FolderView.Dapper.AdministracionBot.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FolderView.Controllers.AdministracionBot
{
    public class UsuarioTelegramController : Controller
    {
        private readonly IUsuarioTelegramRepository _telegramRepository;

        public UsuarioTelegramController(IUsuarioTelegramRepository telegramRepository)
        {
            _telegramRepository = telegramRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("api/telegram-accounts")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _telegramRepository.GetAllAsync();
            return Json(result);
        }

        [HttpGet("api/telegram-accounts/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _telegramRepository.GetByIdAsync(id);
            return Json(result);
        }

        [HttpGet("api/telegram-accounts/usuario/{idUsuario}")]
        public async Task<IActionResult> GetByUsuario(int idUsuario)
        {
            var result = await _telegramRepository.GetByUsuarioAsync(idUsuario);
            return Json(result);
        }

        [HttpGet("api/telegram-accounts/pendientes-verificacion")]
        public async Task<IActionResult> GetPendientesVerificacion()
        {
            var result = await _telegramRepository.GetPendientesVerificacionAsync();
            return Json(result);
        }

        [HttpPost("api/telegram-accounts/{id}/verificar")]
        public async Task<IActionResult> VerificarCuenta(int id)
        {
            var result = await _telegramRepository.VerificarCuentaAsync(id);
            return Json(new { success = result });
        }

        [HttpPost("api/telegram-accounts/{id}/establecer-principal")]
        public async Task<IActionResult> EstablecerPrincipal(int id, [FromBody] EstablecerPrincipalRequest request)
        {
            var result = await _telegramRepository.EstablecerPrincipalAsync(id, request.IdUsuario);
            return Json(new { success = result });
        }

        [HttpPut("api/telegram-accounts/{id}/estado")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] CambiarEstadoRequest request)
        {
            var result = await _telegramRepository.CambiarEstadoAsync(id, request.Estado);
            return Json(new { success = result });
        }

        [HttpGet("api/telegram-accounts/multiples-cuentas")]
        public async Task<IActionResult> GetUsuariosConMultiplesCuentas()
        {
            var result = await _telegramRepository.GetUsuariosConMultiplesCuentasAsync();
            return Json(result);
        }
    }

    public class EstablecerPrincipalRequest
    {
        public int IdUsuario { get; set; }
    }

    public class CambiarEstadoRequest
    {
        public string Estado { get; set; }
    }
}
