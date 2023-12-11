using AGE.Utils.Paginacion;
using AGE.Utils.WebLink;
using Proyecto_SASF.Entities;
using Proyecto_SASF.Entities.DAO.AnimalesDAO;

namespace Proyecto_SASF.Services.AnimalesService
{
    public interface IAnimalesService
    {

        Task<Page<AnimalesDAO>> ConsultarTodos(Pageable pageable);

        Task<AnimalesDAO> ConsultarPorId(int id);

        Task<Page<AnimalesDAO>> ConsultarListaFiltro(string filtro, Pageable pageable);

        Task<Resource<AnimalesDAO>> Insertar(IHttpContextAccessor _httpContextAccessor,AnimalesSaveDAO animal);

        Task<AnimalesDAO> Actualizar(IHttpContextAccessor _httpContextAccessor, AnimalesUpdateDAO animal);

        Task<List<Resource<AnimalesDAO>>> InsertarVarios(IHttpContextAccessor _httpContextAccessor, List<AnimalesSaveDAO> animales);

        Task<List<AnimalesDAO>> ActualizarVarios(IHttpContextAccessor _httpContextAccessor ,List<AnimalesUpdateDAO> animales);

        Task EliminarPorId(int id);

        AnimalesEstadisticasDAO ObtenerEstadisticas();

    }
}
