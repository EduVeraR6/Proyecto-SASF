using AGE.Middleware.Exceptions.BadRequest;
using AGE.Middleware.Exceptions.NotFound;
using AGE.Utils;
using AGE.Utils.Paginacion;
using AGE.Utils.WebLink;
using Proyecto_SASF.Controllers;
using Proyecto_SASF.Entities;
using Proyecto_SASF.Entities.DAO.ZoologicoSecuenciasDAO;
using Proyecto_SASF.Repositories.ZoologicoSecuenciasRepository;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace Proyecto_SASF.Services.ZoologicoSecuenciasService
{
    public class ZoologicoSecuenciasService : IZoologicoSecuenciasService
    {
        private readonly IZoologicoSecuenciasRepository _repository;

        public ZoologicoSecuenciasService(IZoologicoSecuenciasRepository repository)
        {
            _repository = repository;
        }
        public Task<Page<ZoologicoSecuenciasPrimariaDAO>> ConsultarTodos(
                  Pageable pageable)
        {
            pageable.Validar<ZoologicoSecuenciasPrimariaDAO>();

            return _repository.ConsultarTodos(pageable);
        }

        public Task<ZoologicoSecuenciasPrimariaDAO> ConsultarPorId(int codigoSecuencia)
        {
            if (codigoSecuencia <= 0)
                throw new InvalidIdException();

            ZoologicoSecuenciasPrimariaDAO zoologicoSecuenciasPrimariaDAO = _repository.ConsultarPorId(codigoSecuencia).Result;

            return zoologicoSecuenciasPrimariaDAO == null
                ? throw new RegisterNotFoundException("Secuencia primaria con Id: " + codigoSecuencia + " no existe.")
                : Task.FromResult(zoologicoSecuenciasPrimariaDAO);
        }

        public Task<ZoologicoSecuenciasPrimaria> ConsultarCompletePorId(int codigoSecuencia)
        {
            if (codigoSecuencia <= 0)
                throw new InvalidIdException();

            ZoologicoSecuenciasPrimaria zoologicoSecuenciasPrimaria = _repository.ConsultarCompletePorId(codigoSecuencia).Result;

            return Task.FromResult(zoologicoSecuenciasPrimaria);
        }

        public async Task<Resource<ZoologicoSecuenciasPrimariaDAO>> Insertar(
            IHttpContextAccessor _httpContextAccessor,
            ZoologicoSecuenciasSaveDAO zoologicoSecuenciasSaveDAO)
        {

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    ZoologicoSecuenciasPrimaria zoologicoSecuenciasPrimarias = ValidarInsert(_httpContextAccessor, zoologicoSecuenciasSaveDAO);

                    ZoologicoSecuenciasPrimariaDAO ZoologicoSecuenciasPrimariasDAO = await _repository.Insertar(zoologicoSecuenciasPrimarias);

                    Resource<ZoologicoSecuenciasPrimariaDAO> ZoologicoSecuenciasPrimariasDAOWithResource = GetDataWithResource(_httpContextAccessor, ZoologicoSecuenciasPrimariasDAO);

                    transactionScope.Complete();

                    return ZoologicoSecuenciasPrimariasDAOWithResource;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        public async Task<List<Resource<ZoologicoSecuenciasPrimariaDAO>>> InsertarVarios(
            IHttpContextAccessor _httpContextAccessor,
            List<ZoologicoSecuenciasSaveDAO> zoologicoSecuenciasPrimariasSaveDAOList)
        {
            if (zoologicoSecuenciasPrimariasSaveDAOList == null)
                throw new InvalidSintaxisException();

            List<ZoologicoSecuenciasPrimaria> listZoologicoSecuenciasPrimaria = new List<ZoologicoSecuenciasPrimaria>();

            ValidateKeys.ValidarPKDuplicadas(zoologicoSecuenciasPrimariasSaveDAOList, p => new { p.Codigo }, "Id");

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (ZoologicoSecuenciasSaveDAO zoologicoSecuenciasPrimariasSaveDAO in zoologicoSecuenciasPrimariasSaveDAOList)
                    {
                        ZoologicoSecuenciasPrimaria entityObject = ValidarInsert(_httpContextAccessor, zoologicoSecuenciasPrimariasSaveDAO);
                        listZoologicoSecuenciasPrimaria.Add(entityObject);
                    }

                    List<ZoologicoSecuenciasPrimariaDAO> ZoologicoSecuenciasPrimariasDAOList = _repository.InsertarVarios(listZoologicoSecuenciasPrimaria).Result;

                    List<Resource<ZoologicoSecuenciasPrimariaDAO>> ZoologicoSecuenciasPrimariasDAOListWithResource = new List<Resource<ZoologicoSecuenciasPrimariaDAO>>();

                    foreach (ZoologicoSecuenciasPrimariaDAO ZoologicoSecuenciasPrimariasDAO in ZoologicoSecuenciasPrimariasDAOList)
                    {
                        ZoologicoSecuenciasPrimariasDAOListWithResource.Add(GetDataWithResource(_httpContextAccessor, ZoologicoSecuenciasPrimariasDAO));
                    }

                    transactionScope.Complete();

                    return ZoologicoSecuenciasPrimariasDAOListWithResource;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<ZoologicoSecuenciasPrimariaDAO> Actualizar(
            IHttpContextAccessor _httpContextAccessor,
            ZoologicoSecuenciasSaveDAO zoologicoSecuenciasPrimariasSaveDAO)
        {

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    ZoologicoSecuenciasPrimaria ObjectDao = ValidarUpdate(_httpContextAccessor, zoologicoSecuenciasPrimariasSaveDAO);
                    ZoologicoSecuenciasPrimariaDAO result = await _repository.Actualizar(ObjectDao);

                    transactionScope.Complete();

                    return result;

                }
                catch (Exception)
                {

                    throw;
                }
            }


        }

        public async Task<List<ZoologicoSecuenciasPrimariaDAO>> ActualizarVarios(
            IHttpContextAccessor _httpContextAccessor,
            List<ZoologicoSecuenciasSaveDAO> zoologicoSecuenciasPrimariasSaveDAOList)
        {

            ValidateKeys.ValidarPKDuplicadas(zoologicoSecuenciasPrimariasSaveDAOList, p => new { p.Codigo }, "Id");

            List<ZoologicoSecuenciasPrimaria> zoologicoSecuenciasPrimariaDAOList = new List<ZoologicoSecuenciasPrimaria>();

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (ZoologicoSecuenciasSaveDAO zooSaveDAO in zoologicoSecuenciasPrimariasSaveDAOList)
                    {
                        ZoologicoSecuenciasPrimaria ObjectDao = ValidarUpdate(_httpContextAccessor, zooSaveDAO);
                        zoologicoSecuenciasPrimariaDAOList.Add(ObjectDao);
                    }

                    List<ZoologicoSecuenciasPrimariaDAO> result = await _repository.ActualizarVarios(zoologicoSecuenciasPrimariaDAOList);

                    transactionScope.Complete();

                    return result;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        public async Task EliminarPorId(int id)
        {
            if (id <= 0)
                throw new InvalidFieldException();

            await _repository.EliminarPorId(id);
        }


        public void ActualizarSecuenciaPrimaria(
            IHttpContextAccessor _httpContextAccessor,
            ZoologicoSecuenciasPrimaria ZoologicoSecuenciasPrimaria)
        {
            ZoologicoSecuenciasPrimaria.FechaModificacion = DateTime.Now;
            ZoologicoSecuenciasPrimaria.UbicacionModificacion = Ubicacion.getIpAdress(_httpContextAccessor);

            Validator.ValidateObject(ZoologicoSecuenciasPrimaria, new ValidationContext(ZoologicoSecuenciasPrimaria), true);

            _repository.ActualizarSecuenciaPrimaria(ZoologicoSecuenciasPrimaria);
        }

        private static Resource<ZoologicoSecuenciasPrimariaDAO> GetDataWithResource(IHttpContextAccessor _httpContextAccessor, ZoologicoSecuenciasPrimariaDAO ZoologicoSecuenciasPrimariaDAO)
        {
            string rutaSegmentoFinal = $"{ZoologicoSecuenciasPrimariaDAO.Codigo}";

            return Resource<ZoologicoSecuenciasPrimariaDAO>.GetDataWithResource<ZoologicoSecuenciasController>(
                _httpContextAccessor, rutaSegmentoFinal, ZoologicoSecuenciasPrimariaDAO);
        }

        private ZoologicoSecuenciasPrimaria ValidarInsert(
            IHttpContextAccessor _httpContextAccessor,
            ZoologicoSecuenciasSaveDAO zoologicoSecuenciasSaveDAO)
        {
            if (zoologicoSecuenciasSaveDAO == null)
                throw new InvalidSintaxisException();

            ValidarPK(zoologicoSecuenciasSaveDAO.Codigo);

            ZoologicoSecuenciasPrimaria entityObject = FromDAOToEntity(_httpContextAccessor, zoologicoSecuenciasSaveDAO);

            Validator.ValidateObject(entityObject, new ValidationContext(entityObject), true);

            return entityObject;
        }

        private ZoologicoSecuenciasPrimaria ValidarUpdate(IHttpContextAccessor _httpContextAccessor, ZoologicoSecuenciasSaveDAO zoologicoSecuenciasPrimariasSaveDAO)
        {
            if (zoologicoSecuenciasPrimariasSaveDAO == null)
                throw new InvalidSintaxisException();

            ZoologicoSecuenciasPrimaria? ZoologicoSecuenciasPrimaria = ConsultarCompletePorId(zoologicoSecuenciasPrimariasSaveDAO.Codigo).Result ??
                throw new RegisterNotFoundException($"Secuencia primaria con Id: {zoologicoSecuenciasPrimariasSaveDAO.Codigo} no existe.");



            ZoologicoSecuenciasPrimaria.Estado = Globales.ESTADO_ACTIVO;
            ZoologicoSecuenciasPrimaria.FechaEstado = DateTime.Now;



            if (!string.IsNullOrWhiteSpace(zoologicoSecuenciasPrimariasSaveDAO.Descripcion))
                ZoologicoSecuenciasPrimaria.Descripcion = zoologicoSecuenciasPrimariasSaveDAO.Descripcion;

            if (!string.IsNullOrWhiteSpace(zoologicoSecuenciasPrimariasSaveDAO.ObservacionEstado))
                ZoologicoSecuenciasPrimaria.ObservacionEstado = zoologicoSecuenciasPrimariasSaveDAO.ObservacionEstado;

            ZoologicoSecuenciasPrimaria.FechaModificacion = DateTime.Now;
            ZoologicoSecuenciasPrimaria.UbicacionModificacion = Ubicacion.getIpAdress(_httpContextAccessor);

            ZoologicoSecuenciasPrimaria.UsuarioModificacion = zoologicoSecuenciasPrimariasSaveDAO.UsuarioModificacion;

            Validator.ValidateObject(ZoologicoSecuenciasPrimaria, new ValidationContext(ZoologicoSecuenciasPrimaria), true);

            return ZoologicoSecuenciasPrimaria;
        }

        private ZoologicoSecuenciasPrimaria FromDAOToEntity(
            IHttpContextAccessor _httpContextAccessor,
            ZoologicoSecuenciasSaveDAO zoologicoSecuenciasSaveDAO)
        {

            return new ZoologicoSecuenciasPrimaria
            {
                Codigo = zoologicoSecuenciasSaveDAO.Codigo,
                Descripcion = zoologicoSecuenciasSaveDAO.Descripcion,
                ValorInicial = 0,
                IncrementaEn = 1,
                Estado = Globales.ESTADO_ACTIVO,
                ObservacionEstado = zoologicoSecuenciasSaveDAO.ObservacionEstado,
                UsuarioIngreso = zoologicoSecuenciasSaveDAO.UsuarioIngreso,
                FechaEstado = DateTime.Now,
                FechaIngreso = DateTime.Now,
                UbicacionIngreso = Ubicacion.getIpAdress(_httpContextAccessor),
                UsuarioModificacion = null,
                FechaModificacion = null,
                UbicacionModificacion = null
            };
        }

        private void ValidarPK(int codigoSecuenciaPrimaria)
        {
            ValidateKeys.ValidarNoExistenciaKey(
               codigoSecuenciaPrimaria,
               $"Secuencia primaria con Id: {codigoSecuenciaPrimaria} ya existe.",
               _repository.ConsultarCompletePorId);
        }


    }
}
