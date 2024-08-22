using FolderView.Dapper.Entidades;
using FolderView.Dapper.Entidades.CodeViwer;
using FolderView.Dapper.Interfaces;
using FolderView.ModelViews.FolderView;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FolderView.Controllers
{
    public class CodeGeneratorViwerController : Controller
    {
        private readonly IDirectoryRepository _directorioRepositorio;
        private readonly IArchivoRepository _archivoRepositorio;
        private readonly ICodeGeneratorRepository _codeGeneratorRepository;

        public CodeGeneratorViwerController(
            IDirectoryRepository directorioRepositorio,
            IArchivoRepository archivoRepositorio,
            ICodeGeneratorRepository codeGeneratorRepository
        )
        {
            _directorioRepositorio = directorioRepositorio;
            _archivoRepositorio = archivoRepositorio;
            _codeGeneratorRepository = codeGeneratorRepository;
        }

        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                // URL de la API
                string apiUrl = "http://127.0.0.1:5000/coms/getAll"; // Reemplaza con la URL de la API real

                // Crear un cliente HTTP
                using HttpClient client = new HttpClient();

                // Crear el objeto para el cuerpo de la solicitud
                var requestBody = new
                {
                    // Agrega aquí las propiedades necesarias para la API
                    script_table = @"
                                 USE [catalogos]
                                GO

                                CREATE TABLE [dbo].[prtgDevices](
                                    [idPrtg] [int] NULL,
                                    [idObj] [int] NULL,
                                    [device] [varchar](50) NULL,
                                    [host] [varchar](50) NULL,
                                    [tags] [varchar](50) NULL
                                ) ON [PRIMARY]
                                GO
                    "
                };            

                // Serializar el cuerpo en JSON
                var jsonContent = JsonContent.Create(requestBody);

                // Realizar la petición POST a la API con el cuerpo
                var response = await client.PostAsync(apiUrl, jsonContent);

                // Verificar si la petición fue exitosa
                if (response.IsSuccessStatusCode)
                {
                    // Deserializar la respuesta JSON en una lista de FileData
                    var files = await response.Content.ReadFromJsonAsync<List<FileData>>();

                    // Ahora puedes usar el arreglo files en tu lógica
                    return View(files);
                }
                else
                {
                    // Manejar el caso en que la respuesta no sea exitosa
                    return StatusCode((int)response.StatusCode, "Error al obtener datos de la API");
                }
            }
            catch (Exception ex)
            {
                // Loguear el error
                // Puedes usar un sistema de logging aquí
                return StatusCode(500, ex.Message);
            }
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


