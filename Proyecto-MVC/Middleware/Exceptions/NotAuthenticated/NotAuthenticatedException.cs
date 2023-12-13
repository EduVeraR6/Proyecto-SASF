namespace Proyecto_MVC.Middleware.Exceptions.Unauthorized
{
    public class NotAuthenticatedException : Exception
    {
        public NotAuthenticatedException() : base("Es necesario autenticarse para obtener la respuesta solicitada.")
        {
        }
    }
}
