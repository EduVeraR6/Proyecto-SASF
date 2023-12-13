
using Proyecto_SASF.Middleware.Exceptions.NotFound;
using Proyecto_SASF.Entities;
using Proyecto_SASF.Services.ZoologicoSecuenciasService;

namespace Proyecto_SASF.Utils
{
    public class FuncionesSecuencias
    {
        private readonly IZoologicoSecuenciasService _zooSecuService;


        public FuncionesSecuencias(IZoologicoSecuenciasService zooSecuenciasPrimariasService)
        {
            _zooSecuService = zooSecuenciasPrimariasService;
        }
        
        public int ObtenerSecuenciaPrimaria(
            IHttpContextAccessor _httpContextAccessor,
            int codigoSecuencia,
            long Usuario)
        {

            ZoologicoSecuenciasPrimaria? zooSecuenciasPrimaria = _zooSecuService
                .ConsultarCompletePorId(codigoSecuencia).Result;

            if (zooSecuenciasPrimaria == null)
                throw new RegisterNotFoundException($"Secuencia primaria con código {codigoSecuencia} no existe.");

            int id;

            if (zooSecuenciasPrimaria.ValorInicial > zooSecuenciasPrimaria.ValorActual)
                id = zooSecuenciasPrimaria.ValorInicial;
            else
                id = zooSecuenciasPrimaria.ValorActual+ zooSecuenciasPrimaria.IncrementaEn;

            zooSecuenciasPrimaria.ValorActual= id;
            zooSecuenciasPrimaria.UsuarioModificacion = Usuario;

            _zooSecuService.ActualizarSecuenciaPrimaria(_httpContextAccessor, zooSecuenciasPrimaria);

            return id;
        }


    }
}
