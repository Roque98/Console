using FolderView.Dapper.Entidades;
using FolderView.Dapper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FolderView.Controllers
{
    public class CodeGeneratorController : Controller
    {
        private readonly ICodeGeneratorRepository _codeGeneratorRepositorio;

        public CodeGeneratorController(ICodeGeneratorRepository codeGeneratorRepositorio)
        {
            _codeGeneratorRepositorio = codeGeneratorRepositorio;
        }

        [HttpGet("")]
        public async Task<IActionResult> tipoproyecto()
        {
            return View("TipoProyecto");
        }

        [HttpPost("api/tipoproyecto")]
        public async Task<IActionResult> CreateTipoProyecto([FromBody] CreateTipoProyectoEntidad dto)
        {
            var result = await _codeGeneratorRepositorio.CreateTipoProyectoAsync(dto);
            return Ok(result);
        }

        [HttpGet("api/tipoproyecto/{id}")]
        public async Task<IActionResult> GetTipoProyectoById(int id)
        {
            var result = await _codeGeneratorRepositorio.GetTipoProyectoByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("api/tipoproyectos")]
        public async Task<IActionResult> GetAllTipoProyecto()
        {
            var result = await _codeGeneratorRepositorio.GetAllTipoProyectoAsync();
            return Ok(result);
        }

        [HttpPut("api/tipoproyecto")]
        public async Task<IActionResult> UpdateTipoProyecto([FromBody] UpdateTipoProyectoEntidad dto)
        {
            var result = await _codeGeneratorRepositorio.UpdateTipoProyectoAsync(dto);
            return Ok(result);
        }

        [HttpDelete("api/tipoproyecto/{id}")]
        public async Task<IActionResult> DeleteTipoProyecto(int id)
        {
            var result = await _codeGeneratorRepositorio.DeleteTipoProyectoAsync(id);
            return Ok(result);
        }

        [HttpPost("api/tipoarchivos")]
        public async Task<IActionResult> CreateTipoArchivos([FromBody] CreateTipoArchivosEntidad dto)
        {
            var result = await _codeGeneratorRepositorio.CreateTipoArchivosAsync(dto);
            return Ok(result);
        }

        [HttpGet("api/tipoarchivos/{id}")]
        public async Task<IActionResult> GetTipoArchivosById(int id)
        {
            var result = await _codeGeneratorRepositorio.GetTipoArchivosByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("api/tipoarchivos")]
        public async Task<IActionResult> GetAllTipoArchivos()
        {
            var result = await _codeGeneratorRepositorio.GetAllTipoArchivosAsync();
            return Ok(result);
        }

        [HttpPut("api/tipoarchivos")]
        public async Task<IActionResult> UpdateTipoArchivos([FromBody] UpdateTipoArchivosEntidad dto)
        {
            var result = await _codeGeneratorRepositorio.UpdateTipoArchivosAsync(dto);
            return Ok(result);
        }

        [HttpDelete("api/tipoarchivos/{id}")]
        public async Task<IActionResult> DeleteTipoArchivos(int id)
        {
            var result = await _codeGeneratorRepositorio.DeleteTipoArchivosAsync(id);
            return Ok(result);
        }

        [HttpPost("api/tipoarchivotipoproyecto")]
        public async Task<IActionResult> CreateTipoArchivoTipoProyecto([FromBody] CreateTipoArchivoTipoProyectoEntidad dto)
        {
            var result = await _codeGeneratorRepositorio.CreateTipoArchivoTipoProyectoAsync(dto);
            return Ok(result);
        }

        [HttpGet("api/tipoarchivotipoproyecto/{id}")]
        public async Task<IActionResult> GetTipoArchivoTipoProyectoById(int id)
        {
            var result = await _codeGeneratorRepositorio.GetTipoArchivoTipoProyectoByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("api/tipoarchivotipoproyectos")]
        public async Task<IActionResult> GetAllTipoArchivoTipoProyecto()
        {
            var result = await _codeGeneratorRepositorio.GetAllTipoArchivoTipoProyectoAsync();
            return Ok(result);
        }

        [HttpPut("api/tipoarchivotipoproyecto")]
        public async Task<IActionResult> UpdateTipoArchivoTipoProyecto([FromBody] UpdateTipoArchivoTipoProyectoEntidad dto)
        {
            var result = await _codeGeneratorRepositorio.UpdateTipoArchivoTipoProyectoAsync(dto);
            return Ok(result);
        }

        [HttpDelete("api/tipoarchivotipoproyecto/{id}")]
        public async Task<IActionResult> DeleteTipoArchivoTipoProyecto(int id)
        {
            var result = await _codeGeneratorRepositorio.DeleteTipoArchivoTipoProyectoAsync(id);
            return Ok(result);
        }

        [HttpPost("api/proyecto")]
        public async Task<IActionResult> CreateProyecto([FromBody] CreateProyectoEntidad dto)
        {
            var result = await _codeGeneratorRepositorio.CreateProyectoAsync(dto);
            return Ok(result);
        }

        [HttpGet("api/proyecto/{id}")]
        public async Task<IActionResult> GetProyectoById(int id)
        {
            var result = await _codeGeneratorRepositorio.GetProyectoByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("api/proyectos")]
        public async Task<IActionResult> GetAllProyecto()
        {
            var result = await _codeGeneratorRepositorio.GetAllProyectoAsync();
            return Ok(result);
        }

        [HttpPut("api/proyecto")]
        public async Task<IActionResult> UpdateProyecto([FromBody] UpdateProyectoEntidad dto)
        {
            var result = await _codeGeneratorRepositorio.UpdateProyectoAsync(dto);
            return Ok(result);
        }

        [HttpDelete("api/proyecto/{id}")]
        public async Task<IActionResult> DeleteProyecto(int id)
        {
            var result = await _codeGeneratorRepositorio.DeleteProyectoAsync(id);
            return Ok(result);
        }

        [HttpPost("api/folder")]
        public async Task<IActionResult> CreateFolder([FromBody] CreateFolderEntidad dto)
        {
            var result = await _codeGeneratorRepositorio.CreateFolderAsync(dto);
            return Ok(result);
        }

        [HttpGet("api/folder/{id}")]
        public async Task<IActionResult> GetFolderById(int id)
        {
            var result = await _codeGeneratorRepositorio.GetFolderByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("api/folders")]
        public async Task<IActionResult> GetAllFolder()
        {
            var result = await _codeGeneratorRepositorio.GetAllFolderAsync();
            return Ok(result);
        }

        [HttpPut("api/folder")]
        public async Task<IActionResult> UpdateFolder([FromBody] UpdateFolderEntidad dto)
        {
            var result = await _codeGeneratorRepositorio.UpdateFolderAsync(dto);
            return Ok(result);
        }

        [HttpDelete("api/folder/{id}")]
        public async Task<IActionResult> DeleteFolder(int id)
        {
            var result = await _codeGeneratorRepositorio.DeleteFolderAsync(id);
            return Ok(result);
        }

        [HttpPost("api/archivo")]
        public async Task<IActionResult> CreateArchivo([FromBody] CreateArchivoEntidad dto)
        {
            var result = await _codeGeneratorRepositorio.CreateArchivoAsync(dto);
            return Ok(result);
        }

        [HttpGet("api/archivo/{id}")]
        public async Task<IActionResult> GetArchivoById(int id)
        {
            var result = await _codeGeneratorRepositorio.GetArchivoByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("api/archivos")]
        public async Task<IActionResult> GetAllArchivo()
        {
            var result = await _codeGeneratorRepositorio.GetAllArchivoAsync();
            return Ok(result);
        }

        [HttpPut("api/archivo")]
        public async Task<IActionResult> UpdateArchivo([FromBody] UpdateArchivoEntidad dto)
        {
            var result = await _codeGeneratorRepositorio.UpdateArchivoAsync(dto);
            return Ok(result);
        }

        [HttpDelete("api/archivo/{id}")]
        public async Task<IActionResult> DeleteArchivo(int id)
        {
            var result = await _codeGeneratorRepositorio.DeleteArchivoAsync(id);
            return Ok(result);
        }
    }
}