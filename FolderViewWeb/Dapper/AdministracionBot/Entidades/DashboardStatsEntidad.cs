namespace FolderView.Dapper.AdministracionBot.Entidades
{
    public class DashboardStatsEntidad
    {
        public int TotalUsuarios { get; set; }
        public int UsuariosConTelegram { get; set; }
        public int OperacionesHoy { get; set; }
        public int OperacionesDisponibles { get; set; }
        public int EntradasConocimiento { get; set; }
        public DateTime? UltimaActividad { get; set; }
    }

    public class OperacionResultadoEntidad
    {
        public string Resultado { get; set; }
        public int Cantidad { get; set; }
    }

    public class TopOperacionEntidad
    {
        public string Operacion { get; set; }
        public string Comando { get; set; }
        public int TotalEjecuciones { get; set; }
    }

    public class TopUsuarioEntidad
    {
        public string Usuario { get; set; }
        public int TotalOperaciones { get; set; }
        public DateTime UltimaActividad { get; set; }
    }

    public class TasaExitoEntidad
    {
        public DateTime Fecha { get; set; }
        public int Total { get; set; }
        public int Exitosas { get; set; }
        public decimal PorcentajeExito { get; set; }
    }
}
