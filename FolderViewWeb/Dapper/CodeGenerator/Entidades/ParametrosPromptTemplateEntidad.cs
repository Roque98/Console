namespace FolderView.Dapper.Entidades
{
    public class ParametrosPromptTemplateEntidad
    {
        public int Id { get; set; }
        public string NombreParametro { get; set; }
        public int IdPromptTemplate { get; set; }
        public PromptTemplateEntidad PromptTemplate { get; set; }
    }
}
