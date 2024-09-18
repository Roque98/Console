namespace FolderView.Dapper.Entidades
{
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
