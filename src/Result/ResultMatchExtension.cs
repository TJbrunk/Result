using System;
using System.Threading.Tasks;

namespace csharp_utils
{
    public static partial class ResultExtension
    {
        /// <summary>
        /// Functions to execute if Result is Ok or Error
        /// </summary>
        /// <typeparam name="TOk"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="self"></param>
        /// <param name="Ok"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public static TResult? Match<TOk, TError, TResult>(
                this Result<TOk, TError> self,
                Func<TOk, TResult>? Ok = null,
                Func<TError, TResult>? Error = null) {

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

        /// <summary>
        /// Actions to execute if Result is Ok or Error
        /// </summary>
        /// <typeparam name="TOk"></typeparam>
        /// <typeparam name="TError"></typeparam>
        /// <param name="self"></param>
        /// <param name="Ok"></param>
        /// <param name="Error"></param>
        public static void Match<TOk, TError>(
                this Result<TOk, TError> self,
                Action<TOk>? Ok = null,
                Action<TError>? Error = null) {

            if (self.IsOk() && Ok != null)
            {
                Ok(self.OkValue);
            }
            else if (self.IsError() && Error != null)
            {
                Error(self.ErrorValue);
            }
            else
            {
                return;
            }
        }
    }
}