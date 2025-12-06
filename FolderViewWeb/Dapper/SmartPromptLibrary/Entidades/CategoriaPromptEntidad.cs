namespace FolderView.Dapper.SmartPromptLibrary.Entidades
{
    public class CategoriaPromptEntidad
    {
        public int IdCategoriaPrompt { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public string Color { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }

        // Propiedades adicionales para vistas
        public int? TotalPrompts { get; set; }
    }
}
