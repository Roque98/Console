namespace FolderView.Dapper.Entidades
{
    public class CodeGeneratorArchivoEntidad
    {
        public int Id { get; set; }
        public int IdProyecto { get; set; }
        public string Extension { get; set; }
        public string Documentacion { get; set; }
        public string Contenido { get; set; }
        public string path { get; set; }
        public int Version { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? IdArchivoPadre { get; set; }
        public int idPromptTemplate { get; set; }
        public CodeGeneratorArchivoEntidad ArchivoPadre { get; set; }
        public ProyectoEntidad Proyecto { get; set; }
    }
}
