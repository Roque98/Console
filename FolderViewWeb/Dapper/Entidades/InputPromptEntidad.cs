namespace FolderView.Dapper.Entidades
{
    public class InputPromptEntidad
    {
        public int Id { get; set; }
        public int IdTipoArchivoInput { get; set; }
        public int IdPrompt { get; set; }

        public ReadTipoArchivosEntidad TipoArchivoInput { get; set; }
    }
}
