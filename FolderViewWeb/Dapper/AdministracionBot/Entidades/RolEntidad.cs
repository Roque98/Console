namespace FolderView.Dapper.AdministracionBot.Entidades
{
    public class RolEntidad
    {
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
        public int? TotalUsuarios { get; set; }
        public int? PermisosAsignados { get; set; }
    }
}
