using Microsoft.AspNetCore.Http;
using System.Net;

namespace Proyecto_MVC.Middleware.Exceptions.NotImplemented
{
    public class NotImplementedException : Exception
    {
        public NotImplementedException(string mensaje) : base(mensaje)
        {
        }
    }
}
