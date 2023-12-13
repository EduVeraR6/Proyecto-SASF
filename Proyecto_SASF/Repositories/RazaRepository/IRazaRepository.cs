using Proyecto_SASF.Utils.Paginacion;
using Proyecto_SASF.Entities;
using Proyecto_SASF.Entities.DAO.RazaDAO;

namespace Proyecto_SASF.Repositories.RazaRepository
{
    public interface IRazaRepository
    {
        Task<Page<RazaDAO>> ConsultarTodos(Pageable pageable);

        Task<RazaDAO> ConsultarPorId(int id);

        Task<Raza> ConsultarCompleto(int id);

        Task<Page<RazaDAO>> ConsultarListaFiltro(string filtro, Pageable pageable);

        Task<RazaDAO> Insertar(Raza raza);

        Task<RazaDAO> Actualizar(Raza raza);

        Task<List<RazaDAO>> InsertarVarios(List<Raza> razas);

        Task<List<RazaDAO>> ActualizarVarios(List<Raza> razas);

        Task EliminarPorId(int id);

    }
}
