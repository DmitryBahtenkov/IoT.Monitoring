using Iot.Main.Domain.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Iot.Main.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BusinessException businessException)
            {
                context.Result = new JsonResult(new ExceptionResult(businessException.Message))
                {
                    StatusCode = 400
                };
                context.ExceptionHandled = true;
            }
            else if (context.Exception is ValidationException validationException)
            {
                context.Result = new JsonResult(validationException.Result.Errors)
                {
                    StatusCode = 422
                };
                context.ExceptionHandled = true;
            }
            else
            {
                context.Result = new JsonResult(new ExceptionResult(context.Exception.ToString()))
                {
                    StatusCode = 500
                };
                context.ExceptionHandled = true;
            }
        }
    }

    public record ExceptionResult(string Message);
}