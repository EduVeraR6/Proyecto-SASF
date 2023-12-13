namespace Proyecto_SASF.Middleware.Exceptions.BadRequest
{
    public class InvalidPageableParamsException : BadRequestException
    {
        public InvalidPageableParamsException() : base("Parámetros de paginación no válidos.")
        {
        }

        public InvalidPageableParamsException(string mensaje) : base(mensaje)
        {
        }
    }
}
