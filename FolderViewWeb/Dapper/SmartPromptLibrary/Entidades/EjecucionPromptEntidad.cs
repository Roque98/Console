namespace FolderView.Dapper.SmartPromptLibrary.Entidades
{
    public class EjecucionPromptEntidad
    {
        public int IdEjecucionPrompt { get; set; }
        public int IdPrompt { get; set; }
        public string PromptFinal { get; set; }
        public string RespuestaIA { get; set; }
        public bool Exitoso { get; set; }
        public string MensajeError { get; set; }
        public int? TokensUsados { get; set; }
        public decimal? CostoEstimado { get; set; }
        public int? TiempoRespuestaMs { get; set; }
        public int? IdProveedorIA { get; set; }
        public int? IdModeloIA { get; set; }
        public decimal? Temperatura { get; set; }
        public int? MaxTokens { get; set; }
        public DateTime FechaEjecucion { get; set; }
        public string EjecutadoPorUsuario { get; set; }

        // Propiedades de navegación
        public string PromptTitulo { get; set; }
        public string ProveedorNombre { get; set; }
        public string ModeloNombre { get; set; }

        // Lista de parámetros
        public List<ParametroEjecucionEntidad> Parametros { get; set; }
    }
}
