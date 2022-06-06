using System;

namespace csharp_utils
{
    public static partial class ResultExtension
    {
        /// <summary>
        /// Function to execute if Result is an error
        /// Function must return a TResult
        /// </summary>
        /// <typeparam name="TOk"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="self"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Result<TOk, TError> OrElse<TOk, TError>(this Result<TOk, TError> self, Func<TError, Result<TOk, TError>> func) {
            if(func is null || !self.IsError())
            {
                return self;
            }
            else
            {
                return func.Invoke(self.ErrorValue);
            }
        }

        /// <summary>
        /// Action to execute if Result is an error
        /// </summary>
        /// <typeparam name="TOk"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="self"></param>
        /// <param name="action"></param>
        public static void OrElse<TOk, TError>(this Result<TOk, TError> self, Action<TError> action) {
            if (self.IsError() && action != null) {
                action.Invoke(self.ErrorValue);
            }
        }
    }
}