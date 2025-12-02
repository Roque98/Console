namespace FolderView.Dapper.Entidades
{
    public class ProyectoEntidad
    {
        public int id { get; set; }
        public int idTipoProyecto { get; set; }
        public string nombreProyecto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public TipoProyectoEntidad TipoProyecto { get; set; }
    }
}
