using System.ComponentModel.DataAnnotations;

namespace Proyecto_MVC.Models.DAO
{
    public class RazaDAO
    {
        public string ObservacionEstado { get; set; }

        public int UsuarioIngreso { get; set; } = 1;

        public int UsuarioModificacion { get; set; } = 1;

        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es obligatorio")]
        [RegularExpression(@"^[^\d\s]+(?:\s[^\d\s]+){0,2}(?:\s[^\d\s]+)?$", ErrorMessage = "Solo se permiten letras")]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string OrigenGeografico { get; set; }

        public List<AnimalDAO> AnimalesList { get; set; }


    }
}
