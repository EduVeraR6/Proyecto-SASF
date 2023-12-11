namespace AGE.Middleware.Exceptions.BadRequest
{
    public class JsonDeserializationException : BadRequestException
    {
        public JsonDeserializationException()
            : base("Error al convertir el arreglo de bytes en lista.")
        {
        }

        public JsonDeserializationException(string message)
            : base("Error al convertir el arreglo de bytes en lista. " + message)
        {
        }
    }
}
