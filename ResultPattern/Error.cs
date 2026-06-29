using System;
using System.Collections.Generic;
using System.Text;

namespace ResultPattern
{
    public record Error(string Code, string Description, ErrorType ErrorType = ErrorType.Failure)
    {
        public static readonly Error None = new(string.Empty, string.Empty, ErrorType.NoError);

        public static Error Custom(string code, string description, ErrorType errorType = ErrorType.Failure)
            => new(code, description, errorType);
    }
}
