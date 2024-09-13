﻿namespace FolderView.Dapper.Entidades
{
    public class PromptTemplateEntidad
    {
        public int Id { get; set; }
        public string Prompt { get; set; }
        public int Orden { get; set; }
        public int IdTipoProyecto { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public TipoProyectoEntidad TipoProyecto { get; set; }
    }
}