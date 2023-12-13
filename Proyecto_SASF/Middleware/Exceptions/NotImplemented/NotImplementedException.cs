using Microsoft.AspNetCore.Http;
using System.Net;

namespace Proyecto_SASF.Middleware.Exceptions.NotImplemented
{
    public class NotImplementedException : Exception
    {
        public NotImplementedException(string mensaje) : base(mensaje)
        {
        }
    }
}
