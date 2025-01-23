using FolderView.Dapper.Entidades;

namespace FolderView.Dapper.Interfaces
{
    public interface ICodeGeneratorIArchivoRepository
    {
        Task<List<CodeGeneratorArchivoEntidad>> CreateAsync(CodeGeneratorArchivoEntidad dto);
        Task<CodeGeneratorArchivoEntidad> GetByIdAsync(int id);
        Task<CodeGeneratorArchivoEntidad> UpdateAsync(CodeGeneratorArchivoEntidad dto);
        Task<CodeGeneratorArchivoEntidad> DeleteAsync(int id);

        Task<List<CodeGeneratorArchivoEntidad>> GetAllByIdProyectoAsync(int id);
        Task<List<CodeGeneratorArchivoEntidad>> GetAllByIdPrompTemplateAsync(int idPromptTemplate, int idProyecto);
        Task<List<CodeGeneratorArchivoEntidad>> GetAllByIdArchivoPadreAsync(int id);
    }
}