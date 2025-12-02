namespace FolderView.Dapper.AdministracionBot.Entidades
{
    public class UsuarioTelegramEntidad
    {
        public int IdUsuarioTelegram { get; set; }
        public int IdUsuario { get; set; }
        public long TelegramChatId { get; set; }
        public string TelegramUsername { get; set; }
        public string TelegramFirstName { get; set; }
        public string TelegramLastName { get; set; }
        public string Alias { get; set; }
        public bool EsPrincipal { get; set; }
        public string Estado { get; set; }
        public bool Verificado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime? FechaVerificacion { get; set; }
        public DateTime? FechaUltimaActividad { get; set; }
        public bool NotificacionesActivas { get; set; }
        public string CodigoVerificacion { get; set; }
        public int? IntentosVerificacion { get; set; }
        public bool Activo { get; set; }

        // Propiedades de navegación
        public string Usuario { get; set; }
        public string Email { get; set; }
        public int? HorasInactivo { get; set; }
        public int? HorasSinVerificar { get; set; }
        public int? TotalCuentas { get; set; }

        // Alias para compatibilidad con vistas
        public DateTime FechaCreacion
        {
            get => FechaRegistro;
            set => FechaRegistro = value;
        }
    }
}
