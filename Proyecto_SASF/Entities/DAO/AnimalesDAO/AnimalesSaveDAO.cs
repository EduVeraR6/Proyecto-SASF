using Proyecto_SASF.Entities.DAO.ZoologicoCamposGenerales;
using Proyecto_SASF.Utils.AnotacionesPersonalizadas.Atributos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proyecto_SASF.Entities.DAO.AnimalesDAO
{
    public class AnimalesSaveDAO : ZoologicoCamposGeneralesDAO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Los nombres no pueden ser nulos.")]
        [StringLength(50, ErrorMessage = "Los nombres deben contener máximo 50 caracteres.")]
        public string Nombre { get; set; } = null!;
        public int? Edad { get; set; }

        [Required(ErrorMessage = "El nombre de la especie no puede ser nulo.")]
        [StringLength(50, ErrorMessage = "La especie debe contener máximo 50 caracteres.")]
        public string Especie { get; set; } = null!;

        [Required(ErrorMessage = "La raza del animal no puede ser nulo.")]
        [NumerosValidation(nameof(Id_Raza))]
        public int Id_Raza { get; set; }
    }
}
