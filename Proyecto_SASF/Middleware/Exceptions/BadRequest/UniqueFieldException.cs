namespace Proyecto_SASF.Middleware.Exceptions.BadRequest
{
    public class UniqueFieldException : BadRequestException
    {
        public UniqueFieldException(string Fieldname) : base($"El campo {Fieldname} debe ser único.")
        {
        }

        public UniqueFieldException(string Fieldname, string Mensaje) : base($"El campo {Fieldname} debe ser único. {Mensaje}")
        {
        }
    }
}
