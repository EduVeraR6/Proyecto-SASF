using Proyecto_SASF.Utils.Paginacion;
using Proyecto_SASF.Entities;
using Proyecto_SASF.Entities.DAO.ZoologicoSecuenciasDAO;

namespace Proyecto_SASF.Repositories.ZoologicoSecuenciasRepository
{
    public interface IZoologicoSecuenciasRepository
    {
        Task<ZoologicoSecuenciasPrimariaDAO> ConsultarPorId(
            int codigoSecuencia);

        Task<ZoologicoSecuenciasPrimaria> ConsultarCompletePorId(
            int codigoSecuencia);

        Task<Page<ZoologicoSecuenciasPrimariaDAO>> ConsultarTodos(
            Pageable pageable);

        Task<ZoologicoSecuenciasPrimariaDAO> Insertar(
            ZoologicoSecuenciasPrimaria ageSecuenciasPrimaria);

        Task<ZoologicoSecuenciasPrimariaDAO> Actualizar(
            ZoologicoSecuenciasPrimaria ageSecuenciasPrimaria);

        Task<List<ZoologicoSecuenciasPrimariaDAO>> InsertarVarios(
            List<ZoologicoSecuenciasPrimaria> ageSecuenciasPrimariaList);

        Task<List<ZoologicoSecuenciasPrimariaDAO>> ActualizarVarios(
            List<ZoologicoSecuenciasPrimaria> ageSecuenciasPrimariaList);

        Task EliminarPorId(int id);

        void ActualizarSecuenciaPrimaria(
            ZoologicoSecuenciasPrimaria ageSecuenciasPrimaria);


    }
}
