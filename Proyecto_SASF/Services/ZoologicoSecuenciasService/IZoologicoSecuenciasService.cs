using AGE.Utils.Paginacion;
using AGE.Utils.WebLink;
using Proyecto_SASF.Entities;
using Proyecto_SASF.Entities.DAO.ZoologicoSecuenciasDAO;

namespace Proyecto_SASF.Services.ZoologicoSecuenciasService
{
    public interface IZoologicoSecuenciasService
    {
        Task<ZoologicoSecuenciasPrimariaDAO> ConsultarPorId(
                  int codigoSecuencia);

        Task<ZoologicoSecuenciasPrimaria> ConsultarCompletePorId(
            int codigoSecuencia);

        Task<Page<ZoologicoSecuenciasPrimariaDAO>> ConsultarTodos(
            Pageable pageable);

        Task<Resource<ZoologicoSecuenciasPrimariaDAO>> Insertar(
            IHttpContextAccessor httpContextAccessor,
            ZoologicoSecuenciasSaveDAO zooSecuenciasPrimariasSaveDAO);

        Task<ZoologicoSecuenciasPrimariaDAO> Actualizar(
            IHttpContextAccessor httpContextAccessor,
            ZoologicoSecuenciasSaveDAO zooSecuenciasPrimariasSaveDAO);

        Task<List<Resource<ZoologicoSecuenciasPrimariaDAO>>> InsertarVarios(
            IHttpContextAccessor httpContextAccessor,
            List<ZoologicoSecuenciasSaveDAO> zooSecuenciasPrimariasSaveDAOList);

        Task<List<ZoologicoSecuenciasPrimariaDAO>> ActualizarVarios(
            IHttpContextAccessor httpContextAccessor,
            List<ZoologicoSecuenciasSaveDAO> zooSecuenciasPrimariasSaveDAOList);

        Task EliminarPorId(int id);

        void ActualizarSecuenciaPrimaria(
            IHttpContextAccessor _httpContextAccessor,
            ZoologicoSecuenciasPrimaria zooSecuenciasPrimaria);

                
    }
}
