using System.Net;
using System.Text.Json;

namespace premium_calculator_api.CustomMiddlerWare
{
    public class GlobalExceptionMiddleware: IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context); // continue pipeline
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            // For demo: always 500; you can map custom exception types.
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResult = new
            {
                Success = false,
                Message = ex.Message,
                Details = ex.InnerException?.Message,
                Path = context.Request.Path
            };

            return response.WriteAsync(JsonSerializer.Serialize(errorResult));
        }
    }
}
