namespace FolderView.Dapper.Entidades
{
    public class ProyectoEntidad
    {
        public int Id { get; set; }
        public int IdTipoProyecto { get; set; }
        public string NombreProyecto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public TipoProyectoEntidad TipoProyecto { get; set; }
    }
}
