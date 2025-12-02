namespace FolderView.Dapper.AdministracionBot.Entidades
{
    public class LogOperacionEntidad
    {
        public int IdLog { get; set; }
        public DateTime FechaEjecucion { get; set; }
        public int IdUsuario { get; set; }
        public int IdOperacion { get; set; }
        public long? TelegramChatId { get; set; }
        public string TelegramUsername { get; set; }
        public string Parametros { get; set; }
        public string Resultado { get; set; }
        public string MensajeError { get; set; }
        public int? DuracionMs { get; set; }
        public string IpOrigen { get; set; }

        // Propiedades de navegación
        public string Usuario { get; set; }
        public string Email { get; set; }
        public string Operacion { get; set; }
        public string Comando { get; set; }
        public string DescripcionOperacion { get; set; }
        public string Modulo { get; set; }

        // Propiedades adicionales para vistas de usuario
        public bool Exito { get; set; }
        public DateTime FechaHora { get; set; }
        public int? TiempoEjecucion { get; set; }
        public string IpAddress { get; set; }
        public string ErrorDetalle { get; set; }
    }
}
