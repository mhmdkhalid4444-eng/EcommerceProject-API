using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce.Application.Common
{
    public class Result
    {
        public bool IsSuccess { get; }

        public IReadOnlyList<Error> Errors { get; }

        public Result (bool issucess, IReadOnlyList<Error> errors)
        {
            IsSuccess = issucess;
            Errors = errors;
        }


        public static Result ok() => new(true , Array.Empty<Error>());
        public static Result Fail(Error error) => new(false, new[] { error });
        public static Result Fail(IReadOnlyList<Error> errors) => new(false, errors);
    }

    public class Result<TValue> : Result
    {
        private readonly TValue _value;

        public TValue data => IsSuccess ? _value : throw new InvalidOperationException("Cannot access the value of a failed result.");

        private Result(TValue value) : base(true, Array.Empty<Error>())
        {
            _value = value;
        }

        private Result(Error error) : base(false, new[] { error })
        {
            _value = default!;
        }

        private Result(IReadOnlyList<Error> errors) : base(false, errors)
        {
            _value = default!;
        }

        public static Result<TValue> Ok(TValue value) => new(value);
        public static new Result<TValue> Fail(Error error) => new(error);
        public static new Result<TValue> Fail(IReadOnlyList<Error> errors) => new(errors);

      
    }

   
}
