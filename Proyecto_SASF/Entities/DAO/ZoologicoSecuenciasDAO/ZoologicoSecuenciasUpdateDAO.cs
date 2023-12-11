using Proyecto_SASF.Entities.DAO.ZoologicoCamposGenerales;

namespace Proyecto_SASF.Entities.DAO.ZoologicoSecuenciasDAO
{
    public class ZoologicoSecuenciasUpdateDAO : ZoologicoCamposGeneralesDAO
    {
        public int Codigo { get; set; }

        public string? Descripcion { get; set; } = null!;

    }
}
