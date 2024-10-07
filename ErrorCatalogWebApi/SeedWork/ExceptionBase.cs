using System.Linq.Expressions;

namespace ErrorCatalogWebApi.SeedWork
{
    public abstract class ExceptionBase : Exception
    {
        protected ExceptionBase()
        {
        }

        protected ExceptionBase(string errorCode, string errorMessage, params object[] errorMessageArgs) // : base(nameof(ExceptionBase))
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            ErrorMessageArgs = errorMessageArgs;
        }

        public string ErrorCode { get; protected set; }
        public string ErrorMessage { get; protected set; }

        public object[] ErrorMessageArgs { get; protected set; }
    }

    public abstract class ExceptionBase<TErrorCode> : ExceptionBase where TErrorCode : class
    {
        protected ExceptionBase(Expression<Func<TErrorCode, string>> errorCodeSelectorExpression, params object[] errorMessageArgs)
        {
            (ErrorCode, ErrorMessage) = GetErrorFromExpression(errorCodeSelectorExpression);
            ErrorMessageArgs = errorMessageArgs;

        }

        protected (string errorCode, string errorMessage) GetErrorFromExpression(Expression<Func<TErrorCode, string>> errorCodeSelectorExpression)
        {
            var errorCodeTypeName = typeof(TErrorCode).Name;
            var errorCodeMemberName = ((MemberExpression)errorCodeSelectorExpression.Body).Member.Name;
            var errorCode = $"{errorCodeTypeName}.{errorCodeMemberName}";

            var errorClass = Activator.CreateInstance<TErrorCode>();
            var errorCodeMemberValue = errorCodeSelectorExpression.Compile().Invoke(errorClass);

            return (errorCode, errorMessage: errorCodeMemberValue);
        }
    }
}
