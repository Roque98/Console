using Dapper;
using FolderView.Dapper.Entidades;
using FolderView.Dapper.Interfaces;
using FolderView.Dapper.Utils;
using System.Data;

namespace FolderView.Dapper.Repositorios
{
    public class CodeGeneratorArchivoRepositorio : ICodeGeneratorIArchivoRepository
    {
        private readonly DapperContext _context;
        public CodeGeneratorArchivoRepositorio(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<CodeGeneratorArchivoEntidad>> CreateAsync(CodeGeneratorArchivoEntidad dto)
        {
            var query = "consolaMonitoreo..[CodeGenerator_Archivo_Add]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<CodeGeneratorArchivoEntidad>(query, new { dto.IdProyecto, dto.Extension, dto.Documentacion, dto.Contenido, dto.path, dto.Version, dto.IdArchivoPadre, dto.idPromptTemplate }, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }

        public async Task<CodeGeneratorArchivoEntidad> GetByIdAsync(int id)
        {
            var query = "consolaMonitoreo..[CodeGenerator_Archivo_GetById]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QuerySingleOrDefaultAsync<CodeGeneratorArchivoEntidad>(query, new { id }, commandType: CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<CodeGeneratorArchivoEntidad> UpdateAsync(CodeGeneratorArchivoEntidad dto)
        {
            var query = "consolaMonitoreo..[CodeGenerator_Archivo_Update]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QuerySingleOrDefaultAsync<CodeGeneratorArchivoEntidad>(query, new { dto.IdProyecto, dto.Extension, dto.Documentacion, dto.Contenido, dto.path, dto.Version, dto.IdArchivoPadre, dto.idPromptTemplate }, commandType: CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<CodeGeneratorArchivoEntidad> DeleteAsync(int id)
        {
            var query = "consolaMonitoreo..[CodeGenerator_Archivo_Delete]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QuerySingleOrDefaultAsync<CodeGeneratorArchivoEntidad>(query, new { id }, commandType: CommandType.StoredProcedure);
            return resultado;
        }

        public async Task<List<CodeGeneratorArchivoEntidad>> GetAllByIdProyectoAsync(int idProyecto)
        {
            var query = "consolaMonitoreo..[CodeGenerator_Archivo_GetAllByProyecto]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<CodeGeneratorArchivoEntidad>(query, new { idProyecto }, commandType: CommandType.StoredProcedure);
            var result = resultado.ToList();

            // Escapar codigo html
            foreach (var archivo in result.Where(x => x.Extension.Contains("html")))
            {
                archivo.Contenido = CodeGeneratorUtil.EscapeHtml(archivo.Contenido);
            }

            return result; 
        }

        public async Task<List<CodeGeneratorArchivoEntidad>> GetAllByIdArchivoPadreAsync(int id)
        {
            var query = "consolaMonitoreo..[CodeGenerator_Archivo_GetAll]";
            var connection = _context.CreateConnection();
            var resultado = await connection.QueryAsync<CodeGeneratorArchivoEntidad>(query, new { id }, commandType: CommandType.StoredProcedure);
            return resultado.ToList();
        }
    }

}
