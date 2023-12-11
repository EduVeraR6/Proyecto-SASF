using AGE.Utils.Paginacion;
using AGE.Utils.WebLink;
using Proyecto_SASF.Entities.DAO.RazaDAO;

namespace Proyecto_SASF.Services.RazasService
{
    public interface IRazasService
    {
        Task<Page<RazaDAO>> ConsultarTodos(Pageable pageable);

        Task<RazaDAO> ConsultarPorId(int id);

        Task EliminarPorId(int id);
        Task<Page<RazaDAO>> ConsultarListaFiltro(string filtro, Pageable pageable);

        Task<Resource<RazaDAO>> Insertar(IHttpContextAccessor _httpContextAccessor, RazaSaveDAO razaSaveDAO);

        Task<RazaDAO> Actualizar(IHttpContextAccessor _httpContextAccessor, RazaSaveDAO razaSaveDAO);

        Task<List<Resource<RazaDAO>>> InsertarVarios(IHttpContextAccessor _httpContextAccessor, List<RazaSaveDAO> razaSaveDAOList);

        Task<List<RazaDAO>> ActualizarVarios(IHttpContextAccessor _httpContextAccessor, List<RazaSaveDAO> razaSaveDAOList);


    }
}
