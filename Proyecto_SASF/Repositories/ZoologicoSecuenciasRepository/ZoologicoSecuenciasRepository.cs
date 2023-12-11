using AGE.Middleware.Exceptions.NotFound;
using AGE.Utils;
using AGE.Utils.Paginacion;
using Microsoft.EntityFrameworkCore;
using Proyecto_SASF.Entities;
using Proyecto_SASF.Entities.DAO.ZoologicoSecuenciasDAO;
using Proyecto_SASF.Middleware.Exceptions.BadRequest;

namespace Proyecto_SASF.Repositories.ZoologicoSecuenciasRepository
{
    public class ZoologicoSecuenciasRepository : IZoologicoSecuenciasRepository
    {
        private readonly ZoologicoContext _dbContext;
        public ZoologicoSecuenciasRepository(ZoologicoContext zoologicoContext)
        {
            _dbContext = zoologicoContext;
        }

        public async Task<Page<ZoologicoSecuenciasPrimariaDAO>> ConsultarTodos(Pageable pageable)
        {
            if (_dbContext.ZoologicoSecuenciasPrimarias == null)
            {
                throw new RegisterNotFoundException();
            }

            var Query = _dbContext.ZoologicoSecuenciasPrimarias
                .Where(p => p.Estado != Globales.ESTADO_ANULADO);

            return await Paginator<ZoologicoSecuenciasPrimaria, ZoologicoSecuenciasPrimariaDAO>.Paginar(Query, pageable, FromEntityToDAO);
        }

        public async Task<ZoologicoSecuenciasPrimariaDAO> ConsultarPorId(int codigoSecuencia)
        {
            if (_dbContext.ZoologicoSecuenciasPrimarias == null)
            {
                throw new RegisterNotFoundException();
            }

            ZoologicoSecuenciasPrimaria? zooSecuenciasPrimaria = await _dbContext.ZoologicoSecuenciasPrimarias
                .Where(p => p.Estado != Globales.ESTADO_ANULADO &&
                p.Codigo == codigoSecuencia).FirstOrDefaultAsync();

            return FromEntityToDAO(zooSecuenciasPrimaria);
        }

        public async Task<ZoologicoSecuenciasPrimaria> ConsultarCompletePorId(int codigoSecuencia)
        {
            if (_dbContext.ZoologicoSecuenciasPrimarias == null)
            {
                throw new RegisterNotFoundException($"No se encontro una Secuencia Primaria con el id {codigoSecuencia}");
            }

            ZoologicoSecuenciasPrimaria? zooSecuenciasPrimaria = await _dbContext.ZoologicoSecuenciasPrimarias
                .Where(p => p.Estado != Globales.ESTADO_ANULADO &&
                p.Codigo == codigoSecuencia).FirstOrDefaultAsync();

            return zooSecuenciasPrimaria;
        }

        public async Task<ZoologicoSecuenciasPrimariaDAO> Insertar(
            ZoologicoSecuenciasPrimaria zooSecuenciasPrimaria)
        {

            _dbContext.ZoologicoSecuenciasPrimarias.Add(zooSecuenciasPrimaria);
            await _dbContext.SaveChangesAsync();

            ZoologicoSecuenciasPrimariaDAO zooSecuenciasPrimariasDAO = await ConsultarPorId(zooSecuenciasPrimaria.Codigo);

            return zooSecuenciasPrimariasDAO;
        }


        public async Task<ZoologicoSecuenciasPrimariaDAO> Actualizar(
            ZoologicoSecuenciasPrimaria zooSecuenciasPrimaria)
        {

            _dbContext.Entry(zooSecuenciasPrimaria).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            ZoologicoSecuenciasPrimariaDAO zooSecuenciasPrimariasDAO = await ConsultarPorId(zooSecuenciasPrimaria.Codigo);

            return zooSecuenciasPrimariasDAO;
        }


        public async Task<List<ZoologicoSecuenciasPrimariaDAO>> InsertarVarios(
            List<ZoologicoSecuenciasPrimaria> zooSecuenciasPrimariaList)
        {
            await _dbContext.ZoologicoSecuenciasPrimarias.AddRangeAsync(zooSecuenciasPrimariaList);
            await _dbContext.SaveChangesAsync();

            return ConversorEntityToDAOList<ZoologicoSecuenciasPrimaria, ZoologicoSecuenciasPrimariaDAO>
                .FromEntityToDAOList(zooSecuenciasPrimariaList, FromEntityToDAO);
        }


        public async Task<List<ZoologicoSecuenciasPrimariaDAO>> ActualizarVarios(
            List<ZoologicoSecuenciasPrimaria> zooSecuenciasPrimariaList)
        {
            _dbContext.ZoologicoSecuenciasPrimarias.UpdateRange(zooSecuenciasPrimariaList);
            await _dbContext.SaveChangesAsync();

            return ConversorEntityToDAOList<ZoologicoSecuenciasPrimaria, ZoologicoSecuenciasPrimariaDAO>
                .FromEntityToDAOList(zooSecuenciasPrimariaList, FromEntityToDAO);
        }


        public void ActualizarSecuenciaPrimaria(ZoologicoSecuenciasPrimaria zooSecuenciasPrimaria)
        {
            using (var dbContext = new ZoologicoContext())
            {
                dbContext.Entry(zooSecuenciasPrimaria).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        private ZoologicoSecuenciasPrimariaDAO FromEntityToDAO(ZoologicoSecuenciasPrimaria entityObject)
        {
            ZoologicoSecuenciasPrimariaDAO DAOobject = null;

            if (entityObject != null)
            {
                DAOobject = new ZoologicoSecuenciasPrimariaDAO
                {
                    Estado = entityObject.Estado,
                    Codigo = entityObject.Codigo,
                    Descripcion = entityObject.Descripcion
                };
            }

            return DAOobject;

        }

        public async Task EliminarPorId(int id)
        {
            if (_dbContext.ZoologicoSecuenciasPrimarias == null)
                throw new RegisterNotFoundException();

            var secuencia = await _dbContext.ZoologicoSecuenciasPrimarias.FindAsync(id);

            if (secuencia == null)
                throw new RegisterNotFoundException($"No se encontro la secuencia con el {id}");

            if (secuencia.Estado.ToLower().Equals(Globales.ESTADO_ACTIVO.ToLower()))
            {
                secuencia.Estado = Globales.ESTADO_ANULADO;
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new DeleteExistingException($"La secuencia con el id {id} ya esta eliminada ");
            }
        }
    }
}
