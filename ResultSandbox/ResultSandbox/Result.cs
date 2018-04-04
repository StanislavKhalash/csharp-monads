using System;

namespace ResultSandbox
{
    public class Result<T>
    {
        private readonly T value;
        private readonly Exception error;

        public Result(T value)
        {
            this.value = value;
        }

        public Result(Exception error)
        {
            this.error = error;
        }

        public bool Success => Error == null;

        public T Value => value;

        public Exception Error => error;

        public override string ToString()
        {
            if (Success)
            {
                return value.ToString();
            }
            else
            {
                return error.ToString();
            }
        }
    }
}
