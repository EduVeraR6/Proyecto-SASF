using Proyecto_MVC.Middleware.Exceptions.BadRequest;

namespace Proyecto_SASF.Middleware.Exceptions.BadRequest
{
    public class ModelStateInvalidException : BadRequestException
    {
        public ModelStateInvalidException(string mensaje) : base(mensaje)
        {
        }
    }
}
