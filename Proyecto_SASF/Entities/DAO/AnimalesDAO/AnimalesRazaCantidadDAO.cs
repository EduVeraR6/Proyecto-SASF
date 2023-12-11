using Newtonsoft.Json;

namespace Proyecto_SASF.Entities.DAO.AnimalesDAO
{
    [JsonObject]
    public class AnimalesRazaCantidadDAO
    {
        public string Raza { get; set; }
        public int Cantidad { get; set; }
    }
}
