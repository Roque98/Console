namespace FolderView.Dapper.Entidades
{
    public class CreateTipoProyectoEntidad
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
    }

    public class ReadTipoProyectoEntidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UrlImagen { get; set; }
    }

    public class UpdateTipoProyectoEntidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string UrlImagen { get; set; }
    }

    public class DeleteTipoProyectoEntidad
    {
        public int Id { get; set; }
    }

    public class CreateTipoArchivosEntidad
    {
        public string Nombre { get; set; }
        //public string Descripcion { get; set; }
    }

    public class ReadTipoArchivosEntidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class UpdateTipoArchivosEntidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class DeleteTipoArchivosEntidad
    {
        public int Id { get; set; }
    }

    public class CreateTipoArchivoTipoProyectoEntidad
    {
        public int IdTipoProyecto { get; set; }
        public int IdTipoArchivo { get; set; }
        public int? IdPadre { get; set; }
    }

    public class ReadTipoArchivoTipoProyectoEntidad
    {
        public int Id { get; set; }
        public int IdTipoProyecto { get; set; }
        public int IdTipoArchivo { get; set; }
        public int? IdPadre { get; set; }
    }

    public class UpdateTipoArchivoTipoProyectoEntidad
    {
        public int Id { get; set; }
        public int IdTipoProyecto { get; set; }
        public int IdTipoArchivo { get; set; }
        public int? IdPadre { get; set; }
    }

    public class DeleteTipoArchivoTipoProyectoEntidad
    {
        public int Id { get; set; }
    }

    public class CreateProyectoEntidad
    {
        public int IdTipoProyecto { get; set; }
        public string NombreProyecto { get; set; }
    }

    public class ReadProyectoEntidad
    {
        public int Id { get; set; }
        public int IdTipoProyecto { get; set; }
        public string NombreProyecto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }

    public class UpdateProyectoEntidad
    {
        public int Id { get; set; }
        public int IdTipoProyecto { get; set; }
        public string NombreProyecto { get; set; }
    }

    public class DeleteProyectoEntidad
    {
        public int Id { get; set; }
    }

    public class CreateFolderEntidad
    {
        public int IdTipoArchivo { get; set; }
        public int IdProyecto { get; set; }
        public int NumeroVersion { get; set; }
        public int? IdFolderPadre { get; set; }
    }

    public class ReadFolderEntidad
    {
        public int Id { get; set; }
        public int IdTipoArchivo { get; set; }
        public int IdProyecto { get; set; }
        public int NumeroVersion { get; set; }
        public int? IdFolderPadre { get; set; }
    }

    public class UpdateFolderEntidad
    {
        public int Id { get; set; }
        public int IdTipoArchivo { get; set; }
        public int IdProyecto { get; set; }
        public int NumeroVersion { get; set; }
        public int? IdFolderPadre { get; set; }
    }

    public class DeleteFolderEntidad
    {
        public int Id { get; set; }
    }

    public class CreateArchivoEntidad
    {
        public int IdFolder { get; set; }
        public string Extension { get; set; }
        public string Documentacion { get; set; }
        public string Contenido { get; set; }
        public string Path { get; set; }
    }

    public class ReadArchivoEntidad
    {
        public int Id { get; set; }
        public int IdFolder { get; set; }
        public string Extension { get; set; }
        public string Documentacion { get; set; }
        public string Contenido { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Path { get; set; }
    }

    public class UpdateArchivoEntidad
    {
        public int Id { get; set; }
        public int IdFolder { get; set; }
        public string Extension { get; set; }
        public string Documentacion { get; set; }
        public string Contenido { get; set; }
        public string Path { get; set; }
    }

    public class DeleteArchivoEntidad
    {
        public int Id { get; set; }
    }

}
