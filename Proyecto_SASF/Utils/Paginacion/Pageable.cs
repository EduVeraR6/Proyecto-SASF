using AGE.Middleware.Exceptions.BadRequest;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AGE.Utils.Paginacion
{
    public class Pageable
    {

        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "El campo Page debe ser un número entero positivo.")]
        public int Page { get; set; }

        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "El campo Size debe ser un número entero mayor a cero.")]
        public int Size { get; set; }

        public List<string>? Sort { get; set; }
        

        public Pageable() 
        {
            Page = 0;
            Size = Globales.CANTIDAD_ELEMENTOS_PAGINA;
            Sort = new List<string> { "Estado,ASC" };
        }


        public List<Sort<TDao>> getSortList<TDao>()
        {
            return Sort.Select(sortField => new Sort<TDao>(sortField)).ToList();
        }


        private void ValidarSortList<TDao>()
        {
            Regex regex = new Regex(@"^[a-zA-Z]+(\.[a-zA-Z]+)?,(ASC|DESC|asc|desc)$");

            Sort.ForEach(sortField =>
            {
                if (!regex.IsMatch(sortField))
                    throw new InvalidFieldException("El campo Sort debe contener el nombre del campo y la direccion de ordenamiento separado por coma.");
            });

            List<Sort<TDao>> sortList = getSortList<TDao>();

            if (sortList.GroupBy(sf => sf.FieldName, StringComparer.OrdinalIgnoreCase).Any(g => g.Count() > 1))
                throw new InvalidFieldException("Existen campos de ordenamiento repetidos en el campo Sort.");

            sortList.ForEach(s => s.ValidarFieldName());
            
        }


        public void Validar<TDao>()
        {
            ValidarSortList<TDao>();

            Validator.ValidateObject(this, new ValidationContext(this), true);
        }

    }
}
