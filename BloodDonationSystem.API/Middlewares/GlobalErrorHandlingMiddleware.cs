using BloodDonationSystem.Domain.Exceptions;
using BloodDonationSystem.Domain.Validations;
using System.Net;
using System.Text.Json;

namespace BloodDonationSystem.API.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public GlobalErrorHandlingMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            ErrorValidation errorValidation;

            switch (ex)
            {
                case NotFoundException:
                    errorValidation = new ErrorValidation($"{ex.Message} {ex?.InnerException?.Message}", HttpStatusCode.NotFound);
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case ValidationException validationEx:
                    errorValidation = new ErrorValidation(HttpStatusCode.BadRequest);
                    foreach (var error in validationEx.Errors)
                    {
                        errorValidation.AddError(error);
                    }
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    errorValidation = new ErrorValidation("Ocorreu um erro interno. Entre em contato com nossa equipe para mais informações. Visite: https://github.com/Flaviojcf", HttpStatusCode.InternalServerError);
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonSerializer.Serialize(errorValidation);
            context.Response.ContentType = "application/json";
            return context.Response.WriteAsync(result);
        }
    }
}
