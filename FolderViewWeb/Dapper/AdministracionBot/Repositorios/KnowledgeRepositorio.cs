using Dapper;
using FolderView.Dapper.AdministracionBot.Entidades;
using FolderView.Dapper.AdministracionBot.Interfaces;

namespace FolderView.Dapper.AdministracionBot.Repositorios
{
    public class KnowledgeRepositorio : IKnowledgeRepository
    {
        private readonly DapperContext _context;

        public KnowledgeRepositorio(DapperContext context)
        {
            _context = context;
        }

        // Categorías
        public async Task<List<KnowledgeCategoryEntidad>> GetAllCategoriesAsync()
        {
            var query = @"
                SELECT
                    c.id as Id,
                    c.name as Name,
                    c.display_name as DisplayName,
                    c.description as Description,
                    c.icon as Icon,
                    c.active as Active,
                    COUNT(e.id) as TotalEntradas
                FROM abcmasplus..knowledge_categories c
                LEFT JOIN abcmasplus..knowledge_entries e ON c.id = e.category_id AND e.active = 1
                WHERE c.active = 1
                GROUP BY c.id, c.name, c.display_name, c.description, c.icon, c.active
                ORDER BY c.display_name";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<KnowledgeCategoryEntidad>(query);
            return result.ToList();
        }

        public async Task<KnowledgeCategoryEntidad> GetCategoryByIdAsync(int id)
        {
            var query = @"
                SELECT
                    c.id as Id,
                    c.name as Name,
                    c.display_name as DisplayName,
                    c.description as Description,
                    c.icon as Icon,
                    c.active as Active,
                    c.created_at as CreatedAt
                FROM abcmasplus..knowledge_categories c
                WHERE c.id = @id";

            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<KnowledgeCategoryEntidad>(query, new { id });
        }

        public async Task<int> CreateCategoryAsync(KnowledgeCategoryEntidad category)
        {
            var query = @"
                INSERT INTO abcmasplus..knowledge_categories (name, display_name, description, icon, active)
                VALUES (@Name, @DisplayName, @Description, @Icon, 1);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(query, category);
        }

        public async Task<bool> UpdateCategoryAsync(KnowledgeCategoryEntidad category)
        {
            var query = @"
                UPDATE abcmasplus..knowledge_categories
                SET
                    name = @Name,
                    display_name = @DisplayName,
                    description = @Description,
                    icon = @Icon,
                    active = @Active
                WHERE id = @Id";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, category);
            return affected > 0;
        }

        // Entradas
        public async Task<List<KnowledgeEntryEntidad>> GetAllEntriesAsync()
        {
            var query = @"
                SELECT
                    e.id as Id,
                    e.question as Question,
                    e.answer as Answer,
                    e.keywords as Keywords,
                    e.related_commands as RelatedCommands,
                    e.priority as Priority,
                    c.name as Category,
                    c.display_name as CategoryDisplayName,
                    c.icon as CategoryIcon,
                    e.created_at as CreatedAt,
                    e.updated_at as UpdatedAt
                FROM abcmasplus..knowledge_entries e
                INNER JOIN abcmasplus..knowledge_categories c ON e.category_id = c.id
                WHERE e.active = 1 AND c.active = 1
                ORDER BY e.priority DESC, e.id";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<KnowledgeEntryEntidad>(query);
            return result.ToList();
        }

        public async Task<List<KnowledgeEntryEntidad>> GetEntriesByCategoryAsync(int categoryId)
        {
            var query = @"
                SELECT
                    e.id as Id,
                    e.question as Question,
                    e.answer as Answer,
                    e.keywords as Keywords,
                    e.related_commands as RelatedCommands,
                    e.priority as Priority,
                    c.name as Category,
                    c.display_name as CategoryDisplayName,
                    c.icon as CategoryIcon,
                    e.created_at as CreatedAt
                FROM abcmasplus..knowledge_entries e
                INNER JOIN abcmasplus..knowledge_categories c ON e.category_id = c.id
                WHERE e.category_id = @categoryId AND e.active = 1
                ORDER BY e.priority DESC, e.id";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<KnowledgeEntryEntidad>(query, new { categoryId });
            return result.ToList();
        }

        public async Task<KnowledgeEntryEntidad> GetEntryByIdAsync(int id)
        {
            var query = @"
                SELECT
                    e.id as Id,
                    e.category_id as CategoryId,
                    e.question as Question,
                    e.answer as Answer,
                    e.keywords as Keywords,
                    e.related_commands as RelatedCommands,
                    e.priority as Priority,
                    e.active as Active,
                    e.created_at as CreatedAt,
                    e.updated_at as UpdatedAt,
                    e.created_by as CreatedBy
                FROM abcmasplus..knowledge_entries e
                WHERE e.id = @id";

            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<KnowledgeEntryEntidad>(query, new { id });
        }

        public async Task<int> CreateEntryAsync(KnowledgeEntryEntidad entry)
        {
            var query = @"
                INSERT INTO abcmasplus..knowledge_entries
                    (category_id, question, answer, keywords, related_commands, priority, active, created_by)
                VALUES
                    (@CategoryId, @Question, @Answer, @Keywords, @RelatedCommands, @Priority, 1, @CreatedBy);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(query, entry);
        }

        public async Task<bool> UpdateEntryAsync(KnowledgeEntryEntidad entry)
        {
            var query = @"
                UPDATE abcmasplus..knowledge_entries
                SET
                    category_id = @CategoryId,
                    question = @Question,
                    answer = @Answer,
                    keywords = @Keywords,
                    related_commands = @RelatedCommands,
                    priority = @Priority,
                    updated_at = GETDATE()
                WHERE id = @Id";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, entry);
            return affected > 0;
        }

        public async Task<bool> DeleteEntryAsync(int id)
        {
            var query = "UPDATE abcmasplus..knowledge_entries SET active = 0 WHERE id = @id";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, new { id });
            return affected > 0;
        }
    }
}
