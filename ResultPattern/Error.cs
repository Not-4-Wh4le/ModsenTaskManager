using System;
using System.Collections.Generic;
using System.Text;

namespace ResultPattern
{
    public record Error(string Code, string Descrition)
    {
        public static readonly Error None = new(string.Empty, string.Empty);
    }
}
