namespace FolderView.Dapper.AdministracionBot.Entidades
{
    public class UsuarioOperacionEntidad
    {
        public int IdUsuarioOperacion { get; set; }
        public int IdUsuario { get; set; }
        public int IdOperacion { get; set; }
        public bool Permitido { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public int? UsuarioAsignacion { get; set; }
        public string Observaciones { get; set; }
        public bool Activo { get; set; }

        // Propiedades de navegación
        public string Usuario { get; set; }
        public string Rol { get; set; }
        public string Operacion { get; set; }
        public string Comando { get; set; }
    }
}
