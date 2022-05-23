using System.Net;
using System.Text.Json;
using DAAPI.Errors;

namespace DAAPI.Middleware
{
    public class ExceptionMiddleware
    {
       private readonly RequestDelegate _next;
       private readonly ILogger<ExceptionMiddleware> _logger; 
       private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode.ToString(), ex.Message, ex.StackTrace)
                    : new ApiException(context.Response.StatusCode.ToString(), "Internal Server Error");

                    var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                    var json = JsonSerializer.Serialize(response, options);

                    await context.Response.WriteAsync(json);
            }
        }
    }
}