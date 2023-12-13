namespace Proyecto_SASF.Middleware.Exceptions.Unauthorized
{
    public class NotAuthenticatedException : Exception
    {
        public NotAuthenticatedException() : base("Es necesario autenticarse para obtener la respuesta solicitada.")
        {
        }
    }
}
