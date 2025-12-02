namespace FolderView.Dapper.AdministracionBot.Entidades
{
    public class RolOperacionEntidad
    {
        public int IdRolOperacion { get; set; }
        public int IdRol { get; set; }
        public int IdOperacion { get; set; }
        public bool Permitido { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public int? UsuarioAsignacion { get; set; }
        public bool Activo { get; set; }

        // Propiedades de navegación
        public string NombreOperacion { get; set; }
        public string Comando { get; set; }
        public string Descripcion { get; set; }
        public string NivelCriticidad { get; set; }
        public string Modulo { get; set; }
        public string IconoModulo { get; set; }
    }
}
