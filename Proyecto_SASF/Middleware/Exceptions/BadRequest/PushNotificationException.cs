namespace AGE.Middleware.Exceptions.BadRequest
{
    public class PushNotificationException : BadRequestException
    {
        public PushNotificationException() : base("Error al enviar la notificación.")
        {
        }

        public PushNotificationException(string mensaje) : base("Error al enviar la notificación." + mensaje)
        {
        }
    }
}
