using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks
{
    public static class TaskErrors
    {
        public static readonly Error NotFound = new("Taks.NotFound", "Task not found");
    }
}
