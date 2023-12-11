using AGE.Middleware.Exceptions.BadRequest;

namespace Proyecto_SASF.Middleware.Exceptions.BadRequest
{
    public class DeleteExistingException : BadRequestException
    {
        public DeleteExistingException(string mensaje) : base(mensaje)
        {
        }
    }
}
