using Zoo_MVC.Models;

namespace Proyecto_MVC.Models
{
    public class AnimalViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Especie { get; set; }

        public string Estado { get; set; }

        public int id_Raza { get; set; }

        public RazaViewModel Raza = new RazaViewModel(); 

    }
}
