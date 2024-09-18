namespace FolderView.Dapper.Entidades
{
    public class PromptsEntidad
    {
        public int Id { get; set; }
        public string Prompt { get; set; }
        public int IdTipoArchivoTipoProyecto { get; set; }

        public ReadTipoArchivo_TipoProyectoEntidad TipoArchivoTipoProyecto { get; set; }
        public List<InputPromptEntidad> InputsPrompt { get; set; }
    }
}
