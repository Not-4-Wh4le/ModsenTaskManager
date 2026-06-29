using System;
using System.Collections.Generic;
using System.Text;

namespace ResultPattern
{
    public enum ErrorType
    {
        NoError,
        Failure,
        Validation,
        NotFound,
        Conflict,
        Unauthorized
    }
}
