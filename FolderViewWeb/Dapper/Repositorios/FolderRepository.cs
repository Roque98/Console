using Dapper;
using FolderView.Dapper.Entidades;
using FolderView.Dapper.Interfaces;
using System.Data;

namespace FolderView.Dapper.Repositorios
{
    public class FolderRepository: IFolderRepository
    {
        private readonly DapperContext _context;

        public FolderRepository(
            DapperContext context
        )
        {
            _context = context;
        }

        public async Task<List<ReadFolderEntidad>> CreateFolderAsync(CreateFolderEntidad dto)
        {
            var query = "consolaMonitoreo..CodeGenerator_Folder_Add";
            var connection = _context.CreateConnection();
            var param = new { dto.IdTipoArchivo, dto.IdProyecto, dto.NumeroVersion, dto.IdFolderPadre };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return await GetAllFolderAsync();
        }

        public async Task<ReadFolderEntidad> GetFolderByIdAsync(int id)
        {
            var query = "consolaMonitoreo..CodeGenerator_Folder_GetById";
            var connection = _context.CreateConnection();
            var param = new { id };
            var resultado = await connection.QuerySingleOrDefaultAsync<ReadFolderEntidad>(query, param, commandType: CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<List<ReadFolderEntidad>> GetAllFolderAsync()
        {
            var query = "consolaMonitoreo..CodeGenerator_Folder_GetAll";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<ReadFolderEntidad>(query, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<List<ReadFolderEntidad>> UpdateFolderAsync(UpdateFolderEntidad dto)
        {
            var query = "consolaMonitoreo..CodeGenerator_Folder_Update";
            var connection = _context.CreateConnection();
            var param = new { dto.Id, dto.IdTipoArchivo, dto.IdProyecto, dto.NumeroVersion, dto.IdFolderPadre };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return await GetAllFolderAsync();
        }

        public async Task<bool> DeleteFolderAsync(int id)
        {
            var query = "consolaMonitoreo..CodeGenerator_Folder_Delete";
            var connection = _context.CreateConnection();
            var param = new { id };
            await connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
            return true;
        }

    }
}
