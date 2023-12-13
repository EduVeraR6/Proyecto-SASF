using Proyecto_SASF.Utils;
using Newtonsoft.Json;
using Proyecto_SASF.Entities.DAO.RazaDAO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_SASF.Entities.DAO.AnimalesDAO
{
    public class AnimalesDAO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; } 
        public int? Edad { get; set; }
        public string? Especie { get; set; }
        public string Estado { get; set; }
        public int id_Raza { get; set; }

        [JsonProperty]
        public RazaListDAO Raza { get; set; }
        

    }
}

