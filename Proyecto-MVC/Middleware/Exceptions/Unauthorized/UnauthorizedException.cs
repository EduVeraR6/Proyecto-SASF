using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace Proyecto_MVC.Middleware.Exceptions.Unauthorized
{
    public class UnauthorizedException : UnauthorizedAccessException
    {
        public UnauthorizedException(string mensaje) : base(mensaje)
        {
        }
    }
}
