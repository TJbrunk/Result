using System;

namespace csharp_utils {
    /// <summary>
    /// Result child class of a typical scenario where TError is a string
    /// </summary>
    /// <typeparam name="TOk"></typeparam>
    public class Result<TOk> : Result<TOk, string>
    {
        public static implicit operator Result<TOk>(string error) => new(true, default, error);

        public static implicit operator Result<TOk>(TOk ok) => new(true, ok, default);

        private Result(bool isOk, TOk? okValue, string? errorValue) : base(isOk, okValue, errorValue) { }
    }


    /// <summary>
    /// Base Result Class.
    /// OK result and Error result types are defined by the programmer
    /// </summary>
    /// <typeparam name="TOk"></typeparam>
    /// <typeparam name="TError"></typeparam>
    public class Result<TOk, TError> {

        protected bool _isOk;
        protected TOk? _ok;
        protected TError? _error;

        public TError? ErrorValue => _error;
        public TOk? OkValue => _ok;

        protected Result() { }

        protected Result(bool isOk, TOk? okValue, TError? errorValue) {
            _isOk = isOk;
            _ok = okValue;
            _error = errorValue;
        }

        public static Result<TOk, TError> Ok(TOk ok) => new(true, ok ?? default, errorValue: default);

        public static Result<TOk, TError> Error(TError error) => new(false, okValue: default, error);

        public bool IsOk() => _isOk;

        public bool IsError() => !_isOk;

        /// <summary>
        /// Allows for creation of Result<TOk, TError> Error like:
        /// Result<TOk, TError> result = new TError();
        /// </summary>
        /// <param name="error"></param>
        public static implicit operator Result<TOk, TError>(TError error) => new(false, okValue: default, error);

        /// <summary>
        /// Allows for creation of Result<TOk, TError> Ok like:
        /// Result<TOk, TError> result = new TOk();
        /// </summary>
        /// <param name="error"></param>
        public static implicit operator Result<TOk, TError>(TOk ok) => new(true, ok, errorValue: default);

        public override string ToString() => _isOk
                ? $"Result.Ok<{typeof(TOk).Name}>: {_ok}"
                : $"Result.Error<{typeof(TError).Name}>: {_error}";
    }

}
