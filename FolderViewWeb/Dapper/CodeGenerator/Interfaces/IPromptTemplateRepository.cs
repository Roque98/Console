
using FolderView.Dapper.Entidades;

namespace FolderView.Dapper.Interfaces
{
    public interface IPromptTemplateRepository
    {
        Task<List<PromptTemplateEntidad>> CreateAsync(PromptTemplateEntidad dto);
        Task<PromptTemplateEntidad> GetByIdAsync(int id);
        Task<PromptTemplateEntidad> UpdateAsync(PromptTemplateEntidad dto);
        Task<PromptTemplateEntidad> DeleteAsync(int id);

        Task<List<PromptTemplateEntidad>> GetAllByIdTipoProyectoAsync(int id);
    }
}