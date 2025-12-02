namespace FolderView.Dapper.Entidades
{
    public class TipoProyectoEntidad
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public DateTime? fechaCreacion { get; set; }
        public DateTime? fechaModificacion { get; set; }
        public string urlImagen { get; set; }
        public List<PromptTemplateEntidad> promptTemplates { get; set; }
    }
}
