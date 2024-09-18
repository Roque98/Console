namespace FolderView.Dapper.Entidades
{
    // CreateTipoArchivo_TipoProyectoEntidad.cs
    public class CreateTipoArchivo_TipoProyectoEntidad
    {
        public int IdTipoProyecto { get; set; }
        public int IdTipoArchivo { get; set; }
        public int? IdPadre { get; set; }
    }

    // ReadTipoArchivo_TipoProyectoEntidad.cs
    public class ReadTipoArchivo_TipoProyectoEntidad
    {
        public int Id { get; set; }
        public int IdTipoProyecto { get; set; }
        public int IdTipoArchivo { get; set; }
        public int? IdPadre { get; set; }
        public ReadTipoProyectoEntidad TipoProyecto { get; set; }
        public ReadTipoArchivosEntidad TipoArchivo { get; set; }
        public PromptsEntidad promptEntidad { get; set; }
    }

    // UpdateTipoArchivo_TipoProyectoEntidad.cs
    public class UpdateTipoArchivo_TipoProyectoEntidad
    {
        public int Id { get; set; }
        public int IdTipoProyecto { get; set; }
        public int IdTipoArchivo { get; set; }
        public int? IdPadre { get; set; }
    }

    // DeleteTipoArchivo_TipoProyectoEntidad.cs
    public class DeleteTipoArchivo_TipoProyectoEntidad
    {
        public int Id { get; set; }
    }

}
