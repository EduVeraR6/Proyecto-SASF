using System.ComponentModel.DataAnnotations;

namespace Proyecto_SASF.Utils.AnotacionesPersonalizadas.Atributos
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NumerosValidationAttribute : RegularExpressionAttribute
    {
        public NumerosValidationAttribute(string propertyName) : base("^[1-9]\\d*$")
        {
            ErrorMessage = $"El {propertyName} debe ser un número entero mayor a cero.";
        }
    }
}
