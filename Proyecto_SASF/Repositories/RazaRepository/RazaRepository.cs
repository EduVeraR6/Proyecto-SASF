using Proyecto_SASF.Middleware.Exceptions.NotFound;
using Proyecto_SASF.Utils;
using Proyecto_SASF.Utils.Paginacion;
using Microsoft.EntityFrameworkCore;
using Proyecto_SASF.Entities;
using Proyecto_SASF.Entities.DAO.AnimalesDAO;
using Proyecto_SASF.Entities.DAO.RazaDAO;
using Proyecto_SASF.Middleware.Exceptions.BadRequest;

namespace Proyecto_SASF.Repositories.RazaRepository
{
    public class RazaRepository : IRazaRepository
    {
        private readonly ZoologicoContext _context;

        public RazaRepository(ZoologicoContext context)
        {
            _context = context;
        }

        public async Task<Raza> ConsultarCompleto(int id)
        {
            if (_context.Razas == null)
            {
                throw new RegisterNotFoundException();
            }

            Raza? raza = await _context.Razas
                .Where(a => a.Estado != Globales.ESTADO_ANULADO && a.Id == id).FirstOrDefaultAsync();


            return raza;
        }

        public async Task<Page<RazaDAO>> ConsultarListaFiltro(string filtro, Pageable pageable)
        {
            if (_context.Razas == null)
            {
                throw new RegisterNotFoundException();
            }

            var Query = _context.Razas.Where(a => a.Estado != Globales.ESTADO_ANULADO && (
                        a.Nombre.ToLower().Contains(filtro) ||
                        a.OrigenGeografico.ToLower().Contains(filtro)));

            return await Paginator<Raza, RazaDAO>.Paginar(Query, pageable, FromEntityToDAO);
        }

        public async Task<RazaDAO> ConsultarPorId(int id)
        {
            if (_context.Razas == null)
            {
                throw new RegisterNotFoundException();
            }

            Raza? raza = await _context.Razas
                .Where(a => a.Estado != Globales.ESTADO_ANULADO && a.Id == id).FirstOrDefaultAsync();


            return FromEntityToDAO(raza);
        }

        public async Task<Page<RazaDAO>> ConsultarTodos(Pageable pageable)
        {
            if (_context.Razas == null)
            {
                throw new RegisterNotFoundException();
            }

            var Query = _context.Razas.Where(a => a.Estado != Globales.ESTADO_ANULADO);

            return await Paginator<Raza, RazaDAO>.Paginar(Query, pageable, FromEntityToDAO);
        }

        public async Task<RazaDAO> Insertar(Raza raza)
        {
            _context.Razas.Add(raza);
            await _context.SaveChangesAsync();

            RazaDAO razaDAO = await ConsultarPorId(raza.Id);


            return razaDAO;
        }

        public async Task<List<RazaDAO>> InsertarVarios(List<Raza> razas)
        {
            await _context.Razas.AddRangeAsync(razas);
            await _context.SaveChangesAsync();
            return ConversorEntityToDAOList<Raza, RazaDAO>.FromEntityToDAOList(razas, FromEntityToDAO);
        }

        public async Task<RazaDAO> Actualizar(Raza raza)
        {
            _context.Entry(raza).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            RazaDAO razaDAO = await ConsultarPorId(raza.Id);

            return razaDAO;
        }

        public async Task<List<RazaDAO>> ActualizarVarios(List<Raza> razas)
        {
            _context.Razas.UpdateRange(razas);
            await _context.SaveChangesAsync();

            return ConversorEntityToDAOList<Raza, RazaDAO>.FromEntityToDAOList(razas, FromEntityToDAO);
        }

        private RazaDAO FromEntityToDAO(Raza raza)
        {
            RazaDAO razaDAO = null;
            if (raza != null)
            {
                razaDAO = new RazaDAO
                {
                    Id = raza.Id,
                    Descripcion = raza.Descripcion,
                    Estado = raza.Estado,
                    Nombre = raza.Nombre,
                    OrigenGeografico = raza.OrigenGeografico,
                    AnimalesList = new List<AnimalesListaDAO>()
                };

                var animalesDeRaza = _context.Animales.Where(a => a.RazaId == raza.Id && a.Estado.Equals(Globales.ESTADO_ACTIVO)).ToList();

                foreach (var animal in animalesDeRaza)
                {
                    var animalListaDAO = FromEntityToAnimalesListDAO(animal);
                    razaDAO.AnimalesList.Add(animalListaDAO);
                }
            }
            return razaDAO;
        }

        private AnimalesListaDAO FromEntityToAnimalesListDAO(Animale animal)
        {
            AnimalesListaDAO animalesListaDAO = null;
            if (animal != null)
            {
                animalesListaDAO = new AnimalesListaDAO
                {
                    Id = animal.Id,
                    Nombre = animal.Nombre,
                    Edad = animal.Edad,
                    Especie = animal.Especie,
                    id_Raza = animal.RazaId

                };
            }
            return animalesListaDAO;
        }

        public async Task EliminarPorId(int id)
        {
            if (_context.Razas == null)
                throw new RegisterNotFoundException();

            var raza = await _context.Razas.FindAsync(id);

            if (raza == null)
                throw new RegisterNotFoundException($"No se encontro la raza con el {id}");

            if (raza.Estado.ToLower().Equals(Globales.ESTADO_ACTIVO.ToLower()))
            {
                raza.Estado = Globales.ESTADO_ANULADO;
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new DeleteExistingException($"La raza con el id {id} ya esta eliminada ");
            }

        }
    }
}
