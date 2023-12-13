using Microsoft.AspNetCore.WebUtilities;
using System.Net;

namespace Proyecto_MVC.Middleware.Exceptions.Forbidden
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string mensaje) : base(mensaje)
        {
        }
    }
}
