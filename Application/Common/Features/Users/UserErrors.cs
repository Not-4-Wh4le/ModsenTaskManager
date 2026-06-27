using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Users
{
    public static class UserErrors
    {
        public static readonly Error ArgumentException = new("User.ArgumentException", "");
        public static readonly Error EmailNotUnique = new("User.EmailNotUnique", "Email is already in use");
        public static readonly Error FailedLogin = new("User.FailedLogin", "Invalid email or password");
        public static readonly Error Forbidden = new("User.Forbidden", "You do not have access to this resource");
        public static readonly Error NotFound = new("User.NotFound", "User not found");
    }
}
