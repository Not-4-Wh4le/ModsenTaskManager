using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
