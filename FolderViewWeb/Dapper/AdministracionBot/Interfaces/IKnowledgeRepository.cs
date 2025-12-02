using FolderView.Dapper.AdministracionBot.Entidades;

namespace FolderView.Dapper.AdministracionBot.Interfaces
{
    public interface IKnowledgeRepository
    {
        // Categorías
        Task<List<KnowledgeCategoryEntidad>> GetAllCategoriesAsync();
        Task<KnowledgeCategoryEntidad> GetCategoryByIdAsync(int id);
        Task<int> CreateCategoryAsync(KnowledgeCategoryEntidad category);
        Task<bool> UpdateCategoryAsync(KnowledgeCategoryEntidad category);

        // Entradas
        Task<List<KnowledgeEntryEntidad>> GetAllEntriesAsync();
        Task<List<KnowledgeEntryEntidad>> GetEntriesByCategoryAsync(int categoryId);
        Task<KnowledgeEntryEntidad> GetEntryByIdAsync(int id);
        Task<int> CreateEntryAsync(KnowledgeEntryEntidad entry);
        Task<bool> UpdateEntryAsync(KnowledgeEntryEntidad entry);
        Task<bool> DeleteEntryAsync(int id);
    }
}
