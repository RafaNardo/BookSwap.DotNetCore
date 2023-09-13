using BookSwap.Shared.Core.Exceptions;
using BookSwap.Shared.Core.Models;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace BookSwap.Shared.Core.Di.Exceptions
{
    public static class ExceptionExtensions
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
                case StatusCodeException:
                    await Results.NotFound().ExecuteAsync(context);
                    break;
                case ValidationException exception:
                {
                    var response = ErrorResponse.FromValidationException(exception);
                    await Results.BadRequest(response).ExecuteAsync(context);
                    break;
                }
            }
        }
    }
}
