using FolderView.Dapper.Interfaces;
using FolderView.ModelViews.FolderView;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FolderView.Controllers
{
    public class FolderViewController : Controller
    {
        private readonly IDirectoryRepository _directorioRepositorio;
        private readonly IArchivoRepository _archivoRepositorio;

        public FolderViewController(
            IDirectoryRepository  directorioRepositorio,
            IArchivoRepository archivoRepositorio
        )
        {
            _directorioRepositorio = directorioRepositorio;
            _archivoRepositorio = archivoRepositorio;
        }

        public async Task<IActionResult> IndexAsync()
        {
            try
            {
                var modelView = new FolderViewIndexMoldeView();
                var directorioRoot = await _directorioRepositorio.GetByIdAsync(3004);

                if (directorioRoot == null)
                {
                    return View();
                }

                await PopulateDirectorioAsync(directorioRoot);

                modelView.directorio = directorioRoot;
                modelView.dataJson = JsonConvert.SerializeObject(modelView.directorio);


                return View(modelView);
            }
            catch (Exception ex)
            {
                //log error
                return StatusCode(500, ex.Message);
            }

            
        }

        public async Task PopulateDirectorioAsync(Dapper.Entidades.DirectorioEntidad directorio)
        {

            directorio.Archivos = await _archivoRepositorio.GetByDirectorioIdAsync(directorio.DirectorioID);
            directorio.Subdirectorios = await _directorioRepositorio.GetByParentIdAsync(directorio.DirectorioID);

            foreach (var subdirectorio in directorio.Subdirectorios)
            {
               await PopulateDirectorioAsync(subdirectorio);
            }

        }
        
    }
}
