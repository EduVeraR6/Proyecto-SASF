using Newtonsoft.Json;

namespace Proyecto_SASF.Entities.DAO.RazaDAO
{
    [JsonObject]
    public class RazaListDAO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string? OrigenGeografico { get; set; }
    }
}
