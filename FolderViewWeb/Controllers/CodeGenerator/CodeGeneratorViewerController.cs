using FolderView.Dapper.Entidades;
using FolderView.Dapper.Entidades.CodeViwer;
using FolderView.Dapper.Interfaces;
using FolderView.Dapper.Repositorios;
using FolderView.Dapper.Utils;
using FolderView.ModelViews.FolderView;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FolderView.Controllers.CodeGenerator
{
    public class CodeGeneratorViewerController : Controller
    {
        IProyectoRepository _IProyectoRepository;
        ITipoProyectoRepository _TipoProyectoRepository;
        IPromptTemplateRepository _promptTemplateRepository;
        IParametrosPromptTemplateRepository _parametrosPromptTemplateRepository;
        ICodeGeneratorIArchivoRepository _codeGeneratorIArchivoRepository;

        public CodeGeneratorViewerController(
            IProyectoRepository IProyectoRepository,
            ITipoProyectoRepository TipoProyectoRepository,
            IPromptTemplateRepository promptTemplateRepository,
            IParametrosPromptTemplateRepository parametrosPromptTemplateRepository,
            ICodeGeneratorIArchivoRepository codeGeneratorIArchivoRepository
        )
        {
            _IProyectoRepository = IProyectoRepository;
            _TipoProyectoRepository = TipoProyectoRepository;
            _promptTemplateRepository = promptTemplateRepository;
            _parametrosPromptTemplateRepository = parametrosPromptTemplateRepository;
            _codeGeneratorIArchivoRepository = codeGeneratorIArchivoRepository;

        }

        public async Task<IActionResult> IndexAsync()
        {

            return View();
        }

        [HttpPost("api/codeGenerator/")]
        public async Task<IActionResult> GenerateAndSaveFile([FromBody] JsonElement bodyFromRequest, int IdProyecto, int idPromptTemplate, int? idArchivoPadre)
        {
            // 1. Hacer la petición al servicio externo
            var promptTemplate = bodyFromRequest.GetProperty("promptTemplate").GetString();
            var paramsElement = bodyFromRequest.GetProperty("parametros");

            // Puedes deserializar paramsElement a un Dictionary si es necesario
            var paramsDict = JsonSerializer.Deserialize<Dictionary<string, string>>(paramsElement.GetRawText());

            var requestBody = new
            {
                promptTemplate,
                @params = paramsDict
            };

            var url = "http://127.0.0.1:5000/generate/";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.PostAsJsonAsync(url, requestBody);

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, "Error al generar el archivo.");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<dynamic>(responseContent);


                // 2. Obtener todos los archivos del proyecto
                var archivosExistentes = await _codeGeneratorIArchivoRepository.GetAllByIdProyectoAsync(IdProyecto);

                // Buscar la versión más alta si hay un archivo con el mismo IdProyecto y IdArchivoPadre
                int nuevaVersion = 1;
                if (idArchivoPadre.HasValue)
                {
                    var archivoPadre = archivosExistentes
                        .Where(a => a.IdProyecto == IdProyecto && a.IdArchivoPadre == idArchivoPadre)
                        .OrderByDescending(a => a.Version)
                        .FirstOrDefault();

                    if (archivoPadre != null)
                    {
                        nuevaVersion = archivoPadre.Version + 1;
                    }
                }

                // Crear una nueva instancia del archivo
                var nuevoArchivo = new CodeGeneratorArchivoEntidad
                {
                    IdProyecto = IdProyecto,
                    Extension = (string)responseData.extension,
                    Documentacion = (string)responseData.documentacion,
                    Contenido = (string)responseData.contenido,
                    path = (string)responseData.path,
                    Version = nuevaVersion,
                    IdArchivoPadre = idArchivoPadre,
                    idPromptTemplate = idPromptTemplate
                };

                // Guardar el nuevo archivo en la base de datos
                await _codeGeneratorIArchivoRepository.CreateAsync(nuevoArchivo);

                // 3. Retornar todos los archivos del proyecto
                var archivosActualizados = await _codeGeneratorIArchivoRepository.GetAllByIdProyectoAsync(IdProyecto);

                // Escapar codigo html
                foreach (var archivo in archivosActualizados.Where(x => x.Extension.Contains("html")))
                {
                    archivo.Contenido = CodeGeneratorUtil.EscapeHtml(archivo.Contenido);
                }

                return Json(archivosActualizados);
            }
        }

 


        public async Task<IActionResult> Index2(int idProyecto)
        {

            var proyecto = await _IProyectoRepository.GetByIdAsync(idProyecto);

            if (proyecto != null)
            {
                proyecto.TipoProyecto = await _TipoProyectoRepository.GetByIdAsync(proyecto.idTipoProyecto);

                if (proyecto.TipoProyecto != null)
                {
                    proyecto.TipoProyecto.promptTemplates = await _promptTemplateRepository.GetAllByIdTipoProyectoAsync(proyecto.idTipoProyecto);

                    if (proyecto.TipoProyecto.promptTemplates != null)
                    {
                        foreach (var promptTemplates in proyecto.TipoProyecto.promptTemplates)
                        {
                            promptTemplates.ParametrosPromptTemplate = await _parametrosPromptTemplateRepository.GetAllByIdPromptTemplateAsync(promptTemplates.Id);
                        }
                    }
                }
            }

            return View(proyecto);

            //try
            //{
            //    // URL de la API
            //    string apiUrl = "http://127.0.0.1:5000/coms/getAll"; // Reemplaza con la URL de la API real

            //    // Crear un cliente HTTP
            //    using HttpClient client = new HttpClient();

            //    // Crear el objeto para el cuerpo de la solicitud
            //    var requestBody = new
            //    {
            //        // Agrega aquí las propiedades necesarias para la API
            //        script_table = @"
            //                     USE [catalogos]
            //                    GO

            //                    CREATE TABLE [dbo].[prtgDevices](
            //                        [idPrtg] [int] NULL,
            //                        [idObj] [int] NULL,
            //                        [device] [varchar](50) NULL,
            //                        [host] [varchar](50) NULL,
            //                        [tags] [varchar](50) NULL
            //                    ) ON [PRIMARY]
            //                    GO
            //        "
            //    };

            //    // Serializar el cuerpo en JSON
            //    var jsonContent = JsonContent.Create(requestBody);

            //    // Realizar la petición POST a la API con el cuerpo
            //    var response = await client.PostAsync(apiUrl, jsonContent);

            //    // Verificar si la petición fue exitosa
            //    if (response.IsSuccessStatusCode)
            //    {
            //        // Deserializar la respuesta JSON en una lista de FileData
            //        var files = await response.Content.ReadFromJsonAsync<List<FileData>>();

            //        // Ahora puedes usar el arreglo files en tu lógica
            //        return View(files);
            //    }
            //    else
            //    {
            //        // Manejar el caso en que la respuesta no sea exitosa
            //        return StatusCode((int)response.StatusCode, "Error al obtener datos de la API");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // Loguear el error
            //    // Puedes usar un sistema de logging aquí
            //    return StatusCode(500, ex.Message);
            //}
        }




    }
}