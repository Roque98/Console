using FolderView.Dapper.Entidades;
using FolderView.Dapper.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FolderView.Controllers
{
    public class CodeGeneratorViewController : Controller
    {
        private readonly ICodeGeneratorRepository _codeGeneratorRepository;

        public CodeGeneratorViewController(
            ICodeGeneratorRepository codeGeneratorRepository
        )
        {
            _codeGeneratorRepository = codeGeneratorRepository;
        }

        // Métodos para TipoProyecto
        [HttpPost]
        public async Task<IActionResult> CreateTipoProyecto([FromBody] CreateTipoProyectoEntidad dto)
        {
            var result = await _codeGeneratorRepository.CreateTipoProyectoAsync(dto);
            return Json(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTipoProyectoById(int id)
        {
            var result = await _codeGeneratorRepository.GetTipoProyectoByIdAsync(id);
            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTipoProyecto([FromBody] UpdateTipoProyectoEntidad dto)
        {
            var result = await _codeGeneratorRepository.UpdateTipoProyectoAsync(dto);
            return Json(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoProyecto(int id)
        {
            var success = await _codeGeneratorRepository.DeleteTipoProyectoAsync(id);
            return Json(new { success });
        }

        // Métodos para TipoArchivos
        public async Task<IActionResult> TipoArchivos()
        {
            // Obtener todos los TipoArchivo del repositorio
            var tipoArchivos = await _codeGeneratorRepository.GetAllTipoArchivos(); // 0 para obtener todos
            return View(tipoArchivos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTipoArchivos([FromBody] CreateTipoArchivosEntidad dto)
        {
            var result = await _codeGeneratorRepository.CreateTipoArchivosAsync(dto);
            return Json(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTipoArchivosById(int id)
        {
            var result = await _codeGeneratorRepository.GetTipoArchivosByIdAsync(id);
            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTipoArchivos([FromBody] UpdateTipoArchivosEntidad dto)
        {
            var result = await _codeGeneratorRepository.UpdateTipoArchivosAsync(dto);
            return Json(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoArchivos(int id)
        {
            var success = await _codeGeneratorRepository.DeleteTipoArchivosAsync(id);
            return Json(new { success });
        }

        // Métodos para TipoArchivoTipoProyecto
        [HttpPost]
        public async Task<IActionResult> CreateTipoArchivoTipoProyecto([FromBody] CreateTipoArchivoTipoProyectoEntidad dto)
        {
            var result = await _codeGeneratorRepository.CreateTipoArchivoTipoProyectoAsync(dto);
            return Json(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTipoArchivoTipoProyectoById(int id)
        {
            var result = await _codeGeneratorRepository.GetTipoArchivoTipoProyectoByIdAsync(id);
            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTipoArchivoTipoProyecto([FromBody] UpdateTipoArchivoTipoProyectoEntidad dto)
        {
            var result = await _codeGeneratorRepository.UpdateTipoArchivoTipoProyectoAsync(dto);
            return Json(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoArchivoTipoProyecto(int id)
        {
            var success = await _codeGeneratorRepository.DeleteTipoArchivoTipoProyectoAsync(id);
            return Json(new { success });
        }

        // Métodos para Proyecto
        [HttpPost]
        public async Task<IActionResult> CreateProyecto([FromBody] CreateProyectoEntidad dto)
        {
            var result = await _codeGeneratorRepository.CreateProyectoAsync(dto);
            return Json(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProyectoById(int id)
        {
            var result = await _codeGeneratorRepository.GetProyectoByIdAsync(id);
            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProyecto([FromBody] UpdateProyectoEntidad dto)
        {
            var result = await _codeGeneratorRepository.UpdateProyectoAsync(dto);
            return Json(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProyecto(int id)
        {
            var success = await _codeGeneratorRepository.DeleteProyectoAsync(id);
            return Json(new { success });
        }

        // Métodos para Folder
        [HttpPost]
        public async Task<IActionResult> CreateFolder([FromBody] CreateFolderEntidad dto)
        {
            var result = await _codeGeneratorRepository.CreateFolderAsync(dto);
            return Json(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFolderById(int id)
        {
            var result = await _codeGeneratorRepository.GetFolderByIdAsync(id);
            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFolder([FromBody] UpdateFolderEntidad dto)
        {
            var result = await _codeGeneratorRepository.UpdateFolderAsync(dto);
            return Json(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFolder(int id)
        {
            var success = await _codeGeneratorRepository.DeleteFolderAsync(id);
            return Json(new { success });
        }

        // Métodos para Archivo
        [HttpPost]
        public async Task<IActionResult> CreateArchivo([FromBody] CreateArchivoEntidad dto)
        {
            var result = await _codeGeneratorRepository.CreateArchivoAsync(dto);
            return Json(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArchivoById(int id)
        {
            var result = await _codeGeneratorRepository.GetArchivoByIdAsync(id);
            return Json(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateArchivo([FromBody] UpdateArchivoEntidad dto)
        {
            var result = await _codeGeneratorRepository.UpdateArchivoAsync(dto);
            return Json(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArchivo(int id)
        {
            var success = await _codeGeneratorRepository.DeleteArchivoAsync(id);
            return Json(new { success });
        }
    }
}
