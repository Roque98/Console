namespace FolderView.Dapper.Entidades
{
    // CreateFolderEntidad.cs
    public class CreateFolderEntidad
    {
        public int IdTipoArchivo { get; set; }
        public int IdProyecto { get; set; }
        public int NumeroVersion { get; set; }
        public int? IdFolderPadre { get; set; }
    }

    // ReadFolderEntidad.cs
    public class ReadFolderEntidad
    {
        public int Id { get; set; }
        public int IdTipoArchivo { get; set; }
        public int IdProyecto { get; set; }
        public int NumeroVersion { get; set; }
        public int? IdFolderPadre { get; set; }
        public ReadTipoArchivosEntidad TipoArchivo { get; set; }
        public ReadProyectoEntidad Proyecto { get; set; }
        public List<ReadArchivoEntidad> Archivos { get; set; }
    }

    // UpdateFolderEntidad.cs
    public class UpdateFolderEntidad
    {
        public int Id { get; set; }
        public int IdTipoArchivo { get; set; }
        public int IdProyecto { get; set; }
        public int NumeroVersion { get; set; }
        public int? IdFolderPadre { get; set; }
    }

    // DeleteFolderEntidad.cs
    public class DeleteFolderEntidad
    {
        public int Id { get; set; }
    }
}
