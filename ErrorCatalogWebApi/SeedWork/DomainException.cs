using System.Linq.Expressions;

namespace ErrorCatalogWebApi.SeedWork
{
    public class DomainException<TErrorCode>(Expression<Func<TErrorCode, string>> errorCodeSelectorExpression, params object[] errorMessageArgs) 
        : ExceptionBase<TErrorCode>(errorCodeSelectorExpression, errorMessageArgs) where TErrorCode : class;
}
