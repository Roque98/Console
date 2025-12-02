namespace FolderView.Dapper.AdministracionBot.Entidades
{
    public class RolIAEntidad
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
        public int? TotalUsuarios { get; set; }
        public int? TotalGerencias { get; set; }
    }
}
