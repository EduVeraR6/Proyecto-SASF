namespace Proyecto_SASF.Middleware.Exceptions.BadRequest
{
    public class UserDuplicateException : BadRequestException
    {
        public UserDuplicateException()
            : base("El registro se encuentra registrado varias veces.")
        {
        }

        public UserDuplicateException(string message)
            : base("Usuario: " + message + " se encuentra registrado varias veces. ")
        {
        }
    }
}
