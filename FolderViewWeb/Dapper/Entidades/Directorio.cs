using FolderView.Dapper.Utils;

namespace FolderView.Dapper.Entidades
{
    public class DirectorioEntidad
    {
        public int DirectorioID { get; set; }
        public string Nombre { get; set; }
        public long Tamano { get; set; }
        public string Ruta { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public int DirectorioParentID { get; set; }

        /* Populate */
        // Campo extra: Lista de archivos en este directorio
        public List<ArchivoEntidad> Archivos { get; set; }

        // Campo extra: Lista de subdirectorios
        public List<DirectorioEntidad> Subdirectorios { get; set; }

        /* Calculados */
        public bool DirectorioNoVacio
        {
            get
            {
                return (Archivos.Count + Subdirectorios.Count) != 0;
            }
        }

        public string TamanioFormateado
        {
            get
            {
                return Formatos.TamanioFormateado(Tamano);
            }
        }
    }

   
}
