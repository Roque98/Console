using Dapper;
using FolderView.Dapper.AdministracionBot.Entidades;
using FolderView.Dapper.AdministracionBot.Interfaces;

namespace FolderView.Dapper.AdministracionBot.Repositorios
{
    public class OperacionRepositorio : IOperacionRepository
    {
        private readonly DapperContext _context;

        public OperacionRepositorio(DapperContext context)
        {
            _context = context;
        }

        public async Task<List<OperacionEntidad>> GetAllAsync()
        {
            var query = @"
                SELECT
                    o.idOperacion as IdOperacion,
                    o.idModulo as IdModulo,
                    o.nombre as Nombre,
                    o.descripcion as Descripcion,
                    o.comando as Comando,
                    o.requiereParametros as RequiereParametros,
                    o.parametrosEjemplo as ParametrosEjemplo,
                    o.nivelCriticidad as NivelCriticidad,
                    o.orden as Orden,
                    o.activo as Activo,
                    o.fechaCreacion as FechaCreacion,
                    m.nombre as Modulo,
                    m.icono as IconoModulo
                FROM abcmasplus..Operaciones o
                INNER JOIN abcmasplus..Modulos m ON o.idModulo = m.idModulo
                WHERE o.activo = 1 AND m.activo = 1
                ORDER BY m.orden, o.orden";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<OperacionEntidad>(query);
            return result.ToList();
        }

        public async Task<List<OperacionEntidad>> GetByModuloAsync(int idModulo)
        {
            var query = @"
                SELECT
                    o.idOperacion as IdOperacion,
                    o.nombre as Nombre,
                    o.descripcion as Descripcion,
                    o.comando as Comando,
                    o.requiereParametros as RequiereParametros,
                    o.parametrosEjemplo as ParametrosEjemplo,
                    o.nivelCriticidad as NivelCriticidad,
                    o.orden as Orden,
                    o.activo as Activo,
                    m.nombre as Modulo,
                    m.icono as IconoModulo,
                    (SELECT COUNT(*) FROM abcmasplus..LogOperaciones lo WHERE lo.idOperacion = o.idOperacion) as TotalUsos
                FROM abcmasplus..Operaciones o
                INNER JOIN abcmasplus..Modulos m ON o.idModulo = m.idModulo
                WHERE m.idModulo = @idModulo
                ORDER BY o.orden, o.nombre";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<OperacionEntidad>(query, new { idModulo });
            return result.ToList();
        }

        public async Task<OperacionEntidad> GetByIdAsync(int id)
        {
            var query = @"
                SELECT
                    o.idOperacion as IdOperacion,
                    o.idModulo as IdModulo,
                    o.nombre as Nombre,
                    o.descripcion as Descripcion,
                    o.comando as Comando,
                    o.requiereParametros as RequiereParametros,
                    o.parametrosEjemplo as ParametrosEjemplo,
                    o.nivelCriticidad as NivelCriticidad,
                    o.orden as Orden,
                    o.activo as Activo,
                    o.fechaCreacion as FechaCreacion
                FROM abcmasplus..Operaciones o
                WHERE o.idOperacion = @id";

            using var connection = _context.CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<OperacionEntidad>(query, new { id });
        }

        public async Task<int> CreateAsync(OperacionEntidad operacion)
        {
            var query = @"
                INSERT INTO abcmasplus..Operaciones
                    (idModulo, nombre, descripcion, comando, requiereParametros, parametrosEjemplo, nivelCriticidad, orden)
                VALUES
                    (@IdModulo, @Nombre, @Descripcion, @Comando, @RequiereParametros, @ParametrosEjemplo, @NivelCriticidad, @Orden);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using var connection = _context.CreateConnection();
            return await connection.ExecuteScalarAsync<int>(query, operacion);
        }

