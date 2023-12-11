using Proyecto_SASF.Entities.DAO.ZoologicoCamposGenerales;

namespace Proyecto_SASF.Entities.DAO.RazaDAO
{
    public class RazaSaveDAO : ZoologicoCamposGeneralesDAO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string? OrigenGeografico { get; set; }
    }
}
