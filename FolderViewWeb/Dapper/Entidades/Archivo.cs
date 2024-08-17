using FolderView.Dapper.Utils;

namespace FolderView.Dapper.Entidades
{
    public class ArchivoEntidad
    {
        public int ArchivoID { get; set; }
        public string Nombre { get; set; }
        public long Tamano { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string Ruta { get; set; }
        public string Extension { get; set; }
        public int DirectorioID { get; set; }
        /* Calculados */
        public string TamanioFormateado
        {
            get
            {
                return Formatos.TamanioFormateado(Tamano);
            }
        }
    }
}
