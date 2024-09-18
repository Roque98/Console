namespace FolderView.Dapper.Entidades
{
    // CreateProyectoEntidad.cs
    public class CreateProyectoEntidad
    {
        public int IdTipoProyecto { get; set; }
        public string NombreProyecto { get; set; }
    }

    // ReadProyectoEntidad.cs
    public class ReadProyectoEntidad
    {
        public int Id { get; set; }
        public int IdTipoProyecto { get; set; }
        public string NombreProyecto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public ReadTipoProyectoEntidad TipoProyecto { get; set; }
    }

    // UpdateProyectoEntidad.cs
    public class UpdateProyectoEntidad
    {
        public int Id { get; set; }
        public int IdTipoProyecto { get; set; }
        public string NombreProyecto { get; set; }
    }

    // DeleteProyectoEntidad.cs
    public class DeleteProyectoEntidad
    {
        public int Id { get; set; }
    }

}
