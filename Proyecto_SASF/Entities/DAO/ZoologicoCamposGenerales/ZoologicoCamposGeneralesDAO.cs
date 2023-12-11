using Proyecto_SASF.Utils.AnotacionesPersonalizadas.Atributos;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Proyecto_SASF.Entities.DAO.ZoologicoCamposGenerales
{
    public class ZoologicoCamposGeneralesDAO
    {
        [JsonIgnore]
        public DateTime FechaEstado { get; set; }


        public string? ObservacionEstado { get; set; }


        [Required(ErrorMessage = "El campo usuario ingreso no puede ser nulo.")]
        [NumerosValidation(nameof(UsuarioIngreso))]
        public long UsuarioIngreso { get; set; }

        [JsonIgnore]
        public DateTime FechaIngreso { get; set; }

        [JsonIgnore]
        public string? UbicacionIngreso { get; set; }

        public long? UsuarioModificacion { get; set; }
        [JsonIgnore]
        public DateTime? FechaModificacion { get; set; }
        [JsonIgnore]
        public string? UbicacionModificacion { get; set; }


    }
}
