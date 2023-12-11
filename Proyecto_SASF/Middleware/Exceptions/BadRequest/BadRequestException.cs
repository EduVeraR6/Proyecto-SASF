using System.Net;

namespace AGE.Middleware.Exceptions.BadRequest
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string mensaje) : base(mensaje)
        {
        }
    }
}
