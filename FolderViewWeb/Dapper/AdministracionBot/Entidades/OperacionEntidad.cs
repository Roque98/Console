namespace FolderView.Dapper.AdministracionBot.Entidades
{
    public class OperacionEntidad
    {
        public int IdOperacion { get; set; }
        public int IdModulo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Comando { get; set; }
        public bool RequiereParametros { get; set; }
        public string ParametrosEjemplo { get; set; }
        public string NivelCriticidad { get; set; }
        public int Orden { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Propiedades de navegación
        public string Modulo { get; set; }
        public string IconoModulo { get; set; }
        public int? TotalUsos { get; set; }
        public int? RolesConPermiso { get; set; }
        public int? TotalEjecuciones { get; set; }
        public int? Exitosas { get; set; }
        public int? Errores { get; set; }

        // Propiedades adicionales para permisos de usuario
        public string ModuloIcono { get; set; }
        public string TipoPermiso { get; set; }
        public bool TienePoder { get; set; }
    }
}
