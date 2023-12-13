using System.Net;

namespace Proyecto_MVC.Middleware.Exceptions.BadRequest
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string mensaje) : base(mensaje)
        {
        }
    }
}
