namespace FolderView.Dapper.Entidades
{
    // CreateTipoArchivosEntidad.cs
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
}
