namespace FolderView.Dapper.Entidades
{
    public class ParametrosPromptTemplateEntidad
    {
        public int Id { get; set; }
        public string nombre_parametro { get; set; }
        public int IdPromptTemplate { get; set; }
        public int IdPromptTemplateEntrada { get; set; }
        public string label { get; set; }
        public string placeholder { get; set; }
        public PromptTemplateEntidad PromptTemplate { get; set; }
    }
}
