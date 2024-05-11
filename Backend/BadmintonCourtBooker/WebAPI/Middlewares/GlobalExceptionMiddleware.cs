using Application.ErrorHandlers;
using System.Net;

namespace WebAPI.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = GetExceptionStatusCode(ex);

            var responseDetail = new ErrorDetail()
            {
                StatusCode = context.Response.StatusCode,
                Title = GetExceptionTitle(ex),
                Message = ex.Message
            };

            return context.Response.WriteAsync(responseDetail.ToString());
        }

        private static int GetExceptionStatusCode(Exception ex)
        {
            if (ex is BaseException exception)
            {
                return exception.StatusCode;
            }
            return (int)HttpStatusCode.InternalServerError;
        }

        private static string GetExceptionTitle(Exception ex)
        {
            if (ex is BaseException exception)
            {
                return exception.Title ?? string.Empty;
            }
            return "Internal Server Error";
        }
    }
}