        public async Task<bool> UpdateAsync(OperacionEntidad operacion)
        {
            var query = @"
                UPDATE abcmasplus..Operaciones
                SET
                    nombre = @Nombre,
                    descripcion = @Descripcion,
                    comando = @Comando,
                    requiereParametros = @RequiereParametros,
                    parametrosEjemplo = @ParametrosEjemplo,
                    nivelCriticidad = @NivelCriticidad,
                    orden = @Orden,
                    activo = @Activo
                WHERE idOperacion = @IdOperacion";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, operacion);
            return affected > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var query = "UPDATE abcmasplus..Operaciones SET activo = 0 WHERE idOperacion = @id";

            using var connection = _context.CreateConnection();
            var affected = await connection.ExecuteAsync(query, new { id });
            return affected > 0;
        }

        public async Task<List<OperacionEntidad>> GetAllConEstadisticasAsync()
        {
            var query = @"
                SELECT
                    o.idOperacion as IdOperacion,
                    m.nombre as Modulo,
                    m.icono as IconoModulo,
                    o.nombre as Nombre,
                    o.comando as Comando,
                    o.descripcion as Descripcion,
                    o.nivelCriticidad as NivelCriticidad,
                    o.activo as Activo,
                    COUNT(DISTINCT ro.idRol) as RolesConPermiso,
                    (SELECT COUNT(*) FROM abcmasplus..LogOperaciones lo WHERE lo.idOperacion = o.idOperacion) as TotalEjecuciones,
                    (SELECT COUNT(*) FROM abcmasplus..LogOperaciones lo WHERE lo.idOperacion = o.idOperacion AND lo.resultado = 'EXITOSO') as Exitosas,
                    (SELECT COUNT(*) FROM abcmasplus..LogOperaciones lo WHERE lo.idOperacion = o.idOperacion AND lo.resultado = 'ERROR') as Errores
                FROM abcmasplus..Operaciones o
                INNER JOIN abcmasplus..Modulos m ON o.idModulo = m.idModulo
                LEFT JOIN abcmasplus..RolesOperaciones ro ON o.idOperacion = ro.idOperacion AND ro.permitido = 1 AND ro.activo = 1
                WHERE o.activo = 1 AND m.activo = 1
                GROUP BY o.idOperacion, m.nombre, m.icono, o.nombre, o.comando, o.descripcion, o.nivelCriticidad, o.activo
                ORDER BY m.nombre, o.nombre";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<OperacionEntidad>(query);
            return result.ToList();
        }

        public async Task<List<OperacionEntidad>> GetOperacionesByUsuarioAsync(int idUsuario)
        {
            var query = @"
                SELECT DISTINCT
                    o.idOperacion as IdOperacion,
                    o.nombre as Nombre,
                    o.comando as Comando,
                    o.descripcion as Descripcion,
                    o.requiereParametros as RequiereParametros,
                    m.nombre as Modulo,
                    m.icono as ModuloIcono,
                    CASE
                        WHEN uo.idUsuario IS NOT NULL THEN 'PERSONAL'
                        ELSE 'ROL'
                    END as TipoPermiso,
                    CASE
                        WHEN o.nivelCriticidad >= 3 THEN 1
                        ELSE 0
                    END as TienePoder
                FROM abcmasplus..Operaciones o
                INNER JOIN abcmasplus..Modulos m ON o.idModulo = m.idModulo
                LEFT JOIN abcmasplus..UsuariosOperaciones uo ON o.idOperacion = uo.idOperacion AND uo.idUsuario = @idUsuario AND uo.permitido = 1 AND uo.activo = 1
                LEFT JOIN abcmasplus..UsuariosRolesIA ur ON ur.idUsuario = @idUsuario AND ur.activo = 1
                LEFT JOIN abcmasplus..RolesOperaciones ro ON o.idOperacion = ro.idOperacion AND ro.idRol = ur.idRol AND ro.permitido = 1 AND ro.activo = 1
                WHERE o.activo = 1 AND m.activo = 1
                    AND (uo.idUsuario IS NOT NULL OR ro.idRol IS NOT NULL)
                ORDER BY m.nombre, o.nombre";

            using var connection = _context.CreateConnection();
            var result = await connection.QueryAsync<OperacionEntidad>(query, new { idUsuario });
            return result.ToList();
        }
    }
}
