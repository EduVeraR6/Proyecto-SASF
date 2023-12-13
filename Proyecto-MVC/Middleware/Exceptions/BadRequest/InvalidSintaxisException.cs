namespace Proyecto_MVC.Middleware.Exceptions.BadRequest
{
    public class InvalidSintaxisException : BadRequestException
    {
        public InvalidSintaxisException() : base("Sintaxis no válida.")
        {
        }
    }
}
