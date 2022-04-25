using System;

namespace csharp_utils
{
    public static class ResultExtension
    {
        public static TResult? OrElse<TOk, TError, TResult>(this Result<TOk, TError> self, Func<TError, TResult> func) =>
            self.IsError() && func != null
                ? func.Invoke(self.ErrorValue)
                : default;

        public static TResult? AndThen<TOk, TError, TResult>(this Result<TOk, TError> self, Func<TOk, TResult> func) =>
            self.IsOk() && func != null
                ? func.Invoke(self.OkValue)
                : default;

        public static TResult? Match<TOk, TError, TResult>(
                this Result<TOk, TError> self,
                Func<TOk, TResult>? Ok = null,
                Func<TError, TResult>? Error = null)
        {

            if (Ok is null && Error is null)
            {
                return default;
            }

            if (self.IsOk() && Ok != null)
            {
                return Ok(self.OkValue);
            }
            else if (self.IsError() && Error != null)
            {
                return Error(self.ErrorValue);
            }
            else
            {
                return default;
            }
        }
    }
}