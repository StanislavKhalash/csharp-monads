using System;
using System.IO;
using System.Threading.Tasks;

namespace ResultSandbox
{
    public class ResultExperiment
    {
        static Result<int> Divide(int nom, int denom)
        {
            if (denom == 0)
            {
                return new Result<int>(new DivideByZeroException());
            }
            else
            {
                return new Result<int>(nom / denom);
            }
        }

        static int AddOne(int n) => n + 1;

        static async Task<int> WaitAndReturn(int n)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            return n;
        }

        static async Task<Result<int>> WaitAndReturnIfEven(int n)
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            if (n % 2 != 0)
            {
                return new Result<int>(new InvalidDataException());
            }
            else
            {
                return new Result<int>(n);
            }
        }

        public static int Main()
        {
            return MainAsync().GetAwaiter().GetResult();
        }

        public static async Task<int> MainAsync()
        {
            var value1 = await Divide(1, 0)
                .And(AddOne)
                .AndAsync(WaitAndReturn)
                .AndAsync(AddOne)
                .AndAsync(WaitAndReturnIfEven);

            var value2 = await Divide(2, 1)
                .And(AddOne)
                .AndAsync(WaitAndReturn)
                .AndAsync(AddOne)
                .AndAsync(WaitAndReturnIfEven);

            var value3 = await Divide(2, 1)
                .And(AddOne)
                .AndAsync(WaitAndReturn)
                .AndAsync(AddOne)
                .AndAsync(AddOne)
                .AndAsync(WaitAndReturnIfEven);

            return 0;
        }
    }
}
