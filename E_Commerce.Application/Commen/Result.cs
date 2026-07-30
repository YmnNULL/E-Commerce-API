namespace E_Commerce.Application.Commen
{
    public class Result
    {
        public bool IsSuccess { get; }

        public IReadOnlyList<Error> Errors { get; }

        public Result(bool isSuccess, IReadOnlyList<Error> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static Result Ok() => new Result(true , Array.Empty<Error>());
        public static Result Fail(Error error) => new Result(false , new[] {error});
        public static Result Fail(IReadOnlyList<Error> errors) => new Result(false, errors);

    }


    public class Result<TValue> : Result
    {
        private readonly TValue _value;

        public TValue data => IsSuccess ? _value : throw new InvalidOperationException("Cannot Access The Value Of Failed Result");

        private Result(TValue value) :base(true, Array.Empty<Error>())
        {
            _value = value;
        }

        private Result(Error error) : base(false, new[] { error })
        {
            _value = default!;
        }

        private Result(IReadOnlyList<Error> errors) : base(false,errors )
        {
            _value = default!;
        }

        public static Result<TValue> Ok(TValue value) => new Result<TValue>(value);
        public static Result<TValue> Fail(Error error) => new Result<TValue>(error);
        public static Result<TValue> Fail(IReadOnlyList<Error> errors) => new Result<TValue>(errors);

        public static implicit operator Result<TValue>(TValue value) => Ok(value);
        public static implicit operator Result<TValue>(Error error) => Fail(error);

    }


}
