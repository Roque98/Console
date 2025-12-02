namespace FolderView.Dapper.AdministracionBot.Entidades
{
    public class ModuloEntidad
    {
        public int IdModulo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Icono { get; set; }
        public int Orden { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? TotalOperaciones { get; set; }
    }
}
