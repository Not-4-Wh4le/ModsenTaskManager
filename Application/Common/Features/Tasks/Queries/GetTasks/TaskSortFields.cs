using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Tasks.Queries.GetTasks
{
    public static class TaskSortFields
    {
        public const string DueDate = "duedate";
        public const string CreatedAt = "createdat";
        public const string Status = "status";

        public static readonly string[] All = { DueDate, CreatedAt, Status };
    }
}
