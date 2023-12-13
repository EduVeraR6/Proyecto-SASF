using Proyecto_SASF.Middleware.Exceptions.BadRequest;
using System.Reflection;

namespace Proyecto_SASF.Utils.Paginacion
{
    public class Sort<TDao>
    {
        
        public string FieldName { get; set; }

        public string Direction { get; set; }


        public Sort(string sortField) 
        {
            FieldName = getFieldName(sortField);
            Direction = getDirection(sortField);
        }


        private string getFieldName(string sortField)
        {
            string FieldName = sortField.Split(",")[0];

            if (FieldName.Split(".").Length == 1)
                return FieldName;

            if (!FieldName.Split(".")[0].ToUpper().Equals("ID"))
                throw new InvalidPageableParamsException($"El campo Sort debe coincidir con el nombre de un miembro de clase de {typeof(TDao).Name}.");

            return FieldName.Split(".")[1];
        }


        private string getDirection(string sortField)
        {
            return sortField.Split(",")[1];
        }


        public void ValidarFieldName()
        {
            PropertyInfo[] propiedades = typeof(TDao).GetProperties();

            bool isValid = propiedades.Any(propiedad => propiedad.Name.ToUpper().Equals(FieldName.ToUpper()) &&
                                                        propiedad.Name.ToUpper() != "ID") ||

                           propiedades.Any(propiedad => propiedad.Name.ToUpper().Equals("ID") &&
                                                        propiedad.PropertyType.GetProperties()
                                                       .Any(anidada => anidada.Name.ToUpper()
                                                                       .Equals(FieldName.ToUpper())));

            if (!isValid)
                throw new InvalidPageableParamsException($"El campo Sort debe coincidir con el nombre de un miembro de clase de {typeof(TDao).Name}.");

        }

    }
}
