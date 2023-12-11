using AGE.Utils;
using System.ComponentModel.DataAnnotations;

namespace Proyecto_SASF.Utils.AnotacionesPersonalizadas.Atributos
{
    [AttributeUsage(AttributeTargets.Property)]
    public class EstadoValidationAttribute : RegularExpressionAttribute
    {
        public EstadoValidationAttribute() : base("^[AN]$")
        {
            ErrorMessage = $"El campo estado debe ser {Globales.ESTADO_ACTIVO}";
        }
    }
}
