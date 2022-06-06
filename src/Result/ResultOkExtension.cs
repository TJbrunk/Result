using System;
using System.Threading.Tasks;

namespace csharp_utils
{
    public static partial class ResultExtension
    {
        /// <summary>
        /// Function to execute if Result is OK.
        /// Function must return a TResult
        /// </summary>
        /// <typeparam name="TOk"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="self"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Result<TOk, TError> AndThen<TOk, TError>(this Result<TOk, TError> self, Func<TOk, Result<TOk, TError>> func)
        {
            if(func == null || !self.IsOk())
            {
                return self;
            }
            else
            {
                return func.Invoke(self.OkValue);
            }


        }

        /// <summary>
        /// Action to execute if Result is OK
        /// </summary>
        /// <typeparam name="TOk"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="self"></param>
        /// <param name="action"></param>
        public static void AndThen<TOk, TError>(this Result<TOk, TError> self, Action<TOk> action)
        {
            if(self.IsOk() && action != null)
            {
                action.Invoke(self.OkValue);
            }

        }

        /// <summary>
        /// Awaits the task then executes the Action
        /// </summary>
        /// <typeparam name="TOk"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="self"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task AndThen<TOk, TError>(this Task<Result<TOk, TError>> self, Action<TOk> action)
        {
            var result = await self;

            if(result.IsOk() && action != null)
            {
                action.Invoke(result.OkValue);
            }
        }

        /// <summary>
        /// Awaits the task then executes the Function and returns TResult
        /// </summary>
        /// <typeparam name="TOk"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="self"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static async Task<TResult> AndThen<TOk, TError, TResult>(this Task<Result<TOk, TError>> self, Func<TOk, TResult> func)
        {
            var result = await self;

            if(result.IsOk() && func != null)
            {
                return func.Invoke(result.OkValue);
            }
            else
            {
                return default;
            }
        }
    }
}