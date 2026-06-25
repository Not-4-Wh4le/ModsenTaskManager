using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Extensions
{
    public static class CurrentUserServiceExtensions
    {
        public static Guid GetUserId(this ICurrentUserService currentUserService)
        {
            return currentUserService.Id
                ?? throw new UnauthorizedAccessException("User is not authenticated"); 
        }
    }
}
