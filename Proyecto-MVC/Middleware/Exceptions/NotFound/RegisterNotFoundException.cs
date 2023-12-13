using System.Net;

namespace Proyecto_MVC.Middleware.Exceptions.NotFound
{
    public class RegisterNotFoundException : NotFoundException
    {
        public RegisterNotFoundException() : base("No se encontraron registros.")
        {}

        public RegisterNotFoundException(string mensaje) : base("No se encontraron registros. " + mensaje)
        { }
    }
}
