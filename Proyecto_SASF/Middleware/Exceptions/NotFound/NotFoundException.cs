using System.Net;

namespace Proyecto_SASF.Middleware.Exceptions.NotFound
{
    public class NotFoundException : Exception
    {
        //internal string mensaje;

        public NotFoundException(string mensaje) : base(mensaje)
        {
            //mensaje = base.Message;
        }
    }
}
