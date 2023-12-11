using AGE.Utils.Paginacion;
using Proyecto_SASF.Entities;
using Proyecto_SASF.Entities.DAO.AnimalesDAO;

namespace Proyecto_SASF.Repositories.AnimalesRepository
{
    public interface IAnimalesRepository
    {
        Task<Page<AnimalesDAO>> ConsultarTodos(Pageable pageable);

        Task<AnimalesDAO> ConsultarPorId(int id);

        Task<Animale> ConsultarCompleto(int id);

        Task<Page<AnimalesDAO>> ConsultarListaFiltro(string filtro,Pageable pageable);

        Task<AnimalesDAO> Insertar(Animale animal);

        Task<AnimalesDAO> Actualizar(Animale animal);

        Task<List<AnimalesDAO>> InsertarVarios(List<Animale> animales);

        Task<List<AnimalesDAO>> ActualizarVarios(List<Animale> animales);

        AnimalesEstadisticasDAO ObtenerEstadisticas();

        Task EliminarPorId(int id);
    }
}
