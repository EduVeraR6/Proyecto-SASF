using Proyecto_SASF.Entities.DAO.ZoologicoCamposGenerales;

namespace Proyecto_SASF.Entities.DAO.AnimalesDAO
{
    public class AnimalesUpdateDAO : ZoologicoCamposGeneralesDAO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public int Edad { get; set; }
        public string Especie { get; set; } = null!;
        public int Id_Raza { get; set; }
    }
}
