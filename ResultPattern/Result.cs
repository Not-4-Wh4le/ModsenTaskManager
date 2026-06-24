using System;
using System.Collections.Generic;
using System.Text;

namespace ResultPattern
{
    public record Result
    {
        public bool IsSucces { get; }
        public bool IsFailure => !IsSucces;
        public Error Error{ get; }

        protected Result(bool isSuccess, Error error)
        {
            if (isSuccess && error != Error.None)
                throw new ArgumentException("Success result cannot contain error");

            if (!isSuccess && error == Error.None)
                throw new ArgumentException("Failure result must contain error");
            IsSucces = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error error) => new(false, error);

        public static implicit operator Result(Error error) => Failure(error);


    }
}
