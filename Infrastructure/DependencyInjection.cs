using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Security;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            MongoConfiguration.Configure();
            services.AddScoped<MongoDbContext>();

            services.AddSingleton<IPasswordHasher, PasswordHasher>();

            services.AddScoped<IUserRepository, MongoUserRepository>();
            services.AddScoped<IProjectRepository, MongoProjectRepository>();
            services.AddScoped<ITaskRepository, MongoTaskRepository>();

            services.AddSingleton<ITokenService, TokenService.JwtService>();

            return services;
        }
    }
}
