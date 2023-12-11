using AGE.Middleware.Exceptions.BadRequest;
using AGE.Middleware.Exceptions.NotFound;
using AGE.Utils;
using AGE.Utils.Paginacion;
using AGE.Utils.WebLink;
using Proyecto_SASF.Controllers;
using Proyecto_SASF.Entities;
using Proyecto_SASF.Entities.DAO.RazaDAO;
using Proyecto_SASF.Repositories.RazaRepository;
using Proyecto_SASF.Services.ZoologicoSecuenciasService;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace Proyecto_SASF.Services.RazasService
{
    public class RazasService : IRazasService
    {
        private readonly IRazaRepository _repository;
        private readonly IZoologicoSecuenciasService _zoologicoSecuenciasPrimarias;

        public RazasService(IRazaRepository repository, IZoologicoSecuenciasService zoologicoSecuenciasPrimarias)
        {
            _repository = repository;
            _zoologicoSecuenciasPrimarias = zoologicoSecuenciasPrimarias;
        }

        public Task<Page<RazaDAO>> ConsultarListaFiltro(string filtro, Pageable pageable)
        {
            if (string.IsNullOrEmpty(filtro))
                throw new InvalidFieldException("El filtro es requerido.");

            pageable.Validar<RazaDAO>();

            return _repository.ConsultarListaFiltro(filtro, pageable);
        }

        public Task<RazaDAO> ConsultarPorId(int id)
        {
            if (id <= 0)
                throw new InvalidIdException();

            RazaDAO? razaDAO = _repository.ConsultarPorId(id).Result;

            return razaDAO == null ? throw new RegisterNotFoundException("Raza con Id: " + id + " no existe.")
                : Task.FromResult(razaDAO);
        }

        private Raza ConsultarCompletoPorId(int id)
        {
            if (id <= 0)
                throw new InvalidIdException();

            return _repository.ConsultarCompleto(id).Result;
        }


        public Task<Page<RazaDAO>> ConsultarTodos(Pageable pageable)
        {
            pageable.Validar<RazaDAO>();
            return _repository.ConsultarTodos(pageable);
        }

        public async Task<Resource<RazaDAO>> Insertar(IHttpContextAccessor _httpContextAccessor, RazaSaveDAO razaSaveDAO)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    Raza raza = ValidarInsert(_httpContextAccessor, razaSaveDAO);
                    RazaDAO razaDAO = await _repository.Insertar(raza);
                    Resource<RazaDAO> razaDAOWithResource = GetDataWithResource(_httpContextAccessor, razaDAO);
                    transactionScope.Complete();
                    return razaDAOWithResource;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<List<Resource<RazaDAO>>> InsertarVarios(IHttpContextAccessor _httpContextAccessor, List<RazaSaveDAO> razaSaveDAOList)
        {
            if (razaSaveDAOList == null)
                throw new InvalidSintaxisException();

            List<Raza> razaList = new List<Raza>();

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (RazaSaveDAO razaSaveDAO in razaSaveDAOList)
                    {
                        Raza raza = ValidarInsert(_httpContextAccessor, razaSaveDAO);
                        razaList.Add(raza);
                    }

                    List<RazaDAO> razaDAOList = await _repository.InsertarVarios(razaList);

                    List<Resource<RazaDAO>> razaDAOListWithResource = new List<Resource<RazaDAO>>();

                    foreach (RazaDAO razaDAO in razaDAOList)
                    {
                        razaDAOListWithResource.Add(GetDataWithResource(_httpContextAccessor, razaDAO));
                    }

                    transactionScope.Complete();

                    return razaDAOListWithResource;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<RazaDAO> Actualizar(IHttpContextAccessor _httpContextAccessor, RazaSaveDAO razaSaveDAO)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    Raza raza = ValidarUpdate(_httpContextAccessor, razaSaveDAO);
                    RazaDAO result = await _repository.Actualizar(raza);

                    transactionScope.Complete();

                    return result;
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public async Task<List<RazaDAO>> ActualizarVarios(IHttpContextAccessor _httpContextAccessor, List<RazaSaveDAO> razaSaveDAOList)
        {
            if (razaSaveDAOList == null)
                throw new InvalidSintaxisException();

            ValidateKeys.ValidarPKDuplicadas(razaSaveDAOList, p => new { p.Id }, "Id");

            List<Raza> razaList = new List<Raza>();
            Raza raza;

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (RazaSaveDAO razaSaveDAO in razaSaveDAOList)
                    {
                        raza = ValidarUpdate(_httpContextAccessor, razaSaveDAO);
                        razaList.Add(raza);
                    }

                    List<RazaDAO> result = await _repository.ActualizarVarios(razaList);

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
                throw new InvalidIdException();

            await _repository.EliminarPorId(id);
        }


        private Raza ValidarInsert(IHttpContextAccessor _httpContextAccessor, RazaSaveDAO razaSaveDAO)
        {
            if (razaSaveDAO is null)
                throw new InvalidSintaxisException();

            Validator.ValidateObject(razaSaveDAO, new ValidationContext(razaSaveDAO));

            FuncionesSecuencias fg = new FuncionesSecuencias(_zoologicoSecuenciasPrimarias);
            razaSaveDAO.Id = fg.ObtenerSecuenciaPrimaria(
                 _httpContextAccessor,
                 Globales.CODIGO_SECUENCIA_RAZA,
                 razaSaveDAO.UsuarioIngreso
                );

            ValidarPK(razaSaveDAO.Id);

            Raza entityObject = FromDAOToEntity(_httpContextAccessor, razaSaveDAO);
            Validator.ValidateObject(entityObject, new ValidationContext(entityObject), true);
            return entityObject;
        }

        private static Resource<RazaDAO> GetDataWithResource(
          IHttpContextAccessor _httpContextAccessor,
          RazaDAO razaDAO)
        {
            string rutaSegmentoFinal = $"{razaDAO.Id}";

            return Resource<RazaDAO>.GetDataWithResource<RazaController>(
                _httpContextAccessor, rutaSegmentoFinal, razaDAO);
        }

        private Raza ValidarUpdate(
            IHttpContextAccessor _httpContextAccessor,
            RazaSaveDAO razaSaveDAO)
        {
            if (razaSaveDAO == null)
                throw new InvalidSintaxisException();

            if (razaSaveDAO.UsuarioModificacion == null || razaSaveDAO.UsuarioModificacion <= 0)
                throw new InvalidFieldException("El usuario de modificación debe ser un número entero mayor a cero.");

            Raza raza = ConsultarCompletoPorId(razaSaveDAO.Id) ??
                throw new RegisterNotFoundException($"El animal con id: {razaSaveDAO.Id} no existe.");

                raza.Estado = Globales.ESTADO_ACTIVO;
                raza.FechaEstado = DateTime.Now;


            if (!string.IsNullOrWhiteSpace(razaSaveDAO.Nombre))
                raza.Nombre = razaSaveDAO.Nombre;

            if (!string.IsNullOrWhiteSpace(razaSaveDAO.OrigenGeografico))
                raza.OrigenGeografico = razaSaveDAO.OrigenGeografico;

            if (!string.IsNullOrEmpty(razaSaveDAO.Descripcion))
                raza.Descripcion = razaSaveDAO.Descripcion;

            if (!string.IsNullOrWhiteSpace(razaSaveDAO.ObservacionEstado))
                raza.ObservacionEstado = razaSaveDAO.ObservacionEstado;

            raza.FechaModificacion = DateTime.Now;
            raza.UbicacionModificacion = Ubicacion.getIpAdress(_httpContextAccessor);
            raza.UsuarioModificacion = razaSaveDAO.UsuarioModificacion;

            Validator.ValidateObject(raza, new ValidationContext(raza), true);

            return raza;
        }


        private Raza FromDAOToEntity(
            IHttpContextAccessor _httpContextAccessor,
            RazaSaveDAO razaSaveDAO)
        {

            return new Raza
            {
                Id = razaSaveDAO.Id,
                Nombre = razaSaveDAO.Nombre,
                OrigenGeografico = razaSaveDAO.OrigenGeografico,
                Descripcion = razaSaveDAO.Descripcion,
                Estado = Globales.ESTADO_ACTIVO,
                FechaEstado = DateTime.Now,
                FechaIngreso = DateTime.Now,
                ObservacionEstado = razaSaveDAO.ObservacionEstado,
                UsuarioIngreso = razaSaveDAO.UsuarioIngreso,
                UbicacionIngreso = Ubicacion.getIpAdress(_httpContextAccessor),
                UsuarioModificacion = null,
                FechaModificacion = null,
                UbicacionModificacion = null
            };
        }

        private void ValidarPK(int id)
        {
            ValidateKeys.ValidarNoExistenciaKey(
               id,
               $"Raza con Id: {id} ya existe.",
               _repository.ConsultarCompleto);
        }


    }
}
