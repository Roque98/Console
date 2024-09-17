
using FolderView.Dapper.Entidades;

namespace FolderView.Dapper.Interfaces
{
    public interface IParametrosPromptTemplateRepository
    {
        Task<List<ParametrosPromptTemplateEntidad>> CreateAsync(ParametrosPromptTemplateEntidad dto);
        Task<ParametrosPromptTemplateEntidad> GetByIdAsync(int id);
        Task<ParametrosPromptTemplateEntidad> UpdateAsync(ParametrosPromptTemplateEntidad dto);
        Task<ParametrosPromptTemplateEntidad> DeleteAsync(int id);

        Task<List<ParametrosPromptTemplateEntidad>> GetAllByIdPromptTemplateAsync(int id);
    }
}