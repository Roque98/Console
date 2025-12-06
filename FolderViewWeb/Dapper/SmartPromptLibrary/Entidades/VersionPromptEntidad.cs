namespace FolderView.Dapper.SmartPromptLibrary.Entidades
{
    public class VersionPromptEntidad
    {
        public int IdVersionPrompt { get; set; }
        public int IdPrompt { get; set; }
        public int NumeroVersion { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string ContenidoMarkdown { get; set; }
        public string MensajeSistema { get; set; }
        public string MensajeCambio { get; set; }
        public bool EsVersionActual { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string CreadoPorUsuario { get; set; }

        // Propiedades calculadas para la UI
        public string TituloPrompt { get; set; }  // Título del prompt principal
        public string UsuarioAbreviado => !string.IsNullOrEmpty(CreadoPorUsuario) && CreadoPorUsuario.Length > 20
            ? CreadoPorUsuario.Substring(0, 17) + "..."
            : CreadoPorUsuario;
    }
}
