using FolderView.Dapper.Entidades;
using FolderView.Dapper.Entidades.CodeViwer;
using FolderView.Dapper.Interfaces;
using FolderView.Dapper.Repositorios;
using FolderView.ModelViews.FolderView;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FolderView.Controllers
{
    public class CodeGeneratorViewerController : Controller
    {
        IProyectoRepository _IProyectoRepository;
        ITipoProyectoRepository _TipoProyectoRepository;
        IPromptTemplateRepository _promptTemplateRepository;
        IParametrosPromptTemplateRepository _parametrosPromptTemplateRepository;

        public CodeGeneratorViewerController(
            IProyectoRepository IProyectoRepository,
            ITipoProyectoRepository TipoProyectoRepository,
            IPromptTemplateRepository promptTemplateRepository,
            IParametrosPromptTemplateRepository parametrosPromptTemplateRepository
        )
        {
            _IProyectoRepository= IProyectoRepository;
            _TipoProyectoRepository= TipoProyectoRepository;
            _promptTemplateRepository = promptTemplateRepository;
            _parametrosPromptTemplateRepository = parametrosPromptTemplateRepository;
        }

        public async Task<IActionResult> IndexAsync(int idProyecto)
        {

            return View();
        }


        public async Task<IActionResult> Index2(int idProyecto)
        {

            var proyecto = await _IProyectoRepository.GetByIdAsync(idProyecto);

            if (proyecto != null)
            {
                proyecto.TipoProyecto = await _TipoProyectoRepository.GetByIdAsync(proyecto.IdTipoProyecto);

                if (proyecto.TipoProyecto != null)
                {
                    proyecto.TipoProyecto.promptTemplates = await _promptTemplateRepository.GetAllByIdTipoProyectoAsync(proyecto.IdTipoProyecto);

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