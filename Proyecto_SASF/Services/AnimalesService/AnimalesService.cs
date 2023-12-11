using AGE.Middleware.Exceptions.BadRequest;
using AGE.Middleware.Exceptions.NotFound;
using AGE.Utils;
using AGE.Utils.Paginacion;
using AGE.Utils.WebLink;
using Proyecto_SASF.Controllers;
using Proyecto_SASF.Entities;
using Proyecto_SASF.Entities.DAO.AnimalesDAO;
using Proyecto_SASF.Repositories.AnimalesRepository;
using Proyecto_SASF.Services.ZoologicoSecuenciasService;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace Proyecto_SASF.Services.AnimalesService
{
    public class AnimalesService : IAnimalesService
    {
        private readonly IAnimalesRepository _repository;
        private readonly IZoologicoSecuenciasService _zoologicoSecuenciasPrimarias;

        public AnimalesService(IAnimalesRepository repository, IZoologicoSecuenciasService zoologicoSecuenciasPrimarias)
        {
            _repository = repository;
            _zoologicoSecuenciasPrimarias = zoologicoSecuenciasPrimarias;
        }

        public Task<Page<AnimalesDAO>> ConsultarListaFiltro(string filtro, Pageable pageable)
        {
            if (string.IsNullOrEmpty(filtro))
                throw new InvalidFieldException("El filtro es requerido.");

            pageable.Validar<AnimalesDAO>();

            return _repository.ConsultarListaFiltro(filtro, pageable);
        }

        public Task<AnimalesDAO> ConsultarPorId(int id)
        {
            if (id <= 0)
                throw new InvalidIdException();

            AnimalesDAO? animalesDAO = _repository.ConsultarPorId(id).Result;

            return animalesDAO == null ? throw new RegisterNotFoundException("Animal con Id: " + id + " no existe.")
                : Task.FromResult(animalesDAO);
        }

        private Animale ConsultarCompletoPorId(int id)
        {
            if (id <= 0)
                throw new InvalidIdException();

            return _repository.ConsultarCompleto(id).Result;
        }


        public Task<Page<AnimalesDAO>> ConsultarTodos(Pageable pageable)
        {
            pageable.Validar<AnimalesDAO>();
            return _repository.ConsultarTodos(pageable);
        }

        public async Task<Resource<AnimalesDAO>> Insertar(IHttpContextAccessor _httpContextAccessor, AnimalesSaveDAO animalSaveDAO)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    Animale animal = ValidarInsert(_httpContextAccessor, animalSaveDAO);

                    AnimalesDAO animalDAO = await _repository.Insertar(animal);

                    Resource<AnimalesDAO> animalDAOWithResource = GetDataWithResource(_httpContextAccessor, animalDAO);

                    transactionScope.Complete();

                    return animalDAOWithResource;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<List<Resource<AnimalesDAO>>> InsertarVarios(IHttpContextAccessor _httpContextAccessor, List<AnimalesSaveDAO> animalesSaveDAOList)
        {
            if (animalesSaveDAOList == null)
                throw new InvalidSintaxisException();

            List<Animale> animalesList = new List<Animale>();

            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (AnimalesSaveDAO animalSaveDAO in animalesSaveDAOList)
                    {
                        Animale animal = ValidarInsert(_httpContextAccessor, animalSaveDAO);
                        animalesList.Add(animal);
                    }

                    List<AnimalesDAO> animalesDAOList = await _repository.InsertarVarios(animalesList);

                    List<Resource<AnimalesDAO>> animalesDAOListWithResource = new List<Resource<AnimalesDAO>>();

                    foreach (AnimalesDAO animalDAO in animalesDAOList)
                    {
                        animalesDAOListWithResource.Add(GetDataWithResource(_httpContextAccessor, animalDAO));
                    }

                    transactionScope.Complete();

                    return animalesDAOListWithResource;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<AnimalesDAO> Actualizar(IHttpContextAccessor _httpContextAccessor, AnimalesUpdateDAO animalSaveDAO)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    Animale animal = ValidarUpdate(_httpContextAccessor, animalSaveDAO);
                    AnimalesDAO animalDAO = await _repository.Actualizar(animal);

                    transactionScope.Complete();

                    return animalDAO;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public async Task<List<AnimalesDAO>> ActualizarVarios(IHttpContextAccessor _httpContextAccessor, List<AnimalesUpdateDAO> animales)
        {
            if (animales == null)
                throw new InvalidSintaxisException();

            ValidateKeys.ValidarPKDuplicadas(animales, p => new { p.Id }, "Id");

            List<Animale> animalesList = new List<Animale>();
            Animale animal;

            using (var TransactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    foreach (AnimalesUpdateDAO animalSaveDAO in animales)
                    {
                        animal = ValidarUpdate(_httpContextAccessor, animalSaveDAO);
                        animalesList.Add(animal);
                    }

                    List<AnimalesDAO> result = await _repository.ActualizarVarios(animalesList);

                    return result;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }


        private Animale ValidarInsert(IHttpContextAccessor _httpContextAccessor, AnimalesSaveDAO animalSaveDAO)
        {
            if (animalSaveDAO == null)
                throw new InvalidSintaxisException();

            Validator.ValidateObject(animalSaveDAO, new ValidationContext(animalSaveDAO), true);

            FuncionesSecuencias fg = new FuncionesSecuencias(_zoologicoSecuenciasPrimarias);

            animalSaveDAO.Id = fg.ObtenerSecuenciaPrimaria(
                 _httpContextAccessor,
                 Globales.CODIGO_SECUENCIA_ANIMAL,
                 animalSaveDAO.UsuarioIngreso);

            ValidarPK(animalSaveDAO.Id);

            Animale entityObject = FromDAOToEntity(_httpContextAccessor, animalSaveDAO);
            Validator.ValidateObject(entityObject, new ValidationContext(entityObject), true);
            return entityObject;
        }

        private static Resource<AnimalesDAO> GetDataWithResource(
          IHttpContextAccessor _httpContextAccessor,
          AnimalesDAO animalDAO)
        {
            string rutaSegmentoFinal = $"{animalDAO.Id}";

            return Resource<AnimalesDAO>.GetDataWithResource<AnimalesController>(
                _httpContextAccessor, rutaSegmentoFinal, animalDAO);
        }

        private Animale ValidarUpdate(
            IHttpContextAccessor _httpContextAccessor,
            AnimalesUpdateDAO animalSaveDAO)
        {
            if (animalSaveDAO == null)
                throw new InvalidSintaxisException();

            if (animalSaveDAO.UsuarioModificacion == null || animalSaveDAO.UsuarioModificacion <= 0)
                throw new InvalidFieldException("El usuario de modificación debe ser un número entero mayor a cero.");

            Animale animal = ConsultarCompletoPorId(animalSaveDAO.Id) ??
                throw new RegisterNotFoundException($"El animal con id: {animalSaveDAO.Id} no existe.");



            animal.Estado = Globales.ESTADO_ACTIVO;
            animal.FechaEstado = DateTime.Now;


            if (animalSaveDAO.Edad > 0)
                animal.Edad = animalSaveDAO.Edad;

            if (!string.IsNullOrWhiteSpace(animalSaveDAO.Nombre))
                animal.Nombre = animalSaveDAO.Nombre;

            if (!string.IsNullOrWhiteSpace(animalSaveDAO.Especie))
                animal.Especie = animalSaveDAO.Especie;

            if (!string.IsNullOrWhiteSpace(animalSaveDAO.ObservacionEstado))
                animal.ObservacionEstado = animalSaveDAO.ObservacionEstado;

            animal.FechaModificacion = DateTime.Now;
            animal.UbicacionModificacion = Ubicacion.getIpAdress(_httpContextAccessor);
            animal.UsuarioModificacion = animalSaveDAO.UsuarioModificacion;

            Validator.ValidateObject(animal, new ValidationContext(animal), true);

            return animal;
        }


        private Animale FromDAOToEntity(
            IHttpContextAccessor _httpContextAccessor,
            AnimalesSaveDAO animalSaveDAO)
        {

            return new Animale
            {
                Id = animalSaveDAO.Id,
                Nombre = animalSaveDAO.Nombre,
                Edad = animalSaveDAO.Edad,
                Especie = animalSaveDAO.Especie,
                RazaId = animalSaveDAO.Id_Raza,
                Estado = Globales.ESTADO_ACTIVO,
                FechaEstado = DateTime.Now,
                FechaIngreso = DateTime.Now,
                ObservacionEstado = animalSaveDAO.ObservacionEstado,
                UsuarioIngreso = animalSaveDAO.UsuarioIngreso,
                UbicacionIngreso = Ubicacion.getIpAdress(_httpContextAccessor)
            };
        }

        private void ValidarPK(int id)
        {
            ValidateKeys.ValidarNoExistenciaKey(
               id,
               $"Animal con Id: {id} ya existe.",
               _repository.ConsultarCompleto);
        }

        public async Task EliminarPorId(int id)
        {
            if (id <= 0)
                throw new InvalidIdException();

            await _repository.EliminarPorId(id);
        }

        public AnimalesEstadisticasDAO ObtenerEstadisticas()
        {
            return _repository.ObtenerEstadisticas();
        }
    }
}
