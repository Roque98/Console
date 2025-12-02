namespace FolderView.Dapper.AdministracionBot.Entidades
{
    public class KnowledgeEntryEntidad
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Keywords { get; set; }
        public string RelatedCommands { get; set; }
        public int Priority { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedBy { get; set; }

        // Propiedades de navegación
        public string Category { get; set; }
        public string CategoryDisplayName { get; set; }
        public string CategoryIcon { get; set; }
    }
}
