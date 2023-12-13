namespace Proyecto_MVC.Middleware.Exceptions.Forbidden
{
    public class AccessRestrictedException : ForbiddenException
    {
        public AccessRestrictedException() : base("No cuenta con los permisos necesarios para ejecutar esta acción.")
        {
        }
    }
}
