using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects
{
    public static class ProjectErrors
    {
        public static readonly Error NotFound = new("Project.NotFound", "Project not found");
    }
}
