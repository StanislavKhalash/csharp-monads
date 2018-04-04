using System;
using System.Threading.Tasks;

namespace ResultSandbox
{
    public static class ResultExtensions
    {
        public static Result<TOutput> And<TInput, TOutput>(this Result<TInput> source, Func<TInput, TOutput> selector)
        {
            if (source.Success)
            {
                return new Result<TOutput>(selector(source.Value));
            }
            else
            {
                return new Result<TOutput>(source.Error);
            }
        }

        public static Result<TOutput> And<TInput, TOutput>(this Result<TInput> source, Func<TInput, Result<TOutput>> selector)
        {
            if (source.Success)
            {
                return selector(source.Value);
            }
            else
            {
                return new Result<TOutput>(source.Error);
            }
        }

        public static async Task<Result<TOutput>> AndAsync<TInput, TOutput>(this Result<TInput> source, Func<TInput, Task<TOutput>> selector)
        {
            if (source.Success)
            {
                return new Result<TOutput>(await selector(source.Value));
            }
            else
            {
                return new Result<TOutput>(source.Error);
            }
        }

        public static async Task<Result<TOutput>> AndAsync<TInput, TOutput>(this Task<Result<TInput>> sourceTask, Func<TInput, TOutput> selector)
        {
            var source = await sourceTask;
            return source.And(selector);
        }

        public static async Task<Result<TOutput>> AndAsync<TInput, TOutput>(this Task<Result<TInput>> sourceTask, Func<TInput, Task<TOutput>> selector)
        {
            var source = await sourceTask;
            if (source.Success)
            {
                return new Result<TOutput>(await selector(source.Value));
            }
            else
            {
                return new Result<TOutput>(source.Error);
            }
        }

        public static async Task<Result<TOutput>> AndAsync<TInput, TOutput>(this Task<Result<TInput>> sourceTask, Func<TInput, Task<Result<TOutput>>> selector)
        {
            var source = await sourceTask;
            if (source.Success)
            {
                return await selector(source.Value);
            }
            else
            {
                return new Result<TOutput>(source.Error);
            }
        }
    }
}
