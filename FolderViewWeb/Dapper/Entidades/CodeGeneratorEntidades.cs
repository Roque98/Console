namespace FolderView.Dapper.Entidades
{

    // CreateTipoArchivoTipoProyectoEntidad.cs
    public class CreateTipoArchivoTipoProyectoEntidad
    {
        public int Id { get; set; }
        public int IdTipoProyecto { get; set; }
        public int IdTipoArchivo { get; set; }
        public int? IdPadre { get; set; }
        public List<CreateTipoArchivoTipoProyectoEntidad> Hijos { get; set; } = new List<CreateTipoArchivoTipoProyectoEntidad>();
    }

    // ReadTipoArchivoTipoProyectoEntidad.cs
    public class ReadTipoArchivoTipoProyectoEntidad
    {
        public int Id { get; set; }
        public int IdTipoProyecto { get; set; }
        public int IdTipoArchivo { get; set; }
        public int? IdPadre { get; set; }
        public List<ReadTipoArchivoTipoProyectoEntidad> Hijos { get; set; } = new List<ReadTipoArchivoTipoProyectoEntidad>();
    }

    // UpdateTipoArchivoTipoProyectoEntidad.cs
    public class UpdateTipoArchivoTipoProyectoEntidad
    {
        public int Id { get; set; }
        public int IdTipoArchivo { get; set; }
        public int IdTipoProyecto { get; set; }
        public int? IdPadre { get; set; }
    }

    // DeleteTipoArchivoTipoProyectoEntidad.cs
    public class DeleteTipoArchivoTipoProyectoEntidad
    {
        public int Id { get; set; }
    }
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

    // CreateTipoArchivosEntidad.cs
    public class CreateTipoArchivosEntidad
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    // ReadTipoArchivosEntidad.cs
    public class ReadTipoArchivosEntidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    // UpdateTipoArchivosEntidad.cs
    public class UpdateTipoArchivosEntidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    // DeleteTipoArchivosEntidad.cs
    public class DeleteTipoArchivosEntidad
    {
        public int Id { get; set; }
    }

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

    // CreateArchivoEntidad.cs
    public class CreateArchivoEntidad
    {
        public int IdFolder { get; set; }
        public string Extension { get; set; }
        public string Documentacion { get; set; }
        public string Contenido { get; set; }
        public string Path { get; set; }
    }

    // ReadArchivoEntidad.cs
    public class ReadArchivoEntidad
    {
        public int Id { get; set; }
        public int IdFolder { get; set; }
        public string Extension { get; set; }
        public string Documentacion { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Path { get; set; }
        public ReadFolderEntidad Folder { get; set; }
    }

    // UpdateArchivoEntidad.cs
    public class UpdateArchivoEntidad
    {
        public int Id { get; set; }
        public int IdFolder { get; set; }
        public string Extension { get; set; }
        public string Documentacion { get; set; }
        public string Contenido { get; set; }
        public string Path { get; set; }
    }

    // DeleteArchivoEntidad.cs
    public class DeleteArchivoEntidad
    {
        public int Id { get; set; }
    }

}
