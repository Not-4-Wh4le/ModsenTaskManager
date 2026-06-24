using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; init; }
    }
}
