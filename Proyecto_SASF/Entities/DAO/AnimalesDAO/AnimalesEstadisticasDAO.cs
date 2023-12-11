using Newtonsoft.Json;

namespace Proyecto_SASF.Entities.DAO.AnimalesDAO
{

    public class AnimalesEstadisticasDAO
    {
        public int CantidadAnimales { get; set; }
        public double PromedioEdadAnimales { get; set; }

        public int CantidadAnimalesActivos { get; set; }


        [JsonProperty]
        public List<AnimalesRazaCantidadDAO> CantidadPorRaza { get; set; }

        public AnimalesEstadisticasDAO(int cantidadAnimales, double promedioEdadAnimales, int cantidadAnimalesActivos, List<AnimalesRazaCantidadDAO> cantidadXRaza)
        {
            CantidadAnimales = cantidadAnimales;
            PromedioEdadAnimales = promedioEdadAnimales;
            CantidadAnimalesActivos = cantidadAnimalesActivos;
            CantidadPorRaza = cantidadXRaza;
        }
    }
}
