using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Movies.Exceptions.Handlers;

namespace WebApi.Movies.Exceptions
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionFilter> _logger;
        private readonly IDictionary<Type, Action<ExceptionContext>> _excetionHandlers;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;

            _excetionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(EntityNotFoundException), HandlerEntityException.NotFound }
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleExeption(context);
            base.OnException(context);
        }

        private void HandleExeption(ExceptionContext context)
        {
            _logger.LogWarning($"Uma exceção foi gerada. \nExceção: {context.Exception.Message}");

            var type = context.Exception.GetType();
            if(_excetionHandlers.ContainsKey(type))
            {
                _logger.LogDebug("Exceção mapeanda, realizando o seu tratamento...");
                _excetionHandlers[type].Invoke(context);
                _logger.LogDebug("Exceção tratada com sucesso");
            }
            else
            {
                _logger.LogError("Exceção não mapeada", context.Exception);
                HandlerUnknownException.Handler(context);
            }

        }
    }
}
