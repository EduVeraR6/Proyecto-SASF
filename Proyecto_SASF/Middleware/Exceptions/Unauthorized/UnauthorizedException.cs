using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace Proyecto_SASF.Middleware.Exceptions.Unauthorized
{
    public class UnauthorizedException : UnauthorizedAccessException
    {
        public UnauthorizedException(string mensaje) : base(mensaje)
        {
        }
    }
}
