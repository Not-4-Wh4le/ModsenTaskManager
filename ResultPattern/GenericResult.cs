using System;
using System.Collections.Generic;
using System.Text;

namespace ResultPattern
{
    public record Result<T>(T? Value, bool IsSuccess, Error Error) 
        : Result(IsSuccess, Error)
    {
        public static Result<T> Success(T value) => new(value, true, Error.None);
        public static new Result<T> Failure(Error error) => new(default, false, error);

        public static implicit operator Result<T>(T value) => Success(value);
        public static implicit operator Result<T>(Error error) => Failure(error);
    }
}
