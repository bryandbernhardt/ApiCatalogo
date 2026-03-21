using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiCatalogo.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiLoggingFilter> _logger;
    public ApiExceptionFilter(ILogger<ApiLoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "An unhandled error occurred: Status Code 500");
        context.Result = new ObjectResult("an error occurred while handling your request: Status Code 500")
        {
            StatusCode = StatusCodes.Status500InternalServerError,
        };
    }
}