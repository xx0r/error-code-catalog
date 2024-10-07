using ErrorCatalogWebApi.SeedWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ErrorCatalogWebApi.Infrastructure;

public class ErrorResult
{
    public string ErrorCode { get; set; }
    public string ErrorMessage { get; set; }
    public object[] ErrorMessageArgs { get; set; }
}

/// <summary>
/// <see cref="BadRequestObjectResult"/> (HTTP 400) with <see cref="ErrorResult.ErrorCode"/> and <see cref="ErrorResult.ErrorMessage"/>
/// </summary>
public class ErrorObjectResult : BadRequestObjectResult
{

    public ErrorObjectResult(ExceptionBase exceptionBase) : this(exceptionBase.ErrorCode, exceptionBase.ErrorMessage, exceptionBase.ErrorMessageArgs)
    {
    }

    public ErrorObjectResult(string errorCode, string errorMessage, object[] errorMessageArgs = null) : base(new ErrorResult
    {
        ErrorCode = errorCode,
        ErrorMessage = errorMessage,
        ErrorMessageArgs = errorMessageArgs
    })
    {
    }

    /// <inheritdoc />
    public ErrorObjectResult(object error) : base(error)
    {
    }

    /// <inheritdoc />
    public ErrorObjectResult(ModelStateDictionary modelState) : base(modelState)
    {
    }
}
