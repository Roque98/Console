namespace FolderView.Dapper.SmartPromptLibrary.Entidades
{
    public class EtiquetaPromptEntidad
    {
        public int IdEtiquetaPrompt { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Propiedades adicionales para vistas
        public int? TotalPrompts { get; set; }
    }
}
