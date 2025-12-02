namespace FolderView.Dapper.AdministracionBot.Entidades
{
    public class KnowledgeCategoryEntidad
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? TotalEntradas { get; set; }
    }
}
