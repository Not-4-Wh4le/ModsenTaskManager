using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects.Queries.GetProjects
{
    public static class ProjectSortFields
    {
        public const string Date = "date";
        public const string Name = "name";

        public static readonly string[] All = {Date,  Name};
    }
}
