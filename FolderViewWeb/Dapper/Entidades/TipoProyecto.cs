namespace FolderView.Dapper.Entidades
{
    // CreateTipoProyectoEntidad.cs
    public class CreateTipoProyectoEntidad
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
    }

    // ReadTipoProyectoEntidad.cs
    public class ReadTipoProyectoEntidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UrlImagen { get; set; }
        public List<ReadProyectoEntidad> Proyectos { get; set; }
        public List<ReadTipoArchivo_TipoProyectoEntidad> TiposArchivosRelacion = new List<ReadTipoArchivo_TipoProyectoEntidad>();
    }

    // UpdateTipoProyectoEntidad.cs
    public class UpdateTipoProyectoEntidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
    }

    // DeleteTipoProyectoEntidad.cs
    public class DeleteTipoProyectoEntidad
    {
        public int Id { get; set; }
    }
}
