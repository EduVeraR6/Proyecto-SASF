using Proyecto_MVC.Middleware.Exceptions.BadRequest;
using Proyecto_MVC.Middleware.Exceptions.Forbidden;
using Proyecto_MVC.Middleware.Exceptions.NotFound;
using Proyecto_MVC.Middleware.Exceptions.Unauthorized;
using Proyecto_MVC.Middleware.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;
using System.Text.Json;
using NotImplementedException = Proyecto_MVC.Middleware.Exceptions.NotImplemented.NotImplementedException;

namespace Proyecto_SASF.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

                if (httpContext.Response.StatusCode == (int)HttpStatusCode.Unauthorized) //401
                {
                    throw new UnauthorizedException("No tiene acceso.");
                }

                if (httpContext.Response.StatusCode == (int)HttpStatusCode.Forbidden) //403
                {
                    throw new AccessRestrictedException();
                }

            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var response = context.Response;

            var errorResponse = new ErrorResponse();

            switch (exception.GetBaseException())
            {
                case BadRequestException ex:
                    response.StatusCode = 400;
                    errorResponse.message = ex.Message;
                    errorResponse.code = HttpStatusCode.BadRequest;
                    errorResponse.exception = ex.GetType().Name.ToString();
                    break;

                case NotFoundException ex:
                    response.StatusCode = 404;
                    errorResponse.message = ex.Message;
                    errorResponse.code = HttpStatusCode.NotFound;
                    errorResponse.exception = ex.GetType().Name.ToString();
                    break;

                case ForbiddenException ex:
                    response.StatusCode = 403;
                    errorResponse.message = ex.Message;
                    errorResponse.code = HttpStatusCode.Forbidden;
                    errorResponse.exception = ex.GetType().Name.ToString();
                    break;

                case UnauthorizedException ex:
                    response.StatusCode = 401;
                    errorResponse.message = ex.Message;
                    errorResponse.code = HttpStatusCode.Unauthorized;
                    errorResponse.exception = ex.GetType().Name.ToString();
                    break;

                case NotImplementedException ex:
                    response.StatusCode = 501;
                    errorResponse.message = ex.Message;
                    errorResponse.code = HttpStatusCode.NotImplemented;
                    errorResponse.exception = ex.GetType().Name.ToString();
                    break;

                case ValidationException ex:
                    response.StatusCode = 400;
                    errorResponse.message = ex.Message;
                    errorResponse.code = HttpStatusCode.BadRequest;
                    errorResponse.exception = ex.GetType().Name.ToString();
                    break;
                    
                case InvalidOperationException ex:
                    response.StatusCode = 400;
                    errorResponse.message = ex.Message;
                    errorResponse.code = HttpStatusCode.BadRequest;
                    errorResponse.exception = ex.GetType().Name.ToString();
                    break;

                case NullReferenceException ex:
                    response.StatusCode = 400;
                    errorResponse.message = ex.Message;
                    errorResponse.code = HttpStatusCode.BadRequest;
                    errorResponse.exception = ex.GetType().Name.ToString();
                    break;

                case InvalidCastException ex:
                    response.StatusCode = 400;
                    errorResponse.message = ex.Message;
                    errorResponse.code = HttpStatusCode.BadRequest;
                    errorResponse.exception = ex.GetType().Name.ToString();
                    break;

                default:
                    response.StatusCode = 500;
                    errorResponse.message = "Error interno del servidor.";
                    errorResponse.code = HttpStatusCode.InternalServerError;
                    errorResponse.exception = exception.GetType().Name.ToString();
                    break;
            }

            _logger.LogError(exception.Message);
            var result = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(result);
        }
    }
}
