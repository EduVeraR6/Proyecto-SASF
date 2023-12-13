using System.ComponentModel.DataAnnotations;

namespace Proyecto_SASF.Utils.AnotacionesPersonalizadas.Atributos
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UbicacionValidationAttribute : RegularExpressionAttribute
    {
        public UbicacionValidationAttribute(string ubicacion) : base("^[0-9.]+$")
        {
            ErrorMessage = $"La propiedad {ubicacion}  debe contener máximo 200 caracteres.";
        }
    }
}
