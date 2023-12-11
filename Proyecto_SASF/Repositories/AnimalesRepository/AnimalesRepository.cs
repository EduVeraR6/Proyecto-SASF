using AGE.Middleware.Exceptions.NotFound;
using AGE.Utils;
using AGE.Utils.Paginacion;
using Microsoft.EntityFrameworkCore;
using Proyecto_SASF.Entities;
using Proyecto_SASF.Entities.DAO.AnimalesDAO;
using Proyecto_SASF.Entities.DAO.RazaDAO;
using Proyecto_SASF.Middleware.Exceptions.BadRequest;

namespace Proyecto_SASF.Repositories.AnimalesRepository
{
    public class AnimalesRepository : IAnimalesRepository
    {
        private readonly ZoologicoContext _context;

        public AnimalesRepository(ZoologicoContext context)
        {
            _context = context;
        }

        public async Task<Animale> ConsultarCompleto(int id)
        {
            if (_context.Animales == null)
            {
                throw new RegisterNotFoundException();
            }

            Animale? animal = await _context.Animales
                .Where(a => a.Estado != Globales.ESTADO_ANULADO && a.Id == id).FirstOrDefaultAsync();


            return animal;
        }

        public async Task<Page<AnimalesDAO>> ConsultarListaFiltro(string filtro, Pageable pageable)
        {
            if (_context.Animales == null)
            {
                throw new RegisterNotFoundException();
            }

            var Query = _context.Animales.Where(a => a.Estado != Globales.ESTADO_ANULADO && (
                        a.Edad.ToString().ToLower().Contains(filtro) || a.Nombre.ToLower().Contains(filtro) || a.Especie.ToLower().Contains(filtro)));

            return await Paginator<Animale, AnimalesDAO>.Paginar(Query, pageable, FromEntityToDAO);
        }

        public async Task<AnimalesDAO> ConsultarPorId(int id)
        {
            if (_context.Animales == null)
            {
                throw new RegisterNotFoundException();
            }

            Animale? animal = await _context.Animales
                .Where(a => a.Estado != Globales.ESTADO_ANULADO && a.Id == id).FirstOrDefaultAsync();


            return FromEntityToDAO(animal);
        }

        public async Task<Page<AnimalesDAO>> ConsultarTodos(Pageable pageable)
        {
            if (_context.Animales == null)
            {
                throw new RegisterNotFoundException();
            }

            var Query = _context.Animales.Where(a => a.Estado != Globales.ESTADO_ANULADO);

            return await Paginator<Animale, AnimalesDAO>.Paginar(Query, pageable, FromEntityToDAO);
        }

        public async Task<AnimalesDAO> Insertar(Animale animal)
        {
            var raza = await _context.Razas.FindAsync(animal.RazaId);

            if (raza == null)
                throw new NotFoundException($"Raza con el id {animal.RazaId} no encontrada");

            if (raza.Estado == Globales.ESTADO_ANULADO)
                throw new NotFoundException($"Raza con el id {animal.RazaId} no encontrada");


            _context.Animales.Add(animal);
            await _context.SaveChangesAsync();

            AnimalesDAO animalDAO = await ConsultarPorId(animal.Id);

            return animalDAO;
        }

        public async Task<List<AnimalesDAO>> InsertarVarios(List<Animale> animales)
        {
            foreach(var animal in animales)
            {
                var raza = await _context.Razas.FindAsync(animal.RazaId);

                if (raza == null)
                    throw new NotFoundException($"Raza con el id {animal.RazaId} no encontrada");

                if (raza.Estado == Globales.ESTADO_ANULADO)
                    throw new NotFoundException($"Raza con el id {animal.RazaId} no encontrada");
            }

            await _context.Animales.AddRangeAsync(animales);
            await _context.SaveChangesAsync();
            return ConversorEntityToDAOList<Animale, AnimalesDAO>.FromEntityToDAOList(animales, FromEntityToDAO);
        }

        public async Task<AnimalesDAO> Actualizar(Animale animal)
        {
            _context.Entry(animal).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            AnimalesDAO animalDAO = await ConsultarPorId(animal.Id);

            return animalDAO;
        }

        public async Task<List<AnimalesDAO>> ActualizarVarios(List<Animale> animales)
        {
            _context.Animales.UpdateRange(animales);
            await _context.SaveChangesAsync();

            return ConversorEntityToDAOList<Animale, AnimalesDAO>.FromEntityToDAOList(animales, FromEntityToDAO);
        }

        private AnimalesDAO FromEntityToDAO(Animale animal)
        {
            AnimalesDAO animalDAO = null;
            if (animal != null)
            {
                animalDAO = new AnimalesDAO
                {
                    Id = animal.Id,
                    Edad = animal.Edad,
                    Especie = animal.Especie,
                    Estado = animal.Estado,
                    Nombre = animal.Nombre,
                    id_Raza = animal.RazaId
                };

                var busqueda = _context.Razas.Find(animal.RazaId);

                if (busqueda != null)
                {
                    animalDAO.Raza = new RazaListDAO();

                    animalDAO.Raza.Nombre = busqueda.Nombre;
                    animalDAO.Raza.Descripcion = busqueda.Descripcion;
                    animalDAO.Raza.OrigenGeografico = busqueda.OrigenGeografico;
                    animalDAO.Raza.Id = busqueda.Id;
                }
            }
            return animalDAO;
        }

        public async Task EliminarPorId(int id)
        {
            if (_context.Animales == null)
            {
                throw new RegisterNotFoundException();
            }

            var animal = await _context.Animales.FindAsync(id);

            if (animal == null)
                throw new RegisterNotFoundException($"No se encontro el animal con el id:{id}");

            if (animal.Estado.ToLower().Equals(Globales.ESTADO_ACTIVO.ToLower()))
            {
                animal.Estado = Globales.ESTADO_ANULADO;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new RegisterNotFoundException($"No se encontro el animal con el id:{id}");
            }


        }

        public AnimalesEstadisticasDAO ObtenerEstadisticas()
        {
            var activos = _context.Animales.Count(a => a.Estado.Equals(Globales.ESTADO_ACTIVO));
            var total = _context.Animales.Count();
            var promedio = (double)_context.Animales.Average(a => a.Edad);

            var estadisticasPorRaza = _context.Razas.Select(raza => new AnimalesRazaCantidadDAO
            {
                Raza = raza.Nombre,
                Cantidad = _context.Animales.Count(a => a.RazaId == raza.Id && a.Estado.Equals(Globales.ESTADO_ACTIVO))
            }).ToList();

            AnimalesEstadisticasDAO result =
                new AnimalesEstadisticasDAO(total, Math.Round(promedio), activos, estadisticasPorRaza);

            return result;
        }






    }
}
