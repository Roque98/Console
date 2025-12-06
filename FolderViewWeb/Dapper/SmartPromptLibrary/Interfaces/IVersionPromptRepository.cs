using FolderView.Dapper.SmartPromptLibrary.Entidades;

namespace FolderView.Dapper.SmartPromptLibrary.Interfaces
{
    public interface IVersionPromptRepository
    {
        /// <summary>
        /// Obtiene todas las versiones de un prompt específico
        /// </summary>
        Task<List<VersionPromptEntidad>> GetVersionesByPromptIdAsync(int idPrompt);

        /// <summary>
        /// Obtiene una versión específica de un prompt
        /// </summary>
        Task<VersionPromptEntidad> GetVersionByIdAsync(int idVersionPrompt);

        /// <summary>
        /// Obtiene una versión específica por número de versión
        /// </summary>
        Task<VersionPromptEntidad> GetVersionByNumeroAsync(int idPrompt, int numeroVersion);

        /// <summary>
        /// Obtiene la versión actual de un prompt
        /// </summary>
        Task<VersionPromptEntidad> GetVersionActualAsync(int idPrompt);

        /// <summary>
        /// Crea una nueva versión de un prompt (usando stored procedure)
        /// </summary>
        Task<(bool success, int numeroVersion, string error)> CrearNuevaVersionAsync(
            int idPrompt,
            string titulo,
            string descripcion,
            string contenidoMarkdown,
            string mensajeSistema,
            string mensajeCambio,
            string usuario);

        /// <summary>
        /// Restaura una versión anterior de un prompt (usando stored procedure)
        /// </summary>
        Task<(bool success, int numeroVersion, string error)> RestaurarVersionAsync(
            int idPrompt,
            int numeroVersion,
            string usuario);

        /// <summary>
        /// Compara dos versiones de un prompt
        /// </summary>
        Task<(VersionPromptEntidad version1, VersionPromptEntidad version2)> CompararVersionesAsync(
            int idPrompt,
            int numeroVersion1,
            int numeroVersion2);

        /// <summary>
        /// Elimina versiones antiguas de un prompt (mantiene las últimas N versiones)
        /// </summary>
        Task<int> LimpiarVersionesAntiguasAsync(int idPrompt, int mantenerUltimas = 10);
    }
}
