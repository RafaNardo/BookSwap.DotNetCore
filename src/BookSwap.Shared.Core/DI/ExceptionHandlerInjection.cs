using BookSwap.Shared.Core.Exceptions;
using BookSwap.Shared.Core.Models.Responses;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace BookSwap.Shared.Core.DI
{
    public static class ExceptionHandlerInjection
    {
        public static WebApplication UseCustomExceptionHandler(this WebApplication app)
        {
            var options = new ExceptionHandlerOptions
            {
                AllowStatusCode404Response = true,
                ExceptionHandler = ErrorHandler
            };

            app.UseExceptionHandler(options);

            return app;
        }

        private static async Task ErrorHandler(HttpContext context)
        {
            var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

            switch (exceptionHandlerFeature?.Error)
            {
                case BadRequestException badRequestEx:
                    var badReqResponse = ErrorResponse.FromBadRequestException(badRequestEx);
                    await Results.BadRequest(badReqResponse).ExecuteAsync(context);
                    break;
                case StatusCodeException:
                    await Results.NotFound().ExecuteAsync(context);
                    break;
                case ValidationException validationEx:
                    {
                        var validationResponse = ErrorResponse.FromValidationException(validationEx);
                        await Results.BadRequest(validationResponse).ExecuteAsync(context);
                        break;
                    }
            }
        }
    }
}