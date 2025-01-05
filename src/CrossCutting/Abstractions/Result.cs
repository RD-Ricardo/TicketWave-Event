
namespace CrossCutting.Abstractions
{
    public class Result<T>
    {
        public bool IsFail;

        public T Value = default!;

        public List<string>? Errors;

        public ErrorType ErrorType;

        public static Result<T> Fail(Error error)
        {
            return new Result<T>
            {
                IsFail = true,
                Errors = [error.Message],
                ErrorType = error.Type
            };
        }

        public static Result<T> Fail(List<string> errors)
        {
            return new Result<T>
            {
                IsFail = true,
                Errors = errors,
                ErrorType = ErrorType.Invalid
            };
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>
            {
                IsFail = false,
                Value = value
            };
        }

        public TResult Match<TResult>(
    Func<T, TResult> onSuccess,
    Func<(ErrorType ErrorType, List<string>? Errors), TResult> onFailure)
        {
            if (IsFail)
            {
                return onFailure((ErrorType, Errors));
            }

            return onSuccess(Value);
        }


    }







    public enum ErrorType
    {
        NotFound,
        Invalid,
        Unauthorized,
        Forbidden,
        Conflict,
        InternalServerError
    }

    public abstract class Error
    {
        public string Message { get; set; }
        public ErrorType Type { get; set; }
        public Error(string message, ErrorType type)
        {
            Message = message;
            Type = type;
        }
    }

    public class NotFoundError : Error
    {
        public NotFoundError(string message) : base(message, ErrorType.NotFound) { }
    }

    public class InvalidError : Error
    {
        public InvalidError(string message) : base(message, ErrorType.Invalid) { }
    }

    public class UnauthorizedError : Error
    {
        public UnauthorizedError(string message) : base(message, ErrorType.Unauthorized) { }
    }

    public class ForbiddenError : Error
    {
        public ForbiddenError(string message) : base(message, ErrorType.Forbidden) { }
    }
}
