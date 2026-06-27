using AutoMapper;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects.Queries.GetProjects
{
    public class ProjectDtoProfile : Profile
    {
        public ProjectDtoProfile()
        {
            CreateMap<Project, ProjectDto>();
        }
    }
}
