namespace FolderView.Dapper.SmartPromptLibrary.Entidades
{
    public class PromptEntidad
    {
        public int IdPrompt { get; set; }
        public int IdCategoriaPrompt { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string ContenidoMarkdown { get; set; }
        public string MensajeSistema { get; set; }
        public int Version { get; set; }
        public bool Activo { get; set; }
        public bool Favorito { get; set; }
        public int CantidadEjecuciones { get; set; }
        public DateTime? UltimaEjecucion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string CreadoPorUsuario { get; set; }

        // Propiedades de navegación
        public string CategoriaNombre { get; set; }
        public string CategoriaIcono { get; set; }
        public string CategoriaColor { get; set; }

        // Lista de etiquetas (para vistas con detalles)
        public List<EtiquetaPromptEntidad> Etiquetas { get; set; }
    }
}
