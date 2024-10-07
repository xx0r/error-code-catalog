using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ErrorCatalogWebApi.SeedWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ErrorCatalogWebApi.Infrastructure;

public class DomainExceptionFilterAttribute(ILogger<DomainExceptionFilterAttribute> logger) : ExceptionFilterAttribute
{
    /// <inheritdoc />
    public override void OnException(ExceptionContext context)
    {
        if (context.ExceptionHandled)
        {
            base.OnException(context);
            return;
        }

        context.ExceptionHandled = true;

        // Note, that CorrelationId should be tracked through the request (scope)
        var correlationId = Guid.NewGuid().ToString();
        logger.LogError(context.Exception, $"{context.Exception.Message}{Environment.NewLine}CorrelationId: {correlationId}");

        if (context.Exception is ExceptionBase exceptionBase)
        {
            context.Result = new ErrorObjectResult(exceptionBase);

            var exceptionType = exceptionBase.GetType();

            try
            {
                logger.LogError(exceptionBase.ErrorMessage, exceptionBase.ErrorMessageArgs);
            }
            catch (Exception e)
            {
                logger.LogError($"Domain Exception: {exceptionType?.Name} {exceptionBase?.ErrorCode}");
            }

        }
        else
        {
            context.ExceptionHandled = false;
        }


        base.OnException(context);
    }
}
