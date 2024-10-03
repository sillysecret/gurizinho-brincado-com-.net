using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

public class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;

    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var requestPath = context.HttpContext.Request.Path;
        var method = context.HttpContext.Request.Method;
        var exceptionMessage = context.Exception?.Message;
        var exceptionStackTrace = context.Exception?.StackTrace;

        _logger.LogError(context.Exception,
            "Erro não tratado ocorrido: {Message} | Caminho: {RequestPath} | Método: {Method} | Status Code 500",
            exceptionMessage, requestPath, method);

        context.Result = new ObjectResult(new
        {
            Message = "Ocorreu um problema ao tratar a sua solicitação. Por favor, tente novamente mais tarde.",
            ErrorCode = "ERR500",
            RequestPath = requestPath,
            Timestamp = DateTime.UtcNow
        })
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}
