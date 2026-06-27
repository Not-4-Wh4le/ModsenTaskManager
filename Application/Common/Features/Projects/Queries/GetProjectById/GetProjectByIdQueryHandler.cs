using Application.Common.Features.Users;
using Application.Common.Interfaces.Repositories;
using Application.Common.Models;
using MediatR;
using ResultPattern;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Features.Projects.Queries.GetProjectById
{
    public class GetProjectByIdQueryHandler
        : IRequestHandler<GetProjectByIdQuery, Result<ProjectDetailsDto>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        public GetProjectByIdQueryHandler(
            IProjectRepository projectRepository,
            IUserRepository userRepository)
        {
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }
        public async Task<Result<ProjectDetailsDto>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
            var project = await _projectRepository.GetByIdAsync(request.Id, cancellationToken);

            if(project == null)
            {
                return ProjectErrors.NotFound;
            }

            var user = await _userRepository.GetByIdAsync(project.OwnerId, cancellationToken);

            if(user == null)
            {
                return UserErrors.NotFound;
            }

            var dto = new ProjectDetailsDto(
                Id: project.Id,
                Name: project.Name,
                OwnerEmail: user.Email,
                ProjectStatus: project.ProjectStatus,
                CreatedAt: project.CreatedAt);

            return dto;
        }
    }
}
