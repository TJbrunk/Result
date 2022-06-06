
namespace csharp_utils.Result
{
    public sealed class NullResult {
        private NullResult() { }
    }

    public class ErrorResult<TError> : Result<NullResult, TError>
    {
        public static ErrorResult<TError> Ok() => new(true, default);

        public static new ErrorResult<TError> Error(TError error) => new(false, error);
        public static implicit operator ErrorResult<TError>(TError error) => new(false, error);

        private ErrorResult(bool isOk, TError? errorValue) : base(isOk, default, errorValue) { }
    }
}
