using Proyecto_SASF.Entities.DAO.AnimalesDAO;

namespace Proyecto_SASF.Entities.DAO.RazaDAO
{
    public class RazaDAO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string? OrigenGeografico { get; set; }
        public string Estado { get; set; } = null!;
        public List<AnimalesListaDAO> AnimalesList { get; set; }
    }
}
