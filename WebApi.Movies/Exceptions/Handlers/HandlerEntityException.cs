using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Movies.Exceptions.Handlers
{
    public static class HandlerEntityException
    {
        public static void NotFound(ExceptionContext context)
        {
            if (context.Exception is not EntityNotFoundException exception) return;

            var details = new ProblemDetails()
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Entidade não encontrada",
                Type = "https://developer.mozilla.org/pt-BR/docs/Web/HTTP/Status/404",
                Detail = exception.Message
            };

            context.Result = new NotFoundObjectResult(details);
            context.ExceptionHandled = true;
        }
    }
}
